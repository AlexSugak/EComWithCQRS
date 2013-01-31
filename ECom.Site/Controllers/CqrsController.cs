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
	public abstract class CqrsController : BaseController
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
	}
}