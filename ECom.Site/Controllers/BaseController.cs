using ECom.Messages;
using ECom.Utility;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace ECom.Site.Controllers
{
    public abstract class BaseController : Controller
    {
		/// <summary>
		/// Throws <see cref="EntityNotFoundException"/> if value passed is null or returns the value itself if it is not null
		/// </summary>
		protected T ThrowNotFoundIfNull<T>(T value, IIdentity entityId, string message = "", params object[] args)
		{
			if (value == null)
			{
				throw new ReferencedEntityNotFoundException(typeof(T).Name, entityId, string.Format(message, args), null);
			}

			return value;
		}

		/// <summary>
		/// Returnrs model's editor template view. 
		/// </summary>
		protected virtual PartialViewResult EditorFor(object model, string prefix = null)
		{
			Argument.ExpectNotNull(() => model);

			var result = PartialView(
					String.Format(CultureInfo.InvariantCulture, 
					"{0}Views/Shared/EditorTemplates/{1}.cshtml", GetAreaPath(), model.GetType().Name),
					model);

			if (!String.IsNullOrWhiteSpace(prefix))
			{
				var viewData = new ViewDataDictionary(result.ViewData)
				{
					TemplateInfo = new TemplateInfo
					{
						HtmlFieldPrefix = prefix
					}
				};

				result.ViewData = viewData;
			}

			return result;
		}

		protected virtual string GetAreaPath()
		{
			return "~/";
		}
    }
}
