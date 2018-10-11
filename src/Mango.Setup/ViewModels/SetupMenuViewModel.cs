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
using Mango.Infrastructure.Animation;
using Mango.Infrastructure.Models;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Commands;
using Mango.Setup.Views;
using Mango.Infrastructure.ViewModelBase;

namespace Mango.Setup.ViewModels
{
    public class SetupMenuViewModel : ViewModelBase
    {
        private Dispatcher dispatcher;

        private IRegion contentRegion;
        private IRegionManager regionManager;
        private IUnityContainer container;
        private UserControl currentView;
        private bool canManageSetup;

        public SetupMenuViewModel(IRegionManager _regionManager, IUnityContainer _container)
        {
            dispatcher = Deployment.Current.Dispatcher;

            regionManager = _regionManager;
            container = _container;

            SetupCommand = new DelegateCommand(OnSetupCommand);
            SetupHomeCommand = new DelegateCommand(OnSetupHomeCommand);
            ReportCommand = new DelegateCommand(OnReportCommand);

            StaffCommand = new DelegateCommand(OnStaffCommand);
            RoleCommand = new DelegateCommand(OnRoleCommand);
            KpiCommand = new DelegateCommand(OnKpiCommand);
            AppraisalPeriodCommand = new DelegateCommand(OnAppraisalPeriodCommand);
            UploadCommand = new DelegateCommand(OnUploadCommand);

            if (Utility.LoggedInUser != null)
            {
                CanManageSetup = Utility.LoggedInUser.Role.UserRight.CanManageSetup;
            }
        }

        public bool CanManageSetup
        {
            get { return canManageSetup; }
            set
            {
                canManageSetup = value;
                OnPropertyChanged("CanManageRequest");
            }
        }

        public DelegateCommand SetupCommand { get; private set; }
        public DelegateCommand SetupHomeCommand { get; private set; }
        public DelegateCommand ReportCommand { get; private set; }

        public DelegateCommand StaffCommand { get; private set; }
        public DelegateCommand RoleCommand { get; private set; }
        public DelegateCommand KpiCommand { get; private set; }
        public DelegateCommand AppraisalPeriodCommand { get; private set; }
        public DelegateCommand UploadCommand { get; private set; }

        private string RootWebAddress { get; set; }

        private void OnSetupCommand()
        {
            SetupView view = container.Resolve<SetupView>();
            ChangeContent(view);
        }
        private void OnSetupHomeCommand()
        {
            SetupHomeView view = container.Resolve<SetupHomeView>();
            ChangeContent(view);
        }
        private void OnReportCommand()
        {
            try
            {
                AppraisalReportView view = container.Resolve<AppraisalReportView>();
                ChangeContent(view);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void OnStaffCommand()
        {
            try
            {
                StaffSetupView view = container.Resolve<StaffSetupView>();
                ChangeContent(view);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void OnRoleCommand()
        {
            try
            {
                RoleSetupView view = container.Resolve<RoleSetupView>();
                ChangeContent(view);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void OnKpiCommand()
        {
            try
            {
                MetricsSetupView view = container.Resolve<MetricsSetupView>();
                ChangeContent(view);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void OnAppraisalPeriodCommand()
        {
            try
            {
                PeriodSetupView view = container.Resolve<PeriodSetupView>();
                ChangeContent(view);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void OnUploadCommand()
        {
            try
            {
                UploadView view = container.Resolve<UploadView>();
                ChangeContent(view);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void ChangeContent(UserControl view)
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    contentRegion = this.regionManager.Regions[RegionContainer.Content];
                    
                    if (currentView != null)
                    {
                        if (currentView.ToString() == view.ToString())
                        {
                            return;
                        }
                    }

                    if (view.ToString() == "Mango.Setup.Views.SetupView" || view.ToString() == "Mango.Setup.Views.StaffSetupView" || view.ToString() == "Mango.Setup.Views.RoleSetupView" || view.ToString() == "Mango.Setup.Views.PeriodSetupView" || view.ToString() == "Mango.Setup.Views.MetricsSetupView" || view.ToString() == "Mango.Setup.Views.UploadView")
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
            catch (Exception)
            {
                throw;
            }
        }




    }
}
