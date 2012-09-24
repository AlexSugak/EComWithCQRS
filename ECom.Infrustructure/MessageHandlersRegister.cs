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
		//var prodComdsHndl = new ProductCommandHandler(eventsStore);
		//bus.RegisterHandler<AddProduct>(prodComdsHndl.Handle);
		//bus.RegisterHandler<ChangeProductPrice>(prodComdsHndl.Handle);

		//var orderComdsHndl = new OrderCommandHandler(eventsStore);
		//bus.RegisterHandler<AddProductToOrder>(orderComdsHndl.Handle);

		//var productDetails = new ProductDetailsView(readModelRepository);

		//bus.RegisterHandler<ProductAdded>(productDetails.Handle);
		//bus.RegisterHandler<ProductPriceChanged>(productDetails.Handle);

		//var productList = new ProductListView(readModelRepository);

		//bus.RegisterHandler<ProductAdded>(productList.Handle);
		//bus.RegisterHandler<ProductPriceChanged>(productList.Handle);


		public static void RegisterCommandHandlers(IEnumerable<Assembly> cmdHndlrsAssemblies, Bus.Bus bus, IEventStore eventsStore)
		{
			RegisterHandlersInAssembly(cmdHndlrsAssemblies, typeof(ICommand), bus, typeof(IEventStore), eventsStore);
		}

		public static void RegisterEventHandlers(IEnumerable<Assembly> eventHndlrsAssemblies, Bus.Bus bus, IDtoManager manager)
		{
			RegisterHandlersInAssembly(eventHndlrsAssemblies, typeof(IEvent), bus, typeof(IDtoManager), manager);
		}

		private static void RegisterHandlersInAssembly(IEnumerable<Assembly> assemblies, Type messageType, Bus.Bus bus, Type ctorArgType, object ctorArg)
		{
			//among classes in handlers assemblies select any which handle specified message type
			var handlerTypesWithMessages = assemblies
										.SelectMany(a => a.GetTypes())
										.Where(t => !t.IsInterface && t.GetInterfaces()
											.Any(i => i.IsGenericType
												&& i.GetGenericTypeDefinition() == typeof(IHandle<>)
												&& messageType.IsAssignableFrom(i.GetGenericArguments().First())))
										.Select(t => new
										{
											Type = t,
											MessageTypes = t.GetInterfaces().Select(i => i.GetGenericArguments().First())
										});

			foreach (var handler in handlerTypesWithMessages)
			{
				var ctor = handler.Type.GetConstructor(new[] { ctorArgType });
				var handlerInstance = ctor.Invoke(new[] { ctorArg });

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

			//var busType = bus.GetType();

			//var eventHandlerTypes = assemblies.GetTypes().Where(t => t.Name.EndsWith(handlerNameEndsWith));
			//foreach (var eventHandlerType in eventHandlerTypes)
			//{
			//	var ctor = eventHandlerType.GetConstructor(new[] { ctorArgType });
			//	var hndlerInstance = ctor.Invoke(new[] { ctorArg });

			//	var handlerMethods = eventHandlerType.GetMethods().Where(m => m.Name.Equals("Handle", StringComparison.OrdinalIgnoreCase));

			//	foreach (var hndlMethod in handlerMethods)
			//	{
			//		var eventType = hndlMethod.GetParameters().First().ParameterType;
			//		var actionType = typeof(Action<>).MakeGenericType(eventType);

			//		var handler = Delegate.CreateDelegate(actionType, hndlerInstance, hndlMethod);

			//		var genericRegister = busType.GetMethod("RegisterHandler").MakeGenericMethod(new[] { eventType });
			//		genericRegister.Invoke(bus, new[] { handler });
			//	}
			//}
		}
	}
}
