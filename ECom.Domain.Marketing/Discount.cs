using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using ECom.Messages;

namespace ECom.Domain.Marketing
{
    public sealed class Discount : AggregateRoot<DiscountId>
    {
		private DiscountId _id;
        private string _name;
        private decimal _value;

		private ProductId _productId;

        public Discount()
        {
        }

		public Discount(DiscountId id, string name, decimal value)
        {
			Argument.ExpectNotNull(() => id);
			Argument.ExpectNotNullOrWhiteSpace(() => name);
            Argument.Expect(() => value > 0, "value", "discount value must be a positive number");

            ApplyChange(new DiscountCreated(id, name, value));
        }

        private void Apply(DiscountCreated e)
        {
            _id = e.Id;
            _name = e.Name;
            _value = e.Value;
        }

		public override DiscountId Id { get { return _id; } }

		public void AssignToProduct(ProductId productId)
        {
			Argument.ExpectNotNull(() => productId);

            ApplyChange(new DiscountAassignedToProduct(_id, productId));
        }

        public void Apply(DiscountAassignedToProduct e)
        {
            _productId = e.ProductId;
        }
    }
}
