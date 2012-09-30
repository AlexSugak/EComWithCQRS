
/*** HtmlTemplateBase<> ***/
/*** Author: Abu Haider 
/*** September, 2011
/*** To be used as the Template base with the RazorEngine on CodePlex
/*** http://razorengine.codeplex.com
/*** Copyright 2011, Abu Haider, www.haiders.net
/*** Use at your own risk
/***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RazorEngine;
using RazorEngine.Templating;
using System.Web.Mvc;
using System.Web.WebPages;

namespace System.Web.Mvc
{
	[RequireNamespaces("System.Web.Mvc.Html")]
	public class HtmlTemplateBase<T> : TemplateBase<T>, IViewDataContainer
	{
		private HtmlHelper<T> helper = null;
		private ViewDataDictionary viewdata = null;
		private System.Dynamic.DynamicObject viewbag = null;


		public dynamic ViewBag
		{
			get
			{
				return (WebPageContext.Current.Page as WebViewPage).ViewBag;
			}
		}

		public HtmlHelper<T> Html
		{
			get
			{
				if (helper == null)
				{
					var p = WebPageContext.Current;
					var wvp = p.Page as WebViewPage;
					var context = wvp != null ? wvp.ViewContext : null;

					helper = new HtmlHelper<T>(context, this);
				}
				return helper;
			}
		}

		public ViewDataDictionary ViewData
		{
			get
			{
				if (viewbag == null)
				{
					var p = WebPageContext.Current;
					var viewcontainer = p.Page as IViewDataContainer;
					viewdata = new ViewDataDictionary(viewcontainer.ViewData);

					if (this.Model != null)
					{
						viewdata.Model = Model;
					}

				}

				return viewdata;
			}
			set
			{
				viewdata = value;
			}
		}
	}

}
