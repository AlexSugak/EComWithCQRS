using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic.Repository;
using ECom.Utility;
using ECom.ReadModel.Views;
using ECom.Messages;
using ECom.Domain.Exceptions;

namespace ECom.ReadModel
{
    public class SubSonicReadModelFacade : IReadModelFacade
    {
        private SimpleRepository _repository;

        public SubSonicReadModelFacade(SimpleRepository repository)
        {
			Argument.ExpectNotNull(() => repository);

            _repository = repository;
        }

		public IEnumerable<CategoryNode> GetCategories()
		{
			return _repository.All<CategoryNode>();
		}

		public CategoryDetails GetCategoryDetails(string name)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => name);

			return _repository.Single<CategoryDetails>(p => p.ID == name);
		}

		public IEnumerable<ProductList> GetProducts(int pageNum, int pageSize, out int totalCount)
        {
			return GetDtosPaged<ProductList>(pageNum, pageSize, out totalCount);
        }

        public IEnumerable<ProductList> GetCategoryProducts(string categoryName)
        {
            Argument.ExpectNotNullOrWhiteSpace(() => categoryName);

            return _repository.Find<ProductList>(p => p.Category == categoryName);
        }

        public ProductDetails GetProductDetails(ProductId id)
        {
			Argument.ExpectNotNull(() => id);

			var productId = id.GetId();
			return _repository.Single<ProductDetails>(p => p.ID == productId);
        }

		public IEnumerable<ProductRelationship> GetProductRelationships(ProductId id)
		{
			Argument.ExpectNotNull(() => id);

			var productId = id.GetId();
			return _repository.Find<ProductRelationship>(p => p.ParentProductId == productId);
		}



		public UserDetails GetUserDetails(UserId userId)
		{
			Argument.ExpectNotNull(() => userId);

			string rawUserId = userId.GetId();
			return _repository.Single<UserDetails>(i => i.ID == rawUserId);
		}

		public OrderId GetUserActiveOrderId(UserId userId)
		{
			Argument.ExpectNotNull(() => userId);

			string rawUserId = userId.GetId();
			var activeOrder = _repository.Single<ActiveUserOrderDetails>(i => i.ID == rawUserId);
			if (activeOrder != null)
			{
				return new OrderId(activeOrder.OrderId);
			}

			return null;
		}

		public UserOrderDetails GetOrderDetails(UserId userId, OrderId orderId)
		{
			Argument.ExpectNotNull(() => userId);
			Argument.ExpectNotNull(() => orderId);

			var result = _repository.Single<UserOrderDetails>(UserOrderDetails.CompositeId(userId, orderId));

			if (result == null)
			{
				throw new EntityNotFoundException("Order {0} not found for user {1}", orderId, userId);
			}

			return result;
		}

		public IEnumerable<OrderItemDetails> GetOrderItems(OrderId orderId)
		{
			Argument.ExpectNotNull(() => orderId);

			int rawOrderId = orderId.Id;
			return _repository.Find<OrderItemDetails>(i => i.OrderId == rawOrderId);
		}

		public IEnumerable<UserOrderDetails> GetUserOders(UserId userId)
		{
			Argument.ExpectNotNull(() => userId);

			string rawUserId = userId.Id;
			return _repository.Find<UserOrderDetails>(i => i.UserId == rawUserId);
		}







		private IEnumerable<T> GetDtosPaged<T>(int pageNum, int pageSize, out int totalCount)
			where T : Dto, new()
		{
			Argument.Expect(() => pageNum >= 0, "pageNum", "page number must be greater than or equal to 0");
			Argument.Expect(() => pageSize > 0, "pageSize", "page size must be greater than or equal to 1");

			var list = _repository.GetPaged<T>(pageNum, pageSize);
			totalCount = list.TotalCount;

			return list;
		}
    }
}
