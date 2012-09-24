using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Domain.Aggregates.Catalog.Exceptions
{
	[Serializable]
	public class CircularCategoryReferenceDetectedException : Exception
	{
		public CircularCategoryReferenceDetectedException() { }
		public CircularCategoryReferenceDetectedException(string message) : base(message) { }
		public CircularCategoryReferenceDetectedException(string message, Exception inner) : base(message, inner) { }
		protected CircularCategoryReferenceDetectedException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
