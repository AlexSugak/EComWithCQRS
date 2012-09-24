using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic.Repository;
using ECom.Utility;
using ECom.Messages;
using System.Linq.Expressions;

namespace ECom.ReadModel
{
	public class SubSonicDtoManager : IDtoManager
	{
		private SimpleRepository _repository;

		public SubSonicDtoManager(SimpleRepository repository)
		{
			Argument.ExpectNotNull(() => repository);

			_repository = repository;
		}

		public T Get<T>(Expression<Func<T, bool>> by) where T : Dto, new()
		{
			return _repository.Single<T>(by);
		}

		public void Add<T>(T dto) where T : Dto, new()
		{
			_repository.Add(dto);
		}

		public void Delete<T>(IIdentity id) where T : Dto, new()
		{
			Delete<T>(id.GetId());
		}

		public void Delete<T>(string id) where T : Dto, new()
		{
			_repository.Delete<T>(id);
		}

		public void Update<T>(IIdentity id, Action<T> updateAction) where T : Dto, new()
		{
			Update<T>(id.GetId(), updateAction);
		}

		public void Update<T>(string id, Action<T> updateAction) where T : Dto, new()
		{
			var dto = _repository.Single<T>(id);
			updateAction(dto);
			_repository.Update(dto);
		}
	}
}
