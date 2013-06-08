using ECom.Messages;
using ECom.Utility;

namespace ECom.Domain.Aggregates.User
{
	public class UserAggregate : AggregateRoot<UserId>
	{
		private string name;
        private EmailAddress email;
        private string photoUrl;

        private UserAggregate()
        {
            // This ctor is needed for the repository to create empty object to load events into
        }

        public UserAggregate(UserId userId, string name, EmailAddress email, string photoUrl)
		{
            Argument.ExpectNotNull(() => userId);
			Argument.ExpectNotNull(() => email);
			
            ApplyChange(new UserCreated(TimeProvider.Now, this.Version + 1, userId, name, email, photoUrl));
		}

		public void UpdateData(string userName, string photoUrl)
		{
            if (this.name == userName && this.photoUrl == photoUrl)
                return;

            ApplyChange(new UserDataUpdated(TimeProvider.Now, this.Version + 1, this.Id, userName, photoUrl));
		}

        public void ChangeEmail(EmailAddress email)
        {
            Argument.ExpectNotNull(() => email);

            if (this.email == email)
                return;

            ApplyChange(new UserEmailChanged(TimeProvider.Now, this.Version + 1, this.Id, email));
        }

        private void Apply(UserCreated e)
        {
            this.Id = e.Id;
            this.name = e.UserName;
            this.email = e.Email;
            this.photoUrl = e.PhotoUrl;
        }

		private void Apply(UserDataUpdated e)
		{
            this.name = e.UserName;
            this.photoUrl = e.PhotoUrl;
        }

        private void Apply(UserEmailChanged e)
        {
            this.email = e.Email;
        }
	}
}
