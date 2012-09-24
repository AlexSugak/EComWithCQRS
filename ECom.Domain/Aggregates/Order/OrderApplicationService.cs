using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Domain;
using ECom.Utility;
using ECom.Messages;
using ECom.Domain.Aggregates.Order;

namespace ECom.CommandHandlers
{
    public class OrderApplicationService : 
		IHandle<AddProductToOrder>,
		IHandle<RemoveItemFromOrder>
    {
        private readonly IRepository<OrderAggregate, OrderId> _repository;

		public OrderApplicationService(IEventStore eventStore)
		{
			Argument.ExpectNotNull(() => eventStore);
            _repository = new Repository<OrderAggregate, OrderId>(eventStore);
		}

        public OrderApplicationService(IRepository<OrderAggregate, OrderId> repository)
        {
			Argument.ExpectNotNull(() => repository);
            _repository = repository;
        }

		public void Handle(AddProductToOrder cmd)
		{
			OrderAggregate order = _repository.Get(cmd.Id);

			order.AddProduct(cmd.ProductUri, cmd.Name, cmd.Description, cmd.Price, cmd.Quantity, cmd.Size, cmd.Color, cmd.ImageUri);

			_repository.Save(order);
		}

		public void Handle(RemoveItemFromOrder cmd)
		{
			OrderAggregate order = _repository.Get(cmd.Id);

			order.RemoveItem(cmd.OrderItemId);

			_repository.Save(order);
		}
	}
}
