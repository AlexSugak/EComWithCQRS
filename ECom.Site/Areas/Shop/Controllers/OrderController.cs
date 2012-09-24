using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECom.ReadModel.Parsers;
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
		public ActionResult Add()
		{
			var model = new AddNewOrderViewModel();
			return View(model);
		}

		[HttpPost]
		public ActionResult Add(AddNewOrderViewModel model)
		{
			Argument.ExpectNotNull(() => model);

			if (ModelState.IsValid)
			{


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

		[HttpGet]
        public ActionResult Checkout()
        {
            var model = GetStubCartModelFull();
            return View(model);
        }

        [HttpPost]
        public ActionResult Checkout(CartViewModel model)
        {
            ModelState.Clear();

            var address = model.Address;
            var card = model.CreditCard;
            address.ZipCode += 1;
            card.Number += 1;

            var returnModel = GetStubCartModel();
            returnModel.Address = address;
            returnModel.CreditCard = card;

            return View(returnModel);
        }

        [HttpPost]
        public ActionResult Address([Bind(Prefix="Address")]AddressViewModel model)
        {
            ModelState.Clear();

            model.ZipCode += 1;
            return EditorFor(model, "Address");
        }

        [HttpPost]
        public ActionResult CreditCard([Bind(Prefix = "CreditCard")]CreditCardViewModel model)
        {
            ModelState.Clear();

            model.Number += 1;
            return EditorFor(model, "CreditCard");
        }

        private CartViewModel GetStubCartModelFull()
        {
            var model = GetStubCartModel();

            model.CreditCard = new CreditCardViewModel
            {
                Number = "123 123 123",
                Type = "Visa"
            };

            model.Address = new AddressViewModel
            {
                City = "Kharkov",
                Street = "Bluhera",
                ZipCode = "11111",
                
            };

            return model;
        }

        private CartViewModel GetStubCartModel()
        {
            var model = new CartViewModel();
            model.Items = new List<CartItemViewModel>() 
            {
                new CartItemViewModel{ProductId = "123", ProductName="Pants", Price = 12, Quantity = 1},
                new CartItemViewModel{ProductId = "234", ProductName="Shirt", Price = 23, Quantity = 2}
            }.AsPagination(1, 2, 2);

            return model;
        }
    }
}
