using ECom.Domain;
using ECom.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
					new UserCreated(TimeProvider.Now, 1, _userId),
					new UserLoggedInReported(TimeProvider.Now, 2, _userId, "name", null)
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
					new UserCreated(TimeProvider.Now, 1, _userId),
					new UserLoggedInReported(TimeProvider.Now, 2, _userId, "name", null) 
				},
				When = new ReportUserLoggedIn(_userId, "name", null),
				Expect = new IEvent[] { 
					new UserLoggedInReported(TimeProvider.Now, 3, _userId, "name", null)
				}
			};

			Assert(spec);
		}
	}
}
