using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECom.Messages;
using ECom.ReadModel;
using ECom.Site.Core;
using ECom.Site.Models;
using ECom.Utility;

namespace ECom.Site.Controllers
{
	public abstract class CqrsController : Controller
	{
		protected Bus.Bus _bus;
        protected IReadModelFacade _readModel;

		public CqrsController()
        {
            _bus = ServiceLocator.Bus;
            _readModel = ServiceLocator.ReadModel;

			ActionInvoker = new CqrsControllerActionInvoker();
        }

		/// <summary>
		/// Default command action, used as fallback action when no specific command action is specified
		/// </summary>
		public virtual ActionResult SubmitCommand<T>(CommandEnvelopeViewModel<T> cmd) 
            where T : ICommand
		{
			Argument.ExpectNotNull(() => cmd);
            Argument.ExpectNotNull(() => cmd.Command);

			if (ModelState.IsValid)
			{
				try
				{
					_bus.Send(cmd.Command);
					ViewData["CommandProcessed"] = true;
				}
				catch (Exception e)
				{
					ModelState.AddModelError(String.Empty, e.Message);//TODO display friendly message
				}
			}

			return PartialView("CommandBody", cmd);
		}


        /// <summary>
        /// Returnrs model's editor template view. 
        /// </summary>
        protected virtual PartialViewResult EditorFor(object model, string prefix = null)
        {
            Argument.ExpectNotNull(() => model);

            var result = PartialView(
                    String.Format(CultureInfo.InvariantCulture, "{0}Views/Shared/EditorTemplates/{1}.cshtml", GetAreaPath(), model.GetType().Name),
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