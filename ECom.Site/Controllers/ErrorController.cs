using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECom.Site.Controllers
{
    public class ErrorController : Controller
    {
		public ActionResult EntityNotFound()
		{
			return View();
		}

		public ActionResult PageNotFound()
		{
			return View();
		}

		public ActionResult General(Exception error)
		{
			return View(error);
		}
    }
}
