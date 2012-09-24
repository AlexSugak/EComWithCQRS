using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using System.Globalization;
using ECom.Messages;

namespace ECom.Domain.Aggregates.Order
{
    internal sealed class OrderItem
    {
        public OrderItem(OrderItemId id, int quantity)
        {
			Argument.ExpectNotNull(() => id);
			Argument.Expect(() => quantity > 0, "quantity", String.Format(CultureInfo.InvariantCulture, "quantity must be a positive value, was {0}", quantity));

            Id = id;
            Quantity = quantity;
        }

		public OrderItemId Id { get; private set; }
        public int Quantity { get; private set; }

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
