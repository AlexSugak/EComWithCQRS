using ECom.Messages;
using System.Linq;

namespace ECom.Domain
{
    public interface IRepository<TAggregate, TIdentity>
        where TIdentity : IIdentity
        where TAggregate : IAggregateRoot<TIdentity>, new()
    {
        void Save(TAggregate aggregate);
        TAggregate Get(TIdentity id);
    }

    public class Repository<TAggregate, TIdentity> : IRepository<TAggregate, TIdentity>
        where TIdentity : IIdentity
        where TAggregate : IAggregateRoot<TIdentity>, new()
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(TAggregate aggregate)
        {
            _storage.SaveAggregateEvents(aggregate.Id, aggregate.GetType().FullName, aggregate.GetUncommittedChanges());
			aggregate.MarkChangesAsCommitted();
        }

		public TAggregate Get(TIdentity id)
        {
            var events = _storage.GetEventsForAggregate(id).ToList();

            if (events.Count == 0)
            {
                throw new AggregateRootNotFoundException(typeof(TAggregate), id);
            }

            var aggregate = new TAggregate();//TODO: refactor to not have default ctor on aggregate roots

            aggregate.LoadsFromHistory(events);
            return aggregate;
        }
    }
}
