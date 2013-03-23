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
            var eventStoreConnString = ConfigurationManager.ConnectionStrings["EventStore"].ConnectionString;

            var bus = new Bus.Bus();
            var eventStore = new EventStore.SQL.EventStore(eventStoreConnString, bus);

            ServiceLocator.Bus = bus;
            ServiceLocator.EventStore = eventStore;
        }
    }
}
