using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Globalization;
using System.Net;
using System.Web;

namespace ECom.ReadModel.Parsers
{
	public class GapProductPageParser : IProductPageParser
	{
		private readonly string _host;

		public GapProductPageParser(string hostName)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => hostName);

			_host = hostName;
		}

		public ProductPageInfo Parse(Uri productUri)
		{
			Argument.ExpectNotNull(() => productUri);

			var uriString = productUri.ToString();
			var parsedQueryString = HttpUtility.ParseQueryString(uriString.Substring(uriString.IndexOf('?')));

			var productDetailsAjaxUrl =
				new Uri(
					String.Format(CultureInfo.InvariantCulture,
					"http://{0}/browse/productData.do?pid={1}&vid={2}&scid=&actFltr=false&locale=en_US&internationalShippingCurrencyCode=&internationalShippingCountryCode=us&globalShippingCountryCode=us",
					_host,
					parsedQueryString["pid"],
					parsedQueryString.AllKeys.Contains("vid") ? parsedQueryString["vid"] : "1"));

			//var productDetailsAjaxUrl = new Uri("http://oldnavy.gap.com/browse/productData.do?pid=251910&vid=1&scid=&actFltr=false&locale=en_US&internationalShippingCurrencyCode=&internationalShippingCountryCode=us&globalShippingCountryCode=us");

			var client = new WebClient();
			string productDetailsContent = client.DownloadString(productDetailsAjaxUrl);

			//ProductStyle("1","140760","","5123215","261",false,false,false,false,'','',"2093",false,'$44.95 $32.50',true,false,false,false,0,false,false,false,true,99,5,"Flat front sun-washed shorts (9.5")",'Color',"","Machine wash.","","false","false","false",0,0,"C1","1","",true)
			//ProductStyle("1","289852","","5060587","247",false,false,true,false,'','',"2094",false,'$49.95',true,false,false,false,0,false,true,false,true,99,5,"Flannel plaid shirt",'Color',"","Machine wash.","","false","false","false",0,0,"C1","1","",true);

			string productStyle = productDetailsContent.Substring(productDetailsContent.IndexOf("(") + 1, productDetailsContent.IndexOf(");var"));
			var tokens = productStyle.Split(",".ToCharArray());

			string productName = HttpUtility.HtmlDecode(tokens[25].Replace("\"", String.Empty));

			//'<span class="priceDisplay"><span class="priceDisplayStrike">$44.95</span><span class="brandBreak">&#160;</span><span class="priceDisplaySale">$32.50</span></span>'
			decimal price = Decimal.Parse(GetPriceText(tokens[13]), NumberStyles.Currency);

			return new ProductPageInfo(productName, String.Empty, price, null);
		}

		private string GetPriceText(string priceHtml)
		{
			var result = new StringBuilder();
			for (int i = priceHtml.LastIndexOf("$"); i < priceHtml.Length; i++)
			{
				if (priceHtml[i] == "<".ToCharArray()[0])
				{
					break;
				}

				result.Append(priceHtml[i]);
			}

			return result.ToString();
		}
	}
}
