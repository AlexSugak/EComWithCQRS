using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;

namespace ECom.Site.Areas.Shop.Models
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            Items = new List<CartItemViewModel>().AsPagination(1);
        }

        public IPagination<CartItemViewModel> Items { get; set; }
        public decimal Total { get { return Items.Sum(i => i.Price); } }

        public AddressViewModel Address { get; set; }
        public CreditCardViewModel CreditCard { get; set; }
    }

    public class CartItemViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}