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

		public IEnumerable<CategoryNodeDto> GetCategories()
		{
			return _repository.All<CategoryNodeDto>();
		}

		public CategoryDetailsDto GetCategoryDetails(string name)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => name);

			return _repository.Single<CategoryDetailsDto>(p => p.ID == name);
		}

		public IEnumerable<ProductListDto> GetProducts(int pageNum, int pageSize, out int totalCount)
        {
			return GetDtosPaged<ProductListDto>(pageNum, pageSize, out totalCount);
        }

        public IEnumerable<ProductListDto> GetCategoryProducts(string categoryName)
        {
            Argument.ExpectNotNullOrWhiteSpace(() => categoryName);

            return _repository.Find<ProductListDto>(p => p.Category == categoryName);
        }

        public ProductDetailsDto GetProductDetails(ProductId id)
        {
			Argument.ExpectNotNull(() => id);

			var productId = id.GetId();
			return _repository.Single<ProductDetailsDto>(p => p.ID == productId);
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
