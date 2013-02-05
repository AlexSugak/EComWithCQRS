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

namespace ECom.Site.Areas.Admin.Controllers
{
    public class EventViewerController : CqrsController
    {
        private readonly IEventStore _storage;

        public EventViewerController()
			: base()
		{
            _storage = new EventStore.SQL.EventStore(ConfigurationManager.ConnectionStrings["EventStore"].ConnectionString, _bus);
		}
        
        [HttpGet]
        public ActionResult Index(string aggregateType, string AggregateId, string sortField)
        {
            IIdentity id = new OrderId(0);
            
            if (ModelState.IsValidField("AggregateId") && !string.IsNullOrEmpty(AggregateId))
            {
                id = ConvertAggregateId(aggregateType, AggregateId, id);
            }
            
            var eventsList = _storage.GetEventsForAggregateWithDate(id).ToList();
            
            EventComparer comparer = GetComparer(sortField);
            eventsList.Sort(comparer);

            return View(new EventViewerViewModel(eventsList, aggregateType, AggregateId));
        }

        [HttpGet]
        public ActionResult Details(string aggregateType, string aggregateId, int? version)
        {
            IIdentity id = new OrderId(0);

            if (ModelState.IsValidField("AggregateId") && !string.IsNullOrEmpty(aggregateId))
            {
                id = ConvertAggregateId(aggregateType, aggregateId, id);
            }

            var eventsList = _storage.GetEventsForAggregateWithDate(id).ToList();

            var foundEvent = eventsList.Where(p => p.EventVersion == version).First();
            ViewBag.EventType = aggregateType.Wordify();

            return PartialView("_EventDetails", foundEvent);
        }

        private EventComparer GetComparer(string sortField)
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

                if (sortField == savedValue.ToString())
                {
                    Session["SortOrder"] = (SortOrderEnum)((int)Session["SortOrder"] * (-1));
                }
                else
                {
                    Session["SortOrder"] = SortOrderEnum.ASC;
                    Session["SortField"] = (SortFieldEnum)Enum.Parse(typeof(SortFieldEnum), sortField);
                }

                comparer = new EventComparer((SortFieldEnum)Session["SortField"], (SortOrderEnum)Session["SortOrder"]);
            }

            return comparer;
        }

        private static IIdentity ConvertAggregateId(string aggregateType, string AggregateId, IIdentity id)
        {
            switch (aggregateType)
            {
                case "CatalogAggregate":
                    Guid catalogId;
                    Guid.TryParse(AggregateId, out catalogId);

                    id = new CatalogId(catalogId);
                    break;
                case "OrderAggregate":
                    int orderId;
                    int.TryParse(AggregateId, out orderId);

                    id = new OrderId(orderId);
                    break;
                case "UserAggregate":
                    id = new UserId(AggregateId);
                    break;
                case "ProductAggregate":
                    id = new ProductId(AggregateId);
                    break;
            }
            return id;
        }

    }
}
