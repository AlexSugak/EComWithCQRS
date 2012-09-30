using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECom.Site.Helpers
{
	public static class UrlHelpers
	{
		public static string AbsoluteAction(this UrlHelper url, string action, string controller)
		{
			Uri requestUrl = url.RequestContext.HttpContext.Request.Url;

			string absoluteAction = String.Format("{0}://{1}{2}",
												  requestUrl.Scheme,
												  requestUrl.Host,
												  url.Action(action, controller));

			return absoluteAction;
		}
	}
}