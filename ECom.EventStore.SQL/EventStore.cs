using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlTypes;
using System.Runtime.Serialization.Formatters.Binary;
using ECom.Messages;
using ECom.Bus;
using ECom.Utility;

namespace ECom.EventStore.SQL
{
    public class EventStore : IEventStore
    {
        private readonly IEventPublisher _publisher;
        private readonly string _connectionString;
        private readonly IFormatter _formatter;

        public EventStore(string sqlConnectionString, IEventPublisher publisher)
        {
			Argument.ExpectNotNullOrWhiteSpace(() => sqlConnectionString);

            _connectionString = sqlConnectionString;
            _formatter = new BinaryFormatter();
            _publisher = publisher;
        }

		public void SaveAggregateEvents<T>(T aggregateId, string aggregateType, IEnumerable<IEvent<T>> events)
            where T : IIdentity
        {
            var date = DateTime.Now;

			if (!events.Any())
			{
				return;
			}

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int currentVersion = -1;
						
                        const string selectVersionCommandText = @"
                        IF (SELECT COUNT(AggregateId) FROM [Aggregates] WHERE AggregateId = @aggregateId) = 0
                        BEGIN
                            INSERT INTO [Aggregates] VALUES(@aggregateId, @type, 0)
                        END
                        SELECT Version FROM [Aggregates] WHERE AggregateId = @aggregateId";

                        using (var selectVersionCommand = new SqlCommand(selectVersionCommandText, transaction.Connection, transaction))
                        {
                            selectVersionCommand.Parameters.Add(new SqlParameter("@aggregateId", aggregateId.GetId()));
                            selectVersionCommand.Parameters.Add(new SqlParameter("@type", aggregateType));

                            currentVersion = (int)selectVersionCommand.ExecuteScalar();
                        }

						int expectedVersion = events.First().Version - 1;
                        if (currentVersion != expectedVersion)
                        {
                            throw new ConcurrencyViolationException(String.Format("Expected {0} to have verion {1} but was {2}", aggregateType, expectedVersion, currentVersion));
                        }

                        foreach (var @event in events)
                        {
                            const string commandText = "INSERT INTO [Events] VALUES(@aggregateId, @version, @event, @date, @user)";
                            using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
                            {
                                command.Parameters.Add(new SqlParameter("@aggregateId", @event.Id.GetId()));
                                command.Parameters.Add(new SqlParameter("@version", @event.Version));
                                command.Parameters.Add(new SqlParameter("@event", Serialize(@event)));
                                command.Parameters.Add(new SqlParameter("@date", date));
                                command.Parameters.Add(new SqlParameter("@user", ""));//TODO: add user tracking

                                command.ExecuteNonQuery();
                            }
                        }

                        const string updateVersionCommandText = @"
                        UPDATE [Aggregates] 
                        SET Version = @version
                        WHERE AggregateId = @aggregateId";

                        using (var updateVersionCommand = new SqlCommand(updateVersionCommandText, transaction.Connection, transaction))
                        {
                            updateVersionCommand.Parameters.Add(new SqlParameter("@aggregateId", aggregateId.GetId()));
                            updateVersionCommand.Parameters.Add(new SqlParameter("@version", events.Last().Version));

                            updateVersionCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                foreach (var @event in events)
                {
                    _publisher.Publish(@event);
                }
            }
        }

		public IEnumerable<IEvent<T>> GetEventsForAggregate<T>(T aggregateId)
            where T : IIdentity
        {
            var events = new List<IEvent<T>>();

            var commandText = @"SELECT Event FROM [Events] WHERE [AggregateId] = @aggregateId ORDER BY [Version] ASC";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.Add(new SqlParameter("@aggregateId", aggregateId.GetId()));

                    using (var reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            events.Add(Deserialize<IEvent<T>>((byte[])reader["Event"]));
                        }
                    }
                }
            }

            return events;
        }

        public IEnumerable<IEvent<T>> GetAllEvents<T>()
            where T : IIdentity
        {
            var events = new List<IEvent<T>>();

            var commandText = @"SELECT Event FROM [Events] ORDER BY [Version] ASC";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            events.Add(Deserialize<IEvent<T>>((byte[])reader["Event"]));
                        }
                    }
                }
            }

            return events;
        }

        public string GetAggregateType(string aggregateId)
        {
            var commandText = @"SELECT [Type] FROM [Aggregates] WHERE [AggregateId] = @aggregateId";
            string type = string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.Add(new SqlParameter("@aggregateId", aggregateId));

                    type = (string)command.ExecuteScalar();
                }
            }

            return type;
        }

        private byte[] Serialize(object theObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                _formatter.Serialize(memoryStream, theObject);
                return memoryStream.ToArray();
            }
        }

        private TType Deserialize<TType>(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return (TType)_formatter.Deserialize(memoryStream);
            }
        }
    }
}
