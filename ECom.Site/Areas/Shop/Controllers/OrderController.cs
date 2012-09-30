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
		private UserId UserId
		{
			get { return new UserId(User.Identity.Name); }
		}

        protected override string GetAreaPath()
        {
            return "~/Areas/Shop/";
        }

		[HttpGet]
		public ActionResult Details(OrderId id)
		{
			Argument.ExpectNotNull(() => id);

			UserOrderDetails userOrder = _readModel.GetOrderDetails(id);
			IEnumerable<OrderItemDetails> orderItems = _readModel.GetOrderItems(id);

			return View(new OrderDetailsViewModel(userOrder, orderItems));
		}

		[HttpGet]
		public ActionResult MyOrders()
		{
			IEnumerable<UserOrderDetails> userOrders = _readModel.GetUserOders(UserId);

			return View(new UserOrdersViewModel(userOrders));
		}

		[HttpPost]
		public ActionResult Submit(AddNewOrderViewModel model)
		{
			var cmd = new SubmitOrder(model.OrderId);
			_bus.Send(cmd);

			return RedirectToAction("Submitted", new { id = model.OrderId.Id });
		}

		[HttpGet]
		public ActionResult Submitted(OrderId id)
		{
			return View();
		}

		[HttpGet]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public ActionResult Add()
		{
			OrderId activeOrderId = _readModel.GetUserActiveOrderId(UserId);

			if (activeOrderId == null)
			{
				activeOrderId = new OrderId(ServiceLocator.IdentityGenerator.GenerateNewId());
				_bus.Send(new CreateNewOrder(activeOrderId, UserId));
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
