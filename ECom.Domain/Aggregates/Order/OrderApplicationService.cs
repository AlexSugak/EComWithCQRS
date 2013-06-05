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
		IHandle<CreateNewOrder>,
		IHandle<AddProductToOrder>,
		IHandle<RemoveItemFromOrder>,
		IHandle<SubmitOrder>
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

		public void Handle(CreateNewOrder cmd)
		{
            var order = new OrderAggregate(cmd.Id, cmd.UserId);

			_repository.Save(order, 0);
		}

		public void Handle(AddProductToOrder cmd)
		{
			OrderAggregate order = _repository.Get(cmd.Id);

			order.AddProduct(cmd.OrderItemId, cmd.ProductUri, cmd.Name, cmd.Description, cmd.Price, cmd.Quantity, cmd.Size, cmd.Color, cmd.ImageUri);

			_repository.Save(order, cmd.OriginalVersion);
		}

		public void Handle(RemoveItemFromOrder cmd)
		{
			OrderAggregate order = _repository.Get(cmd.Id);

			order.RemoveItem(cmd.OrderItemId);

			_repository.Save(order, cmd.OriginalVersion);
		}

		public void Handle(SubmitOrder cmd)
		{
			OrderAggregate order = _repository.Get(cmd.Id);

			order.Submit();

			_repository.Save(order, cmd.OriginalVersion);
		}
	}
}
