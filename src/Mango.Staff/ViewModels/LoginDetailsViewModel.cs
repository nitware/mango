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
using System.ComponentModel;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.ViewModelBase;
using Mango.Staff.Interfaces;
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Models;

namespace Mango.Staff.ViewModel
{
    public class LoginDetailsViewModel : ViewModelBase
    {
        private Dispatcher dispatcher;

        private ILoginDetailService service;

        private ICollectionView loginDetails;
        private LoginDetail loginDetail;

        private bool canSaveItem;
        private bool canResetItem;
        private bool canClearView;
        private string recordCount;

        public LoginDetailsViewModel(ILoginDetailService _service, IEventAggregator eventAggregator)
        {
            dispatcher = Deployment.Current.Dispatcher; 
            service = _service;

            SaveCommand = new DelegateCommand(OnSaveCommand, CanSave);
            ClearCommand = new DelegateCommand(OnClearCommand, CanClear);
            ResetCommand = new DelegateCommand(OnResetCommand, CanReset);

            LoadAllLoginDetail();

            //eventAggregator.GetEvent<SetupPersonEvent>().Subscribe(OnInitialise);
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand ClearCommand { get; private set; }
        public DelegateCommand ResetCommand { get; private set; }

        public string TabCaption
        {
            get { return "Login Detail"; }
        }
        public string RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                base.OnPropertyChanged("RecordCount");
            }
        }
        public ICollectionView LoginDetails
        {
            get { return loginDetails; }
            set
            {
                loginDetails = value;
                OnPropertyChanged("LoginDetails");
            }
        }
        public LoginDetail LoginDetail
        {
            get { return loginDetail; }
            set
            {
                loginDetail = value;
                OnPropertyChanged("LoginDetail");
            }
        }

        public bool CanSaveItem
        {
            get { return canSaveItem; }
            set
            {
                canSaveItem = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CanResetItem
        {
            get { return canResetItem; }
            set
            {
                canResetItem = value;
                ResetCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CanClearView
        {
            get { return canClearView; }
            set
            {
                canClearView = value;
                ClearCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanSave()
        {
            return CanSaveItem;
        }
        private bool CanClear()
        {
            return CanClearView;
        }
        private bool CanReset()
        {
            return CanResetItem;
        }

        //public void OnInitialise(string refresh)
        //{
        //    LoadAllLoginDetail();
        //}

        private void LoadAllLoginDetail()
        {
            try
            {
                LoadAllLoginDetailCompleted();
                service.LoadAll();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void OnSaveCommand()
        {
            try
            {
                if (LoginDetail == null)
                {
                    Utility.DisplayMessage("No login detail selected from the tray! Please select ");
                    return;
                }

                ActionCompleted();
                service.Modify(LoginDetail);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        protected void OnClearCommand()
        {
            LoginDetail = new LoginDetail();
        }
        protected void OnResetCommand()
        {
            try
            {
                if (LoginDetail == null)
                {
                    Utility.DisplayMessage("No login detail selected from the tray! Please select ");
                    return;
                }

                ActionCompleted();
                service.Reset(LoginDetail);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllLoginDetailCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllLoginDetailCompletedHelper();
                    service.GetAllLoginDetailsCompleted -= handler;
                };

                service.GetAllLoginDetailsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllLoginDetailCompletedHelper()
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

                               LoginDetails = new PagedCollectionView(service.LoginDetails);
                               if (service.LoginDetails != null && service.LoginDetails.Count > 0)
                               {
                                   RecordCount = "Record Count: " + service.LoginDetails.Count;

                                   LoginDetails.MoveCurrentTo(null);
                                   LoginDetails.CurrentChanged += (s, e) =>
                                   {
                                       LoginDetail = LoginDetails.CurrentItem as LoginDetail;

                                       CanSaveItem = true;
                                       CanResetItem = true;
                                       CanClearView = true;
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void ActionCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    ActionCompletedHelper();
                    service.ActionCompleted -= handler;
                };

                service.ActionCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void ActionCompletedHelper()
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

                              if (service.Done)
                              {
                                  OnClearCommand();
                                  LoadAllLoginDetail();

                                  CanSaveItem = false;
                                  CanResetItem = false;
                                  CanClearView = false;

                                  Utility.DisplayMessage("Password has been successfully reset.");

                                  //Utility.DisplayMessage(LoginDetail.Staff.LoginName + " has been successfully reset.");
                              }
                              else
                              {
                                  Utility.DisplayMessage("Password reset operation failed!");
                                  //Utility.DisplayMessage(LoginDetail.Staff.LoginName + " reset operation failed!");
                              }
                          });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }


    }



}
