using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
	[Serializable]
	public class UserDetails : Dto
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string PhotoUrl { get; set; }
		public string Email { get; set; }

		public UserDetails()
		{
		}

		public UserDetails(string email, string name, string photoUrl)
		{
			ID = email;
			Name = name;
			PhotoUrl = photoUrl ?? String.Empty;
			Email = String.Empty;
		}
	}

    public interface IUserDetailsView
    {
        UserDetails GetUserDetails(UserId userId);
    }

	public class UserDetailsView : Projection,
        IProjection<UserDetails>,
        IUserDetailsView,
		IHandle<UserLoggedInReported>,
		IHandle<UserEmailSet>
	{
		public UserDetailsView(IDtoManager manager)
			: base(manager)
		{
		}

		public void Handle(UserLoggedInReported e)
		{
			_manager.Delete<UserDetails>(e.Id);
			_manager.Add<UserDetails>(e.Id.Id, new UserDetails(e.Id.Id, e.UserName, e.PhotoUrl));
		}

		public void Handle(UserEmailSet e)
		{
			_manager.Update<UserDetails>(e.Id, ud => ud.Email = e.Email.RawAddress);
		}

        public UserDetails GetUserDetails(UserId userId)
        {
            Argument.ExpectNotNull(() => userId);

            return _manager.Get<UserDetails>(userId);
        }
    }
}
