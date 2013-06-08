using ECom.Messages;
using ECom.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ECom.Domain.Aggregates.Order
{
    public sealed class OrderAggregate : AggregateRoot<OrderId>
    {
		private UserId _userId;

		private readonly List<OrderItem> _items = new List<OrderItem>();
		private bool _isSubmitted;

        private OrderAggregate()
        {
            // This ctor is needed for the repository to create empty object to load events into
        }

        public OrderAggregate(OrderId orderId, UserId userId)
        {
			Argument.ExpectNotNull(() => orderId);
			Argument.ExpectNotNull(() => userId);

            ApplyChange(new NewOrderCreated(TimeProvider.Now, this.Version+1, orderId, userId));
        }

        public void AddProduct(OrderItemId itemId, Uri productUri, string name, string description, decimal price, int quantity, string size, string color, Uri imageUrl)
        {
			Argument.ExpectNotNull(() => productUri);
			Argument.Expect(() => !_items.Any(i => i.Id == itemId) , "itemId", String.Format(CultureInfo.InvariantCulture, "Order already has item with id {0}", itemId));
			Argument.Expect(() => price > 0, "price", String.Format(CultureInfo.InvariantCulture, "price must be a positive value, was {0}", price));
			Argument.Expect(() => quantity > 0, "quantity", String.Format(CultureInfo.InvariantCulture, "quantity must be a positive value, was {0}", quantity));

            ApplyChange(new ProductAddedToOrder(TimeProvider.Now, this.Version + 1, this.Id, itemId, productUri, name, description, price, quantity, size, color, imageUrl));
        }

		public void RemoveItem(OrderItemId itemId)
		{
			Argument.ExpectNotNull(() => itemId);
			Argument.Expect(() => _items.Exists(i => i.Id == itemId), "itemId", String.Format(CultureInfo.InvariantCulture, "Order does not contain item with id {0}", itemId.Id));

			ApplyChange(new ItemRemovedFromOrder(TimeProvider.Now, this.Version + 1, this.Id, itemId));
		}

		public void Submit()
		{
			if (_isSubmitted)
			{
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Cannot submit order {0}. Order already submitted.", Id.Id));
			}

			if (!_items.Any())
			{
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Cannot submit order {0}. Order does not have any items added.", Id.Id));
			}

			ApplyChange(new OrderSubmited(TimeProvider.Now, this.Version + 1, this.Id, this._userId, this._items.Count, this._items.Sum(x => x.Total)));
		}

        private void Apply(NewOrderCreated e)
        {
            this.Id = e.OrderId;
            this._userId = e.UserId;
        }

        private void Apply(ProductAddedToOrder e)
        {
            this._items.Add(new OrderItem(e.OrderItemId, e.Quantity, e.Price));
        }

        private void Apply(ItemRemovedFromOrder e)
        {
            this._items.Remove(_items.Find(i => i.Id == e.OrderItemId));
        }

		private void Apply(OrderSubmited e)
		{
			this._isSubmitted = true;
		}
	}
}
