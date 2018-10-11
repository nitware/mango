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
    public class JobRoleSupervisorService : IMetricBaseService<JobRoleSupervisor>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetModelsCompleted;
        //public event EventHandler GetAllModelsCompleted;
        public event EventHandler GetAllSupervisorJobRolesByPeriodCompleted;

        public Fault Fault { get; set; }
        public ObservableCollection<JobRoleSupervisor> Models { get; set; }
        public ObservableCollection<JobRoleSupervisor> JobRoleSupervisors { get; set; }
        public bool Done { get; set; }

        public void Save(ObservableCollection<JobRoleSupervisor> models)
        {
            try
            {
                service = new ServiceClient();
                service.AddJobRoleSupervisorsCompleted += new EventHandler<AddJobRoleSupervisorsCompletedEventArgs>(service_AddJobRoleSupervisorsCompleted);
                service.AddJobRoleSupervisorsAsync(models);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(ObservableCollection<JobRoleSupervisor> models)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyJobRoleSupervisorsCompleted += new EventHandler<ModifyJobRoleSupervisorsCompletedEventArgs>(service_ModifyJobRoleSupervisorsCompleted);
                service.ModifyJobRoleSupervisorsAsync(models);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LoadJobRolesUnderSupervisorByPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period)
        {
            try
            {
                service = new ServiceClient();
                service.GetJobRolesUnderSupervisorByPeriodCompleted += new EventHandler<GetJobRolesUnderSupervisorByPeriodCompletedEventArgs>(service_GetJobRolesUnderSupervisorByPeriodCompleted);
                service.GetJobRolesUnderSupervisorByPeriodAsync(companyDepartmentJobRole, period);
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
                service.GetAllSupervisorJobRolesByPeriodCompleted += new EventHandler<GetAllSupervisorJobRolesByPeriodCompletedEventArgs>(service_GetAllSupervisorJobRolesByPeriodCompleted);
                service.GetAllSupervisorJobRolesByPeriodAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RemoveBySupervisorJobRole(CompanyDepartmentJobRole companyDepartmentJobRole, Period period)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveJobRoleSupervisorBySupervisorCompanyDepartmentJobRoleCompleted += new EventHandler<RemoveJobRoleSupervisorBySupervisorCompanyDepartmentJobRoleCompletedEventArgs>(service_RemoveJobRoleSupervisorBySupervisorCompanyDepartmentJobRoleCompleted);
                service.RemoveJobRoleSupervisorBySupervisorCompanyDepartmentJobRoleAsync(companyDepartmentJobRole, period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveJobRoleSupervisorBySupervisorCompanyDepartmentJobRoleCompleted(object sender, RemoveJobRoleSupervisorBySupervisorCompanyDepartmentJobRoleCompletedEventArgs e)
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
       
        private void service_GetAllSupervisorJobRolesByPeriodCompleted(object sender, GetAllSupervisorJobRolesByPeriodCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                JobRoleSupervisors = e.Result;

                if (GetAllSupervisorJobRolesByPeriodCompleted != null)
                {
                    GetAllSupervisorJobRolesByPeriodCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_AddJobRoleSupervisorsCompleted(object sender, AddJobRoleSupervisorsCompletedEventArgs e)
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
        private void service_ModifyJobRoleSupervisorsCompleted(object sender, ModifyJobRoleSupervisorsCompletedEventArgs e)
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
        private void service_GetJobRolesUnderSupervisorByPeriodCompleted(object sender, GetJobRolesUnderSupervisorByPeriodCompletedEventArgs e)
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
