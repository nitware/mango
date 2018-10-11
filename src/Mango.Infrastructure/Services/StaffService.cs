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

namespace Mango.Infrastructure.Services
{
    public class StaffService : ISetupService<Staff>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<Staff> Models { get; set; }
        public ObservableCollection<Staff> Users { get; set; }
        public Fault Fault { get; set; }
        public bool Done { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllStaffsCompleted += new EventHandler<GetAllStaffsCompletedEventArgs>(service_GetAllStaffsCompleted);
                service.GetAllStaffsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public void LoadAll(Staff staff)
        {
            try
            {
                service = new ServiceClient();
                service.GetStaffsCompleted += new EventHandler<GetStaffsCompletedEventArgs>(service_GetStaffsCompleted);
                service.GetStaffsAsync(staff);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save(Staff staff)
        {
            try
            {
                service = new ServiceClient();
                service.AddStaffCompleted += new EventHandler<AddStaffCompletedEventArgs>(service_AddStaffCompleted);
                service.AddStaffAsync(staff);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(Staff staff)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyStaffCompleted += new EventHandler<ModifyStaffCompletedEventArgs>(service_ModifyStaffCompleted);
                service.ModifyStaffAsync(staff);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(Staff staff)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveStaffCompleted += new EventHandler<RemoveStaffCompletedEventArgs>(service_RemoveStaffCompleted);
                service.RemoveStaffAsync(staff);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveStaffCompleted(object sender, RemoveStaffCompletedEventArgs e)
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
        private void service_AddStaffCompleted(object sender, AddStaffCompletedEventArgs e)
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
        private void service_ModifyStaffCompleted(object sender, ModifyStaffCompletedEventArgs e)
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
        private void service_GetAllStaffsCompleted(object sender, GetAllStaffsCompletedEventArgs e)
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
        private void service_GetStaffsCompleted(object sender, GetStaffsCompletedEventArgs e)
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
