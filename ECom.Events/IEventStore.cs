using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Events
{
    public interface IEventStore
    {
        void SaveAggregateEvents(Guid aggregateId, string aggregateType, IEnumerable<IEvent> events, int expectedVersion);

        IEnumerable<IEvent> GetEventsForAggregate(Guid aggregateId);
    }
}
