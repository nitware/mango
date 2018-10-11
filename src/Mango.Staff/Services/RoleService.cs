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

using System.Collections.ObjectModel;
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.MangoService;

namespace Mango.Users.Services
{
    public class RoleService : ISetupService<Role>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<Role> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll(Infrastructure.MangoService.Staff staff)
        {
            try
            {
                service = new ServiceClient();
                service.GetRolesCompleted += new EventHandler<GetRolesCompletedEventArgs>(service_GetRolesCompleted);
                service.GetRolesAsync(staff);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllRolesCompleted += new EventHandler<GetAllRolesCompletedEventArgs>(service_GetAllRolesCompleted);
                service.GetAllRolesAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save(Role role)
        {
            try
            {
                service = new ServiceClient();
                service.AddRoleCompleted += new EventHandler<AddRoleCompletedEventArgs>(service_AddRoleCompleted);
                service.AddRoleAsync(role);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(Role role)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyRoleCompleted += new EventHandler<ModifyRoleCompletedEventArgs>(service_ModifyRoleCompleted);
                service.ModifyRoleAsync(role);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(Role role)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveRoleCompleted += new EventHandler<RemoveRoleCompletedEventArgs>(service_RemoveRoleCompleted);
                service.RemoveRoleAsync(role);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveRoleCompleted(object sender, RemoveRoleCompletedEventArgs e)
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
        private void service_AddRoleCompleted(object sender, AddRoleCompletedEventArgs e)
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
        private void service_ModifyRoleCompleted(object sender, ModifyRoleCompletedEventArgs e)
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
        private void service_GetAllRolesCompleted(object sender, GetAllRolesCompletedEventArgs e)
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
        private void service_GetRolesCompleted(object sender, GetRolesCompletedEventArgs e)
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
