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
    public class RightService : ISetupService<Right>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<Right> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllRightsCompleted += new EventHandler<GetAllRightsCompletedEventArgs>(service_GetAllRightsCompleted);
                service.GetAllRightsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(Right right)
        {
            try
            {
                service = new ServiceClient();
                service.AddRightCompleted += new EventHandler<AddRightCompletedEventArgs>(service_AddRightCompleted);
                service.AddRightAsync(right);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(Right right)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyRightCompleted += new EventHandler<ModifyRightCompletedEventArgs>(service_ModifyRightCompleted);
                service.ModifyRightAsync(right);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(Right right)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveRightCompleted += new EventHandler<RemoveRightCompletedEventArgs>(service_RemoveRightCompleted);
                service.RemoveRightAsync(right);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveRightCompleted(object sender, RemoveRightCompletedEventArgs e)
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
        private void service_AddRightCompleted(object sender, AddRightCompletedEventArgs e)
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
        private void service_ModifyRightCompleted(object sender, ModifyRightCompletedEventArgs e)
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
        private void service_GetAllRightsCompleted(object sender, GetAllRightsCompletedEventArgs e)
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
