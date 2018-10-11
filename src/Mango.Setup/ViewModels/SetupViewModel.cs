using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Events;
using Mango.Infrastructure.Models;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;

namespace Mango.Setup.ViewModels
{
    public class SetupViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;
        private IUnityContainer container;
        private IRegionManager regionManager;
        private int selectedTabIndex;

        public SetupViewModel(IRegionManager _regionManager, IUnityContainer _container, IEventAggregator _eventAggregator)
        {
            container = _container;
            regionManager = _regionManager;
            eventAggregator = _eventAggregator;
            
            LoggedInUser = Utility.LoggedInUser;

            //TabItemSelectedCommand = new DelegateCommand(OnTabItemSelectedCommand);
        }

        public int SelectedTabIndex
        {
            get { return selectedTabIndex; }
            set
            {
                selectedTabIndex = value;
                base.OnPropertyChanged("SelectedTabIndex");
            }
        }

        public DelegateCommand TabItemSelectedCommand { get; private set; }

        private void OnTabItemSelectedCommand()
        {
            eventAggregator.GetEvent<SetupEvent>().Publish(null);
        }


    }



}
