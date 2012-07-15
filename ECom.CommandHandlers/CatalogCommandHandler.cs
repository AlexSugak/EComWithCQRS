using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Domain;
using ECom.Messages;
using ECom.Utility;
using ECom.Domain.Catalog;
using ECom.Domain.Exceptions;

namespace ECom.CommandHandlers
{
	public class CatalogCommandHandler : 
		IHandle<CreateCategory>,
		IHandle<MoveCategory>
	{
		private readonly IRepository<Catalog, CatalogId> _repository;

		public CatalogCommandHandler(IEventStore eventStore)
		{
			Argument.ExpectNotNull(() => eventStore);

            _repository = new Repository<Catalog, CatalogId>(eventStore);
		}

		public void Handle(CreateCategory message)
		{
			var catalog = GetCatalog();
			catalog.AddCategory(message.Name);

			_repository.Save(catalog);
		}

		public void Handle(MoveCategory message)
		{
			var catalog = GetCatalog();
			catalog.MoveCategory(message.Name, message.TargetCategory);

			_repository.Save(catalog);
		}




		private Catalog GetCatalog()
		{
			Catalog catalog;

			try
			{
				catalog = _repository.Get(Catalog.MainCatalogId);
			}
			catch (AggregateRootNotFoundException)
			{
				catalog = new Catalog();
			}

			return catalog;
		}
	}
}
