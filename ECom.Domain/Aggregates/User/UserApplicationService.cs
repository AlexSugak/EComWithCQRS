using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.Domain.Aggregates.User
{
	public class UserApplicationService :
        IHandle<CreateUser>,
        IHandle<UpdateUserData>,
        IHandle<ChangeUserEmail>
	{
		private readonly IRepository<UserAggregate, UserId> _repository;

		public UserApplicationService(IEventStore eventStore)
		{
			Argument.ExpectNotNull(() => eventStore);
			_repository = new Repository<UserAggregate, UserId>(eventStore);
		}

        public void Handle(CreateUser command)
        {
            Argument.ExpectNotNull(() => command.UserId);

            bool userExist = true;
            try
            {
                _repository.Get(command.UserId);
            }
            catch (AggregateRootNotFoundException)
            {
                userExist = false;
            }
            if (userExist)
                throw new DuplicateEntityException(typeof(UserAggregate), command.UserId);

            var user = new UserAggregate(command.UserId, command.UserName, command.Email, command.PhotoUrl);
            _repository.Save(user, 0);
        }

		public void Handle(UpdateUserData command)
		{
			var user = _repository.Get(command.Id);

			user.UpdateData(command.UserName, command.PhotoUrl);

			_repository.Save(user, command.OriginalVersion);
		}

        public void Handle(ChangeUserEmail command)
        {
            var user = _repository.Get(command.Id);

            user.ChangeEmail(command.Email);

            _repository.Save(user, command.OriginalVersion);
        }

	}
}
