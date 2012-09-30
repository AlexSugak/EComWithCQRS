using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;
using ECom.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECom.Domain.Exceptions;

namespace ECom.CommandHandlers.Tests.Specifications
{
	[TestClass]
	public abstract class UserCommandSpecificationTest<T> : CommandSpecificationTest<T>
		where T : ICommand<UserId>
	{
		protected UserId _userId;

		[TestInitialize]
		public override void SetUp()
		{
			base.SetUp();

			_userId = new UserId("123");
		}
	}

	[TestClass]
	public class ReportUserLoggedInSpecs : UserCommandSpecificationTest<ReportUserLoggedIn>
	{
		[TestMethod]
		public void user_loggs_in_for_the_first_time()
		{
			var spec = new CommandSpecification<ReportUserLoggedIn>
			{
				Given = Enumerable.Empty<IEvent>(),
				When = new ReportUserLoggedIn(_userId, "name", null),
				Expect = new IEvent[] { 
					new UserCreated(_userId),
					new UserLoggedInReported(_userId, "name", null)
				}
			};

			Assert(spec);
		}

		[TestMethod]
		public void user_loggs_in_second_time()
		{
			var spec = new CommandSpecification<ReportUserLoggedIn>
			{
				Given = new IEvent[] { 
					new UserCreated(_userId),
					new UserLoggedInReported(_userId, "name", null) 
				},
				When = new ReportUserLoggedIn(_userId, "name", null),
				Expect = new IEvent[] { 
					new UserLoggedInReported(_userId, "name", null)
				}
			};

			Assert(spec);
		}
	}
}
