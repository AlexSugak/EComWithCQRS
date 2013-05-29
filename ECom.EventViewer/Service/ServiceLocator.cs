using ECom.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.EventViewer.Service
{
    public class ServiceLocator
    {
        public static Bus.Bus Bus { get; set; }
        public static IEventStore EventStore { get; set; }
    }
}
