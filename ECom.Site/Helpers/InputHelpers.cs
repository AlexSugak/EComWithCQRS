using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ECom.Site.Helpers
{
	public static class InputHelpers
	{
		public static MvcHtmlString FormDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string title)
		{
			var result = new StringBuilder();

			var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

			result.AppendFormat("<dt>{0}:</dt>", title);
			result.AppendFormat("<dd>{0}</dd>", html.DisplayFor(expression));

			return new MvcHtmlString(result.ToString());
		}

		public static MvcHtmlString FormEditorFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string title, string @class = "")
		{
			var result = new StringBuilder();

			var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

			result.AppendFormat("<div class='control-group {0}'>", HasErrors(html.ViewData, metadata.PropertyName) ? "error" : String.Empty);
			result.AppendFormat("<label class='control-label'>{0}</label>", title);
			result.Append("<div class='controls'>");
			//result.AppendFormat("{0} {1}", html.TextBoxFor(expression, new { @class = @class }), html.ValidationMessageFor(expression));
			result.Append(html.TextBoxFor(expression, new { @class = @class }));
			result.Append("</div>");
			result.Append("</div>");

			return new MvcHtmlString(result.ToString());
		}

		private static bool HasErrors<TModel>(ViewDataDictionary<TModel> viewData, string propertyName)
		{
			return viewData.ModelState.ContainsKey(propertyName)
				&& viewData.ModelState[propertyName] != null
				&& viewData.ModelState[propertyName].Errors != null
				&& viewData.ModelState[propertyName].Errors.Count > 0;
		}
	}
}