using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;

namespace ECom.Bus
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}
