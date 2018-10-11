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
using Microsoft.Practices.Prism.Commands;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.ViewModelBase;
using Mango.Users.Interfaces;
using Mango.Infrastructure.Models;
using Mango.Infrastructure.Events;
using Mango.Infrastructure.Animation;
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Converters;
using Mango.Infrastructure.Interfaces;
using System.Collections.Generic;
using Mango.Staff.Interfaces;
using Mango.Staff.Views.Popups;
using Mango.Staff.ViewModels;

namespace mobak.Users.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        protected ChildWindow PopUp;
        private Dispatcher dispatcher = Deployment.Current.Dispatcher;

        private IUnityContainer container;
        private IRegionManager regionManager;
        private IEventAggregator eventAggregator;
        private ILoginService loginService;
        private ICurrentPeriodService currentPeriodService;

        private string userName;
        private string password;
        private Staff staff;
        private Period period;
        private bool isProcessing;

        public LoginViewModel(IRegionManager _regionManager, IUnityContainer _container, IEventAggregator _eventAggregator, ILoginService _loginService, ICurrentPeriodService _currentPeriodService)
        {
            container = _container;
            regionManager = _regionManager;
            eventAggregator = _eventAggregator;

            loginService = _loginService;
            currentPeriodService = _currentPeriodService;

            PopUp = new ChangePasswordView();
            PopUp.Closed += new EventHandler(PopUpView_Closed);

            eventAggregator.GetEvent<LogOutEvent>().Subscribe(OnLogOutLinkClicked, ThreadOption.UIThread);
            LoginButtonCommand = new DelegateCommand(OnLoginButtonCommandClick, IsEnabled);
        }

        public LoginDetail LoginDetail { get; set; }
        public DelegateCommand LoginButtonCommand { get; private set; }

        public Period Period
        {
            get { return period; }
            set
            {
                period = value;
                base.OnPropertyChanged("Period");
            }
        }
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                base.OnPropertyChanged("UserName");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                base.OnPropertyChanged("Password");
            }
        }
        public Staff Staff
        {
            get { return staff; }
            set
            {
                staff = value;
                base.OnPropertyChanged("Staff");
            }
        }

        private bool IsEnabled()
        {
            return true;
        }

        public bool IsProcessing
        {
            get { return isProcessing; }
            set
            {
                isProcessing = value;
                base.OnPropertyChanged("IsProcessing");
            }
        }

        public void OnLoginButtonCommandClick()
        {
            try
            {
                if (InvalidLoginDetailsEntered())
                {
                    return;
                }

                IsProcessing = true;
                CurrentPeriodLoadCompleted();
                currentPeriodService.GetCurrentPeriod();

                //UserLoginValidationCompleted();
                //loginService.ValidateUser(UserName, Password);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private bool InvalidLoginDetailsEntered()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName))
                {
                    Utility.DisplayMessage("Please enter your User Name!");
                    return true;
                }
                else if (string.IsNullOrWhiteSpace(Password))
                {
                    Utility.DisplayMessage("Please enter your password!");
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UserLoginValidationCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    //UserLoginValidationCompletedHelper(e);

                    UserLoginValidationCompletedHelper(e);
                    loginService.UserValidationCompleted -= handler;
                };

                loginService.UserValidationCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void UserLoginValidationCompletedHelper(EventArgs e)
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    IsProcessing = false;

                    ValidateStaffCompletedEventArgs args = (ValidateStaffCompletedEventArgs)e;
                    if (args.fault != null)
                    {
                        Utility.DisplayMessage(args.fault.Message);
                        return;
                    }

                    if (loginService.LoggedInUser == null || loginService.LoggedInUser.Id == null)
                    {
                        Utility.DisplayMessage("Invalid user name or password!");
                        return;
                    }
                    else if (Period == null || Period.Id <= 0)
                    {
                        Utility.DisplayMessage("No appraisal period found! Please try again");
                        return;
                    }
                    else if (HasAppraisalPeriodExpired() && !loginService.LoggedInUser.IsAdmin)
                    {
                        return;
                    }
                    else if (loginService.LoggedInUser.IsAdmin == false && loginService.LoggedInUser.IsActive == false)
                    {
                        Utility.DisplayMessage("Your Login Acount is not active! Please contact your HR department");
                        return;
                    }

                    LoginDetail = args.Result;
                    if (LoginDetail != null)
                    {
                        Utility.LoggedInUser = LoginDetail.Staff;
                        Staff = LoginDetail.Staff;

                        if (LoginDetail.IsFirstLogon && Staff.IsAdmin == false)
                        {
                            PopUp.Show();
                        }
                        else
                        {
                            if (UserAccountIsActive(LoginDetail.IsActivated, LoginDetail.IsLocked))
                            {
                                Login();
                            }
                        }

                        //Login();
                    }
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void PopUpView_Closed(object sender, EventArgs e)
        {
            try
            {
                if (PopUp.DialogResult != null)
                {
                    bool result = (PopUp.DialogResult != null) ? Convert.ToBoolean(PopUp.DialogResult) : false;
                    ChangePasswordViewModel popupDialogViewModel = (ChangePasswordViewModel)PopUp.DataContext;

                    if (result)
                    {
                        if (popupDialogViewModel.LoginDetail != null)
                        {
                            if (UserAccountIsActive(popupDialogViewModel.LoginDetail.IsActivated, popupDialogViewModel.LoginDetail.IsLocked))
                            {
                                Staff = popupDialogViewModel.LoginDetail.Staff;
                                Login();
                            }
                            else
                            {
                                Utility.DisplayMessage("Your account is either de-activated or locked! Please contact your system administrator to activate your account.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private bool UserAccountIsActive(bool isActive, bool isLocked)
        {
            try
            {
                if (isActive == true && isLocked == false)
                {
                    return true;
                }

                Utility.DisplayMessage("Your account is either de-activated or locked! Please contact your system administrator to activate or unlock your account");
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Login()
        {
            try
            {
                NavigateToHomePage();
                Utility.LoggedInUser = loginService.LoggedInUser;
                eventAggregator.GetEvent<StaffEvent>().Publish(loginService.LoggedInUser);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private bool HasAppraisalPeriodExpired()
        {
            try
            {
                TimeSpan difference = Period.EndDate - DateTime.Now;
                if (difference.Days < 0)
                {
                    if (difference.Days == -1)
                    {
                        MessageBox.Show("The " + Period.Type.Name + " period has expired on " + Period.EndDate.ToLongDateString() + " ( i.e yesterday )! Contact your HR department", "Appraisal Period Closed", MessageBoxButton.OK);
                    }
                    else if (difference.Days == -2)
                    {
                        MessageBox.Show("The " + Period.Type.Name + " period has expired on " + Period.EndDate.ToLongDateString() + " ( i.e day before yesterday )! Contact your HR department", "Appraisal Period Closed", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("The " + Period.Type.Name + " period has expired on " + Period.EndDate.ToLongDateString() + " ( i.e " + Math.Abs(difference.Days) + " days ago )! Contact your HR department", "Appraisal Period Closed", MessageBoxButton.OK);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void CurrentPeriodLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    CurrentPeriodLoadHelper();
                    currentPeriodService.CurrentPeriodLoadCompleted -= handler;
                };

                currentPeriodService.CurrentPeriodLoadCompleted += handler;
            }
            catch (Exception ex)
            {
               Utility.DisplayMessage(ex.Message);
            }
        }

        protected void CurrentPeriodLoadHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    Period = currentPeriodService.Period;
                    Utility.Period = currentPeriodService.Period;

                    if (Period != null)
                    {
                        //ValidateUser();

                        UserLoginValidationCompleted();
                        loginService.ValidateUser(UserName, Password);
                    }
                    else
                    {
                        IsProcessing = false;
                        Utility.DisplayMessage("No appraisal Period found! Contact your HR department");
                    }
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void NavigateToHomePage()
        {
            try
            {
                Uri homeViewUri = new Uri("HomeView", UriKind.Relative);
                regionManager.RequestNavigate("ContentRegion", homeViewUri);

                //eventAggregator.GetEvent<StaffEvent>(staffDictionary).Publish(homeViewUri.ToString());
                Animation.SwitchToPage();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        public void OnLogOutLinkClicked(string logOut)
        {
            try
            {
                Uri loginViewUri = new Uri("LoginView", UriKind.Relative);
                regionManager.RequestNavigate("ContentRegion", loginViewUri);

                Uri staffMenuViewUri = new Uri("StaffMenuView", UriKind.Relative);
                regionManager.RequestNavigate("MenuRegion", staffMenuViewUri);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void ShowMenuView()
        {
            try
            {
                Uri menuViewUri = new Uri("AppraisalMenuView", UriKind.Relative);
                regionManager.RequestNavigate("MenuRegion", menuViewUri);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void ShowAppraisalView()
        {
            try
            {
                Uri appraisalViewUri = new Uri("AppraisalView", UriKind.Relative);
                regionManager.RequestNavigate("ContentRegion", appraisalViewUri);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }



    }

}
