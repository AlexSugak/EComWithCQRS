using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ECom.Domain;
using ECom.CommandHandlers;
using ECom.Messages;
using SubSonic.Repository;
using ECom.ReadModel;
using ECom.ReadModel.Views;
using System.Configuration;
using ECom.Domain.Catalog;
using ECom.Site.Core;
using System.Reflection;
using ECom.Bus;
using ECom.Infrastructure;
using ECom.Site.Models;

namespace ECom.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

			ModelMetadataProviders.Current = new CommandMetadataProvider();

            InitServices();
        }

        private static void InitServices()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["EventStore"].ConnectionString;

            var bus = new Bus.Bus();
            var eventStore = new EventStore.SQL.EventStore(connectionString, bus);
			
			var readModelRepo = new SimpleRepository("ReadModel", SimpleRepositoryOptions.RunMigrations);
			var dtoManager = new SubSonicDtoManager(readModelRepo);
			var readModel = new SubSonicReadModelFacade(readModelRepo);

			var commandHandlersAssembly = Assembly.Load(new AssemblyName("ECom.CommandHandlers"));
			var eventHandlersAssembly = Assembly.Load(new AssemblyName("ECom.ReadModel"));
			MessageHandlersRegister.RegisterCommandHandlers(commandHandlersAssembly, bus, eventStore);
			MessageHandlersRegister.RegisterEventHandlers(eventHandlersAssembly, bus, dtoManager);

            ServiceLocator.Bus = bus;
            ServiceLocator.ReadModel = readModel;
        }
    }
}