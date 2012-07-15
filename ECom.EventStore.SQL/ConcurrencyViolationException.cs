using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.EventStore.SQL
{
    [Serializable]
    public class ConcurrencyViolationException : Exception
    {
        public ConcurrencyViolationException() { }
        public ConcurrencyViolationException(string message) : base(message) { }
        public ConcurrencyViolationException(string message, Exception inner) : base(message, inner) { }
        protected ConcurrencyViolationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
