using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Bus;
using System.Reflection;
using ECom.Utility;
using SubSonic.Repository;
using SubSonic.DataProviders;
using ECom.ReadModel;
using SubSonic.Query;
using System.IO;
using ECom.Messages;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ECom.Infrastructure
{
	public static class ReadModelRebuilder
	{
		public static void Rebuild(string readModelConnectionString, string eventStoreConnectionString)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => readModelConnectionString);

			var readModelAssembly = Assembly.Load(new AssemblyName("ECom.ReadModel"));
			var bus = new Bus.Bus();
			var eventStore = new EventStore.SQL.EventStore(eventStoreConnectionString, bus);


			//recreate all dto tables
			IDataProvider readModelDbProvider = ProviderFactory.GetProvider(readModelConnectionString, DbClientTypeName.MsSql);
			var dtoTypes = readModelAssembly.GetTypes().Where(t => t != typeof(Dto) && typeof(Dto).IsAssignableFrom(t));
			foreach (var dtoType in dtoTypes)
			{
				var sql = String.Format("IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{0}s')) DROP TABLE [{0}s];\r\n", dtoType.Name);
				readModelDbProvider.ExecuteQuery(new QueryCommand(sql, readModelDbProvider));

				var genericRegister = readModelDbProvider.GetType().GetMethod("MigrateToDatabase").MakeGenericMethod(new[] { dtoType });
				genericRegister.Invoke(readModelDbProvider, new[] { readModelAssembly });
			}

			var readModelRepo = new SimpleRepository(readModelDbProvider, SimpleRepositoryOptions.None);
			var dtoManager = new SubSonicDtoManager(readModelRepo);

			MessageHandlersRegister.RegisterEventHandlers(readModelAssembly, bus, dtoManager);

			//republish all events to build read model views
			IDataProvider eventStoreDbProvider = ProviderFactory.GetProvider(eventStoreConnectionString, DbClientTypeName.MsSql);
			var formatter = new BinaryFormatter();
			using (var reader = eventStoreDbProvider.ExecuteReader(new QueryCommand("SELECT * FROM [Events] ORDER BY [Date] ASC", eventStoreDbProvider)))
			{
				while (reader.Read())
				{
                    var @event = Deserialize<IEvent>((byte[])reader["Event"], formatter);
					bus.Publish(@event);
				}
			}
		}

		private static TType Deserialize<TType>(byte[] bytes, IFormatter formatter)
		{
			using (var memoryStream = new MemoryStream(bytes))
			{
				return (TType)formatter.Deserialize(memoryStream);
			}
		}
	}
}
