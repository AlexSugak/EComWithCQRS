using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.Domain.Aggregates.User
{
	public class UserApplicationService : 
		IHandle<ReportUserLoggedIn>,
		IHandle<SetUserEmail>
	{
		private readonly IRepository<UserAggregate, UserId> _repository;

		public UserApplicationService(IEventStore eventStore)
		{
			Argument.ExpectNotNull(() => eventStore);
			_repository = new Repository<UserAggregate, UserId>(eventStore);
		}

		public void Handle(ReportUserLoggedIn cmd)
		{
			UserAggregate user;

			try
			{
				user = _repository.Get(cmd.Id);
			}
			catch (AggregateRootNotFoundException)
			{
				user = new UserAggregate();
				user.Create(cmd.Id);
			}

			user.ReportLoggedIn(cmd.UserName, cmd.PhotoUrl);

			_repository.Save(user, 0);
		}

		public void Handle(SetUserEmail cmd)
		{
			UserAggregate user = _repository.Get(cmd.Id);

			user.SetEmailAddress(cmd.Email);

			_repository.Save(user, cmd.OriginalVersion);
		}
	}
}
