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

		public ProductsListViewModel(IPagination<ProductListDto> products)
        {
			Argument.ExpectNotNull(() => products);

            Products = products;
        }

        public IPagination<ProductListDto> Products { get; set; }

        public AddProduct AddProductCommand 
        {
            get 
            {
                var cmd = new AddProduct { Id = new ProductId(Guid.NewGuid()) };
                return cmd;
            }
        }
    }


    interface IProp
    {
        string Value { get; set; }
    }

    class Prop : IProp
    {
        public string Value { get; set; }
    }

    interface ITest<out T>
        where T : IProp
    {
        T Prop { get; }
    }

    class Test : ITest<Prop>
    {
        public Prop Prop { get; set; }
    }

    static class TestExtentions
    {
        public static string Test(this ITest<IProp> t)
        {
            return t.Prop.Value;
        }
    }

    class TestProg
    {
        void Test()
        {
            var test = new Test();
            var value = test.Test();
        }
    }
}