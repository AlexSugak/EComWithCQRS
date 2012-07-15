using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Domain.Exceptions;
using System.Globalization;

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
            _storage.SaveAggregateEvents(aggregate.Id, aggregate.GetType().FullName, aggregate.GetUncommittedChanges(), aggregate.Version);
        }

		public TAggregate Get(TIdentity id)
        {
            var events = _storage.GetEventsForAggregate(id).ToList();

            if (events.Count == 0)
            {
                throw new AggregateRootNotFoundException(
                                String.Format(
                                        CultureInfo.InvariantCulture, 
                                        "{0} with id {1} was not found", 
                                        typeof(TAggregate).Name, 
                                        id));
            }

            var aggregate = new TAggregate();//TODO: refactor to not have default ctor on aggregate roots

            aggregate.LoadsFromHistory(events);
            return aggregate;
        }
    }
}
