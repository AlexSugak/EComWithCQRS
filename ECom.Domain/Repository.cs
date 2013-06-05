using ECom.Messages;
using System;
using System.Linq;

namespace ECom.Domain
{
    public interface IRepository<TAggregate, TIdentity>
        where TIdentity : IIdentity
        where TAggregate : IAggregateRoot<TIdentity>
    {
        void Save(TAggregate aggregate, int expectedVersion);
        TAggregate Get(TIdentity id);
    }

    public class Repository<TAggregate, TIdentity> : IRepository<TAggregate, TIdentity>
        where TIdentity : IIdentity
        where TAggregate : IAggregateRoot<TIdentity>
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(TAggregate aggregate, int expectedVersion)
        {
            _storage.SaveAggregateEvents(aggregate.Id, aggregate.GetType().FullName, aggregate.GetUncommittedChanges(), expectedVersion);
			aggregate.MarkChangesAsCommitted();
        }

		public TAggregate Get(TIdentity id)
        {
            var events = _storage.GetEventsForAggregate(id).ToList();

            if (events.Count == 0)
            {
                throw new AggregateRootNotFoundException(typeof(TAggregate), id);
            }

            var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);

            aggregate.LoadsFromHistory(events);
            return aggregate;
        }
    }
}
