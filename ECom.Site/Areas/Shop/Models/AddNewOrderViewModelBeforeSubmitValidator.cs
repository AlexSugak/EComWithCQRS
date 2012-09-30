using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace ECom.Site.Areas.Shop.Models
{
	public class AddNewOrderViewModelBeforeSubmitValidator : AbstractValidator<AddNewOrderViewModel>
	{
		public AddNewOrderViewModelBeforeSubmitValidator()
		{
			RuleFor(m => m.Email).NotEmpty().EmailAddress();
		}
	}
}