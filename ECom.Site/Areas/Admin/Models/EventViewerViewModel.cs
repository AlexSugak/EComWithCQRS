using ECom.Utility;
using MvcContrib.Pagination;

namespace ECom.Site.Areas.Admin.Models
{
    public class EventViewerViewModel
    {
        public EventViewerViewModel()
        {
        }

        public EventViewerViewModel(IPagination<EventViewModel> events, string aggregateId)
        {
            Argument.ExpectNotNull(() => events);

            Events              = events;
            AggregateId         = aggregateId;
        }

        public IPagination<EventViewModel> Events { get; set; }
        public string AggregateId { get; set; }
    }
}