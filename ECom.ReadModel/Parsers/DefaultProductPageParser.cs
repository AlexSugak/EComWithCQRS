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

			string name = GetValueSomewhereInThePage(document, "name", "title", metaTags);
			string description = GetValueSomewhereInThePage(document, "description", "description", metaTags);
			string priceText = GetValueSomewhereInThePage(document, "price", "price", metaTags);
			string imageUrl = GetValueSomewhereInThePage(document, "image", "image", metaTags);

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

		private string GetValueSomewhereInThePage(HtmlNode document, string propertyName, string metaName, IEnumerable<HtmlNode> metaTags)
		{
			string result = null;

			var hiddenField = document.FindHiddenField(propertyName);
			if (hiddenField != null)
			{
				result = hiddenField.GetAttributeValue("value");
			}

			if (String.IsNullOrWhiteSpace(result))
			{
				var ogMetaField = metaTags.FirstOrDefault(m => m.HasAttribute("property") && m.GetAttributeValue("property") == "og:" + propertyName);
				if (ogMetaField != null)
				{
					result = ogMetaField.GetAttributeValue("content");
				}
			}

			if (String.IsNullOrWhiteSpace(result))
			{
				var customMetaField = metaTags.FirstOrDefault(m => m.HasAttribute("name") && m.GetAttributeValue("name") == metaName);
				if (customMetaField != null)
				{
					result = customMetaField.GetAttributeValue("content");
				}
			}

			return result;
		}
	}
}
