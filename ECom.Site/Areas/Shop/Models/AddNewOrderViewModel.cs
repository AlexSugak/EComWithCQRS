using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.Messages;
using ECom.ReadModel.Views;
using ECom.Utility;
using FluentValidation.Attributes;

namespace ECom.Site.Areas.Shop.Models
{
	[Validator(typeof(AddNewOrderViewModelValidator))]
	public class AddNewOrderViewModel
	{
		public AddNewOrderViewModel()
		{
		}

		public AddNewOrderViewModel(OrderId orderId, string userEmail)
		{
			Argument.ExpectNotNull(() => orderId);

			OrderId = orderId;
			Email = userEmail;
			OriginalEmail = userEmail;
		}

		public OrderId OrderId { get; set; }

		public string ProductUrl { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public decimal? Price { get; set; }
		public string ImageUrl { get; set; }

		public string Size { get; set; }
		public string Color { get; set; }
		public int? Quantity { get; set; }

		public string OriginalEmail { get; set; }
		public string Email { get; set; }

		public bool IsEmailChanged { get { return OriginalEmail != Email; } }

		public List<OrderItemDetails> Items 
		{ 
			get 
			{
				return ServiceLocator.ReadModel.GetOrderItems(OrderId).ToList();
			} 
		}

		public decimal Total
		{
			get 
			{
				return Items.Any() ? Items.Sum(i => i.Total) : 0;
			}
		}
	}
}