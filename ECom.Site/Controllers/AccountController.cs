﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ECom.Messages;
using ECom.Site.Models;
using Newtonsoft.Json.Linq;
using ECom.Site.Helpers;
using System.Threading;
using System.Text;
using System.IO;
using ECom.ReadModel.Views;

namespace ECom.Site.Controllers
{
	public class AccountController : CqrsController
    {
        private readonly IUserDetailsView _userDetailsView;

        public AccountController()
        {
            _userDetailsView = new UserDetailsView(ServiceLocator.DtoManager);
        }

        public ActionResult LogOn()
        {
            return View();
        }

		[HttpGet]
		public ActionResult FacebookLogin(string token, string returnUrl)
		{
			FacebookUserDetals userDetails = FacebookAuth.LoadUserDetails(token);

			var userId = new UserId(userDetails.Email);
			_bus.Send(new ReportUserLoggedIn(userId, userDetails.Name, userDetails.PictureUrl));
			SetUserEmailIfNeeded(userId, new EmailAddress(userDetails.Email));

            FormsAuthentication.SetAuthCookie(userDetails.Email, true);
			return SuccessfullLoginRedirect(returnUrl);
		}

		[HttpGet]
		public ActionResult VkontakteLogin(string uid, string firstName, string lastName, string photo, string returnUrl)
		{
			var userId = new UserId(uid);
			_bus.Send(new ReportUserLoggedIn(userId, firstName + " " + lastName, photo));

            FormsAuthentication.SetAuthCookie(uid, true);
			return SuccessfullLoginRedirect(returnUrl);
		}

		[HttpGet]
		public ActionResult GoogleLogin(string code, string state)
		{
			string accessToken = GoogleAuth.ObtainAccessToken(code, state);
			GoogleUserDetals userDetails = GoogleAuth.LoadUserDetails(accessToken);

			var userId = new UserId(userDetails.Email);
			_bus.Send(new ReportUserLoggedIn(userId, userDetails.Name, userDetails.PictureUrl));
			SetUserEmailIfNeeded(userId, new EmailAddress(userDetails.Email));

            FormsAuthentication.SetAuthCookie(userDetails.Email, true);
			return SuccessfullLoginRedirect(null);
		}

		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Index", "Home");
		}

		private ActionResult SuccessfullLoginRedirect(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
								   && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		private void SetUserEmailIfNeeded(UserId userId, EmailAddress email)
		{
            UserDetails userDetails = _userDetailsView.GetUserDetails(userId);

			if (String.IsNullOrWhiteSpace(userDetails.Email))
			{
				_bus.Send(new SetUserEmail(userId, email));
			}
		}
    }
}
