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
    public class CompanyDepartmentJobRoleService : ISetupService<CompanyDepartmentJobRole>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<CompanyDepartmentJobRole> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllCompanyDepartmentJobRolesCompleted += new EventHandler<GetAllCompanyDepartmentJobRolesCompletedEventArgs>(service_GetAllCompanyDepartmentJobRolesCompleted);
                service.GetAllCompanyDepartmentJobRolesAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(CompanyDepartmentJobRole companyDepartmentJobRole)
        {
            try
            {
                service = new ServiceClient();
                service.AddCompanyDepartmentJobRoleCompleted += new EventHandler<AddCompanyDepartmentJobRoleCompletedEventArgs>(service_AddCompanyDepartmentJobRoleCompleted);
                service.AddCompanyDepartmentJobRoleAsync(companyDepartmentJobRole);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(CompanyDepartmentJobRole companyDepartmentJobRole)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyCompanyDepartmentJobRoleCompleted += new EventHandler<ModifyCompanyDepartmentJobRoleCompletedEventArgs>(service_ModifyCompanyDepartmentJobRoleCompleted);
                service.ModifyCompanyDepartmentJobRoleAsync(companyDepartmentJobRole);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(CompanyDepartmentJobRole companyDepartmentJobRole)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveCompanyDepartmentJobRoleCompleted += new EventHandler<RemoveCompanyDepartmentJobRoleCompletedEventArgs>(service_RemoveCompanyDepartmentJobRoleCompleted);
                service.RemoveCompanyDepartmentJobRoleAsync(companyDepartmentJobRole);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveCompanyDepartmentJobRoleCompleted(object sender, RemoveCompanyDepartmentJobRoleCompletedEventArgs e)
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
        private void service_AddCompanyDepartmentJobRoleCompleted(object sender, AddCompanyDepartmentJobRoleCompletedEventArgs e)
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
        private void service_ModifyCompanyDepartmentJobRoleCompleted(object sender, ModifyCompanyDepartmentJobRoleCompletedEventArgs e)
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
        private void service_GetAllCompanyDepartmentJobRolesCompleted(object sender, GetAllCompanyDepartmentJobRolesCompletedEventArgs e)
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
