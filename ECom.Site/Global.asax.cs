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
using Email;
using ECom.Site.Controllers;
using ECom.Domain.Exceptions;
using System.Threading.Tasks;

namespace ECom.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
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
			ModelBinders.Binders.DefaultBinder = new DynamicValidationModelBinder(new AttributedValidatorFactory());

            ModelBinders.Binders.Add(typeof(ProductId), new IdentityBinder());
			ModelBinders.Binders.Add(typeof(OrderId), new IdentityBinder());
			ModelBinders.Binders.Add(typeof(OrderItemId), new IdentityBinder());
			ModelBinders.Binders.Add(typeof(UserId), new IdentityBinder());

            Init();
        }

        private static void Init()
        {
            var bus = new Bus.Bus(false);
            var redisUri = ConfigurationManager.AppSettings["REDISCLOUD_URL_STRIPPED"];
            var eventStore = new EventStore.Redis.EventStore(redisUri, bus);
			
            var dtoManager = new RedisDtoManager(redisUri);

			var commandHandlersAssemblies = new [] 
			{ 
				Assembly.Load(new AssemblyName("ECom.Domain"))
			};

            ReadModelRebuilder.Rebuild(redisUri);

			MessageHandlersRegister.RegisterCommandHandlers(commandHandlersAssemblies, bus, eventStore);
			RegisterEventHandlers(bus, dtoManager);

            ServiceLocator.Bus = bus;
            ServiceLocator.DtoManager = dtoManager;
            ServiceLocator.IdentityGenerator = new RedisIdGenerator(redisUri);
            ServiceLocator.EventStore = eventStore;
        }

		private static void RegisterEventHandlers(Bus.Bus bus, IDtoManager dtoManager)
		{
			var eventHandlersAssemblies = new[] { Assembly.Load(new AssemblyName("ECom.ReadModel")) };
			MessageHandlersRegister.RegisterEventHandlers(eventHandlersAssemblies, bus, dtoManager);


			var mailSender = new MailGunEmailSender(ConfigurationManager.AppSettings["MailGunApiKey"], ConfigurationManager.AppSettings["MailGunAppDomain"]);
			var mailBodyGenerator = new RazorMessageBodyGenerator();
			var emailService = new EmailService(mailSender, new UserDetailsView(dtoManager), mailBodyGenerator);

			bus.RegisterHandler<OrderSubmited>(e => Task.Factory.StartNew(() => emailService.Handle(e)));
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception exception = Server.GetLastError();

			Response.Clear();

			HttpException httpException = exception as HttpException;
			var notFoundException = exception as EntityNotFoundException;

			RouteData routeData = new RouteData();
			routeData.Values.Add("controller", "Error");

			if (notFoundException != null)
			{
				routeData.Values.Add("action", "EntityNotFound");
			}
			else if (httpException == null)
			{
				routeData.Values.Add("action", "General");
			}
			else //It's an Http Exception, Let's handle it.
			{
				switch (httpException.GetHttpCode())
				{
					case 404:
						routeData.Values.Add("action", "PageNotFound");
						break;

					// Here you can handle Views to other error codes.
					// I choose a General error template  
					default:
						routeData.Values.Add("action", "General");
						break;
				}
			}

			// Pass exception details to the target error View.
			routeData.Values.Add("error", exception);

			// Clear the error on server.
			Server.ClearError();

			// Avoid IIS7 getting in the middle
			Response.TrySkipIisCustomErrors = true;

			// Call target Controller and pass the routeData.
			IController errorController = new ErrorController();
			errorController.Execute(new RequestContext(
				 new HttpContextWrapper(Context), routeData));
		}
    }
}