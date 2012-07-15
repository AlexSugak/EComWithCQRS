using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECom.Utility;
using ECom.Messages;
using ECom.ReadModel;

namespace ECom.Site.Core
{
	/// <summary>
	/// Provides metadata for command classes when they are used as view models
	/// </summary>
	public class CommandMetadataProvider : DataAnnotationsModelMetadataProvider
	{
		protected override ModelMetadata CreateMetadata(
				IEnumerable<Attribute> attributes,
				Type containerType,
				Func<object> modelAccessor,
				Type modelType,
				string propertyName)
		{
			if (!typeof(ICommand).IsAssignableFrom(containerType))
			{
				return base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
			}

			var metadata = new ModelMetadata(this, containerType, modelAccessor, modelType, propertyName);

			if (String.IsNullOrWhiteSpace(propertyName))
			{
				return metadata;
			}

			if (propertyName.EndsWith("id", StringComparison.OrdinalIgnoreCase) 
				|| 
				propertyName.Equals("version", StringComparison.OrdinalIgnoreCase))
			{
				metadata.TemplateHint = "HiddenInput";
				metadata.HideSurroundingHtml = true;
			}

			metadata.IsRequired = true;
			metadata.DisplayName = propertyName.Wordify();

			return metadata;
		}
	}
}
