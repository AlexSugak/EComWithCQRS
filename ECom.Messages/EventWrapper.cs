using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using System.ComponentModel;

namespace ECom.Messages
{
    public class EventWrapper
    {
        public EventWrapper() { }

        public EventWrapper(IEvent eventObj, string date)
        {
            string classNameReversed = eventObj.ToString().Reverse();

            EventId         = ((IEvent<IIdentity>)eventObj).Id.GetId();
            EventName       = classNameReversed.Substring(0, classNameReversed.IndexOf('.')).Reverse().Wordify();
            EventVersion    = ((IEvent<IIdentity>)eventObj).Version;

            try
            {
                EventDate = Convert.ToDateTime(date);
            }
            catch (FormatException)
            {
                EventDate = DateTime.MinValue;
            }
        }

        [DisplayName("Aggregate ID")]
        public string EventId { get; set; }

        [DisplayName("Event name")]
        public string EventName { get; set; }

        [DisplayName("Event version")]
        public int EventVersion { get; set; }

        [DisplayName("Event date")]
        public DateTime EventDate { get; set; }
    }
}
