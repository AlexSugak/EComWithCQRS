using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text.RegularExpressions;
using ECom.Messages;
using System.Text;
using ECom.Utility;

namespace ECom.Site.Helpers
{
	public static class CommandHelpers
	{
		public static MvcHtmlString Command(this HtmlHelper helper, Command cmd)
		{
			Argument.ExpectNotNull(() => cmd);

			StringBuilder builder = new StringBuilder();
			builder.Append(helper.Partial("CommandControl", cmd));
			return MvcHtmlString.Create(builder.ToString());
		}

		public static MvcHtmlString SplitCamelCase(this HtmlHelper helper, string stringInCamelCase)
		{
			return MvcHtmlString.Create(stringInCamelCase.Wordify());
		}
	}
}