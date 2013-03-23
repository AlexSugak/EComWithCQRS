using ECom.Domain;
using ECom.EventViewer.Core;
using ECom.EventViewer.Service;
using ECom.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ICommandExt = System.Windows.Input.ICommand;

namespace ECom.EventViewer.ViewModels
{
    public class EventListViewModel : ViewModelBase
    {
        private readonly IEventStore _storage;
        private static EventListViewModel _instance = null;
        private ObservableCollection<EventViewModel> _eventList = null;
        private EventViewModel _selectedEvent = null;
        private ICommandExt _searchCommand;

        public ObservableCollection<EventViewModel> EventList 
        {
            get
            {
                return _eventList;
            }
            set
            {
                _eventList = value;
                OnPropertyChanged("EventList");
            }
        }

        public EventViewModel SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                OnPropertyChanged("SelectedEvent");
            }
        }

        public ICommandExt SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(i => this.Search(), null);
                }
                return _searchCommand;
            }
        }

        public string AggregateId { get; set; }

        private EventListViewModel()
        {
            this._storage = ServiceLocator.EventStore;
            GetAllEvents();
        }

        public static EventListViewModel Instance()
        {
            if (_instance == null)
                _instance = new EventListViewModel();
            return _instance;
        }

        internal void GetAllEvents()
        {
            if (_eventList == null)
                _eventList = new ObservableCollection<EventViewModel>();

            _eventList.Clear();

            EventList = GetEvents(_storage.GetAllEvents<IIdentity>().ToList());
        }

        private void Search()
        {
            if (!string.IsNullOrEmpty(AggregateId))
            {
                if (_eventList == null)
                    _eventList = new ObservableCollection<EventViewModel>();

                _eventList.Clear();

                string aggregateType = _storage.GetAggregateType(AggregateId);
                IIdentity id = GetTypedAggregateId(AggregateId, aggregateType);

                EventList = GetEvents(_storage.GetEventsForAggregate(id).ToList());
            }
            else
            {
                GetAllEvents();
            }
        }

        #region Helpers

        private ObservableCollection<EventViewModel> GetEvents(List<IEvent<IIdentity>> list)
        {
            ObservableCollection<EventViewModel> eventsList = new ObservableCollection<EventViewModel>();

            foreach (IEvent item in list)
            {
                eventsList.Add(new EventViewModel(item));
            }

            return eventsList;
        }

        #endregion

        #region ReflectionMethods

        private IIdentity GetTypedAggregateId(string agrId, string agrType)
        {
            if (!string.IsNullOrEmpty(agrType))
            {
                // obtain existing aggregate roots
                var type = typeof(AggregateRoot<>);
                var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => IsSubclassOfRawGeneric(type, p) && p != type).ToList();

                // compare with value from DB
                var agrRoot = types.Find(p => p.FullName == agrType);

                if (agrRoot != null)
                {
                    // get type which implements IIdentity (OrderId, UserId and so on)
                    var myType = agrRoot.BaseType.GetGenericArguments()[0];

                    // get argument type (Type of aggregate id: int, guid, string)
                    var argumentType = myType.BaseType.BaseType.GetGenericArguments()[0];

                    object arg;

                    // create instance of a argument type class 
                    if (argumentType.IsClass && argumentType.FullName == "System.String")
                    {
                        arg = agrId;
                    }
                    else if (argumentType.IsClass)
                    {
                        arg = Activator.CreateInstance(argumentType, agrId);
                    }
                    else
                    {
                        TypeConverter tc = TypeDescriptor.GetConverter(argumentType);
                        arg = tc.ConvertFromString(agrId);
                    }

                    // create instance of a class which implements IIdentity
                    IIdentity obj = (IIdentity)Activator.CreateInstance(myType, arg);

                    return obj;
                }
            }

            return new NullId();
        }

        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        #endregion

    }
}
