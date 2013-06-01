using ECom.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Text;
using ECom.Utility;

namespace ECom.Site.Areas.Admin.Models
{
    public class EventDetailsViewModel
    {
        public EventDetailsViewModel() { }

        public EventDetailsViewModel(IEvent<IIdentity> @event)
        {
            string rawType = @event.Id.GetType().Name.Replace("Id", String.Empty);
            AggregateType = rawType.Wordify();

            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            JsConfig.ExcludeTypeInfo = true;
            EventDetails = HtmlEncode(JsvFormatter.Format(JsonSerializer.SerializeToString(@event)));
        }

        public string AggregateType { get; set; }
        public string EventDetails { get; set; }

        private static string HtmlEncode(string jsonFormattedStr)
        {
            return jsonFormattedStr.Replace("\r\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
        }
    }
}