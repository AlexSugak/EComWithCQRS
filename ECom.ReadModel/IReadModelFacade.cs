using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.ReadModel.Views;

namespace ECom.ReadModel
{
    public interface IReadModelFacade
    {
		IEnumerable<CategoryNode> GetCategories();
		CategoryDetails GetCategoryDetails(string name);

        IEnumerable<ProductList> GetCategoryProducts(string categoryName);

        IEnumerable<ProductList> GetProducts(int pageNum, int pageSize, out int totalCount);

        ProductDetails GetProductDetails(ProductId id);

		IEnumerable<ProductRelationship> GetProductRelationships(ProductId id);



		IEnumerable<OrderItemDetails> GetOrderItems(OrderId orderId);
    }
}
