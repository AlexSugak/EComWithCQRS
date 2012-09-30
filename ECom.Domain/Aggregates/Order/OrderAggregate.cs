﻿using System;
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
		private UserId _userId;

		private readonly List<OrderItem> _items = new List<OrderItem>();
		private bool _isSubmitted;

        public OrderAggregate()
        {
        }

		public override OrderId Id
		{
			get { return _id; }
		}

        public void Create(OrderId id, UserId userId)
        {
			Argument.ExpectNotNull(() => id);
			Argument.ExpectNotNull(() => userId);

            ApplyChange(new NewOrderCreated(id, userId));
        }

        private void Apply(NewOrderCreated e)
        {
            _id = e.Id;
			_userId = e.UserId;
        }

        public void AddProduct(OrderItemId itemId, Uri productUri, string name, string description, decimal price, int quantity, string size, string color, Uri imageUrl)
        {
			Argument.ExpectNotNull(() => productUri);
			Argument.Expect(() => !_items.Any(i => i.Id == itemId) , "itemId", String.Format(CultureInfo.InvariantCulture, "Order already has item with id {0}", itemId));
			Argument.Expect(() => price > 0, "price", String.Format(CultureInfo.InvariantCulture, "price must be a positive value, was {0}", price));
			Argument.Expect(() => quantity > 0, "quantity", String.Format(CultureInfo.InvariantCulture, "quantity must be a positive value, was {0}", quantity));

			ApplyChange(new ProductAddedToOrder(_id, itemId, productUri, name, description, price, quantity, size, color, imageUrl));
        }

        private void Apply(ProductAddedToOrder e)
        {
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

		public void Submit()
		{
			if (_isSubmitted)
			{
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Cannot submit order {0}. Order already submitted.", _id.Id));
			}

			if (!_items.Any())
			{
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Cannot submit order {0}. Order does not have any items added.", _id.Id));
			}

			ApplyChange(new OrderSubmited(_id, _userId));
		}

		private void Apply(OrderSubmited e)
		{
			_isSubmitted = true;
		}
	}
}
