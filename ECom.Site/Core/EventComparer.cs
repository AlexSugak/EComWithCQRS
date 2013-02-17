using ECom.Site.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.Site.Core
{
    /// <summary>
    /// Comparer class.
    /// </summary>
    public class EventComparer : IComparer<EventViewModel>
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

        public int Compare(EventViewModel x, EventViewModel y)
        {
            if (SortField == SortFieldEnum.ID)
            {
                return SortOrder == SortOrderEnum.ASC ? string.Compare(x.AggregateId, y.AggregateId) : string.Compare(y.AggregateId, x.AggregateId);
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