using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;

namespace ECom.CommandHandlers.Tests
{
    public class CommandSpecification<T>
        where T : ICommand
    {
        public IEnumerable<IEvent> Given;
        public T When;
        public IEnumerable<IEvent> Expect;
    }
}
