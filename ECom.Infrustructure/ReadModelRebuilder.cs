using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Bus;
using System.Reflection;
using ECom.Utility;
using SubSonic.Repository;
using SubSonic.DataProviders;
using ECom.ReadModel;
using SubSonic.Query;
using System.IO;
using ECom.Messages;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ECom.Infrastructure
{
	public static class ReadModelRebuilder
	{
		public static void Rebuild(string redisDbUri)
		{
            Argument.ExpectNotNullOrWhiteSpace(() => redisDbUri);

			var readModelAssembly = Assembly.Load(new AssemblyName("ECom.ReadModel"));
			var bus = new Bus.Bus(false);
            var eventStore = new EventStore.Redis.EventStore(redisDbUri, bus);

			var dtoManager = new RedisDtoManager(redisDbUri);

            var dtoTypes = readModelAssembly.GetTypes().Where(t => !t.IsAbstract && typeof(Dto).IsAssignableFrom(t));
            foreach (var dtoType in dtoTypes)
            {
                MethodInfo genericDeleteAll = dtoManager.GetType().GetMethod("DeleteAll").MakeGenericMethod(new[] { dtoType });
                genericDeleteAll.Invoke(dtoManager, new object[0]);
            }

			MessageHandlersRegister.RegisterEventHandlers(new [] { readModelAssembly }, bus, dtoManager);

            var allEvents = eventStore.GetAllEvents();
            foreach (var e in allEvents)
            {
                bus.Publish(e);
            }
		}
	}
}
