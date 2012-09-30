using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECom.Messages;
using ECom.Utility;

namespace Email
{
	public class EmailMessage
	{
		public EmailMessage(EmailAddress from, EmailAddress to, string subject, string body, bool isHtmlMessage = false)
		{
			Argument.ExpectNotNull(() => from);
			Argument.ExpectNotNull(() => to);
			Argument.ExpectNotNullOrWhiteSpace(() => subject);
			Argument.ExpectNotNullOrWhiteSpace(() => body);

			From = from;
			To = to;
			Subject = subject;
			Body = body;
			IsHtmlMessage = isHtmlMessage;
		}

		public EmailAddress From { get; private set; }
		public EmailAddress To { get; private set; }
		public string Subject { get; private set; }
		public string Body { get; private set; }
		public bool IsHtmlMessage { get; private set; } 
	}
}
