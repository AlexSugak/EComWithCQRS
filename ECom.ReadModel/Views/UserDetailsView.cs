using ECom.Messages;
using ECom.Utility;
using System;

namespace ECom.ReadModel.Views
{
	[Serializable]
	public class UserDetails : Dto
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string PhotoUrl { get; set; }
		public string Email { get; set; }
        public int Version { get; set; }

		public UserDetails()
		{
		}

		public UserDetails(string id, string email, string name, string photoUrl)
		{
			ID = email;
			Name = name;
			PhotoUrl = photoUrl ?? String.Empty;
			Email = email;
            Version = 0;
		}
	}

    public interface IUserDetailsView
    {
        UserDetails GetUserDetails(UserId userId);
    }

	public class UserDetailsView : Projection,
        IProjection<UserDetails>,
        IUserDetailsView,
        IHandle<UserCreated>,
        IHandle<UserDataUpdated>,
        IHandle<UserEmailChanged>
	{
		public UserDetailsView(IDtoManager manager)
			: base(manager)
		{
		}

        public void Handle(UserCreated e)
        {
            _manager.Add<UserDetails>(e.Id.Id, new UserDetails(e.Id.Id, e.Email.RawAddress, e.UserName, e.PhotoUrl));
        }

		public void Handle(UserDataUpdated e)
		{
            _manager.Update<UserDetails>(e.Id, ud => { ud.Name = e.UserName; ud.PhotoUrl = e.PhotoUrl; ud.Version = e.Version; });
		}

        public void Handle(UserEmailChanged e)
        {
            _manager.Update<UserDetails>(e.Id, ud => { ud.Email = e.Email.RawAddress; ud.Version = e.Version; });
        }

        public UserDetails GetUserDetails(UserId userId)
        {
            Argument.ExpectNotNull(() => userId);

            return _manager.Get<UserDetails>(userId);
        }
    }
}
