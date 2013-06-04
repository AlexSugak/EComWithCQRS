using ECom.Messages;
using ECom.ReadModel.Parsers;
using ECom.ReadModel.Views;
using ECom.Site.Areas.Shop.Models;
using ECom.Site.Controllers;
using ECom.Site.Core;
using ECom.Utility;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;

namespace ECom.Site.Areas.Shop.Controllers
{
	[Authorize]
    public class OrderController : CqrsController
    {
        private readonly IUserOrdersView _orderDetailsView;
        private readonly IOrderItemsView _orderItemsView;
        private readonly IUserDetailsView _userDetailsView;
        private readonly IUserActiveOrderView _activeOrderView;

        public OrderController()
        {
            _orderDetailsView = new UserOrdersView(ServiceLocator.DtoManager);
            _orderItemsView = new OrderItemsView(ServiceLocator.DtoManager);
            _userDetailsView = new UserDetailsView(ServiceLocator.DtoManager);
            _activeOrderView = new UserActiveOrderView(ServiceLocator.DtoManager);
        }

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

            UserOrderDetails userOrder = ThrowNotFoundIfNull(_orderDetailsView.GetOrderDetails(UserId, id),  id, "Order {0} not found for user {1}", id.Id, UserId.Id);

            IEnumerable<OrderItemDetails> orderItems = _orderItemsView.GetOrderItems(id);

			return View(new OrderDetailsViewModel(userOrder, orderItems));
		}

		[HttpGet]
		public ActionResult MyOrders()
		{
            IEnumerable<UserOrderDetails> userOrders = _orderDetailsView.GetUserOders(UserId);

			return View(new UserOrdersViewModel(userOrders));
		}

		[HttpPost]
		[ActionSpecificValidator(typeof(AddNewOrderViewModelBeforeSubmitValidator))]
		public ActionResult Submit(AddNewOrderViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.IsEmailChanged)
				{
					_bus.Send(new SetUserEmail(UserId, new EmailAddress(model.Email), 0));
					Thread.Sleep(200);
				}

				_bus.Send(new SubmitOrder(model.OrderId));

				return RedirectToAction("Submitted", new { id = model.OrderId.Id });
			}

			return View("Add", model);
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
            OrderId activeOrderId = _activeOrderView.GetUserActiveOrderId(UserId);
            UserDetails userDetails = _userDetailsView.GetUserDetails(UserId);

			if (activeOrderId == null)
			{
				activeOrderId = new OrderId((int)ServiceLocator.IdentityGenerator.GenerateNewId());
				_bus.Send(new CreateNewOrder(activeOrderId, UserId));
			}

			var model = new AddNewOrderViewModel(activeOrderId, userDetails.Email);

			return View(model);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Add(AddNewOrderViewModel model)
		{
			Argument.ExpectNotNull(() => model);

			if (ModelState.IsValid)
			{
				var itemId = new OrderItemId((int)ServiceLocator.IdentityGenerator.GenerateNewId());
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
								String.IsNullOrEmpty(model.ImageUrl) ? null : new Uri(model.ImageUrl), 0);
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

			try
			{
				var url = new Uri(productUrl);
				IProductPageParser parser = ServiceLocator.ProductPageParserFactory.Create(url);

				ProductPageInfo productInfo = parser.Parse(url);

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
			catch (Exception)//if product page parsing failed - ignore and let user enter all product details manually
			{
			}

			return Json(new { parsed = false }, JsonRequestBehavior.AllowGet);
		}
    }
}
