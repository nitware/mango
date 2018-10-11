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
    public class StaffLocationService : ISetupService<StaffLocation>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<StaffLocation> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllStaffLocationsCompleted += new EventHandler<GetAllStaffLocationsCompletedEventArgs>(service_GetAllStaffLocationsCompleted);
                service.GetAllStaffLocationsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(StaffLocation staffLocation)
        {
            try
            {
                service = new ServiceClient();
                service.AddStaffLocationCompleted += new EventHandler<AddStaffLocationCompletedEventArgs>(service_AddStaffLocationCompleted);
                service.AddStaffLocationAsync(staffLocation);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(StaffLocation staffLocation)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyStaffLocationCompleted += new EventHandler<ModifyStaffLocationCompletedEventArgs>(service_ModifyStaffLocationCompleted);
                service.ModifyStaffLocationAsync(staffLocation);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(StaffLocation staffLocation)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveStaffLocationCompleted += new EventHandler<RemoveStaffLocationCompletedEventArgs>(service_RemoveStaffLocationCompleted);
                service.RemoveStaffLocationAsync(staffLocation);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveStaffLocationCompleted(object sender, RemoveStaffLocationCompletedEventArgs e)
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
        private void service_AddStaffLocationCompleted(object sender, AddStaffLocationCompletedEventArgs e)
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
        private void service_ModifyStaffLocationCompleted(object sender, ModifyStaffLocationCompletedEventArgs e)
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
        private void service_GetAllStaffLocationsCompleted(object sender, GetAllStaffLocationsCompletedEventArgs e)
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
