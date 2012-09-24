using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel.Views;
using ECom.Utility;

namespace ECom.Site.Areas.Admin.Models
{
	public class ProductDetailsViewModel
	{
		public ProductDetailsViewModel(ProductDetails productDetails, IEnumerable<ProductRelationship> related)
		{
			Argument.ExpectNotNull(() => productDetails);
			Argument.ExpectNotNull(() => related);

			ProductDetails = productDetails;
			RelatedProducts = related.ToList();
		}

		public ProductDetails ProductDetails { get; set; }
		public List<ProductRelationship> RelatedProducts { get; set; }
	}
}