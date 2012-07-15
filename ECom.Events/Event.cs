using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;

namespace ECom.Events
{
    [Serializable]
    public class Event : IEvent
    {
        public Guid AggregateId { get; protected set; }
        public int Version { get; set; }

        public Event()
        {
        }
    }
}
