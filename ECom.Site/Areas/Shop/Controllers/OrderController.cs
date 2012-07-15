using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECom.Site.Areas.Shop.Models;
using ECom.Site.Controllers;
using ECom.Site.Core;

namespace ECom.Site.Areas.Shop.Controllers
{
    public class OrderController : CqrsController
    {
        protected override string GetAreaPath()
        {
            return "~/Areas/Shop/";
        }

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
