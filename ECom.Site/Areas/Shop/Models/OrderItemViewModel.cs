using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel.Views;

namespace ECom.Site.Areas.Shop.Models
{
	public class OrderItemViewModel
	{
		public OrderItemViewModel()
		{
		}

		public OrderItemViewModel(OrderItemDetails orderItem)
		{

		}

		public string ProductUrl { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }

		public string Size { get; set; }
		public string Color { get; set; }
	}
}