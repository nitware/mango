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

using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Mango.Infrastructure.Models;
using Mango.Infrastructure.Animation;
using Mango.Users.Views;
using Mango.Staff.Views;

namespace mobak.Users.ViewModels
{
    public class StaffMenuViewModel
    {
        private UserControl currentView;
        private IRegion contentRegion;
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        private Dispatcher dispatcher;

        public StaffMenuViewModel(IRegionManager _regionManager, IUnityContainer _container)
        {
            container = _container;
            regionManager = _regionManager;
            
            dispatcher = Deployment.Current.Dispatcher;

            UserHomeCommand = new DelegateCommand(OnUserHomeCommand);
            AccessControlCommand = new DelegateCommand(OnAccessControlCommand);
        }

        public DelegateCommand PersonTypeCommand { get; private set; }
        public DelegateCommand UserHomeCommand { get; private set; }
        public DelegateCommand AccessControlCommand { get; private set; }
              
        private void OnUserHomeCommand()
        {
            StaffHomeView view = container.Resolve<StaffHomeView>();
            SwitchView(view);
        }
        private void OnAccessControlCommand()
        {
            AccessControlView view = container.Resolve<AccessControlView>();
            SwitchView(view);
        }

        private void SwitchView(UserControl view)
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    contentRegion = this.regionManager.Regions[RegionContainer.Content];

                    AccessControlView vw = container.Resolve<AccessControlView>();
                    string v = vw.ToString();

                    if (currentView != null)
                    {
                        if (currentView.ToString() == view.ToString())
                        {
                            return;
                        }
                    }



                    if (view.ToString() == "Mango.Staff.Views.AccessControlView")
                    {
                        if (contentRegion.GetView(view.ToString()) == null)
                        {
                            contentRegion.Add(view, null, true); //create a scoped region
                        }
                    }
                    else
                    {
                        contentRegion.Add(view);
                    }

                    currentView = view;
                    contentRegion.Activate(view);

                    Animation.SwitchToPage();
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }



    }

}
