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
using Mango.Setup.Interfaces;
using System.Collections.ObjectModel;

namespace Mango.Setup.Services
{
    public class CompanyDepartmentService : ICompanyDepartmentService
    {
        private ServiceClient service;

        public event EventHandler GetCompanyDepartmentsByCompanyCompleted;

        public ObservableCollection<CompanyDepartment> CompanyDepartments { get; set; }
        public Fault Fault { get; set; }

        public void LoadBy(Company company)
        {
            try
            {
                service = new ServiceClient();
                service.GetCompanyDepartmentByCompanyCompleted += new EventHandler<GetCompanyDepartmentByCompanyCompletedEventArgs>(service_GetCompanyDepartmentByCompanyCompleted);
                service.GetCompanyDepartmentByCompanyAsync(company);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_GetCompanyDepartmentByCompanyCompleted(object sender, GetCompanyDepartmentByCompanyCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                CompanyDepartments = e.Result;

                if (GetCompanyDepartmentsByCompanyCompleted != null)
                {
                    GetCompanyDepartmentsByCompanyCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




    }



}
