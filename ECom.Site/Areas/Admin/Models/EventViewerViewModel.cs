using ECom.Domain;
using ECom.Messages;
using ECom.ReadModel.Views;
using ECom.Site.Core;
using ECom.Utility;
using MvcContrib.Pagination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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