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
		private string _name;

        private UserAggregate()
        {
            // This ctor is needed for the repository to create empty object to load events into
        }

        public UserAggregate(UserId userId)
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
			Id = e.Id;
		}

		public void ReportLoggedIn(string userName, string photoUrl)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => userName);

            ApplyChange(new UserLoggedInReported(TimeProvider.Now, this.Version + 1, Id, userName, photoUrl));
		}

		public void Apply(UserLoggedInReported e)
		{ }

		public void SetEmailAddress(EmailAddress emailAddress)
		{
			Argument.ExpectNotNull(() => emailAddress);

            ApplyChange(new UserEmailSet(TimeProvider.Now, this.Version + 1, Id, emailAddress));
		}

		public void Apply(UserEmailSet e)
		{ }
	}
}
