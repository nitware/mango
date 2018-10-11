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
    public class StaffCdjrService : ISetupService<StaffCdjr>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<StaffCdjr> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllStaffCdjrsCompleted += new EventHandler<GetAllStaffCdjrsCompletedEventArgs>(service_GetAllStaffCdjrsCompleted);
                service.GetAllStaffCdjrsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(StaffCdjr staffCdjr)
        {
            try
            {
                service = new ServiceClient();
                service.AddStaffCdjrCompleted += new EventHandler<AddStaffCdjrCompletedEventArgs>(service_AddStaffCdjrCompleted);
                service.AddStaffCdjrAsync(staffCdjr);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(StaffCdjr staffCdjr)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyStaffCdjrCompleted += new EventHandler<ModifyStaffCdjrCompletedEventArgs>(service_ModifyStaffCdjrCompleted);
                service.ModifyStaffCdjrAsync(staffCdjr);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(StaffCdjr staffCdjr)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveStaffCdjrCompleted += new EventHandler<RemoveStaffCdjrCompletedEventArgs>(service_RemoveStaffCdjrCompleted);
                service.RemoveStaffCdjrAsync(staffCdjr);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveStaffCdjrCompleted(object sender, RemoveStaffCdjrCompletedEventArgs e)
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
        private void service_AddStaffCdjrCompleted(object sender, AddStaffCdjrCompletedEventArgs e)
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
        private void service_ModifyStaffCdjrCompleted(object sender, ModifyStaffCdjrCompletedEventArgs e)
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
        private void service_GetAllStaffCdjrsCompleted(object sender, GetAllStaffCdjrsCompletedEventArgs e)
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
