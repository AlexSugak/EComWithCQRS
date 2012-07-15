using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.Pagination;
using MvcContrib.UI.Pager;

namespace ECom.Site.Helpers
{
    public static class Bootstrap
    {
		public static BootPager BootPager(this HtmlHelper helper, IPagination pagination)
		{
			return new BootPager(pagination, helper.ViewContext);
		}
    }

	public class BootPager : IHtmlString
	{
		private IPagination _grid;
		private ViewContext _context;

		public BootPager(IPagination pagination, ViewContext context)
		{
			_grid = pagination;
			_context = context;
		}

		public string ToHtmlString()
		{
			if (_grid.TotalItems == 0 || _grid.TotalPages == 1)
			{
				return null;
			}

			var builder = new StringBuilder();

			builder.Append("<div class='pagination'>");
			builder.Append("<ul>");

			RenderPages(builder);

			builder.Append(@"</ul>");
			builder.Append(@"</div>");

			return builder.ToString();
		}

		private void RenderPages(StringBuilder builder)
		{
			RenderPage(builder, _grid.PageNumber - 1, _grid.PageNumber, "←", _grid.PageNumber == 1);

			for (int i = 0; i < _grid.TotalPages; i++)
			{
				RenderPage(builder, i + 1, _grid.PageNumber);
			}

			RenderPage(builder, _grid.PageNumber + 1, _grid.PageNumber, "→", _grid.PageNumber == _grid.TotalPages);
		}

		private void RenderPage(StringBuilder builder, int pageNum, int currentPage, string text = null, bool disabled = false)
		{
			if (pageNum == currentPage)
			{
				builder.Append("<li class='active'>");
			}
			else if(disabled)
			{
				builder.Append("<li class='disabled'>");
			}
			else
			{
				builder.Append("<li>");
			}

			builder.Append(CreatePageLink(pageNum, text ?? pageNum.ToString(), disabled));

			builder.Append("</li>");
		}

		private string CreatePageLink(int pageNumber, string text, bool disabled = false)
		{
			var builder = new TagBuilder("a");
			builder.SetInnerText(text);
			builder.MergeAttribute("href", disabled ? "#" : CreateDefaultUrl(pageNumber));
			return builder.ToString(TagRenderMode.Normal);
		}

		private string CreateDefaultUrl(int pageNumber)
		{
			var routeValues = new RouteValueDictionary();

			foreach (var key in _context.RequestContext.HttpContext.Request.QueryString.AllKeys.Where(key => key != null))
			{
				routeValues[key] = _context.RequestContext.HttpContext.Request.QueryString[key];
			}

			routeValues["page"] = pageNumber;

			var url = UrlHelper.GenerateUrl(null, null, null, routeValues, RouteTable.Routes, _context.RequestContext, true);
			return url;
		}
	}
}
