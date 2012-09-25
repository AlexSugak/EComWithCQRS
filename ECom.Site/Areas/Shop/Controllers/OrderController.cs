using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ECom.Messages;
using ECom.ReadModel.Parsers;
using ECom.ReadModel.Views;
using ECom.Site.Areas.Shop.Models;
using ECom.Site.Controllers;
using ECom.Site.Core;
using ECom.Utility;

namespace ECom.Site.Areas.Shop.Controllers
{
	[Authorize]
    public class OrderController : CqrsController
    {
        protected override string GetAreaPath()
        {
            return "~/Areas/Shop/";
        }

		[HttpGet]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public ActionResult Add()
		{
			var userId = new UserId(User.Identity.Name);
			OrderId activeOrderId = _readModel.GetUserActiveOrderId(userId);

			if (activeOrderId == null)
			{
				activeOrderId = new OrderId(ServiceLocator.IdentityGenerator.GenerateNewId());
				_bus.Send(new CreateNewOrder(activeOrderId, userId));
			}

			var model = new AddNewOrderViewModel(activeOrderId);

			return View(model);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Add(AddNewOrderViewModel model)
		{
			Argument.ExpectNotNull(() => model);

			if (ModelState.IsValid)
			{
				var itemId = new OrderItemId(ServiceLocator.IdentityGenerator.GenerateNewId());
				var cmd = new AddProductToOrder(
								model.OrderId, 
								itemId,
								new Uri(model.ProductUrl), 
								model.Name, 
								model.Description, 
								model.Price.Value, 
								model.Quantity.Value, 
								model.Size, 
								model.Color, 
								String.IsNullOrEmpty(model.ImageUrl) ? null : new Uri(model.ImageUrl));
				_bus.Send(cmd);

				//TODO: figure out how to avoid this
				Thread.Sleep(100);

				return RedirectToAction("Add");
			}

			return View(model);
		}

		[HttpGet]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public JsonResult ParseProductUrl(string productUrl)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => productUrl);

			ProductPageInfo productInfo = null;

			try
			{
				var url = new Uri(productUrl);
				var parser = ServiceLocator.ProductPageParserFactory.Create(url);

				productInfo = parser.Parse(url);

				if (productInfo != null)
				{
					return Json(new 
					{ 
						parsed = true, 
						name = productInfo.Name, 
						description = productInfo.Description, 
						price = productInfo.Price,
						image = productInfo.ImageUrl
					}, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception)//if product page parsing failed - ignore and let user enter product details manually
			{
			}

			return Json(new { parsed = false }, JsonRequestBehavior.AllowGet);
		}
    }
}
