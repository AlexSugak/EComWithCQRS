using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;
using System.Threading;

namespace ECom.Bus
{
    public class Bus : IBus
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _routes = new Dictionary<Type, List<Action<IMessage>>>();
		private readonly bool _isAsynchronousEventsHandling;

		public Bus()
			: this(true)
		{
		}

		public Bus(bool isAsynchronousEventsHandling)
		{
			_isAsynchronousEventsHandling = isAsynchronousEventsHandling;
		}

        public void RegisterHandler<T>(Action<T> handler) where T : IMessage
        {
            List<Action<IMessage>> handlers;
            if (!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IMessage>>();
                _routes.Add(typeof(T), handlers);
            }
            handlers.Add(DelegateAdjuster.CastArgument<IMessage, T>(x => handler(x)));
        }

        public void Send<T>(T command) where T : ICommand
        {
            List<Action<IMessage>> handlers;
            if (_routes.TryGetValue(typeof(T), out handlers))
            {
                if (handlers.Count != 1) throw new InvalidOperationException("cannot send to more than one handler");
                handlers[0](command);
            }
            else
            {
                throw new InvalidOperationException("no handler registered");
            }
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            List<Action<IMessage>> handlers;
            if (!_routes.TryGetValue(@event.GetType(), out handlers)) return;
            foreach (var handler in handlers)
            {
                var handler1 = handler;

				if (_isAsynchronousEventsHandling)
				{
					ThreadPool.QueueUserWorkItem(x => handler1(@event));
				}
				else
				{
					handler1(@event);
				}
            }
        }
    }
}
