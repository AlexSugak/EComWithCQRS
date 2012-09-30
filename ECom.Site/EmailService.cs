using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ECom.Messages;
using ECom.ReadModel;
using ECom.Utility;
using Email;

namespace ECom.Site
{
	public class EmailService
		: IHandle<OrderSubmited>
	{
		private readonly IEmailSender _emailSender;
		private readonly IReadModelFacade _readModel;
		private readonly IMessageBodyGenerator _messageBodyGenerator;

		private EmailAddress appAddress = new EmailAddress("noreply@crowdshop.com", "Crowdshop");

		public EmailService(IEmailSender emailSender, IReadModelFacade readModel, IMessageBodyGenerator messageBodyGenerator)
		{
			Argument.ExpectNotNull(() => emailSender);
			Argument.ExpectNotNull(() => readModel);
			Argument.ExpectNotNull(() => messageBodyGenerator);

			_emailSender = emailSender;
			_readModel = readModel;
			_messageBodyGenerator = messageBodyGenerator;
		}

		public void Handle(OrderSubmited e)
		{
			var bodyTemplate =
	@"<html>
      <head>
      </head>
      <body>
        <h3>Здравствуйте, @Model.User.Name!</h3>
		<div>
			Ваш заказ принят. Уникальный номер вашего заказа: <b>@Model.OrderId</b>.
		</div>
		<div>
			Вы всегда можете узнать статус Вашего заказа перейдя по <a href='http://@Model.Host/Shop/Order/Details/@Model.OrderId'>этой ссылке</a>.
		</div>
      </body>
    </html>";

			var userDetails = _readModel.GetUserDetails(e.UserId);
			string body = _messageBodyGenerator.Generate(bodyTemplate, new { User = userDetails, OrderId = e.Id.Id, Host = ConfigurationManager.AppSettings["SiteHostName"] });

			var message = new EmailMessage(
					appAddress,
					new EmailAddress(userDetails.Email),
					"Order Submited",
					body,
					true);

			_emailSender.Send(message);
		}
	}
}