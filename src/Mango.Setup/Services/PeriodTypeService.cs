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
using Mango.Infrastructure.Interfaces;
using System.Collections.ObjectModel;

namespace Mango.Setup.Services
{
    public class PeriodTypeService : ISetupService<PeriodType>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<PeriodType> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllPeriodTypesCompleted += new EventHandler<GetAllPeriodTypesCompletedEventArgs>(service_GetAllPeriodTypesCompleted);
                service.GetAllPeriodTypesAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(PeriodType periodType)
        {
            try
            {
                service = new ServiceClient();
                service.AddPeriodTypeCompleted += new EventHandler<AddPeriodTypeCompletedEventArgs>(service_AddPeriodTypeCompleted);
                service.AddPeriodTypeAsync(periodType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(PeriodType periodType)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyPeriodTypeCompleted += new EventHandler<ModifyPeriodTypeCompletedEventArgs>(service_ModifyPeriodTypeCompleted);
                service.ModifyPeriodTypeAsync(periodType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(PeriodType periodType)
        {
            try
            {
                service = new ServiceClient();
                service.RemovePeriodTypeCompleted += new EventHandler<RemovePeriodTypeCompletedEventArgs>(service_RemovePeriodTypeCompleted);
                service.RemovePeriodTypeAsync(periodType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemovePeriodTypeCompleted(object sender, RemovePeriodTypeCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
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
        private void service_AddPeriodTypeCompleted(object sender, AddPeriodTypeCompletedEventArgs e)
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
        private void service_ModifyPeriodTypeCompleted(object sender, ModifyPeriodTypeCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
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
        private void service_GetAllPeriodTypesCompleted(object sender, GetAllPeriodTypesCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
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



    }
}
