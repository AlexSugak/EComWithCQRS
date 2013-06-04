using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;
using ECom.Domain.Aggregates.Product.Exceptions;
using ECom.Infrastructure;

namespace ECom.Domain.Aggregates.Product
{
    public class ProductAggregate : AggregateRoot<ProductId>
    {
		private ProductId _id;
        private string _name;
        private decimal _price;
        private string _category;

		private bool _removed;

        public ProductAggregate()
        {
        }

		public ProductAggregate(ProductId id, string name, decimal price)
        {
            Argument.ExpectNotNull(() => id);
            Argument.ExpectNotNullOrWhiteSpace(() => name);

			ApplyChange(new ProductAdded(TimeProvider.Now, this.Version + 1, id, name, price));
        }

		public override ProductId Id
        {
            get { return _id; }
        }

        private void Apply(ProductAdded e)
        {
            _id = e.Id;
            _name = e.ProductName;
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


		private readonly List<ProductId> _relatedProductIds = new List<ProductId>();

		public void AddRelatedProduct(ProductAggregate relatedProduct)
		{
			Argument.ExpectNotNull(() => relatedProduct);
			CheckNotRemoved();

			if (_relatedProductIds.Any(p => p == relatedProduct.Id))
			{
				throw new InvalidOperationException("The products are already related");
			}

			ApplyChange(new RelatedProductAdded(_id, relatedProduct.Id));
		}

		public void Apply(RelatedProductAdded e)
		{
			_relatedProductIds.Add(e.TargetProductId);
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
