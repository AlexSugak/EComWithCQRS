using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel.Views;
using ECom.Utility;

namespace ECom.Site.Areas.Shop.Models
{
	public class OrderDetailsViewModel
	{
		public OrderDetailsViewModel(UserOrderDetails details, IEnumerable<OrderItemDetails> items)
		{
			Argument.ExpectNotNull(() => details);
			Argument.ExpectNotNull(() => items);

			Details = details;
			Items = items.ToList();
		}

		public UserOrderDetails Details { get; set; }
		public List<OrderItemDetails> Items { get; set; }
	}
}