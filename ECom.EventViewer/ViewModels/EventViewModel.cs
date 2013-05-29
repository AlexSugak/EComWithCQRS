using ECom.EventViewer.Service;
using ECom.Messages;
using ECom.Utility;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.EventViewer.ViewModels
{
    public class EventViewModel
    {
        private readonly IEventStore _storage;

        public EventViewModel(IEvent eventObj)
        {
            this._storage = ServiceLocator.EventStore;

            string classNameReversed = eventObj.ToString().Reverse();

            AggregateId = ((IEvent<IIdentity>)eventObj).Id.GetId();
            EventName = classNameReversed.Substring(0, classNameReversed.IndexOf('.')).Reverse().Wordify();
            EventVersion = ((IEvent<IIdentity>)eventObj).Version;
            EventDate = ((IEvent<IIdentity>)eventObj).Date;

            string rawAggregateType = _storage.GetAggregateType(AggregateId);

            if (rawAggregateType != null)
            {
                string reversedType = rawAggregateType.Reverse();
                AggregateType = reversedType.Substring(0, reversedType.IndexOf('.')).Reverse().Wordify();
            }

            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            JsConfig.ExcludeTypeInfo = true;
            EventDetails = JsvFormatter.Format(JsonSerializer.SerializeToString((IEvent<IIdentity>)eventObj));
        }

        internal EventViewModel()
        {
        }

        public string AggregateId { get; set; }
        public string EventName { get; set; }
        public int EventVersion { get; set; }
        public DateTime EventDate { get; set; }
        public string AggregateType { get; set; }
        public string EventDetails { get; set; }
    }
}
