using ECom.EventViewer.Service;
using ECom.Messages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace ECom.EventViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitServices();
        }

        private static void InitServices()
        {
            var eventStoreConnString = ConfigurationManager.AppSettings["REDISCLOUD_URL_STRIPPED"];

            var bus = new Bus.Bus();
            var eventStore = new EventStore.Redis.EventStore(eventStoreConnString, bus);

            ServiceLocator.Bus = bus;
            ServiceLocator.EventStore = eventStore;
        }
    }
}
