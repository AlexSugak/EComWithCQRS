using ECom.Domain;
using ECom.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ECom.CommandHandlers.Tests.Specifications
{
	[TestClass]
	public abstract class OrderCommandSpecificationTest<T> : CommandSpecificationTest<T>
		where T : ICommand<OrderId>
	{
		protected OrderId _orderId;
		protected UserId _userId;
		protected OrderItemId _productId;
		protected Uri _productUri;

		[TestInitialize]
		public override void SetUp()
		{
            StaticsInitializer.Dummy();
			base.SetUp();

            _orderId = new OrderId(123);
			_userId = new UserId("user123");

			_productId = new OrderItemId(2345);
			_productUri = new Uri("http://someshop.com/product/123");
		}
	}


	[TestClass]
	public class CreateNewOrderSpecs : OrderCommandSpecificationTest<CreateNewOrder>
	{
		[TestMethod]
		public void user_starts_to_fill_the_order()
		{
			var spec = new CommandSpecification<CreateNewOrder>
			{
				Given = Enumerable.Empty<IEvent>(),
				When = new CreateNewOrder(_orderId, _userId),
				Expect = new[] { 
					new NewOrderCreated(TimeProvider.Now, 1,_orderId, _userId) 
				}
			};

			Assert(spec);
		}
	}

	[TestClass]
	public class AddProductToOrderSpecs : OrderCommandSpecificationTest<AddProductToOrder>
	{
		[TestMethod]
		public void adding_product_to_the_order()
		{
			var spec = new CommandSpecification<AddProductToOrder>
			{
				Given = new IEvent[] {
					new NewOrderCreated(TimeProvider.Now, 1,_orderId, _userId)
				},
				When = new AddProductToOrder(_orderId, _productId, _productUri, "product 1", null, 123, 2, null, null, null, 1),
				Expect = new[] { 
					new ProductAddedToOrder(TimeProvider.Now, 2, _orderId, _productId, _productUri, "product 1", null, 123, 2, null, null, null) 
				}
			};

			Assert(spec);
		}
	}

	[TestClass]
	public class RemoveItemFromOrderSpecs : OrderCommandSpecificationTest<RemoveItemFromOrder>
	{
		[TestMethod]
		public void removing_product_from_order()
		{
			var spec = new CommandSpecification<RemoveItemFromOrder>
			{
				Given = new IEvent[] {
					new NewOrderCreated(TimeProvider.Now, 1,_orderId, _userId),
					new ProductAddedToOrder(TimeProvider.Now, 2, _orderId, _productId, _productUri, "product 1", null, 123, 2, null, null, null)
				},
				When = new RemoveItemFromOrder(_orderId, _productId, 0),
				Expect = new[] { 
					new ItemRemovedFromOrder(TimeProvider.Now, 3, _orderId, _productId)
				}
			};

			Assert(spec);
		}

		[TestMethod]
		public void trying_to_remove_not_added_product()
		{
			var spec = new FailingCommandSpecification<RemoveItemFromOrder>
			{
				Given = new IEvent[] {
					new NewOrderCreated(TimeProvider.Now, 1,_orderId, _userId),
				},
				When = new RemoveItemFromOrder(_orderId, _productId, 0),
				ExpectException = new ArgumentException()
			};

			Assert(spec);
		}
	}
}
