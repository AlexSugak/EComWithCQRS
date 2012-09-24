using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Domain;
using ECom.Messages;
using ECom.Utility;
using ECom.Domain.Exceptions;
using ECom.Domain.Aggregates.Catalog;

namespace ECom.Domain.Aggregates.Catalog
{
	public class CatalogApplicationService : 
		IHandle<CreateCategory>,
		IHandle<MoveCategory>
	{
		private readonly IRepository<CatalogAggregate, CatalogId> _repository;

		public CatalogApplicationService(IEventStore eventStore)
		{
			Argument.ExpectNotNull(() => eventStore);

			_repository = new Repository<CatalogAggregate, CatalogId>(eventStore);
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




		private CatalogAggregate GetCatalog()
		{
			CatalogAggregate catalog;

			try
			{
				catalog = _repository.Get(CatalogAggregate.MainCatalogId);
			}
			catch (AggregateRootNotFoundException)
			{
				catalog = new CatalogAggregate();
			}

			return catalog;
		}
	}
}
