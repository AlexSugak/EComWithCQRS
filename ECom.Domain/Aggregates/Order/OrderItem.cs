using ECom.Messages;
using ECom.Utility;
using System;
using System.Globalization;

namespace ECom.Domain.Aggregates.Order
{
    internal sealed class OrderItem
    {
        public OrderItem(OrderItemId id, int quantity, decimal price)
        {
			Argument.ExpectNotNull(() => id);
			Argument.Expect(() => quantity > 0, "quantity", String.Format(CultureInfo.InvariantCulture, "quantity must be a positive value, was {0}", quantity));

            Id = id;
            Quantity = quantity;
            Price = price;
        }

		public OrderItemId Id { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal Total { get { return Price * Quantity; } }

        public void AddQuantity(int quantityAdded)
        {
			Argument.Expect(() => quantityAdded > 0, "quantityAdded", String.Format(CultureInfo.InvariantCulture, "quantity must be a positive value, was {0}", quantityAdded));

            Quantity += quantityAdded;
        }

        public void RemoveQuantity(int quantityRemoved)
        {
            if (quantityRemoved >= Quantity)
            {
                throw new ArgumentException("Cannot remove quantity more or eual to existing quantity");
            }

            Quantity -= quantityRemoved;
        }
    }
}
