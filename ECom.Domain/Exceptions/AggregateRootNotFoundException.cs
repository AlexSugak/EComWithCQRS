using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Domain.Exceptions
{
    [Serializable]
    public class AggregateRootNotFoundException : Exception
    {
        public AggregateRootNotFoundException() { }
        public AggregateRootNotFoundException(string message) : base(message) { }
        public AggregateRootNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected AggregateRootNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
