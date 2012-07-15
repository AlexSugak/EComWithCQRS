using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;

namespace ECom.Events
{
    public interface IEvent : Message
    {
        Guid AggregateId { get; }
        int Version { get; set; }
    }
}
