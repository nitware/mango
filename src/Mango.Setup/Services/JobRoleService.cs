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
    public class JobRoleService : ISetupService<JobRole>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<JobRole> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllJobRolesCompleted += new EventHandler<GetAllJobRolesCompletedEventArgs>(service_GetAllJobRolesCompleted);
                service.GetAllJobRolesAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(JobRole jobRole)
        {
            try
            {
                service = new ServiceClient();
                service.AddJobRoleCompleted += new EventHandler<AddJobRoleCompletedEventArgs>(service_AddJobRoleCompleted);
                service.AddJobRoleAsync(jobRole);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(JobRole jobRole)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyJobRoleCompleted += new EventHandler<ModifyJobRoleCompletedEventArgs>(service_ModifyJobRoleCompleted);
                service.ModifyJobRoleAsync(jobRole);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(JobRole jobRole)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveJobRoleCompleted += new EventHandler<RemoveJobRoleCompletedEventArgs>(service_RemoveJobRoleCompleted);
                service.RemoveJobRoleAsync(jobRole);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveJobRoleCompleted(object sender, RemoveJobRoleCompletedEventArgs e)
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
        private void service_AddJobRoleCompleted(object sender, AddJobRoleCompletedEventArgs e)
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
        private void service_ModifyJobRoleCompleted(object sender, ModifyJobRoleCompletedEventArgs e)
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
        private void service_GetAllJobRolesCompleted(object sender, GetAllJobRolesCompletedEventArgs e)
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
