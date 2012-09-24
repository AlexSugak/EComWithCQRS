using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel.Views;
using ECom.Utility;

namespace ECom.Site.Areas.Shop.Models
{
	public class AddNewOrderViewModel
	{
		public AddNewOrderViewModel()
		{
		}

		public AddNewOrderViewModel(IEnumerable<OrderItemDetails> items)
		{
			Argument.ExpectNotNull(() => items);

			Items = items.ToList();
		}

		public string ProductUrl { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public string Price { get; set; }
		public string ImageUrl { get; set; }

		public string Size { get; set; }
		public string Color { get; set; }


		public List<OrderItemDetails> Items { get; set; }
	}
}