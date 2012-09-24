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
	public class AbercrombieProductPageParser : ProductPageParser
	{
		protected override ProductPageInfo ParsePage(HtmlNode document)
		{
			IEnumerable<HtmlNode> metaTags = document.QuerySelectorAll("meta");

			//let if fire a NRE if some element is not found
			string name = document.FindHiddenField("name").GetAttributeValue("value");
			string description = metaTags.First(m => m.GetAttributeValue("property") == "og:description").GetAttributeValue("content");
			string imageUrl = metaTags.First(m => m.GetAttributeValue("property") == "og:image").GetAttributeValue("content");
			string priceText = document.FindHiddenField("price").GetAttributeValue("value");

			return new ProductPageInfo(name, description, Decimal.Parse(priceText, NumberStyles.Currency), imageUrl);
		}
	}
}
