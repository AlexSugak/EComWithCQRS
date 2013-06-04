﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;
using ECom.Infrastructure;

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

		public void Create(UserId userId)
		{
			Argument.ExpectNotNull(() => userId);
			
			if (Version != 0)
			{
				throw new InvalidOperationException("User already has non zero version and cannot be created!");
			}

            ApplyChange(new UserCreated(TimeProvider.Now, this.Version + 1, userId));
		}

		public void Apply(UserCreated e)
		{
			_id = e.Id;
		}

		public void ReportLoggedIn(string userName, string photoUrl)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => userName);

            ApplyChange(new UserLoggedInReported(TimeProvider.Now, this.Version + 1, _id, userName, photoUrl));
		}

		public void Apply(UserLoggedInReported e)
		{ }

		public void SetEmailAddress(EmailAddress emailAddress)
		{
			Argument.ExpectNotNull(() => emailAddress);

            ApplyChange(new UserEmailSet(TimeProvider.Now, this.Version + 1, _id, emailAddress));
		}

		public void Apply(UserEmailSet e)
		{ }
	}
}
