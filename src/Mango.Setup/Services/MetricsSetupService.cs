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
using Mango.Infrastructure.Models;
using Mango.Setup.Interfaces;

namespace Mango.Setup.Services
{
    public class MetricsSetupService : IMetricsSetupService
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;
        public event EventHandler GetModelsByCdjrAndPeriodCompleted;
        public event EventHandler GetAllMetricesByPeriodAndPerspectiveCompleted;

        public event EventHandler ModifyNpsCompleted;
        public event EventHandler LoadNpsCompleted;

        public ObservableCollection<Metrics> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                //Period currentPeriod = Utility.Period;
                if (Utility.Period == null)
                {
                    throw new Exception("No current period found!");
                }

                service = new ServiceClient();
                service.GetAllMetricesByPeriodCompleted += new EventHandler<GetAllMetricesByPeriodCompletedEventArgs>(service_GetAllMetricesByPeriodCompleted);
                service.GetAllMetricesByPeriodAsync(Utility.Period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void GetAllNpsByPeriod(Period period)
        {
            try
            {
                service = new ServiceClient();
                service.GetAllNpsByPeriodCompleted += new EventHandler<GetAllNpsByPeriodCompletedEventArgs>(service_GetAllNpsByPeriodCompleted);
                service.GetAllNpsByPeriodAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void GetAllMetricsByPeriodAndPerspective(Period period, MetricsPerspective perspective)
        {
            try
            {
                service = new ServiceClient();
                service.GetAllMetricesByPeriodAndPerspectiveCompleted += new EventHandler<GetAllMetricesByPeriodAndPerspectiveCompletedEventArgs>(service_GetAllMetricesByPeriodAndPerspectiveCompleted);
                service.GetAllMetricesByPeriodAndPerspectiveAsync(period, perspective);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadByCompanyDepartmentJobRoleAndPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period)
        {
            try
            {

                service = new ServiceClient();
                service.GetMetricesByCompanyDepartmetJobRoleAndPeriodCompleted += new EventHandler<GetMetricesByCompanyDepartmetJobRoleAndPeriodCompletedEventArgs>(service_GetMetricesByCompanyDepartmetJobRoleAndPeriodCompleted);
                service.GetMetricesByCompanyDepartmetJobRoleAndPeriodAsync(companyDepartmentJobRole, period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public void Save(Metrics metrics)
        //{
        //    try
        //    {
        //        service = new ServiceClient();
        //        service.AddMetricsCompleted += new EventHandler<AddMetricsCompletedEventArgs>(service_AddMetricsCompleted);
        //        service.AddMetricsAsync(metrics);
        //        service.CloseAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public void Modify(Metrics metrics)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyMetricCompleted += new EventHandler<ModifyMetricCompletedEventArgs>(service_ModifyMetricCompleted);
                service.ModifyMetricAsync(metrics);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ModifyNps(ObservableCollection<Metrics> models)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyNpsCompleted += new EventHandler<ModifyNpsCompletedEventArgs>(service_ModifyNpsCompleted);
                service.ModifyNpsAsync(models);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public void Remove(Metrics metrics)
        //{
        //    //try
        //    //{
        //    //    service = new ServiceClient();
        //    //    service.RemoveMetricsCompleted += new EventHandler<RemoveMetricsCompletedEventArgs>(service_RemoveMetricsCompleted);
        //    //    service.RemoveMetricsAsync(metrics);
        //    //    service.CloseAsync();
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    throw;
        //    //}
        //}

        //private void service_RemoveCompanyCompleted(object sender, RemoveCompanyCompletedEventArgs e)
        //{
        //    try
        //    {
        //        Fault = e.fault;
        //        Done = e.Result;

        //        if (ActionCompleted != null)
        //        {
        //            ActionCompleted(this, e);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //private void service_AddMetricsCompleted(object sender, AddMetricsCompletedEventArgs e)
        //{
        //    try
        //    {
        //        Done = e.Result;
        //        Fault = e.fault;

        //        if (ActionCompleted != null)
        //        {
        //            ActionCompleted(this, e);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        private void service_ModifyMetricCompleted(object sender, ModifyMetricCompletedEventArgs e)
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
        private void service_GetAllMetricesByPeriodCompleted(object sender, GetAllMetricesByPeriodCompletedEventArgs e)
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
        private void service_GetAllMetricesByPeriodAndPerspectiveCompleted(object sender, GetAllMetricesByPeriodAndPerspectiveCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Models = e.Result;

                if (GetAllMetricesByPeriodAndPerspectiveCompleted != null)
                {
                    GetAllMetricesByPeriodAndPerspectiveCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_GetMetricesByCompanyDepartmetJobRoleAndPeriodCompleted(object sender, GetMetricesByCompanyDepartmetJobRoleAndPeriodCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Models = e.Result;

                if (GetModelsByCdjrAndPeriodCompleted != null)
                {
                    GetModelsByCdjrAndPeriodCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_ModifyNpsCompleted(object sender, ModifyNpsCompletedEventArgs e)
        {
            try
            {
                Done = e.Result;
                Fault = e.fault;

                if (ModifyNpsCompleted != null)
                {
                    ModifyNpsCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_GetAllNpsByPeriodCompleted(object sender, GetAllNpsByPeriodCompletedEventArgs e)
        {
            try
            {
                Models = e.Result;
                Fault = e.fault;

                if (LoadNpsCompleted != null)
                {
                    LoadNpsCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }





}
