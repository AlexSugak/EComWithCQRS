﻿using ECom.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using ECom.Utility;
using System.Globalization;
using ECom.Bus;

namespace ECom.EventStore.Redis
{
    /// <summary>
    /// Simple Event Store implementation using Redis to store events
    /// </summary>
    public class EventStore : IEventStore
    {
        private readonly string _redisHost;
        private readonly string _allEventsListId = "urn:AllEventIds";
        private readonly IEventPublisher _publisher;

        public EventStore(string redisHost, IEventPublisher publisher)
        {
            Argument.ExpectNotNull(() => redisHost);
            Argument.ExpectNotNull(() => publisher);

            _redisHost = redisHost;
            _publisher = publisher;
        }

        public void SaveAggregateEvents<T>(T aggregateId, string aggregateType, IEnumerable<IEvent<T>> events) where T : IIdentity
        {
            Argument.ExpectNotNull(() => aggregateId);
            Argument.ExpectNotNull(() => events);
            Argument.ExpectNotNullOrWhiteSpace(() => aggregateType);

            if (!events.Any())
            {
                return;
            }

            using (var client = new PooledRedisClientManager(_redisHost).GetClient())
            {
                var aggregateRootEventsListId = AggregateRootEventsListId(aggregateId.GetId());

                IEvent<T> lastEvent = null;
                string lastEventId = client.Lists[aggregateRootEventsListId].LastOrDefault();
                if (lastEventId != null)
                {
                    lastEvent = (IEvent<T>)client.As<IEvent>()[EventId(lastEventId)];
                }

                int currentVersion = lastEvent != null ? lastEvent.Version : 0;
                int expectedVersion = events.First().Version - 1;
                if (currentVersion != expectedVersion)
                {
                    throw new ConcurrencyViolationException(String.Format(
                                    "Expected {0} to have version {1} but was {2}",
                                    aggregateType,
                                    expectedVersion,
                                    currentVersion));
                }

                using (var trans = client.CreateTransaction())
                {
                    foreach (var e in events)
                    {
                        var eventId = Guid.NewGuid().ToString();

                        //save event itself
                        trans.QueueCommand(c => c.As<IEvent>().SetEntry(EventId(eventId), e));
                        //append event id to the list of aggregate events
                        trans.QueueCommand(c => c.AddItemToList(aggregateRootEventsListId, eventId));
                        //append event id to global list of events
                        trans.QueueCommand(c => c.AddItemToList(_allEventsListId, eventId));
                    }

                    trans.Commit();
                }
            }

            foreach (var @event in events)
            {
                _publisher.Publish(@event);
            }
        }

        public IEnumerable<IEvent<T>> GetEventsForAggregate<T>(T aggregateId) where T : IIdentity
        {
            Argument.ExpectNotNull(() => aggregateId);

            string id = aggregateId.GetId();
            return GetEventsForAggregate(id).OfType<IEvent<T>>();
        }

        public IEnumerable<IEvent> GetEventsForAggregate(string aggregateId)
        {
            Argument.ExpectNotNullOrWhiteSpace(() => aggregateId);

            using (var client = new PooledRedisClientManager(_redisHost).GetClient())
            {
                var aggregateEventIds = client.Lists[AggregateRootEventsListId(aggregateId)];
                foreach (var eventId in aggregateEventIds)
                {
                    yield return client.As<IEvent>()[EventId(eventId)];
                }
            }
        }

        public IEnumerable<IEvent> GetAllEvents()
        {
            using (var client = new PooledRedisClientManager(_redisHost).GetClient())
            {
                var allEventIds = client.Lists[_allEventsListId];
                foreach (var eventId in allEventIds)
                {
                    yield return client.As<IEvent>()[EventId(eventId)];
                }
            }
        }

        private static string AggregateRootEventsListId(string id)
        {
            return String.Format(CultureInfo.InvariantCulture, "urn:AggregateRoot:{0}", id);
        }

        private static string EventId(string id)
        {
            return String.Format(CultureInfo.InvariantCulture, "urn:Event:{0}", id);
        }
    }
}
