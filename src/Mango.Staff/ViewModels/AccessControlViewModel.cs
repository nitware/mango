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

using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Commands;
using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Models;

namespace Mango.Staff.ViewModels
{
    public class AccessControlViewModel : ViewModelBase
    {
        private IUnityContainer container;
        private IRegionManager regionManager;

        private int selectedTabIndex;

        public AccessControlViewModel(IRegionManager _regionManager, IUnityContainer _container)
        {
            regionManager = _regionManager;
            container = _container;

            LoggedInUser = Utility.LoggedInUser;
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


    }
}
