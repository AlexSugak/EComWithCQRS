using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECom.Messages;

namespace ECom.Site.Core
{
    public class IdentityBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string attemptedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            if (String.IsNullOrWhiteSpace(attemptedValue))
            {
                return NullId.Instance;
            }

            switch (bindingContext.ModelType.Name)
            {
                case "NullId":
                    return NullId.Instance;
                case "ProductId":
					return new ProductId(attemptedValue);
                case "CatalogId":
					return new CatalogId(Guid.Parse(attemptedValue));
                case "DiscountId":
					return new DiscountId(Guid.Parse(attemptedValue));
                case "OrderId":
					return new OrderId(attemptedValue);
                default:
                    var message = string.Format("Unknown identity: {0}", attemptedValue);
                    throw new InvalidOperationException(message);
            }
        }
    }
}