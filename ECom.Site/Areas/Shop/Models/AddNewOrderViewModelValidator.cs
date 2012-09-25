using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace ECom.Site.Areas.Shop.Models
{
	public class AddNewOrderViewModelValidator : AbstractValidator<AddNewOrderViewModel>
	{
		public AddNewOrderViewModelValidator()
		{
			RuleFor(m => m.ProductUrl).NotEmpty();
			RuleFor(m => m.Name).NotEmpty();
			RuleFor(m => m.Price).NotNull().GreaterThan(0);
			RuleFor(m => m.Quantity).NotNull().GreaterThan(0);
		}
	}
}