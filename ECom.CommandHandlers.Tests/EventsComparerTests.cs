using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECom.Messages;

namespace ECom.CommandHandlers.Tests
{
    [TestClass]
	public class AssertEventsClass
    {
		[TestClass]
		public class AreSameMethod
		{
			[TestMethod]
			public void must_return_true_for_two_empty_lists_of_events()
			{
				var left = new List<IEvent>();
				var right = new List<IEvent>();

				Assert.IsTrue(AssertEvents.AreSame(left, right));
			}

			[TestMethod]
			public void must_return_true_for_same_single_events()
			{
				var id = new ProductId(Guid.NewGuid());
				var left = new List<IEvent>() { new ProductAdded(id, "aa", 12) };
				var right = new List<IEvent>() { new ProductAdded(id, "aa", 12) };

				Assert.IsTrue(AssertEvents.AreSame(left, right));
			}

			[TestMethod]
			public void must_return_true_for_same_event_sets()
			{
				var id = new ProductId(Guid.NewGuid());
				var left = new List<IEvent>() { new ProductAdded(id, "aa", 12), new ProductPriceChanged(id, 23) };
				var right = new List<IEvent>() { new ProductAdded(id, "aa", 12), new ProductPriceChanged(id, 23) };

				Assert.IsTrue(AssertEvents.AreSame(left, right));
			}

			[TestMethod]
			public void must_return_false_for_same_single_svents_with_different_property_values()
			{
				var id = new ProductId(Guid.NewGuid());
                var left = new List<IEvent>() { new ProductAdded(id, "aa2", 12) };
                var right = new List<IEvent>() { new ProductAdded(id, "aa", 123) };

				Assert.IsFalse(AssertEvents.AreSame(left, right));
			}

			[TestMethod]
			public void must_return_false_for_different_numbers_of_events()
			{
                var left = new List<IEvent>();
                var right = new List<IEvent>() { new ProductAdded(new ProductId(Guid.NewGuid()), "aa", 12) };

				Assert.IsFalse(AssertEvents.AreSame(left, right));
			}
		}
    }
}
