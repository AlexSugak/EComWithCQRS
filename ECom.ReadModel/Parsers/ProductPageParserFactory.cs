using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ECom.ReadModel.Parsers
{
	public class ProductPageParserFactory
	{
		public IProductPageParser Create(Uri productPageUri)
		{
			string shopName = productPageUri.Host;

			switch (shopName)
			{
				case "gap.com":
				case "www.gap.com":
					return new GapProductPageParser("www.gap.com");
				case "oldnavy.gap.com":
				case "www.oldnavy.gap.com":
					return new GapProductPageParser("oldnavy.gap.com");
				case "kohls.com":
				case "www.kohls.com":
					return new KohlsProductPageParser();

				case "abercrombie.com":
				case "www.abercrombie.com":

				case "hollisterco.com":
				case "www.hollisterco.com":

				case "abercrombiekids.com":
				case "www.abercrombiekids.com":

				case "gillyhicks.com":
				case "www.gillyhicks.com":
					return new AbercrombieProductPageParser();
				default:
					//throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture, "Shop '{0}' is not supported", shopName));
					return new DefaultProductPageParser();
			}
		}
	}
}
