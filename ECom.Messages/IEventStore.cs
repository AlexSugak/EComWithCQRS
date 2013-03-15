﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Messages
{
    public interface IEventStore
    {
		void SaveAggregateEvents<T>(T aggregateId, string aggregateType, IEnumerable<IEvent<T>> events)
                                    where T : IIdentity;

        IEnumerable<IEvent<T>> GetEventsForAggregate<T>(T aggregateId)
                                    where T : IIdentity;

        IEnumerable<IEvent<T>> GetAllEvents<T>()
                                    where T : IIdentity;

        string GetAggregateType(string aggregateId);
    }
}
