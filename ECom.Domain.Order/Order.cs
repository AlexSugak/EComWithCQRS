using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using ECom.Messages;
using ECom.Domain.Order.Entities;
using System.Globalization;

namespace ECom.Domain.Order
{
    public sealed class Order : AggregateRoot<OrderId>
    {
		private OrderId _id;
		private Dictionary<ProductId, LineItem> _products = new Dictionary<ProductId, LineItem>();

        public Order()
        {
        }

        public Order(OrderId id)
        {
			Argument.ExpectNotNull(() => id);

            ApplyChange(new NewOrderCreated(id));
        }

        private void Apply(NewOrderCreated e)
        {
            _id = e.Id;
        }

		public override OrderId Id
        {
            get { return _id; }
        }

        public void AddProduct(ProductId productId, int quantity)
        {
			Argument.ExpectNotNull(() => productId);
			Argument.Expect(() => quantity > 0, "quantity", String.Format(CultureInfo.InvariantCulture, "quantity must be a positive value, was {0}", quantity));

            ApplyChange(new ProductAddedToOrder(_id, productId, quantity));
        }

        private void Apply(ProductAddedToOrder e)
        {
            if (_products.ContainsKey(e.ProductId))
            {
                _products[e.ProductId].AddQuantity(e.Quantity);
            }
            else
            {
                _products.Add(e.ProductId, new LineItem(e.ProductId, e.Quantity ));
            }
        }
    }
}
