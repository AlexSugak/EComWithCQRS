using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel.Views;
using ECom.Utility;

namespace ECom.Site.Areas.Shop.Models
{
	public class UserOrdersViewModel
	{
		public UserOrdersViewModel(IEnumerable<UserOrderDetails> orders)
		{
			Argument.ExpectNotNull(() => orders);

			Orders = orders.ToList();
		}

		public List<UserOrderDetails> Orders { get; set; }
	}
}