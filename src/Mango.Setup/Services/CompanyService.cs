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
    public class CompanyService : ISetupService<Company>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<Company> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllCompaniesCompleted += new EventHandler<GetAllCompaniesCompletedEventArgs>(service_GetAllCompaniesCompleted);
                service.GetAllCompaniesAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(Company company)
        {
            try
            {
                service = new ServiceClient();
                service.AddCompanyCompleted += new EventHandler<AddCompanyCompletedEventArgs>(service_AddCompanyCompleted);
                service.AddCompanyAsync(company);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(Company company)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyCompanyCompleted += new EventHandler<ModifyCompanyCompletedEventArgs>(service_ModifyCompanyCompleted);
                service.ModifyCompanyAsync(company);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(Company company)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveCompanyCompleted += new EventHandler<RemoveCompanyCompletedEventArgs>(service_RemoveCompanyCompleted);
                service.RemoveCompanyAsync(company);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveCompanyCompleted(object sender, RemoveCompanyCompletedEventArgs e)
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
        private void service_AddCompanyCompleted(object sender, AddCompanyCompletedEventArgs e)
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
        private void service_ModifyCompanyCompleted(object sender, ModifyCompanyCompletedEventArgs e)
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
        private void service_GetAllCompaniesCompleted(object sender, GetAllCompaniesCompletedEventArgs e)
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
