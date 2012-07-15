using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel.Views;
using ECom.Utility;
using ECom.Messages;
using MvcContrib.Pagination;


namespace ECom.Site.Areas.Admin.Models
{
    public class ProductsListViewModel
    {
        public ProductsListViewModel()
        {
        }

		public ProductsListViewModel(IPagination<ProductListDto> products)
        {
			Argument.ExpectNotNull(() => products);

            Products = products;
        }

        public IPagination<ProductListDto> Products { get; set; }

		public AddProduct AddProductCommand { get { return new AddProduct(Guid.NewGuid()); } }
    }
}