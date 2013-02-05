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

        public EventViewerViewModel(List<EventWrapper> events, string selectedEventType, string aggregateId)
        {
            Argument.ExpectNotNull(() => events);

            Events              = events;
            AvailableEventTypes = GetEventTypes();
            SelectedEventType   = selectedEventType;
            AggregateId         = aggregateId;
        }

        public List<EventWrapper> Events { get; set; }
        public List<SelectListItem> AvailableEventTypes { get; set; }
        public string SelectedEventType { get; set; }

        [Required(ErrorMessage = "aggregate id is required")]
        public string AggregateId { get; set; }


        #region SomeMethods

        private List<SelectListItem> GetEventTypes()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var aggregateRootName in GetAggregateRoots())
            {
                items.Add(new SelectListItem { Text = StringExtentions.Wordify(aggregateRootName), Value = aggregateRootName });
            }

            return items;
        }

        private List<string> GetAggregateRoots()
        {
            var type = typeof(AggregateRoot<>);
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(s => s.GetTypes())
                .Where(p => IsSubclassOfRawGeneric(type, p) && p != type);

            List<string> listOfTypes = new List<string>();

            foreach (var item in types)
            {
                listOfTypes.Add(item.Name);
            }

            return listOfTypes;
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