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

using Mango.Setup.Services;
using Mango.Infrastructure.Models;
using Mango.Infrastructure.MangoService;
using System.Windows.Data;
using Mango.Infrastructure.ViewModelBase;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Threading;
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.Services;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.Events;

namespace Mango.Setup.ViewModels
{
    public class CurrentPeriodViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;

        private Period period;
        private ICollectionView periods;
        private PeriodService service;
        private ICurrentPeriodService currentPeriodService;

        private Dispatcher dispatcher;

        private bool isCurrentPeriodSettable;
        private Period currentPeriod;

        public CurrentPeriodViewModel(IEventAggregator _eventAggregator)
        {
            service = new PeriodService();
            currentPeriodService = new CurrentPeriodService();
            SetCurrentPeriodCommand = new DelegateCommand(OnSetCurrentPeriodCommand, CanSetCurrentPeriod);

            IsCurrentPeriodSettable = false;
            dispatcher = Deployment.Current.Dispatcher;
            eventAggregator = _eventAggregator;

            //LoadAllPeriodCompleted();
            //service.LoadAll();

            //LoadCurrentPeriodCompleted();
            //currentPeriodService.GetCurrentPeriod();

            //eventAggregator = _eventAggregator;
            //_eventAggregator.GetEvent<SetupEvent>().Subscribe(OnInitialise);

            OnInitialise("");
        }

        public void OnInitialise(string refresh)
        {
            LoadAllPeriodCompleted();
            service.LoadAll();

            LoadCurrentPeriodCompleted();
            currentPeriodService.GetCurrentPeriod();
        }

        public string TabCaption
        {
            //get { return "Current Period"; }
            get { return "Current"; }
        }

        public DelegateCommand SetCurrentPeriodCommand { get; private set; }
        
        public ICollectionView Periods
        {
            get { return periods; }
            set
            {
                periods = value;
                base.OnPropertyChanged("Periods");
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
        public Period CurrentPeriod
        {
            get { return currentPeriod; }
            set
            {
                currentPeriod = value;
                base.OnPropertyChanged("CurrentPeriod");
            }
        }
        public bool IsCurrentPeriodSettable
        {
            get { return isCurrentPeriodSettable; }
            set
            {
                isCurrentPeriodSettable = value;
                SetCurrentPeriodCommand.RaiseCanExecuteChanged();
            }
        }
        private bool CanSetCurrentPeriod()
        {
            return IsCurrentPeriodSettable;
        }

        private void OnSetCurrentPeriodCommand()
        {
            try
            {
                if (Period == null || Period.Id == 0)
                {
                    Utility.DisplayMessage("Please specify period!");
                    return;
                }

                //CurrentPeriod = Period;

                CurrentPeriod currentPeriod = new CurrentPeriod();
                currentPeriod.Period = Period;

                SetCurrentPeriodCompleted();
                currentPeriodService.SetCurrent(currentPeriod);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllPeriodCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllPeriodCompletedHelper();
                    service.GetAllModelsCompleted -= handler;
                };

                service.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllPeriodCompletedHelper()
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

                               if (service.Models != null && service.Models.Count > 0)
                               {
                                   service.Models.Insert(0, new Period() { Id = 0, Name = "<< Select Period >>" });

                                   Periods = new PagedCollectionView(service.Models);
                                   Periods.MoveCurrentToFirst();
                                   Periods.CurrentChanged += (s, e) =>
                                   {
                                       Period = Periods.CurrentItem as Period;
                                       if (Period != null && Period.Id > 0)
                                       {
                                           IsCurrentPeriodSettable = true;
                                       }
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadCurrentPeriodCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadCurrentPeriodCompletedCompletedHelper();
                    currentPeriodService.CurrentPeriodLoadCompleted -= handler;
                };

                currentPeriodService.CurrentPeriodLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadCurrentPeriodCompletedCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               //if (Utility.FaultExist(currentPeriodService.Fault))
                               //{
                               //    return;
                               //}

                               CurrentPeriod = currentPeriodService.Period;

                               //LoadDocumentTypesCompleted();
                               //documentTypeService.LoadAll();

                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void SetCurrentPeriodCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    SetCurrentPeriodCompletedCompletedHelper();
                    currentPeriodService.ActionCompleted -= handler;
                };

                currentPeriodService.ActionCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void SetCurrentPeriodCompletedCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(currentPeriodService.Fault))
                               {
                                   return;
                               }

                               if (currentPeriodService.Done)
                               {
                                   Utility.DisplayMessage("Current Period has been successfully set");
                                   if (Period != null)
                                   {
                                       CurrentPeriod = Period;
                                       Utility.Period = Period;
                                   }
                               }
                               else
                               {
                                   Utility.DisplayMessage("Current period was not set successfully");
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
