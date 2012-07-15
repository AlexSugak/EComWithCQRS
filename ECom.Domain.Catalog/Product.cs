using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Domain.Catalog.Exceptions;
using ECom.Messages;
using ECom.Utility;

namespace ECom.Domain.Catalog
{
    public class Product : AggregateRoot<ProductId>
    {
		private ProductId _id;
        private string _name;
        private decimal _price;
        private string _category;

		private bool _removed;

        public Product()
        {
        }

		public Product(ProductId id, string name, decimal price)
        {
            Argument.ExpectNotNull(() => id);
            Argument.ExpectNotNullOrWhiteSpace(() => name);

			ApplyChange(new ProductAdded(id, name, price));
        }

		public override ProductId Id
        {
            get { return _id; }
        }

        private void Apply(ProductAdded e)
        {
            _id = e.Id;
            _name = e.Name;
            _price = e.Price;
        }

        public void ChangePrice(decimal newPrice)
        {
			Argument.Expect(() => newPrice > 0, "newPrice", "product price must be a positive value");

            ApplyChange(new ProductPriceChanged(_id, newPrice));
        }

        public void Apply(ProductPriceChanged e)
        {
            _price = e.NewPrice;
        }

		public void Remove()
		{
			CheckNotRemoved();

			ApplyChange(new ProductRemoved(_id));
		}

		public void Apply(ProductRemoved e)
		{
			_removed = true;
		}

        public void AddToCategory(string categoryName)
        {
            Argument.ExpectNotNullOrWhiteSpace(() => categoryName);
            CheckNotRemoved();

            ApplyChange(new ProductAddedToCategory(_id, categoryName));
        }

        public void Apply(ProductAddedToCategory e)
        {
            _category = e.CategoryName;
        }

		private void CheckNotRemoved()
		{
			if (_removed)
			{
				throw new CannotModifyRemovedProductException();
			}
		}
	}
}
