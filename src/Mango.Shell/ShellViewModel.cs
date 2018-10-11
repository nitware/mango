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

using Microsoft.VisualBasic;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;
using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Events;
using Mango.Infrastructure.Models;
using Mango.Infrastructure.Animation;
using Mango.Infrastructure.MangoService;
using Mango.Home.Views;
using Mango.Users.Views;
using Mango.Users.View;
using Mango.Infrastructure.Converters;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using Mango.Appraisal.Views;
using Mango.Setup.Views;
using Mango.Staff.Views;

namespace Mango.Shell
{
    public class ShellViewModel : ViewModelBase
    {
        private Dispatcher dispatcher;

        private string loginStatus;
        private bool isUserLoggedIn;

        private UserControl currentView;
        private IRegion contentRegion;
        private IRegion menuRegion;
        private IRegionManager regionManager;
        private IUnityContainer container;
        private IEventAggregator eventAggregator;

        private Period period;
        private ImageSource image;
        
        //private const string LOGIN = "Log In";
        //private const string LOGOUT = "Log Out";

        private const string LOGIN = "Login";
        private const string LOGOUT = "Logout";

        public ShellViewModel(IRegionManager _regionManager, IUnityContainer _container, IEventAggregator _eventAggregator)
        {
            container = _container;
            regionManager = _regionManager;
            eventAggregator = _eventAggregator;

            ResetLoggedInUser();
            dispatcher = Deployment.Current.Dispatcher;

            HomeCommand = new DelegateCommand(OnHomeCommand);
            AppraisalCommand = new DelegateCommand(OnAppraisalCommand, CanDoAppraisal);
            UsersCommand = new DelegateCommand(OnUsersCommand, CanManageUser);
            ReportCommand = new DelegateCommand(OnReportCommand, CanViewReport);
            SetupCommand = new DelegateCommand(OnSetupCommand, CanManageSetup);

            Utility.RootWebAddress = GetBaseAddress();
            eventAggregator.GetEvent<StaffEvent>().Subscribe(OnUserLogin, ThreadOption.UIThread);
            eventAggregator.GetEvent<LogOutEvent>().Subscribe(LogOutLinkClicked);
                        
            LoginStatus = LOGIN;
            LogOutLinkButtonCommand = new DelegateCommand(OnLogOutLinkButtonCommand);

            //Utility.Period = new Period() { Id = 9 };
        }

