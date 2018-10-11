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
    public class DepartmentService : ISetupService<Department>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<Department> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllDepartmentsCompleted += new EventHandler<GetAllDepartmentsCompletedEventArgs>(service_GetAllDepartmentsCompleted);
                service.GetAllDepartmentsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(Department department)
        {
            try
            {
                service = new ServiceClient();
                service.AddDepartmentCompleted += new EventHandler<AddDepartmentCompletedEventArgs>(service_AddDepartmentCompleted);
                service.AddDepartmentAsync(department);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(Department department)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyDepartmentCompleted += new EventHandler<ModifyDepartmentCompletedEventArgs>(service_ModifyDepartmentCompleted);
                service.ModifyDepartmentAsync(department);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(Department department)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveDepartmentCompleted += new EventHandler<RemoveDepartmentCompletedEventArgs>(service_RemoveDepartmentCompleted);
                service.RemoveDepartmentAsync(department);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveDepartmentCompleted(object sender, RemoveDepartmentCompletedEventArgs e)
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
        private void service_AddDepartmentCompleted(object sender, AddDepartmentCompletedEventArgs e)
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
        private void service_ModifyDepartmentCompleted(object sender, ModifyDepartmentCompletedEventArgs e)
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
        private void service_GetAllDepartmentsCompleted(object sender, GetAllDepartmentsCompletedEventArgs e)
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
