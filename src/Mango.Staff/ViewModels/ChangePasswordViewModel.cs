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

using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Models;
using Mango.Staff.Interfaces;
using Mango.Infrastructure.MangoService;
using Mango.Staff.Services;

namespace Mango.Staff.ViewModels
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        private ChildWindow PopUp;

        //private Window PopUp;
        private Dispatcher dispatcher;

        private ILoginDetailService service;

        private string newPassword;
        private string confirmNewPassword;
        private LoginDetail loginDetail;

        public ChangePasswordViewModel()
        {
            service = new LoginDetailService();
            dispatcher = Deployment.Current.Dispatcher;
            IEventAggregator eventAggregator = new EventAggregator();
          
            OkCommand = new DelegateCommand(OnOkCommand);
            CancelCommand = new DelegateCommand(OnCancelCommand);
            SetPopUpCommand = new DelegateCommand<object>(OnSetPopUpCommand);
        }

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand OkCommand { get; private set; }
        public ICommand SetPopUpCommand { get; private set; }
              
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                base.OnPropertyChanged("NewPassword");
            }
        }
        public string ConfirmNewPassword
        {
            get { return confirmNewPassword; }
            set
            {
                confirmNewPassword = value;
                base.OnPropertyChanged("ConfirmNewPassword");
            }
        }
        public LoginDetail LoginDetail
        {
            get { return loginDetail; }
            set
            {
                loginDetail = value;
                base.OnPropertyChanged("LoginDetail");
            }
        }

        private void OnOkCommand()
        {
            try
            {
                if (InvalidEntry())
                {
                    return;
                }

                ChangePasswordCompleted();
                service.ChangePassword(Utility.LoggedInUser, NewPassword);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private bool InvalidEntry()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewPassword))
                {
                    Utility.DisplayMessage("New password cannot be empty!");
                    return true;
                }
                else if (string.IsNullOrWhiteSpace(ConfirmNewPassword))
                {
                    Utility.DisplayMessage("Confirm new password cannot be empty!");
                    return true;
                }
                else if (NewPassword != ConfirmNewPassword)
                {
                    Utility.DisplayMessage("New password is not equal to confirm new password!");
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void OnCancelCommand()
        {
            PopUp.DialogResult = false;
        }

        public void OnSetPopUpCommand(object param)
        {
            //PopUp = (Window)param;
            PopUp = (ChildWindow)param;
        }
       
        private void ChangePasswordCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    ChangePasswordCompletedHelper();
                    service.ChangePasswordCompleted -= handler;
                };

                service.ChangePasswordCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage("Error occured! " + ex.Message);
            }
        }

        private void ChangePasswordCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                          (() =>
                          {
                              if (Utility.FaultExist(service.Fault))
                              {
                                  return;
                              }

                              if (service.LoginDetail != null)
                              {
                                  LoginDetail = service.LoginDetail;
                              }

                              PopUp.DialogResult = true;
                          });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            } 
        }




    }
}
