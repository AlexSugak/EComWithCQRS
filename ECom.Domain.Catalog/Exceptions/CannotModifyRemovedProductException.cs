using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Domain.Catalog.Exceptions
{
	[Serializable]
	public class CannotModifyRemovedProductException : Exception
	{
		public CannotModifyRemovedProductException() { }
		public CannotModifyRemovedProductException(string message) : base(message) { }
		public CannotModifyRemovedProductException(string message, Exception inner) : base(message, inner) { }
		protected CannotModifyRemovedProductException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
