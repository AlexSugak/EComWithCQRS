using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECom.Messages;
using ECom.Infrastructure;
using System.Reflection;

namespace ECom.CommandHandlers.Tests
{
    [TestClass]
    public abstract class CommandSpecificationTest<TCommand>
        where TCommand : ICommand
    {
        protected Exception Cought;
        protected Bus.Bus Bus;
		protected FakeEventStore FakeEventStore;

        [TestInitialize]
        public virtual void SetUp()
        {
            Cought = null;

			var commandHandlersAssembly = Assembly.Load(new AssemblyName("ECom.Domain"));
			FakeEventStore = new FakeEventStore();
			Bus = new Bus.Bus();
			MessageHandlersRegister.RegisterCommandHandlers(new []{ commandHandlersAssembly }, Bus, FakeEventStore);
        }

        protected void Assert(CommandSpecification<TCommand> specification)
        {
            List<IEvent> producedEvents = null;
            List<IEvent> expectedEvents = null;

            FakeEventStore.SetupEventsHistory(specification.Given);

            try
            {
				Bus.Send(specification.When);
            }
            catch (Exception e)
            {
                Cought = e;
            }

            if (specification is FailingCommandSpecification<TCommand>)
            {
                if (Cought == null || Cought.GetType() != (specification as FailingCommandSpecification<TCommand>).ExpectException.GetType())
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Expected exception not cought");
                    return;
                }
            }
            else if (Cought != null)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Unexpected exception was thrown: {0}", Cought.Message);
                return;
            }
            else
            {
                producedEvents = FakeEventStore.NewEvents().ToList();
                expectedEvents = specification.Expect.ToList();

				CollectionAssert.AreEqual(expectedEvents, producedEvents);
            }
        }
    }
}
