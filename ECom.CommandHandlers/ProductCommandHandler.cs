using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Domain;
using ECom.Messages;
using ECom.Domain.Catalog;
using ECom.Utility;

namespace ECom.CommandHandlers
{
    public class ProductCommandHandler : 
		IHandle<AddProduct>, 
		IHandle<ChangeProductPrice>,
		IHandle<RemoveProduct>
    {
        private readonly IRepository<Product, ProductId> _repository;

		public ProductCommandHandler(IEventStore eventStore)
		{
			Argument.ExpectNotNull(() => eventStore);
            _repository = new Repository<Product, ProductId>(eventStore);
		}

        public ProductCommandHandler(IRepository<Product, ProductId> repository)
        {
			Argument.ExpectNotNull(() => repository);
            _repository = repository;
        }

        public void Handle(AddProduct cmd)
        {
            var product = new Product(cmd.Id, cmd.Name, cmd.Price);
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
	}
}
