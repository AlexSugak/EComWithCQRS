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
using ECom.Site.Core;
using System.Reflection;
using ECom.Bus;
using ECom.Infrastructure;
using ECom.Site.Models;
using FluentValidation.Mvc;
using FluentValidation.Attributes;

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

			ModelValidatorProviders.Providers.Clear();
			ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new AttributedValidatorFactory()));

            ModelBinders.Binders.Add(typeof(ProductId), new IdentityBinder());
			ModelBinders.Binders.Add(typeof(OrderId), new IdentityBinder());
			ModelBinders.Binders.Add(typeof(OrderItemId), new IdentityBinder());
			ModelBinders.Binders.Add(typeof(UserId), new IdentityBinder());

            InitServices();
        }

        private static void InitServices()
        {
            var eventStoreConnString = ConfigurationManager.ConnectionStrings["EventStore"].ConnectionString;

            var bus = new Bus.Bus();
            var eventStore = new EventStore.SQL.EventStore(eventStoreConnString, bus);
			
			var readModelRepo = new SimpleRepository("ReadModel", SimpleRepositoryOptions.RunMigrations);
			var dtoManager = new SubSonicDtoManager(readModelRepo);
			var readModel = new SubSonicReadModelFacade(readModelRepo);

			var commandHandlersAssemblies = new [] 
			{ 
				Assembly.Load(new AssemblyName("ECom.Domain"))
			};

			var eventHandlersAssemblies = new [] { Assembly.Load(new AssemblyName("ECom.ReadModel")) };

			MessageHandlersRegister.RegisterCommandHandlers(commandHandlersAssemblies, bus, eventStore);
			MessageHandlersRegister.RegisterEventHandlers(eventHandlersAssemblies, bus, dtoManager, readModel);

            ServiceLocator.Bus = bus;
            ServiceLocator.ReadModel = readModel;
			ServiceLocator.IdentityGenerator = new SqlTableDomainIdentityGenerator(eventStoreConnString);
        }
    }
}