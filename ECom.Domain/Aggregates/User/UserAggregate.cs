using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.Domain.Aggregates.User
{
	public class UserAggregate : AggregateRoot<UserId>
	{
		private UserId _id;
		private string _name;

		public override UserId Id
		{
			get { return _id; }
		}

		public void Create(UserId userId, string userName)
		{
			Argument.ExpectNotNull(() => userId);
			Argument.ExpectNotNullOrWhiteSpace(() => userName);

			if (Version != 0)
			{
				throw new InvalidOperationException("User already has non zero version and cannot be created!");
			}

			ApplyChange(new UserCreated(userId, userName));
		}

		public void Apply(UserCreated e)
		{
			_id = e.Id;
			_name = e.UserName;
		}

		public void ReportLoggedIn()
		{
			ApplyChange(new UserLoggedInReported(_id, _name));
		}

		public void Apply(UserLoggedInReported e)
		{
		}
	}
}
