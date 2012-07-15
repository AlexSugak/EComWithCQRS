using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;

namespace ECom.CommandHandlers.Tests
{
    public class FailingCommandSpecification<T> : CommandSpecification<T>
        where T : ICommand
    {
        public Exception ExpectException;
    }
}
