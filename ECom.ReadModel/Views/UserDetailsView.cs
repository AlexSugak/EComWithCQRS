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

		public UserDetails()
		{
		}

		public UserDetails(string email, string name)
		{
			ID = email;
			Name = name;
		}
	}

	public class UserDetailsView :
		IHandle<UserCreated>
	{
		private IDtoManager _manager;

		public UserDetailsView(IDtoManager manager)
		{
			Argument.ExpectNotNull(() => manager);

			_manager = manager;
		}

		public void Handle(UserCreated e)
		{
			_manager.Add<UserDetails>(new UserDetails(e.Id.Id, e.UserName));
		}
	}
}
