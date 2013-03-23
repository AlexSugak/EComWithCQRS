using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using ECom.Messages;

namespace ECom.Domain
{
	public interface IAggregateRoot<T>
        where T : IIdentity
	{
		T Id { get; }
		int Version { get; set; }
		IEnumerable<IEvent<T>> GetUncommittedChanges();
		void MarkChangesAsCommitted();
		void LoadsFromHistory(IEnumerable<IEvent<T>> history);
	}

	public abstract class AggregateRoot<T> : IAggregateRoot<T>
		where T : IIdentity
    {
        private readonly List<IEvent<T>> _changes = new List<IEvent<T>>();

		public AggregateRoot()
		{
			Version = 0;
		}

        public abstract T Id { get; }

        public int Version { get; set; }

        public IEnumerable<IEvent<T>> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<IEvent<T>> history)
        {
			foreach (var e in history)
			{
				ApplyChange(e, false);
			}
        }

        protected void ApplyChange(IEvent<T> @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(IEvent<T> @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
			Version += 1;

			if (isNew)
			{
				@event.Version = Version;
                @event.Date = DateTime.Now;
				_changes.Add(@event);
			}
        }
    }
}