        public DelegateCommand LogOutLinkButtonCommand { get; private set; }
        public DelegateCommand HomeCommand { get; private set; }
        public DelegateCommand AppraisalCommand { get; private set; }
        public DelegateCommand UsersCommand { get; private set; }
        public DelegateCommand ReportCommand { get; private set; }
        public DelegateCommand SetupCommand { get; private set; }

        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                base.OnPropertyChanged("Image");
            }
        }
        public Period Period
        {
            get { return period; }
            set
            {
                period = value;
                base.OnPropertyChanged("Period");
            }
        }

        public void LogOutLinkClicked(string logOut)
        {
            Image = null;

            try
            {
                HomeMenuView menuView = container.Resolve<HomeMenuView>();
                LoginView view = container.Resolve<LoginView>();

                LoggedInUser = null;
                LoginStatus = LOGIN;
                IsUserLoggedIn = false;
                currentView = null;

                ResetLoggedInUser();

                ChangeMenu(menuView);
                ChangeContent(view);
                RaiseButtonCanExecuteEvent();

                //YouAreHere = CurrentModule.LOGIN.ToString();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        public ImageSource LoadCompanyLogo(string companyName)
        {
            string csl = "/wire.Shell;component/Images/cslLogo.png";
            string capitalMarkets = "/wire.Shell;component/Images/fcmbLogo.jpg";

            switch (companyName)
            {
                case "FCMB Capital Markets":
                    {
                        return GetImage(capitalMarkets);
                    }
                default:
                    {
                        return GetImage(csl);
                    }
            }

           
        }
        public ImageSource GetImage(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        private void ResetLoggedInUser()
        {
            try
            {
                LoggedInUser = new Infrastructure.MangoService.Staff();
                LoggedInUser.Role = new Role();
                LoggedInUser.Role.UserRight = new UserRight();
                LoggedInUser.Name = "Guest";
                LoggedInUser.Role.Name = "Guest";
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private bool CanDoAppraisal()
        {
            if (LoggedInUser != null)
            {
                return LoggedInUser.Role.UserRight.CanDoAppraisal;
            }

            return false;
        }
        private bool CanViewReport()
        {
            if (LoggedInUser != null)
            {
                return LoggedInUser.Role.UserRight.CanManageReport;
            }

            return false;
        }
              
        private bool CanManageUser()
        {
            if (LoggedInUser != null)
            {
                return LoggedInUser.Role.UserRight.CanManageUser;
            }

            return false;
        }

        private bool CanManageSetup()
        {
            if (LoggedInUser != null)
            {
                return LoggedInUser.Role.UserRight.CanManageSetup;
            }

            return false;
        }

        public bool IsUserLoggedIn
        {
            get { return isUserLoggedIn; }
            set
            {
                isUserLoggedIn = value;
                base.OnPropertyChanged("IsUserLoggedIn");
            }
        }
       
        public string LoginStatus
        {
            get { return loginStatus; }
            set
            {
                loginStatus = value;
                base.OnPropertyChanged("LoginStatus");
            }
        }

        //public void OnUserLogin(Dictionary<string, string> staff)
        public void OnUserLogin(Infrastructure.MangoService.Staff staff)
        {
            if (staff != null)
            {
                Image = LoadCompanyLogo(staff.Company.Name);
            }

            LoggedInUser = staff;
            Utility.LoggedInUser = staff;
            Period = Utility.Period;

            if (staff != null)
            {
                LoginStatus = LOGOUT;
                IsUserLoggedIn = true;
            }
            else
            {
                IsUserLoggedIn = false;
            }

            RaiseButtonCanExecuteEvent();
        }

        //public void OnUserLogin(Staff staff)
        //{
        //    LoggedInUser = staff;
        //    Utility.LoggedInUser = staff;

        //    if (staff != null)
        //    {
        //        LoginStatus = LOGOUT;
        //        IsUserLoggedIn = true;
        //    }
        //    else
        //    {
        //        IsUserLoggedIn = false;
        //    }
        //    RaiseButtonCanExecuteEvent();
        //}

        private void OnLogOutLinkButtonCommand()
        {
            HomeMenuView menuView = container.Resolve<HomeMenuView>();
            LoginView view = container.Resolve<LoginView>();

            LoggedInUser = null;
            LoginStatus = LOGIN;
            IsUserLoggedIn = false;
            currentView = null;

            ResetLoggedInUser();

            Period = new Period();

            ChangeMenu(menuView);
            ChangeContent(view);

            RaiseButtonCanExecuteEvent();
        }

        private void RaiseButtonCanExecuteEvent()
        {
            AppraisalCommand.RaiseCanExecuteChanged();
            UsersCommand.RaiseCanExecuteChanged();
            ReportCommand.RaiseCanExecuteChanged();
            SetupCommand.RaiseCanExecuteChanged();
        }

        private void OnHomeCommand()
        {
            HomeView view = container.Resolve<HomeView>();
            HomeMenuView menuView = container.Resolve<HomeMenuView>();

            ChangeMenu(menuView);
            ChangeContent(view);
        }
        private void OnAppraisalCommand()
        {
            AppraisalView view = container.Resolve<AppraisalView>();
            AppraisalMenuView menuView = container.Resolve<AppraisalMenuView>();

            ChangeMenu(menuView);
            ChangeContent(view);

            eventAggregator.GetEvent<StaffEvent>().Publish(Utility.LoggedInUser);
        }
        private void OnUsersCommand()
        {
            StaffHomeView view = container.Resolve<StaffHomeView>();
            StaffMenuView menuView = container.Resolve<StaffMenuView>();

            ChangeMenu(menuView);
            ChangeContent(view);
        }
        private void OnSetupCommand()
        {
            SetupHomeView view = container.Resolve<SetupHomeView>();
            SetupMenuView menuView = container.Resolve<SetupMenuView>();

            ChangeMenu(menuView);
            ChangeContent(view);
        }
        private void OnReportCommand()
        {
            //ReportView view = container.Resolve<ReportView>();
            //VoteMenuView menuView = container.Resolve<VoteMenuView>();

            //ChangeMenu(menuView);
            //ChangeContent(view);
        }
      
        private void ChangeMenu(UserControl view)
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    if (menuRegion == null)
                    {
                        menuRegion = this.regionManager.Regions[RegionContainer.SubMenu];
                    }

                    if (currentView != null)
                    {
                        if (currentView.ToString() == view.ToString())
                        {
                            return;
                        }
                    }

                    currentView = view;
                    menuRegion.Add(view);
                    menuRegion.Activate(view);
                });
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
                    if (contentRegion == null)
                    {
                        contentRegion = this.regionManager.Regions[RegionContainer.Content];
                    }

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

                    Animation.SwitchToPage();
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private static string GetBaseAddress()
        {
            string strBaseWebAddress = App.Current.Host.Source.AbsoluteUri;
            int PositionOfClientBin = App.Current.Host.Source.AbsoluteUri.ToLower().IndexOf(@"/clientbin");
            return Strings.Left(strBaseWebAddress, PositionOfClientBin);
        }



    }


}
