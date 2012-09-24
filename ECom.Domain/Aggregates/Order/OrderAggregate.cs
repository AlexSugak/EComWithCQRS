using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using ECom.Messages;
using System.Globalization;

namespace ECom.Domain.Aggregates.Order
{
    public sealed class OrderAggregate : AggregateRoot<OrderId>
    {
		private OrderId _id;
		private readonly List<OrderItem> _items = new List<OrderItem>();
		private int _maxOrderItemId = 0;

        public OrderAggregate()
        {
        }

        public OrderAggregate(OrderId id)
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

        public void AddProduct(Uri productUri, string name, string description, decimal price, int quantity, string size, string color, Uri imageUrl)
        {
			Argument.ExpectNotNull(() => productUri);
			Argument.Expect(() => price > 0, "price", String.Format(CultureInfo.InvariantCulture, "price must be a positive value, was {0}", price));
			Argument.Expect(() => quantity > 0, "quantity", String.Format(CultureInfo.InvariantCulture, "quantity must be a positive value, was {0}", quantity));

			var newItemId = new OrderItemId(_maxOrderItemId + 1);

			ApplyChange(new ProductAddedToOrder(_id, newItemId, productUri, name, description, price, quantity, size, color, imageUrl));
        }

        private void Apply(ProductAddedToOrder e)
        {
			_maxOrderItemId = e.OrderItemId.Id;
            _items.Add(new OrderItem(e.OrderItemId, e.Quantity ));
        }

		public void RemoveItem(OrderItemId itemId)
		{
			Argument.ExpectNotNull(() => itemId);
			Argument.Expect(() => _items.Exists(i => i.Id == itemId), "itemId", String.Format(CultureInfo.InvariantCulture, "Order does not contain item with id {0}", itemId.Id));

			ApplyChange(new ItemRemovedFromOrder(_id, itemId));
		}

		private void Apply(ItemRemovedFromOrder e)
		{
			_items.Remove(_items.Find(i => i.Id == e.OrderItemId)); 
		}
    }
}
