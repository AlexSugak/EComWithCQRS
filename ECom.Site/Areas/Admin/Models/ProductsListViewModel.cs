using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel.Views;
using ECom.Utility;
using ECom.Messages;
using MvcContrib.Pagination;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using ECom.Site.Models;


namespace ECom.Site.Areas.Admin.Models
{
    public class ProductsListViewModel
    {
        public ProductsListViewModel()
        {
        }

		public ProductsListViewModel(IPagination<ProductList> products)
        {
			Argument.ExpectNotNull(() => products);

            Products = products;
        }

        public IPagination<ProductList> Products { get; set; }

        public AddProduct AddProductCommand 
        {
            get 
            {
                var cmd = new AddProduct();
                return cmd;
            }
        }
    }
}