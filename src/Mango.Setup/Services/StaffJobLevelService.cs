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
using Mango.Infrastructure.Interfaces;

namespace Mango.Setup.Services
{
    public class StaffJobLevelService : ISetupService<StaffLevel>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<StaffLevel> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllStaffLevelsCompleted += new EventHandler<GetAllStaffLevelsCompletedEventArgs>(service_GetAllStaffLevelsCompleted);
                service.GetAllStaffLevelsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(StaffLevel staffLevel)
        {
            try
            {
                service = new ServiceClient();
                service.AddStaffLevelCompleted += new EventHandler<AddStaffLevelCompletedEventArgs>(service_AddStaffLevelCompleted);
                service.AddStaffLevelAsync(staffLevel);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(StaffLevel staffLevel)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyStaffLevelCompleted += new EventHandler<ModifyStaffLevelCompletedEventArgs>(service_ModifyStaffLevelCompleted);
                service.ModifyStaffLevelAsync(staffLevel);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(StaffLevel staffLevel)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveStaffLevelCompleted += new EventHandler<RemoveStaffLevelCompletedEventArgs>(service_RemoveStaffLevelCompleted);
                service.RemoveStaffLevelAsync(staffLevel);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveStaffLevelCompleted(object sender, RemoveStaffLevelCompletedEventArgs e)
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
        private void service_AddStaffLevelCompleted(object sender, AddStaffLevelCompletedEventArgs e)
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
        private void service_ModifyStaffLevelCompleted(object sender, ModifyStaffLevelCompletedEventArgs e)
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
        private void service_GetAllStaffLevelsCompleted(object sender, GetAllStaffLevelsCompletedEventArgs e)
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
