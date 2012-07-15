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


		public static void RegisterCommandHandlers(Assembly cmdHndlrsAssembly, Bus.Bus bus, IEventStore eventsStore)
		{
			RegisterHandlersInAssembly(cmdHndlrsAssembly, bus, typeof(IEventStore), eventsStore, "CommandHandler");
		}

		public static void RegisterEventHandlers(Assembly eventHndlrsAssembly, Bus.Bus bus, IDtoManager manager)
		{
			RegisterHandlersInAssembly(eventHndlrsAssembly, bus, typeof(IDtoManager), manager, "View");
		}

		private static void RegisterHandlersInAssembly(Assembly assembly, Bus.Bus bus, Type ctorArgType, object ctorArg, string handlerNameEndsWith)
		{
			var busType = bus.GetType();

			var eventHandlerTypes = assembly.GetTypes().Where(t => t.Name.EndsWith(handlerNameEndsWith));
			foreach (var eventHandlerType in eventHandlerTypes)
			{
				var ctor = eventHandlerType.GetConstructor(new[] { ctorArgType });
				var hndlerInstance = ctor.Invoke(new[] { ctorArg });

				var handlerMethods = eventHandlerType.GetMethods().Where(m => m.Name.Equals("Handle", StringComparison.OrdinalIgnoreCase));

				foreach (var hndlMethod in handlerMethods)
				{
					var eventType = hndlMethod.GetParameters().First().ParameterType;
					var actionType = typeof(Action<>).MakeGenericType(eventType);

					var handler = Delegate.CreateDelegate(actionType, hndlerInstance, hndlMethod);

					var genericRegister = busType.GetMethod("RegisterHandler").MakeGenericMethod(new[] { eventType });
					genericRegister.Invoke(bus, new[] { handler });
				}
			}
		}
	}
}
