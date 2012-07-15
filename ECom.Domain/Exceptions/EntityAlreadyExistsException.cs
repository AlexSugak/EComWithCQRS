using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Domain.Exceptions
{
	[Serializable]
	public class EntityAlreadyExistsException : Exception
	{
		public EntityAlreadyExistsException() { }
		public EntityAlreadyExistsException(string message) : base(message) { }
		public EntityAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
		protected EntityAlreadyExistsException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
