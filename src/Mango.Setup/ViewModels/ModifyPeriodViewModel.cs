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

using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.Models;
using Mango.Setup.Interfaces;
using Mango.Setup.Services;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.Events;

namespace Mango.Setup.ViewModels
{
    public class ModifyPeriodViewModel : PeriodViewModelBase
    {
        private string message;
        private const int noOfDays = 14;
        private const string cannotSavePeriod = "New Period cannot be created from this page, but can only be modified. Please select Period from the list to modify.";

        private IStatusService statusService;

        private Status status;
        private ICollectionView allStatus;
        
        public ModifyPeriodViewModel(ISetupService<Period> _service, IStatusService _statusService, IEventAggregator _eventAggregator)
            : base(_service, _eventAggregator)
        {
            modelName = "Period";
            Initialize();

            Model.Span = noOfDays;
            Model.StartDate = DateTime.Now;
            Model.EndDate = DateTime.Now.AddDays(noOfDays);

            statusService = _statusService;
            //LoadAllStatusCompleted();
            //statusService.LoadAll();

            base.addSelector = l => l.Name.Equals(Model.Name, StringComparison.OrdinalIgnoreCase);
            base.modifySelector = l => l.Name.Equals(Model.Name, StringComparison.OrdinalIgnoreCase) && l.Id != Model.Id;

            //_eventAggregator.GetEvent<SetupEvent>().Subscribe(OnInitialise);

            OnInitialise("");
        }

        public void OnInitialise(string refresh)
        {
            LoadAllStatusCompleted();
            statusService.LoadAll();
        }

        public string TabCaption
        {
            get { return "Modify"; }
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                base.OnPropertyChanged("Message");
            }
        }
        public ICollectionView AllStatus
        {
            get { return allStatus; }
            set
            {
                allStatus = value;
                base.OnPropertyChanged("AllStatus");
            }
        }
        public Status Status
        {
            get { return status; }
            set
            {
                status = value;
                base.OnPropertyChanged("Status");
            }
        }
        
        protected override void OnSaveCommand()
        {
            try
            {
                TimeSpan difference = Model.EndDate - Model.StartDate;

               

                //if (base.InvalidEntry(Model.Name))
                //{
                //    return;
                //}
                if (Status == null || Status.Id == 0)
                {
                    Utility.DisplayMessage("Please specify status");
                    return;
                }
                else if (Type == null)
                {
                    Utility.DisplayMessage("Please select type");
                    return;
                }
                if (difference.Days <= 0)
                {
                    MessageBox.Show("Appraisal Start Date cannot be greater than or equal to Appraisal End Date! ", "Appraisal Period", MessageBoxButton.OK);
                    return;
                }

                if (ViewState == Edit.Mode.Editing)
                {
                    ActionCompleted();
                    SetActionMessage(modifySuccessfulMessage, modifyFailedMessage);

                    Model.Type = Type;
                    Model.Year = Year.Id;
                    Model.Name = Year.Name + " " + Type.Name;
                    Model.Span = (byte)difference.Days;
                    Model.Status = Status;
                                       
                    service.Modify(Model);
                }
                else
                {
                    Message = cannotSavePeriod;
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override void OnClearCommand()
        {
            try
            {
                UpdateViewState(Edit.Mode.Adding);
                Model = new Period();
                Model.StartDate = DateTime.Now;
                Model.EndDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        //protected override void OnRemoveCommand()
        //{
        //    try
        //    {
        //        if (ViewState == Edit.Mode.Editing)
        //        {
        //            SetActionMessage(removeSuccessfulMessage, removeFailedMessage);

        //            ActionCompleted();
        //            service.Remove(Model);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}

        protected override void ActionSuccessHelper()
        {
            //LoadAll();
            //OnClearCommand();

            //Utility.DisplayMessage(ActionSuccessfulMessage);
                        
            if (Utility.Period != null && Model != null)
            {
                if (Utility.Period.Id == Model.Id)
                {
                    Utility.Period = Model;
                }
            }

            base.ActionSuccessHelper();
        }

        protected override void SetSelectedModel()
        {
            try
            {
                if (Models != null)
                {
                    Model = Models.CurrentItem as Period;
                    UpdateViewState(Edit.Mode.Editing);
                }

                if (Model != null)
                {
                    if (AllStatus != null)
                    {

                        ObservableCollection<Status> status = (ObservableCollection<Status>)AllStatus.SourceCollection;
                        if (status != null)
                        {
                            Status selectedStatus = status.Where(s => s.Name == Model.Status.Name).SingleOrDefault();
                            if (selectedStatus != null)
                            {
                                AllStatus.MoveCurrentTo(selectedStatus);
                            }
                        }
                    }

                    if (Types != null)
                    {
                        ObservableCollection<PeriodType> periodTypes = (ObservableCollection<PeriodType>)Types.SourceCollection;
                        if (periodTypes != null)
                        {
                            PeriodType selectedType = periodTypes.Where(p => p.Id == Model.Type.Id).SingleOrDefault();
                            if (selectedType != null)
                            {
                                Types.MoveCurrentTo(selectedType);
                            }
                        }
                    }

                    if (Years != null)
                    {
                        ObservableCollection<Value> years = (ObservableCollection<Value>)Years.SourceCollection;
                        if (years != null)
                        {
                            Value selectedYear = years.Where(y => y.Id == Model.Year).SingleOrDefault();
                            if (selectedYear != null)
                            {
                                Years.MoveCurrentTo(selectedYear);
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

        protected void LoadAllStatusCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllStatusCompletedHelper();
                    statusService.GetAllModelsCompleted -= handler;
                };

                statusService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllStatusCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(statusService.Fault))
                               {
                                   return;
                               }

                               if (statusService.Models != null && statusService.Models.Count > 0)
                               {
                                   statusService.Models.Insert(0, new Status() { Name = "<< Select Status >>" });
                                   
                                   AllStatus = new PagedCollectionView(statusService.Models);
                                   AllStatus.MoveCurrentToFirst();
                                   AllStatus.CurrentChanged += (s, e) =>
                                   {
                                       Status = AllStatus.CurrentItem as Status;
                                   };
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
