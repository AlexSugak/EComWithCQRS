using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ECom.Utility;
using HtmlAgilityPack;

namespace ECom.ReadModel.Parsers
{
	public abstract class ProductPageParser : IProductPageParser
	{
		public ProductPageInfo Parse(Uri productUri)
		{
			Argument.ExpectNotNull(() => productUri);

			var client = new WebClient();
			string productDetailsContent = client.DownloadString(productUri);

			var html = new HtmlDocument();
			html.LoadHtml(productDetailsContent);

			HtmlNode document = html.DocumentNode;

			return ParsePage(document);
		}

		protected abstract ProductPageInfo ParsePage(HtmlNode document);
	}
}
