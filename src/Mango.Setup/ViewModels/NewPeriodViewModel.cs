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
using System.ComponentModel;
using System.Windows.Data;
using Mango.Setup.Services;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;

namespace Mango.Setup.ViewModels
{
    public class NewPeriodViewModel : PeriodViewModelBase
    {
        private bool isBusy;
        private string message;
        private const int noOfDays = 14;
        private const string cannotSavePeriod = "Period cannot be saved from this page, Please use the Modify Period tab.";      

        public NewPeriodViewModel(ISetupService<Period> _service, IEventAggregator _eventAggregator)
            : base(_service, _eventAggregator)
        {
            modelName = "Period";
            Initialize();

            Model.Span = noOfDays;
            Model.StartDate = DateTime.Now;
            Model.EndDate = DateTime.Now.AddDays(noOfDays);
            Message = string.Empty;

            //base.addSelector = l => l.Name.Equals(Model.Name, StringComparison.OrdinalIgnoreCase);
            //base.modifySelector = p => p.Name.Equals(Model.Name, StringComparison.OrdinalIgnoreCase) && p.Id != Model.Id;

            base.addSelector = p => p.Type.Id == Model.Type.Id && p.Year == Model.Year;
            base.modifySelector = p => p.Type.Id == Model.Type.Id && p.Year == Model.Year && p.Id != Model.Id;
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                base.OnPropertyChanged("IsBusy");
            }
        }
        public string TabCaption
        {
            //get { return "New Period"; }
            get { return "New"; }
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
        
        
        protected override void OnSaveCommand()
        {
            try
            {
                TimeSpan difference = Model.EndDate - Model.StartDate;

                if (Year == null || Year.Id == 0)
                {
                    Utility.DisplayMessage("Please select year!");
                    return;
                }
                else if (Type == null || Type.Id == 0)
                {
                    Utility.DisplayMessage("Please select type!");
                    return;
                }
                
                if (difference.Days <= 0)
                {
                    MessageBox.Show("Appraisal Start Date cannot be greater than or equal to Appraisal End Date! ", "Appraisal Period", MessageBoxButton.OK);
                    return;
                }

                Model.Type = Type;
                Model.Year = Year.Id;
                Model.Name = Model.Year + " " + Model.Type.Name;
                Model.Span = (byte)difference.Days;
                
                //Model.StartDate = DateTime.Now;
                //Model.EndDate = DateTime.Now.AddDays(noOfDays);


               
               

               




                if (base.InvalidEntry(Model.Name))
                {
                    return;
                }

                IsBusy = true;

                if (ViewState != Edit.Mode.Editing)
                {
                    ActionCompleted();
                    SetActionMessage(saveSuccessfulMessage, saveFailedMessage);
                    
                    service.Save(Model);
                }
                else
                {
                    Message = cannotSavePeriod;
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override void ActionCommandCompletedHelper()
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

                              IsBusy = false;

                              if (service.Done)
                              {
                                  ActionSuccessHelper();
                              }
                              else
                              {
                                  Utility.DisplayMessage(ActionFailedMessage);
                              }
                          });
            }
            catch (Exception ex)
            {
                IsBusy = false;
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override void SetSelectedModel()
        {
            try
            {
                //Model = Models.CurrentItem as T;
                //UpdateViewState(Edit.Mode.Editing);

                Message = cannotSavePeriod;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        
        
        //protected virtual void OnSaveCommand()
        //{
        //    try
        //    {
        //        ActionCompleted();

        //        if (ViewState == Edit.Mode.Editing)
        //        {
        //            SetActionMessage(modifySuccessfulMessage, modifyFailedMessage);
        //            service.Modify(Model);
        //        }
        //        else
        //        {
        //            SetActionMessage(saveSuccessfulMessage, saveFailedMessage);
        //            service.Save(Model);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}




    }



}
