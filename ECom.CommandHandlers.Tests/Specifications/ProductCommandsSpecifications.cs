using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;
using ECom.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECom.Domain.Exceptions;
using ECom.Domain.Aggregates.Product.Exceptions;

namespace ECom.CommandHandlers.Tests.Specifications
{
    [TestClass]
    public abstract class ProductCommandSpecificationTest<T> : CommandSpecificationTest<T>
        where T : ICommand<ProductId>
    {
        protected ProductId _productId;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            _productId = new ProductId("123");
        }
    }

    [TestClass]
    public class CreateProductSpecs : ProductCommandSpecificationTest<AddProduct>
    {
        [TestMethod]
        public void when_adding_valid_product()
        {
			var spec = new CommandSpecification<AddProduct>
            {
                Given = Enumerable.Empty<IEvent>(),
                When = new AddProduct(_productId, "Pants", 12),
                Expect = new[] { new ProductAdded(_productId, "Pants", 12) }
            };

			Assert(spec);
        }

        [TestMethod]
        public void when_productId_not_specified()
        {
            var spec = new FailingCommandSpecification<AddProduct>
            {
                Given = Enumerable.Empty<IEvent>(),
                When = new AddProduct(null, "Pants", 12),
                ExpectException = new ArgumentNullException()
            };

            Assert(spec);
        }

        [TestMethod]
        public void when_product_name_not_specified()
        {
            var spec = new FailingCommandSpecification<AddProduct>
            {
                Given = Enumerable.Empty<IEvent>(),
                When = new AddProduct(_productId, null, 12),
                ExpectException = new ArgumentNullException()
            };

            Assert(spec);
        }
    }

	[TestClass]
	public class RemoveProductSpecs : ProductCommandSpecificationTest<RemoveProduct>
	{
		[TestMethod]
		public void when_removing_product()
		{
			var spec = new CommandSpecification<RemoveProduct>
			{
				Given = new[] { new ProductAdded(_productId, "Shirt", 11) },
				When = new RemoveProduct(_productId),
				Expect = new[] { new ProductRemoved(_productId) }
			};

			Assert(spec);
		}

		[TestMethod]
		public void when_removing_already_removed_product()
		{
			var spec = new FailingCommandSpecification<RemoveProduct>
			{
				Given = new IEvent[] 
								{ 
									new ProductAdded(_productId, "Shirt", 11), 
									new ProductRemoved(_productId) 
								},
				When = new RemoveProduct(_productId),
				ExpectException = new CannotModifyRemovedProductException()
			};

			Assert(spec);
		}
	}

    [TestClass]
    public class ChangeProductPriceSpecs : ProductCommandSpecificationTest<ChangeProductPrice>
    {
        [TestMethod]
        public void when_changing_product_price()
        {
            var spec = new CommandSpecification<ChangeProductPrice>
            {
                Given = new[] { new ProductAdded(_productId, "Shirt", 11) },
                When = new ChangeProductPrice(_productId, 23),
                Expect =  new[] { new ProductPriceChanged(_productId, 23) }
            };

            Assert(spec);
        }

        [TestMethod]
        public void when_changing_price_of_not_existing_product()
        {
			var spec = new FailingCommandSpecification<ChangeProductPrice>
            {
                Given = Enumerable.Empty<IEvent>(),
                When = new ChangeProductPrice(_productId, 23),
                ExpectException = new AggregateRootNotFoundException()
            };

			Assert(spec);
        }

        [TestMethod]
        public void when_changing_price_of_product_to_negative_value()
        {
			var spec = new FailingCommandSpecification<ChangeProductPrice>
            {
				Given = new[] { new ProductAdded(_productId, "Shirt", 11) },
                When = new ChangeProductPrice(_productId, -1),
                ExpectException = new ArgumentException()
            };

			Assert(spec);
        }
    }

	[TestClass]
	public class AddRelatedProductSpecs : ProductCommandSpecificationTest<AddRelatedProduct>
	{
		[TestMethod]
		public void when_adding_valid_product_relationship()
		{
			var product1 = new ProductId("123");
			var product2 = new ProductId("234");

			var spec = new CommandSpecification<AddRelatedProduct>
			{
				Given = new[] { 
					new ProductAdded(product1, "Shirt", 11),
					new ProductAdded(product2, "Pants", 34) 
				},
				When = new AddRelatedProduct(product1, product2),
				Expect = new[] { 
					new RelatedProductAdded(product1, product2),
				}
			};

			Assert(spec);
		}

		[TestMethod]
		public void when_trying_to_add_duplicate_relationship()
		{
			var product1 = new ProductId("123");
			var product2 = new ProductId("234");

			var spec = new FailingCommandSpecification<AddRelatedProduct>
			{
				Given = new IEvent[] { 
					new ProductAdded(product1, "Shirt", 11),
					new ProductAdded(product2, "Pants", 34),
					new RelatedProductAdded(product1, product2)
				},
				When = new AddRelatedProduct(product1, product2),
				ExpectException = new InvalidOperationException()
			};

			Assert(spec);
		}

		[TestMethod]
		public void when_trying_to_add_relationship_to_not_existing_product()
		{
			var product1 = new ProductId("123");
			var product2 = new ProductId("234");

			var spec = new FailingCommandSpecification<AddRelatedProduct>
			{
				Given = new IEvent[] { 
					new ProductAdded(product1, "Shirt", 11),
				},
				When = new AddRelatedProduct(product1, product2),
				ExpectException = new AggregateRootNotFoundException()
			};

			Assert(spec);
		}
	}
}
