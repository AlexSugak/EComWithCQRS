using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;

namespace ECom.Site.Helpers
{
	public class FacebookUserDetals
	{
		public string Name;
		public string Email;
		public string PictureUrl;
	}

	public static class FacebookAuth
	{
		public static FacebookUserDetals LoadUserDetails(string accessToken)
		{
			WebClient client = new WebClient();
			string jsonResult = client.DownloadString(String.Concat("https://graph.facebook.com/me?access_token=", accessToken, "&fields=email,name,picture"));
			JObject jsonUserInfo = JObject.Parse(jsonResult);

			// you can get more user's info here. Please refer to:
			//     http://developers.facebook.com/docs/reference/api/user/

			return new FacebookUserDetals()
			{
				Name = jsonUserInfo.Value<string>("name"),
				Email = jsonUserInfo.Value<string>("email"),
				PictureUrl = jsonUserInfo["picture"]["data"].Value<string>("url")
			};
		}
	}
}