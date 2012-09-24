using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;

namespace ECom.ReadModel.Parsers
{
	public class ProductPageInfo
	{
		public ProductPageInfo(string name, string description, decimal price, string imageUrl)
		{
			Name = name;
			Description = description;
			Price = price;
			ImageUrl = imageUrl;
		}

		public string Name { get; private set; }
		public string Description { get; private set; }
		public decimal Price { get; private set; }
		public string ImageUrl { get; private set; }
	}
}
