using ECom.EventStore.Redis;
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
        private static readonly ECom.EventStore.Redis.JsonSerializer serializer;

        static EventViewModel()
        {
            //
            // We need to provide KnownTypes to serizalizer, and those are events
            // We consider events to be anything assignable to IEvent, also we load only events from the same assembly as IEvent
            //
            var ieventType = typeof(IEvent);
            var eventTypes = ieventType.Assembly.GetTypes().Where(x => ieventType.IsAssignableFrom(x) && x.IsClass).ToArray();

            EventViewModel.serializer = new ECom.EventStore.Redis.JsonSerializer(eventTypes);

        }

        public EventViewModel(IEvent eventObj)
        {
            this._storage = ServiceLocator.EventStore;

            string classNameReversed = eventObj.ToString().Reverse();

            // WTF is that? Where're the nulls checks?
            AggregateId = ((IEvent<IIdentity>)eventObj).Id.GetId();
            EventName = classNameReversed.Substring(0, classNameReversed.IndexOf('.')).Reverse().Wordify();
            EventVersion = ((IEvent<IIdentity>)eventObj).Version;
            EventDate = ((IEvent<IIdentity>)eventObj).Date;

            string rawAggregateType = ((IEvent<IIdentity>)eventObj).Id.GetType().Name.Replace("Id", String.Empty);

            if (rawAggregateType != null)
            {
                AggregateType = rawAggregateType.Wordify();
            }

            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            JsConfig.ExcludeTypeInfo = true;
            EventDetails = JsvFormatter.Format(EventViewModel.serializer.Serialize(eventObj));
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
