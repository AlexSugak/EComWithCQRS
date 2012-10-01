using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECom.Utility;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Mvc;

namespace ECom.Site.Core
{
	/// <summary>
	/// Uses action attributes to dynamically select view model validator
	/// </summary>
	public class DynamicValidationModelBinder : DefaultModelBinder
	{
		private readonly IValidatorFactory _validatorFactory;

		private readonly InstanceCache cache = new InstanceCache();

		public DynamicValidationModelBinder(IValidatorFactory validatorFactory)
		{
			Argument.ExpectNotNull(() => validatorFactory);

			_validatorFactory = validatorFactory;
		}

		protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var model = bindingContext.Model;
			base.OnModelUpdated(controllerContext, bindingContext);

			var actionName = controllerContext.RouteData.GetRequiredString("action");

			IValidator validator = null;

			var actionMethod = controllerContext.Controller.GetType().GetMethods().FirstOrDefault(m => m.Name == actionName && m.GetParameters().Any(p => p.ParameterType == model.GetType()));
			if (actionMethod != null)
			{
				var validatorAttribute = Attribute.GetCustomAttribute(actionMethod, typeof(ActionSpecificValidatorAttribute));
				if(validatorAttribute != null)
				{
					validator = cache.GetOrCreateInstance(((ActionSpecificValidatorAttribute)validatorAttribute).ValidatorType) as IValidator;
				}
			}

			if(validator == null)
			{
				validator = _validatorFactory.GetValidator(bindingContext.ModelType);
			}

			if (validator != null)
			{
				var result = validator.Validate(model);
				if (!result.IsValid)
				{
					result.AddToModelState(bindingContext.ModelState, "");
				}
			}
		}
	}
}