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

using System.Collections.ObjectModel;
using Mango.Infrastructure.MangoService;
using Mango.Setup.Interfaces;
using Mango.Infrastructure.Interfaces;

namespace Mango.Setup.Services
{
    public class PeriodService : ISetupService<Period> //: IPeriodSetupService
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public Fault Fault { get; set; }
        public ObservableCollection<Period> Models { get; set; }
        public bool Done { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllPeriodsCompleted += new EventHandler<GetAllPeriodsCompletedEventArgs>(service_GetAllPeriodsCompleted);
                service.GetAllPeriodsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save(Period period)
        {
            try
            {
                service = new ServiceClient();
                service.CreateNewPeriodCompleted += new EventHandler<CreateNewPeriodCompletedEventArgs>(service_CreateNewPeriodCompleted);
                service.CreateNewPeriodAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(Period period)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyPeriodCompleted += new EventHandler<ModifyPeriodCompletedEventArgs>(service_ModifyPeriodCompleted);
                service.ModifyPeriodAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(Period period)
        {
            try
            {
                service = new ServiceClient();
                service.RemovePeriodCompleted += new EventHandler<RemovePeriodCompletedEventArgs>(service_RemovePeriodCompleted);
                service.RemovePeriodAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
       

        private void service_GetAllPeriodsCompleted(object sender, GetAllPeriodsCompletedEventArgs e)
        {
            try
            {
                Models = e.Result;
                               
                if (GetAllModelsCompleted != null)
                {
                    GetAllModelsCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_CreateNewPeriodCompleted(object sender, CreateNewPeriodCompletedEventArgs e)
        {
            try
            {
                Done = e.Result;

                if (ActionCompleted != null)
                {
                    ActionCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void service_ModifyPeriodCompleted(object sender, ModifyPeriodCompletedEventArgs e)
        {
            try
            {
                Done = e.Result;
                Fault = e.fault;

                if (ActionCompleted != null)
                {
                    ActionCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_RemovePeriodCompleted(object sender, RemovePeriodCompletedEventArgs e)
        {
            try
            {
                Done = e.Result;
                Fault = e.fault;

                if (ActionCompleted != null)
                {
                    ActionCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
