using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Domain.Exceptions;
using ECom.Messages;
using ECom.Utility;

namespace ECom.Domain.Aggregates.User
{
	public class UserApplicationService 
		: IHandle<ReportUserLoggedIn>
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

			user.ReportLoggedIn();

			_repository.Save(user);
		}
	}
}
