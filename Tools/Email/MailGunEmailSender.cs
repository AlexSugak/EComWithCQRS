using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECom.Utility;
using RestSharp;

namespace Email
{
	public class MailGunEmailSender : IEmailSender
	{
		private readonly string _mailGunApiKey;
		private readonly string _appDomain;

		public MailGunEmailSender(string mailGunApiKey, string appDomain)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => mailGunApiKey);
			Argument.ExpectNotNullOrWhiteSpace(() => appDomain);

			_mailGunApiKey = mailGunApiKey;
			_appDomain = appDomain;
		}

		public void Send(EmailMessage message)
		{
			Argument.ExpectNotNull(() => message);

			RestRequest request = GetEmailRequest(message);

			if (message.IsHtmlMessage)
			{
				request.AddParameter("html", message.Body);
			}
			else
			{
				request.AddParameter("text", message.Body);
			}

			IRestResponse response = Send(request);
		}

		private RestRequest GetEmailRequest(EmailMessage message)
		{
			RestRequest request = new RestRequest();

			request.AddParameter("domain", _appDomain, ParameterType.UrlSegment);
			request.Resource = "{domain}/messages";
			request.AddParameter("from", message.From.Address);
			request.AddParameter("to", message.To.Address);
			request.AddParameter("subject", message.Subject);
			request.AddParameter("text", message.Body);

			return request;
		}

		private IRestResponse Send(RestRequest request)
		{
			request.Method = Method.POST;

			RestClient client = new RestClient();
			client.BaseUrl = "https://api.mailgun.net/v2";
			client.Authenticator = new HttpBasicAuthenticator("api", _mailGunApiKey);

			return client.Execute(request);
		}
	}
}
