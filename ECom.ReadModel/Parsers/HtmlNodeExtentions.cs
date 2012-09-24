using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Globalization;
using ECom.Utility;

namespace ECom.ReadModel.Parsers
{
	public static class HtmlNodeExtentions
	{
		public static bool HasAttribute(this HtmlNode document, string attributeName)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => attributeName);

			return document.Attributes.OfType<HtmlAttribute>()
							.Any(a => a.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase));
		}

		public static string GetAttributeValue(this HtmlNode document, string attributeName)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => attributeName);

			if (document.HasAttribute(attributeName))
			{
				return document.Attributes[attributeName].Value;
			}

			return null;
		}

		public static HtmlNode FindHiddenField(this HtmlNode document, string name)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => name);

			return document.QuerySelector(String.Format(CultureInfo.InvariantCulture, "input[type='hidden'][name='{0}']", name));
		}
	}
}
