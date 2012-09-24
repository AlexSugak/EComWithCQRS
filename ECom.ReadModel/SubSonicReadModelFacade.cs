using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic.Repository;
using ECom.Utility;
using ECom.ReadModel.Views;
using ECom.Messages;

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


		public IEnumerable<OrderItemDetails> GetOrderItems(OrderId orderId)
		{
			Argument.ExpectNotNull(() => orderId);

			string rawOrderId = orderId.GetId();
			return _repository.Find<OrderItemDetails>(i => i.OrderId == rawOrderId);
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
