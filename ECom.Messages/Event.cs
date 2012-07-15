using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;

namespace ECom.Messages
{
    public interface IEvent : IMessage
    { }

    public interface IEvent<out T> : IEvent
        where T : IIdentity 
    {
        T Id { get; }
        int Version { get; set; }
    }
}
