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
			var categories = _readModel.GetCategories();

			return View(new CategoriesTreeViewModel(categories));
		}
	}
}