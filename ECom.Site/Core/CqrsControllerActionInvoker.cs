using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ECom.Messages;
using ECom.Site.Controllers;

namespace ECom.Site.Core
{
	/// <summary>
	/// Uses command POST action name convention to fallback to generic command action if no specific action is defined
	/// </summary>
	public class CqrsControllerActionInvoker : ControllerActionInvoker
	{
		protected override ActionDescriptor FindAction(
			ControllerContext controllerContext, 
			ControllerDescriptor controllerDescriptor, 
			string actionName)
		{
			var action = base.FindAction(controllerContext, controllerDescriptor, actionName);
			if (action != null)
			{
				return action;
			}

			if (typeof(CqrsController).IsAssignableFrom(controllerDescriptor.ControllerType))
			{
				//TODO: cache command types?
				var messagesAssembly = Assembly.Load(new AssemblyName("ECom.Messages"));
				var commandTypes = messagesAssembly.GetTypes()
									.Where(t => typeof(ICommand).IsAssignableFrom(t))
									.Select(t => new { Name = t.Name, Type = t });

				var command = commandTypes.FirstOrDefault(c => c.Name.Equals(actionName, StringComparison.InvariantCultureIgnoreCase));

				if (command != null)//we have a command action but the action itself is not declared
				{
					//fallback to cqrs controller generic command action
                    var actionInfo = controllerDescriptor.ControllerType.GetMethod("SubmitCommand").MakeGenericMethod(command.Type);

					if (actionInfo != null)
					{
						return new ReflectedActionDescriptor(actionInfo, actionName, controllerDescriptor);
					}
				}
			}

			return null;
		}
	}
}