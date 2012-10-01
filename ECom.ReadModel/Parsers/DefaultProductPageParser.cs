using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ECom.Utility;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Globalization;

namespace ECom.ReadModel.Parsers
{
	public class DefaultProductPageParser : ProductPageParser
	{
		protected override ProductPageInfo ParsePage(HtmlNode document)
		{
			IEnumerable<HtmlNode> metaTags = document.QuerySelectorAll("meta");

			string name = GetValueSomewhereInThePage(document, new[] { "name", "title" }, metaTags);
			string description = GetValueSomewhereInThePage(document, new[] { "description" }, metaTags);
			string priceText = GetValueSomewhereInThePage(document, new[] { "price" }, metaTags);
			string imageUrl = GetValueSomewhereInThePage(document, new[] { "image" }, metaTags);

			if (!String.IsNullOrWhiteSpace(name) 
				|| !String.IsNullOrWhiteSpace(description) 
				|| !String.IsNullOrWhiteSpace(priceText)
				|| !String.IsNullOrWhiteSpace(imageUrl))
			{
				decimal price = String.IsNullOrWhiteSpace(priceText) ? 0 : Decimal.Parse(priceText, NumberStyles.Currency);
				return new ProductPageInfo(name, description, price, imageUrl);
			}

			return null;
		}

		private string GetValueSomewhereInThePage(HtmlNode document, string[] names, IEnumerable<HtmlNode> metaTags)
		{
			string result = null;

			var possibleNames = names.SelectMany(n => new[] { n, "og:" + n });

			var metaField = metaTags.FirstOrDefault(m => m.HasAttribute("property") && possibleNames.Contains(m.GetAttributeValue("property")));
			if (metaField == null)
			{
				metaField = metaTags.FirstOrDefault(m => m.HasAttribute("name") && possibleNames.Contains(m.GetAttributeValue("name")));
			}

			if (metaField != null)
			{
				result = metaField.GetAttributeValue("content");
			}

			if (String.IsNullOrWhiteSpace(result))
			{
				foreach (var name in possibleNames)
				{
					if (!String.IsNullOrWhiteSpace(result))
					{
						break;
					}

					var hiddenField = document.FindHiddenField(name);
					if (hiddenField != null)
					{
						result = hiddenField.GetAttributeValue("value");
					}
				}
			}

			return result;
		}
	}
}
