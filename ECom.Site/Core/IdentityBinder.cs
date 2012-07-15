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

            var args = attemptedValue.Split(new[] { '-' }, 2);
            var id = args[1];
            switch (args[0])
            {
                case NullId.TagValue:
                    return NullId.Instance;
                case ProductId.TagValue:
                    return new ProductId(Guid.Parse(id));
                case CatalogId.TagValue:
                    return new CatalogId(Guid.Parse(id));
                case DiscountId.TagValue:
                    return new DiscountId(Guid.Parse(id));
                case OrderId.TagValue:
                    return new OrderId(Guid.Parse(id));
                default:
                    var message = string.Format("Unknown identity: {0}", attemptedValue);
                    throw new InvalidOperationException(message);
            }
        }
    }
}