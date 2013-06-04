using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ECom.Messages;
using SubSonic.Repository;
using ECom.ReadModel;

namespace ECom.Infrastructure
{
	public static class MessageHandlersRegister
	{
		public static void RegisterCommandHandlers(IEnumerable<Assembly> cmdHndlrsAssemblies, Bus.Bus bus, IEventStore eventsStore)
		{
			RegisterHandlersInAssembly(cmdHndlrsAssemblies, typeof(ICommand), bus, new[] { typeof(IEventStore) }, new[] { eventsStore });
		}

		public static void RegisterEventHandlers(IEnumerable<Assembly> eventHndlrsAssemblies, Bus.Bus bus, IDtoManager manager)
		{
			RegisterHandlersInAssembly(eventHndlrsAssemblies, typeof(IEvent), bus, new[] { typeof(IDtoManager) }, new object[] { manager });
		}

		public static void RegisterEventHandlers(IEnumerable<Type> eventHndlrs, Bus.Bus bus, IDtoManager manager)
		{
			RegisterHandlers(eventHndlrs, typeof(IEvent), bus, new[] { typeof(IDtoManager) }, new object[] { manager });
		}

		private static void RegisterHandlersInAssembly(IEnumerable<Assembly> assemblies, Type messageType, Bus.Bus bus, Type[] ctorArgTypes, object[] ctorArgs)
		{
			var handlerTypes = assemblies
								.SelectMany(a => a.GetTypes())
								.Where(t => !t.IsInterface && t.GetInterfaces()
									.Any(i => i.IsGenericType
										&& i.GetGenericTypeDefinition() == typeof(IHandle<>)
										&& messageType.IsAssignableFrom(i.GetGenericArguments().First())));

			RegisterHandlers(handlerTypes, messageType, bus, ctorArgTypes, ctorArgs);
		}

		private static void RegisterHandlers(IEnumerable<Type> handlers, Type messageType, Bus.Bus bus, Type[] ctorArgTypes, object[] ctorArgs)
		{
			//among classes in handlers assemblies select any which handle specified message type
			var handlerTypesWithMessages = handlers
										.Select(t => new
										{
											Type = t,
											MessageTypes = t
                                                .GetInterfaces()
												.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandle<>))
                                                .Select(i => i.GetGenericArguments().First())
										});

			foreach (var handler in handlerTypesWithMessages)
			{
				var ctor = handler.Type.GetConstructor(ctorArgTypes);
				var handlerInstance = ctor.Invoke(ctorArgs);

				foreach (Type msgType in handler.MessageTypes)
				{
					MethodInfo handleMethod = handler.Type
												.GetMethods()
												.Where(m => m.Name.Equals("Handle", StringComparison.OrdinalIgnoreCase))
												.First(m => msgType.IsAssignableFrom(m.GetParameters().First().ParameterType));

					Type actionType = typeof(Action<>).MakeGenericType(msgType);
					Delegate handlerDelegate = Delegate.CreateDelegate(actionType, handlerInstance, handleMethod);

					MethodInfo genericRegister = bus.GetType().GetMethod("RegisterHandler").MakeGenericMethod(new[] { msgType });
					genericRegister.Invoke(bus, new[] { handlerDelegate });
				}
			}
		}
	}
}
