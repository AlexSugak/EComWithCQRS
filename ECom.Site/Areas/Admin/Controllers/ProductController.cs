using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECom.ReadModel;
using ECom.Site.Areas.Admin.Models;
using ECom.Messages;
using ECom.Site.Controllers;
using ECom.Site.Models;
using MvcContrib.Pagination;
using ECom.ReadModel.Views;
using ECom.Site.Core;

namespace ECom.Site.Areas.Admin.Controllers
{
    public class ProductController : CqrsController
    {
        public ProductController()
			: base()
        {
        }

		[HttpGet]
        public ActionResult Index(int? page)
        {
			int totalCount;
			var products = _readModel.GetProducts(page.GetValueOrDefault(1) - 1, 10, out totalCount)
									.AsPagination(page.GetValueOrDefault(1), 10, totalCount);

            return View(new ProductsListViewModel(products));
        }

		[HttpGet]
		public ActionResult Details(string id)
		{
			ProductDetails product = _readModel.GetProductDetails(new ProductId(id));
			IEnumerable<ProductRelationship> relatedProducts = 
				_readModel.GetProductRelationships(new ProductId(id));

			var model = new ProductDetailsViewModel(product, relatedProducts);

			return View(model);
		}
    }
}
