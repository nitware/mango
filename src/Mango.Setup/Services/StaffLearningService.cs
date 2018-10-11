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
    public class StaffLearningService : ISetupService<StaffLearning>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<StaffLearning> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllStaffLearningCompleted += new EventHandler<GetAllStaffLearningCompletedEventArgs>(service_GetAllStaffLearningCompleted);
                service.GetAllStaffLearningAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(StaffLearning staffLearning)
        {
            try
            {
                service = new ServiceClient();
                service.AddStaffLearningCompleted += new EventHandler<AddStaffLearningCompletedEventArgs>(service_AddStaffLearningCompleted);
                service.AddStaffLearningAsync(staffLearning);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(StaffLearning staffLearning)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyStaffLearningCompleted += new EventHandler<ModifyStaffLearningCompletedEventArgs>(service_ModifyStaffLearningCompleted);
                service.ModifyStaffLearningAsync(staffLearning);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(StaffLearning staffLearning)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveStaffLearningCompleted += new EventHandler<RemoveStaffLearningCompletedEventArgs>(service_RemoveStaffLearningCompleted);
                service.RemoveStaffLearningAsync(staffLearning);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveStaffLearningCompleted(object sender, RemoveStaffLearningCompletedEventArgs e)
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
        private void service_AddStaffLearningCompleted(object sender, AddStaffLearningCompletedEventArgs e)
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
        private void service_ModifyStaffLearningCompleted(object sender, ModifyStaffLearningCompletedEventArgs e)
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
        private void service_GetAllStaffLearningCompleted(object sender, GetAllStaffLearningCompletedEventArgs e)
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
