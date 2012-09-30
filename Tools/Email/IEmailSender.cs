using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
	public interface IEmailSender
	{
		void Send(EmailMessage message);
	}
}
