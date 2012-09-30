using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.Site.Core
{
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