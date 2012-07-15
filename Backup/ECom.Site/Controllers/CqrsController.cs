using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECom.Messages;
using ECom.ReadModel;
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
        }

		protected virtual ActionResult SubmitCommand<T>(T cmd) where T : Command
		{
			Argument.ExpectNotNull(() => cmd);

			if (ModelState.IsValid)
			{
				try
				{
					_bus.Send(cmd);
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