using ECom.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.Site.Core
{
    /// <summary>
    /// Comparer class.
    /// </summary>
    public class EventComparer : IComparer<EventWrapper>
    {
        public EventComparer()
            :this(SortFieldEnum.DATE, SortOrderEnum.ASC)
        {
        }

        public EventComparer(SortFieldEnum sortField, SortOrderEnum sortOrder)
        {
            SortField = sortField;
            SortOrder = sortOrder;
        }

        public SortFieldEnum SortField { get; set; }
        public SortOrderEnum SortOrder { get; set; }

        public int Compare(EventWrapper x, EventWrapper y)
        {
            if (SortField == SortFieldEnum.ID)
            {
                return SortOrder == SortOrderEnum.ASC ? string.Compare(x.EventId, y.EventId) : string.Compare(y.EventId, x.EventId);
            }
            else if (SortField == SortFieldEnum.NAME)
            {
                return (SortOrder == SortOrderEnum.ASC ? string.Compare(x.EventName, y.EventName) : string.Compare(y.EventName, x.EventName));
            }
            else if (SortField == SortFieldEnum.DATE)
            {
                return (SortOrder == SortOrderEnum.ASC ? DateTime.Compare(x.EventDate, y.EventDate) : DateTime.Compare(y.EventDate, x.EventDate));
            }
            else
            {
                return DateTime.Compare(x.EventDate, y.EventDate);
            }
        }
    }

    public enum SortOrderEnum
    {
        ASC = -1,
        DESC = 1
    }

    public enum SortFieldEnum
    {
        ID,
        NAME,
        DATE
    }
}