using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Domain;
using ECom.Utility;
using ECom.Messages;
using ECom.Domain.Order;

namespace ECom.CommandHandlers
{
    public class OrderCommandHandler : IHandle<CreateNewOrder>, IHandle<AddProductToOrder>
    {
        private readonly IRepository<Order, OrderId> _repository;

		public OrderCommandHandler(IEventStore eventStore)
		{
			Argument.ExpectNotNull(() => eventStore);
            _repository = new Repository<Order, OrderId>(eventStore);
		}

        public OrderCommandHandler(IRepository<Order, OrderId> repository)
        {
			Argument.ExpectNotNull(() => repository);
            _repository = repository;
        }

        public void Handle(CreateNewOrder cmd)
        {
            var order = new Order(cmd.Id);
            _repository.Save(order);
        }

		public void Handle(AddProductToOrder cmd)
		{
			throw new NotImplementedException();
		}
    }
}
