using ECom.Messages;
using ECom.Site.Areas.Admin.Models;
using ECom.Site.Controllers;
using ECom.Site.Core;
using MvcContrib.Pagination;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace ECom.Site.Areas.Admin.Controllers
{
    public class EventViewerController : CqrsController
    {
        public int PageSize { get; set; }
        
        private readonly IEventStore _storage;
        private bool _saveSessionVars;

        public EventViewerController()
			: base()
		{
            _storage = ServiceLocator.EventStore;
            PageSize = 15;
            _saveSessionVars = true;
		}

        public EventViewerController(IEventStore storage)
            : base()
        {
            _storage = storage;
            _saveSessionVars = false;
        }

        [HttpGet]
        public ActionResult Index(string AggregateId, string sortField, int? page)
        {
            List<EventViewModel> eventsList = new List<EventViewModel>();

            if (!string.IsNullOrEmpty(AggregateId))
            {
                eventsList = GetEvents(_storage.GetEventsForAggregate(AggregateId).ToList());
            }
            else
            {
                eventsList = GetEvents(_storage.GetAllEvents().ToList());
            }
            
            EventComparer comparer = GetComparer(sortField, page);
            eventsList.Sort(comparer);

            var pagedEvents = eventsList.AsPagination(page.GetValueOrDefault(1), PageSize);

            return View(new EventViewerViewModel(pagedEvents, AggregateId));
        }

        [HttpGet]
        public ActionResult Details(string aggregateId, int? version)
        {
            string aggregateType = String.Empty;

            IEnumerable<IEvent<IIdentity>> eventList = _storage.GetEventsForAggregate(aggregateId).OfType<IEvent<IIdentity>>();
            IEvent<IIdentity> foundEvent = eventList.First(p => p.Version == version);

            return PartialView("_EventDetails", new EventDetailsViewModel(foundEvent));
        }

        #region Helpers

        private EventComparer GetComparer(string sortField, int? page)
        {
            EventComparer comparer;

            if (string.IsNullOrEmpty(sortField))
            {
                if (_saveSessionVars)
                {
                    Session["SortOrder"] = SortOrderEnum.ASC;
                    Session["SortField"] = SortFieldEnum.DATE;
                }

                comparer = new EventComparer();
            }
            else
            {
                SortFieldEnum savedValue = (SortFieldEnum)Session["SortField"];

                if (sortField == savedValue.ToString() && page == null)
                {
                    Session["SortOrder"] = (SortOrderEnum)((int)Session["SortOrder"] * (-1));
                }
                else if (sortField != savedValue.ToString())
                {
                    Session["SortOrder"] = SortOrderEnum.ASC;
                    Session["SortField"] = (SortFieldEnum)Enum.Parse(typeof(SortFieldEnum), sortField);
                }

                comparer = new EventComparer((SortFieldEnum)Session["SortField"], (SortOrderEnum)Session["SortOrder"]);
            }

            return comparer;
        }

        private List<EventViewModel> GetEvents(List<IEvent> list)
        {
            List<EventViewModel> eventsList = new List<EventViewModel>();

            foreach (IEvent item in list)
            {
                eventsList.Add(new EventViewModel(item));
            }

            return eventsList;
        }

        #endregion

        #region ReflectionMethods

        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        #endregion

    }
}
