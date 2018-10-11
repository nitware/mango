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
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Mango.Infrastructure.Events;
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Models;
using Mango.Appraisal.Views;

namespace Mango.Appraisal.ViewModels
{
    public class AppraisalMenuViewModel : ViewModelBase
    {
        //private int height;
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        private IEventAggregator eventAggregator;
        //Dictionary<string, string> staffDictionary;

        Staff staffDictionary;

        private Dispatcher dispatcher;
        private UserControl currentView;
        private IRegion contentRegion;

        public AppraisalMenuViewModel(IRegionManager _regionManager, IUnityContainer _container, IEventAggregator _eventAggregator)
        {
            //staffDictionary = new Dictionary<string, string>();
            staffDictionary = new Staff();
            MyReportMenuItemClickCommand = new DelegateCommand(OnMyReportMenuItemClick, CanViewReport);
            HODAppraiseesMenuItemClickCommand = new DelegateCommand(OnHODAppraiseesMenuItemClickCommand, CanHODAppraiseesMenuItemExecute);
            MyAppraisalCommand = new DelegateCommand(OnMyAppraisalCommand);
            //SetupPeriodMenuItemClickCommand = new DelegateCommand(OnSetupPeriodMenuItemClickCommand, CanSetupPeriod);
            
            dispatcher = Deployment.Current.Dispatcher;

            eventAggregator = _eventAggregator;
            regionManager = _regionManager;
            container = _container;

            eventAggregator.GetEvent<StaffEvent>().Subscribe(OnLoad, ThreadOption.UIThread);
        }

        public string RootWebAddress { get; set; }
        //public int Height
        //{
        //    get { return height; }
        //    set
        //    {
        //        height = value;
        //        base.OnPropertyChanged("Height");
        //    }
        //}

        //public void OnUiHeightChaged(int height)
        //{
        //    //Height = height + 35;

           
        //    dispatcher.BeginInvoke(() =>
        //    {
        //        Height = height + 35;
        //    });
        //}
        public void OnLoad(Staff _staffDictionary)
        {
            staffDictionary = _staffDictionary;
            HODAppraiseesMenuItemClickCommand.RaiseCanExecuteChanged();
            if (SetupPeriodMenuItemClickCommand != null)
            {
                SetupPeriodMenuItemClickCommand.RaiseCanExecuteChanged();
            }
        }
        //public void OnLoad(Dictionary<string, string> _staffDictionary)
        //{
        //    staffDictionary = _staffDictionary;
        //    HODAppraiseesMenuItemClickCommand.RaiseCanExecuteChanged();
        //    SetupPeriodMenuItemClickCommand.RaiseCanExecuteChanged();
        //}
        
        public ICommand MyReportMenuItemClickCommand { get;private set; }
        public DelegateCommand HODAppraiseesMenuItemClickCommand { get; private set; }
        public DelegateCommand SetupPeriodMenuItemClickCommand { get; private set; }
        public DelegateCommand MyAppraisalCommand { get; private set; }

        private bool CanViewReport()
        {
            //bool d = Convert.ToBoolean(staffDictionary["isAdmin"]);
            //return Convert.ToBoolean(staffDictionary["isAdmin"]) ? true : false;

            bool d = Convert.ToBoolean(staffDictionary.IsAdmin);
            return Convert.ToBoolean(staffDictionary.IsAdmin) ? true : false;
        }
        private bool CanSetupPeriod()
        {
            //byte staffType = 0;
            //if (staffDictionary != null)
            //{
            //    staffType = Convert.ToByte(staffDictionary["type"]);
            //}

            //return staffType == 4 ? true : false;

            //string staffId = staffDictionary["id"];

            //return Convert.ToInt32(staffDictionary["companyDepartmentJobRoleId"]) == 42 ? true : false;

            return Convert.ToInt32(staffDictionary.CompanyDepartmentJobRoleId) == 42 ? true : false;
        }

        private bool CanHODAppraiseesMenuItemExecute()
        {
            return EnableMenu() == 3 ? true : false;
        }

        private byte EnableMenu()
        {
            byte staffType = 0;
            if (staffDictionary != null)
            {
                //staffType = Convert.ToByte(staffDictionary["type"]);
                staffType = Convert.ToByte(staffDictionary.Type);
            }

            return staffType;
        }

        public void OnMyAppraisalCommand()
        {
            //eventAggregator.GetEvent<StaffEvent>().Publish(staffDictionary);
            eventAggregator.GetEvent<StaffEvent>().Publish(Utility.LoggedInUser); //publish event
        }

        public void OnMyReportMenuItemClick()
        {
            eventAggregator.GetEvent<StaffEvent>().Publish(Utility.LoggedInUser); //publish event
            Utility.DisplayReport(Utility.RootWebAddress + "/ReportPresenters/appraisalReport.aspx");
        }

        public void OnHODAppraiseesMenuItemClickCommand()
        {
            eventAggregator.GetEvent<HodAppraiseesEvent>().Publish(Utility.LoggedInUser);
        }

        //public void OnSetupPeriodMenuItemClickCommand()
        //{
        //    eventAggregator.GetEvent<StaffEvent>().Publish(Utility.LoggedInUser); //publish event
        //    PeriodSetupView view = container.Resolve<PeriodSetupView>();
        //    SwitchView(view);
        //}
        
        private void ShowAppraisalView()
        {
            AppraisalView view = container.Resolve<AppraisalView>();
            IRegion contentRegion = this.regionManager.Regions["ContentRegion"];
            contentRegion.Add(view);
            contentRegion.Activate(view);
        }

        private void SwitchView(UserControl view)
        {
            dispatcher.BeginInvoke(() =>
            {
                //if (contentRegion == null)
                //{
                //contentRegion = null;
                contentRegion = this.regionManager.Regions["ContentRegion"];
                //}

                //contentRegion = container.Resolve<>

                if (currentView != null)
                {
                    if (currentView.ToString() == view.ToString())
                    {
                        return;
                    }
                }

                currentView = view;
                contentRegion.Add(view);
                contentRegion.Activate(view);

                //Animation.SwitchToPage();
                //eventAggregator.GetEvent<UserEvent>().Publish(LoggedInUser);
            });
        }

        public void GetRootWebAddress(string rootWebAddress)
        {
            RootWebAddress = rootWebAddress;
        }

    }


}
