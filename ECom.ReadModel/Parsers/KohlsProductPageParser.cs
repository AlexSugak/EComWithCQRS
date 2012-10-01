using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ECom.Utility;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Globalization;

namespace ECom.ReadModel.Parsers
{
	public class KohlsProductPageParser : ProductPageParser
	{
		protected override ProductPageInfo ParsePage(HtmlNode document)
		{
			var metaTags = document.QuerySelectorAll("meta");

			//let if fire NRE if some element is not found
			string name = metaTags.First(m => m.GetAttributeValue("name") == "title").GetAttributeValue("content");
			string description = metaTags.First(m => m.GetAttributeValue("name") == "description").GetAttributeValue("content");
			string imageUrl = metaTags.First(m => m.GetAttributeValue("property") == "og:image").GetAttributeValue("content");

			var priceHidden = document.FindHiddenField("ADD_CART_ITEM<>salePriceAmt");

			if (priceHidden == null)
			{
				priceHidden = document.FindHiddenField("ADD_CART_ITEM_ARRAY<>salePriceAmt");
			}

			string priceText = priceHidden.GetAttributeValue("value");

			return new ProductPageInfo(name, description, Decimal.Parse(priceText, NumberStyles.Currency), imageUrl);
		}
	}
}
