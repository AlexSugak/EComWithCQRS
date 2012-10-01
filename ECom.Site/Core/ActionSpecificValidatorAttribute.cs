using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.Site.Core
{
	/// <summary>
	/// Assigns specific validator type to be used when validating view model
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class ActionSpecificValidatorAttribute : Attribute
	{
		/// <summary>
		/// The type of the validator used to validate the model.
		/// </summary>
		public Type ValidatorType { get; private set; }

		/// <summary>
		/// Creates an instance of the ActionSpecificValidatorAttribute allowing a validator type to be specified.
		/// </summary>
		public ActionSpecificValidatorAttribute(Type validatorType)
		{
			ValidatorType = validatorType;
		}
	}
}