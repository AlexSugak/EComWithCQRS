using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.Site.Controllers;
using System.Web.Mvc;
using ECom.Site.Core;
using ECom.Site.Areas.Admin.Models;
using ECom.Messages;

namespace ECom.Site.Areas.Admin.Controllers
{
	public class CategoryController : CqrsController
	{
		public CategoryController()
			: base()
		{
		}


		[HttpGet]
		public ActionResult Index(int? page)
		{
			int totalCount;
			var products = _readModel.GetCategories(page.GetValueOrDefault(1) - 1, 10, out totalCount)
									.AsPagination(page.GetValueOrDefault(1), 10, totalCount);

			return View(new CategoriesListViewModel(products));
		}

		[HttpPost]
		public ActionResult CreateCategory(CreateCategory cmd)
		{
			return SubmitCommand(cmd);
		}
	}
}