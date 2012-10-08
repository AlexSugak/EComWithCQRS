﻿using System;
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


		UserDetails GetUserDetails(UserId userId);

		OrderId GetUserActiveOrderId(UserId userId);

		UserOrderDetails GetOrderDetails(UserId userId, OrderId orderId);

		IEnumerable<OrderItemDetails> GetOrderItems(OrderId orderId);

		IEnumerable<UserOrderDetails> GetUserOders(UserId userId);
    }
}
