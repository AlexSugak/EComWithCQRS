using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECom.Domain;
using ECom.Messages;
using ECom.Utility;
using ECom.Site.Controllers;
using System.Configuration;
using ECom.Site.Areas.Admin.Models;
using ECom.Site.Core;
using MvcContrib.Pagination;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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

        private IIdentity GetTypedAggregateId(string agrId, string agrType)
        {
            if (!string.IsNullOrEmpty(agrType))
            {
                // obtain existing aggregate roots
                var type = typeof(AggregateRoot<>);
                var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => IsSubclassOfRawGeneric(type, p) && p != type).ToList();

                // compare with value from DB
                var agrRoot = types.Find(p => p.FullName == agrType);

                if (agrRoot != null)
                {
                    // get type which implements IIdentity (OrderId, UserId and so on)
                    var myType = agrRoot.BaseType.GetGenericArguments()[0];
                    
                    // get argument type (Type of aggregate id: int, guid, string)
                    var argumentType = myType.BaseType.BaseType.GetGenericArguments()[0];

                    object arg;

                    // create instance of a argument type class 
                    if (argumentType.IsClass && argumentType.FullName == "System.String")
                    {
                        arg = agrId;
                    }
                    else if (argumentType.IsClass)
                    {
                        arg = Activator.CreateInstance(argumentType, agrId);
                    }
                    else
                    {
                        TypeConverter tc = TypeDescriptor.GetConverter(argumentType);
                        arg = tc.ConvertFromString(agrId);
                    }

                    // create instance of a class which implements IIdentity
                    IIdentity obj = (IIdentity)Activator.CreateInstance(myType, arg);

                    return obj;
                }
            }

            return new NullId();
        }

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
