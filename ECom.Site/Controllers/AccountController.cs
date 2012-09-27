using System;
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

namespace ECom.Site.Controllers
{
	public class AccountController : CqrsController
    {
        public ActionResult LogOn()
        {
            return View();
        }

		[HttpGet]
		public ActionResult FacebookLogin(string token, string returnUrl)
		{
			WebClient client = new WebClient();
			string jsonResult = client.DownloadString(String.Concat("https://graph.facebook.com/me?access_token=", token, "&fields=email,name,picture"));
			JObject jsonUserInfo = JObject.Parse(jsonResult);
			// you can get more user's info here. Please refer to:
			//     http://developers.facebook.com/docs/reference/api/user/
			string name = jsonUserInfo.Value<string>("name");
			string email = jsonUserInfo.Value<string>("email");
			string picture = jsonUserInfo["picture"]["data"].Value<string>("url");

			FormsAuthentication.SetAuthCookie(email, true);

			_bus.Send(new ReportUserLoggedIn(new UserId(email), name, picture));
			Thread.Sleep(200);

			return SuccessfullLoginRedirect(returnUrl);
		}

		[HttpGet]
		public ActionResult VkontakteLogin(string uid, string firstName, string lastName, string photo, string returnUrl)
		{
			FormsAuthentication.SetAuthCookie(uid, true);

			_bus.Send(new ReportUserLoggedIn(new UserId(uid), firstName + " " + lastName, photo));
			Thread.Sleep(200);

			return SuccessfullLoginRedirect(returnUrl);
		}

		[HttpGet]
		public ActionResult GoogleLogin(string code, string state)
		{
			JObject jsonAccessToken;

			string url = "https://accounts.google.com/o/oauth2/token";

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url.ToString());
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";

			// You mus do the POST request before getting any response
			UTF8Encoding utfenc = new UTF8Encoding();
			var parameters = String.Concat("code=", code, "&"
											, "client_id=485526445533.apps.googleusercontent.com&"
											, "client_secret=bZ6WkBjseRTVT5UFo5pcWhJj&"
											, "redirect_uri=", state, "&"
											, "grant_type=authorization_code");
			byte[] bytes = utfenc.GetBytes(parameters);
			Stream os = null;
			request.ContentLength = bytes.Length; 
			os = request.GetRequestStream();
			os.Write(bytes, 0, bytes.Length);        
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				string responseBody = null;

				using (StreamReader sr = new StreamReader(response.GetResponseStream()))
				{
					responseBody = sr.ReadToEnd();
				}

				jsonAccessToken = JObject.Parse(responseBody);
			}
			
			var accessToken = jsonAccessToken.Value<string>("access_token");

			WebClient client = new WebClient();
			string jsonResult = client.DownloadString(String.Concat("https://www.googleapis.com/oauth2/v1/userinfo?access_token=", accessToken));
			JObject jsonUserInfo = JObject.Parse(jsonResult);
			string name = jsonUserInfo.Value<string>("name");
			string email = jsonUserInfo.Value<string>("email");
			string picture = jsonUserInfo.Value<string>("picture");

			FormsAuthentication.SetAuthCookie(email, true);

			_bus.Send(new ReportUserLoggedIn(new UserId(email), name, picture));
			Thread.Sleep(200);

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
    }
}
