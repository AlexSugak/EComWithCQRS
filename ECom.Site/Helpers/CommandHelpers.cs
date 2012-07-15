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
using System.Web.Script.Serialization;

namespace ECom.Site.Helpers
{
	public static class CommandHelpers
	{
        public static MvcHtmlString Command(this HtmlHelper helper, object cmd)
        {
            return Command(helper, cmd, null);
        }

        public static MvcHtmlString Command(this HtmlHelper helper, object cmd, string icon)
		{
			Argument.ExpectNotNull(() => cmd);

			StringBuilder builder = new StringBuilder();

            if (String.IsNullOrWhiteSpace(icon))
            {
                builder.Append(helper.Partial("CommandControl", cmd));
            }
            else 
            {
                builder.Append(helper.Partial("CommandControl", cmd, new ViewDataDictionary { { "cmd-icon", icon } }));
            }

			return MvcHtmlString.Create(builder.ToString());
		}

		public static MvcHtmlString SplitCamelCase(this HtmlHelper helper, string stringInCamelCase)
		{
			return MvcHtmlString.Create(stringInCamelCase.Wordify());
		}
	}
}