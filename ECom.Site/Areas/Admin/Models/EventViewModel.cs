using ECom.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.Utility;
using System.ComponentModel;

namespace ECom.Site.Areas.Admin.Models
{
    public class EventViewModel
    {
        public EventViewModel() { }

        public EventViewModel(IEvent eventObj)
        {
            string classNameReversed = eventObj.ToString().Reverse();

            AggregateId     = ((IEvent<IIdentity>)eventObj).Id.GetId();
            EventName       = classNameReversed.Substring(0, classNameReversed.IndexOf('.')).Reverse().Wordify();
            EventVersion    = ((IEvent<IIdentity>)eventObj).Version;
            EventDate       = ((IEvent<IIdentity>)eventObj).Date;
        }

        [DisplayName("Aggregate ID")]
        public string AggregateId { get; set; }

        [DisplayName("Event name")]
        public string EventName { get; set; }

        [DisplayName("Event version")]
        public int EventVersion { get; set; }

        [DisplayName("Event date")]
        public DateTime EventDate { get; set; }
    }
}