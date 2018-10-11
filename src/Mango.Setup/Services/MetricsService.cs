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
using Mango.Setup.Interfaces;

namespace Mango.Setup.Services
{
    public class MetricsService : IMetricBaseService<Metrics>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetModelsCompleted;

        //public bool RemoveAssociatedRatings { get; set; }
        public Fault Fault { get; set; }
        public ObservableCollection<Metrics> Models { get; set; }
        public bool Done { get; set; }

        public void LoadByPeriod(Period period)
        {
            try
            {
                service = new ServiceClient();
                service.GetMetricesByPeriodCompleted += new EventHandler<GetMetricesByPeriodCompletedEventArgs>(service_GetMetricesByPeriodCompleted);
                service.GetMetricesByPeriodAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public void Save(ObservableCollection<Metrics> models)
        {
            try
            {
                service = new ServiceClient();
                service.AddMetricesCompleted += new EventHandler<AddMetricesCompletedEventArgs>(service_AddMetricesCompleted);
                service.AddMetricesAsync(models);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ModifyAndRemoveAssociatedRatings(ObservableCollection<Metrics> models)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyMetricsCompleted += new EventHandler<ModifyMetricsCompletedEventArgs>(service_ModifyMetricsCompleted);
                service.ModifyMetricsAsync(models, true);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public void Modify(ObservableCollection<Metrics> models)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyMetricsCompleted += new EventHandler<ModifyMetricsCompletedEventArgs>(service_ModifyMetricsCompleted);
                service.ModifyMetricsAsync(models, false);
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
        public void RemoveBy(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, bool removeMetrics)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveMetricsByCompanyDepartmentJobRoleAndPeriodCompleted += new EventHandler<RemoveMetricsByCompanyDepartmentJobRoleAndPeriodCompletedEventArgs>(service_RemoveMetricsByCompanyDepartmentJobRoleAndPeriodCompleted);
                service.RemoveMetricsByCompanyDepartmentJobRoleAndPeriodAsync(companyDepartmentJobRole, period, removeMetrics);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RemoveBy(Metrics metrics)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveMetricAndAssociatedRatingsCompleted += new EventHandler<RemoveMetricAndAssociatedRatingsCompletedEventArgs>(service_RemoveMetricAndAssociatedRatingsCompleted);
                service.RemoveMetricAndAssociatedRatingsAsync(metrics);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_GetMetricesByPeriodCompleted(object sender, GetMetricesByPeriodCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Models = e.Result;

                if (GetModelsCompleted != null)
                {
                    GetModelsCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_AddMetricesCompleted(object sender, AddMetricesCompletedEventArgs e)
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
        private void service_ModifyMetricsCompleted(object sender, ModifyMetricsCompletedEventArgs e)
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
        private void service_RemoveMetricsByCompanyDepartmentJobRoleAndPeriodCompleted(object sender, RemoveMetricsByCompanyDepartmentJobRoleAndPeriodCompletedEventArgs e)
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
        private void service_GetMetricesByCompanyDepartmetJobRoleAndPeriodCompleted(object sender, GetMetricesByCompanyDepartmetJobRoleAndPeriodCompletedEventArgs e)
        {
            try
            {
                Models = e.Result;
                Fault = e.fault;

                if (GetModelsCompleted != null)
                {
                    GetModelsCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void service_RemoveMetricAndAssociatedRatingsCompleted(object sender, RemoveMetricAndAssociatedRatingsCompletedEventArgs e)
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
