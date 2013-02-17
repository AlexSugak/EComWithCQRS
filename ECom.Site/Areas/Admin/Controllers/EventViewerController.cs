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
using ServiceStack.Text;
using System.ComponentModel;

namespace ECom.Site.Areas.Admin.Controllers
{
    public class EventViewerController : CqrsController
    {
        private readonly IEventStore _storage;

        public EventViewerController()
			: base()
		{
            _storage = ServiceLocator.EventStore;
		}

        public EventViewerController(IEventStore storage)
            : base()
        {
            _storage = storage;
        }

        [HttpGet]
        public ActionResult Index(string AggregateId, string sortField, int? page)
        {
            IIdentity id = new NullId();
            bool showAllEvents = true;
            string aggregateType = string.Empty;

            if (!string.IsNullOrEmpty(AggregateId))
            {
                aggregateType = _storage.GetAggregateType(AggregateId);
                id = GetTypedAggregateId(AggregateId, aggregateType);
                showAllEvents = false;
            }

            List<EventViewModel> eventsList = GetEvents(_storage.GetEventsForAggregate(id, showAllEvents).ToList());

            EventComparer comparer = GetComparer(sortField, page);
            eventsList.Sort(comparer);

            var pagedEvents = eventsList.AsPagination(page.GetValueOrDefault(1), 15);

            return View(new EventViewerViewModel(pagedEvents, AggregateId));
        }

        [HttpGet]
        public ActionResult Details(string aggregateId, int? version)
        {
            IIdentity id = new NullId();
            string aggregateType = string.Empty;

            if (!string.IsNullOrEmpty(aggregateId))
            {
                aggregateType = _storage.GetAggregateType(aggregateId);
                id = GetTypedAggregateId(aggregateId, aggregateType);
            }

            string reversedType = aggregateType.Reverse();
            ViewBag.AggregateType = reversedType.Substring(0, reversedType.IndexOf('.')).Reverse().Wordify();

            IEnumerable<IEvent<IIdentity>> eventList = _storage.GetEventsForAggregate(id, false);
            IEvent<IIdentity> foundEvent = eventList.Where(p => p.Version == version).First();

            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            JsConfig.ExcludeTypeInfo = true;
            ViewBag.EventDetails = HtmlEncode(JsvFormatter.Format(JsonSerializer.SerializeToString(foundEvent)));

            return PartialView("_EventDetails");
        }

        #region Helpers

        private EventComparer GetComparer(string sortField, int? page)
        {
            EventComparer comparer;

            if (string.IsNullOrEmpty(sortField))
            {
                Session["SortOrder"] = SortOrderEnum.ASC;
                Session["SortField"] = SortFieldEnum.DATE;
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

        private List<EventViewModel> GetEvents(List<IEvent<IIdentity>> list)
        {
            List<EventViewModel> eventsList = new List<EventViewModel>();

            foreach (IEvent item in list)
            {
                eventsList.Add(new EventViewModel(item));
            }

            return eventsList;
        }

        private static string HtmlEncode(string jsonFormattedStr)
        {
            return jsonFormattedStr.Replace("\r\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
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
