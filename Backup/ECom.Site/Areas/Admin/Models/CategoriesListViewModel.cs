using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel.Views;
using MvcContrib.Pagination;
using ECom.Utility;
using ECom.Messages;

namespace ECom.Site.Areas.Admin.Models
{
	public class CategoriesListViewModel
	{
		public CategoriesListViewModel()
		{
		}

		public CategoriesListViewModel(IPagination<CategoryDetailsDto> categories)
		{
			Argument.ExpectNotNull(() => categories);

			Categories = categories;
		}

		public IPagination<CategoryDetailsDto> Categories { get; set; }

		public CreateCategory CreateCategory { get { return new CreateCategory(Guid.NewGuid()); } }
	}
}