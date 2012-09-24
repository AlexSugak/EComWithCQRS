using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Domain;
using ECom.Messages;
using ECom.Utility;
using ECom.Domain.Aggregates.Product;

namespace ECom.CommandHandlers
{
    public class ProductApplicationService : 
		IHandle<AddProduct>, 
		IHandle<ChangeProductPrice>,
		IHandle<RemoveProduct>,
		IHandle<AddRelatedProduct>
    {
        private readonly IRepository<ProductAggregate, ProductId> _repository;

		public ProductApplicationService(IEventStore eventStore)
		{
			Argument.ExpectNotNull(() => eventStore);
			_repository = new Repository<ProductAggregate, ProductId>(eventStore);
		}

		public ProductApplicationService(IRepository<ProductAggregate, ProductId> repository)
        {
			Argument.ExpectNotNull(() => repository);
            _repository = repository;
        }

        public void Handle(AddProduct cmd)
        {
			var product = new ProductAggregate(cmd.Id, cmd.Name, cmd.Price);
            _repository.Save(product);
        }

        public void Handle(ChangeProductPrice cmd)
        {
            var product = _repository.Get(cmd.Id);
            product.ChangePrice(cmd.NewPrice);
            _repository.Save(product);
        }

		public void Handle(RemoveProduct cmd)
		{
            var product = _repository.Get(cmd.Id);
			product.Remove();
			_repository.Save(product);
		}

		public void Handle(AddRelatedProduct cmd)
		{
			ProductAggregate product = _repository.Get(cmd.Id);
			ProductAggregate relatedProduct = _repository.Get(cmd.TargetProductId);
			product.AddRelatedProduct(relatedProduct);

			_repository.Save(product);
		}
	}
}
