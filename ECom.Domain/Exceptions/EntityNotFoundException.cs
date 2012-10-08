using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ECom.Domain.Exceptions
{
	[Serializable]
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException() { }
		public EntityNotFoundException(string message) : base(message) { }
		public EntityNotFoundException(string messageFormat, params object[] args) : base(String.Format(CultureInfo.InvariantCulture, messageFormat, args)) { }
		public EntityNotFoundException(string entityType, string entityId) : base(String.Format(CultureInfo.InvariantCulture, "{0} with id '{1}' not found", entityType, entityId)) { }
		public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }
		protected EntityNotFoundException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
