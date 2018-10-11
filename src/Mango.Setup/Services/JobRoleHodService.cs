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
using System.Collections.ObjectModel;
using Mango.Infrastructure.Interfaces;

namespace Mango.Setup.Services
{
    public class JobRoleHodService : IMetricBaseService<JobRoleHod>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetModelsCompleted;
        public event EventHandler GetAllModelsCompleted;
        public event EventHandler GetAllHodJobRolesByPeriodCompleted;

        public Fault Fault { get; set; }
        public ObservableCollection<JobRoleHod> Models { get; set; }
        public ObservableCollection<JobRoleHod> JobRoleHods { get; set; }
        public bool Done { get; set; }
                
        public void Save(ObservableCollection<JobRoleHod> models)
        {
            try
            {
                service = new ServiceClient();
                service.AddJobRoleHodsCompleted += new EventHandler<AddJobRoleHodsCompletedEventArgs>(service_AddJobRoleHodsCompleted);
                service.AddJobRoleHodsAsync(models);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(ObservableCollection<JobRoleHod> models)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyJobRoleHodsCompleted += new EventHandler<ModifyJobRoleHodsCompletedEventArgs>(service_ModifyJobRoleHodsCompleted);
                service.ModifyJobRoleHodsAsync(models);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LoadJobRolesUnderHodByPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period)
        {
            try
            {
                service = new ServiceClient();
                service.GetJobRolesUnderHodByPeriodCompleted += new EventHandler<GetJobRolesUnderHodByPeriodCompletedEventArgs>(service_GetJobRolesUnderHodByPeriodCompleted);
                service.GetJobRolesUnderHodByPeriodAsync(companyDepartmentJobRole, period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LoadByPeriod(Period period)
        {
            try
            {
                service = new ServiceClient();
                service.GetAllHodJobRolesByPeriodCompleted += new EventHandler<GetAllHodJobRolesByPeriodCompletedEventArgs>(service_GetAllHodJobRolesByPeriodCompleted);
                service.GetAllHodJobRolesByPeriodAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RemoveByHodJobRole(CompanyDepartmentJobRole companyDepartmentJobRole, Period period)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveJobRoleHodByHodCompanyDepartmentJobRoleCompleted += new EventHandler<RemoveJobRoleHodByHodCompanyDepartmentJobRoleCompletedEventArgs>(service_RemoveJobRoleHodByHodCompanyDepartmentJobRoleCompleted);
                service.RemoveJobRoleHodByHodCompanyDepartmentJobRoleAsync(companyDepartmentJobRole, period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveJobRoleHodByHodCompanyDepartmentJobRoleCompleted(object sender, RemoveJobRoleHodByHodCompanyDepartmentJobRoleCompletedEventArgs e)
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
        private void service_GetAllJobRoleHodsCompleted(object sender, GetAllJobRoleHodsCompletedEventArgs e)
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
        private void service_GetAllHodJobRolesByPeriodCompleted(object sender, GetAllHodJobRolesByPeriodCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                JobRoleHods = e.Result;

                if (GetAllHodJobRolesByPeriodCompleted != null)
                {
                    GetAllHodJobRolesByPeriodCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_AddJobRoleHodsCompleted(object sender, AddJobRoleHodsCompletedEventArgs e)
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
        private void service_ModifyJobRoleHodsCompleted(object sender, ModifyJobRoleHodsCompletedEventArgs e)
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
        private void service_GetJobRolesUnderHodByPeriodCompleted(object sender, GetJobRolesUnderHodByPeriodCompletedEventArgs e)
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




    }


}
