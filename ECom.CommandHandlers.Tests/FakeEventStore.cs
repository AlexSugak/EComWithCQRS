﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;

namespace ECom.CommandHandlers.Tests
{
    public class FakeEventStore : IEventStore
    {
        private Dictionary<string, List<IEvent>> _events = new Dictionary<string, List<IEvent>>();
        private List<IEvent> _newEvents = new List<IEvent>();

        public FakeEventStore()
        {
        }

        public void SetupEventsHistory(IEnumerable<IEvent> events)
        {
            foreach(var @event in events)
            {
                var aggregateIdProp = @event.GetType().GetProperty("Id");
                var aggregateId = (aggregateIdProp.GetValue(@event, null) as IIdentity).GetId();

                if (_events.ContainsKey(aggregateId))
                {
                    _events[aggregateId].Add(@event);
                }
                else
                {
                    _events.Add(aggregateId, new List<IEvent> { @event });
                }
            }
        }

        public void SaveAggregateEvents<T>(T aggregateId, string aggregateType, IEnumerable<IEvent<T>> events)
            where T : IIdentity
        {
            if (_events.ContainsKey(aggregateId.GetId()))
            {
                _events[aggregateId.GetId()].AddRange(events);
            }
            else
            {
                _events.Add(aggregateId.GetId(), events.Cast<IEvent>().ToList());
            }

            _newEvents.AddRange(events);
        }

        public IEnumerable<IEvent<T>> GetEventsForAggregate<T>(T aggregateId) where T : IIdentity
        {
            string id = aggregateId.GetId();
            return GetEventsForAggregate(id).OfType<IEvent<T>>();
        }

        public IEnumerable<IEvent> GetEventsForAggregate(string aggregateId)
        {
            if (!_events.ContainsKey(aggregateId))
            {
                return Enumerable.Empty<IEvent>();
            }

            return _events[aggregateId];
        }

        public IEnumerable<IEvent> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEvent> NewEvents()
        {
            return _newEvents;
        }
    }
}
