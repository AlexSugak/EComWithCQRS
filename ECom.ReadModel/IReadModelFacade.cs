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
		IEnumerable<CategoryNodeDto> GetCategories();
		CategoryNodeDto GetCategoryDetails(string id);

        IEnumerable<ProductListDto> GetCategoryProducts(string categoryName);

        IEnumerable<ProductListDto> GetProducts(int pageNum, int pageSize, out int totalCount);
        ProductDetailsDto GetProductDetails(ProductId id);
    }
}
