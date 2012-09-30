using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace ECom.Site.Helpers
{
	public class GoogleUserDetals
	{
		public string Name;
		public string Email;
		public string PictureUrl;
	}

	public static class GoogleAuth
	{
		private const string _appClientId = "485526445533.apps.googleusercontent.com";
		private const string _appClientSecred = "bZ6WkBjseRTVT5UFo5pcWhJj";

		public static string GoogleLoginUrl(this UrlHelper url, string redirectAction = "GoogleLogin", string redirectController = "Account")
		{
			var result = new StringBuilder();

			string redirectUrl = url.Encode(url.Action(redirectAction, redirectController, new { Area = String.Empty }, "http"));

			result.Append("https://accounts.google.com/o/oauth2/auth?scope=https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fuserinfo.email+https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fuserinfo.profile");
			result.AppendFormat("&redirect_uri={0}", redirectUrl);
			result.Append("&response_type=code");
			result.AppendFormat("&client_id={0}", _appClientId);
			result.AppendFormat("&state={0}", redirectUrl);

			return result.ToString();
		}

		public static string ObtainAccessToken(string code, string redirectUrl)
		{
			JObject jsonAccessToken;

			string url = "https://accounts.google.com/o/oauth2/token";

			var request = (HttpWebRequest)WebRequest.Create(url.ToString());
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";

			var utfenc = new UTF8Encoding();
			var parameters = String.Concat("code=", code
											, "&client_id=", _appClientId
											, "&client_secret=", _appClientSecred
											, "&redirect_uri=", redirectUrl
											, "&grant_type=authorization_code");

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

			return jsonAccessToken.Value<string>("access_token");
		}

		public static GoogleUserDetals LoadUserDetails(string accessToken)
		{
			WebClient client = new WebClient();
			string jsonResult = client.DownloadString(String.Concat("https://www.googleapis.com/oauth2/v1/userinfo?access_token=", accessToken));
			JObject jsonUserInfo = JObject.Parse(jsonResult);

			return new GoogleUserDetals()
			{
				Name = jsonUserInfo.Value<string>("name"),
				Email = jsonUserInfo.Value<string>("email"),
				PictureUrl = jsonUserInfo.Value<string>("picture")
			};
		}
	}
}