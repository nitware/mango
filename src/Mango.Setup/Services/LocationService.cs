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
    public class LocationService : ISetupService<Location>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<Location> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllLocationsCompleted += new EventHandler<GetAllLocationsCompletedEventArgs>(service_GetAllLocationsCompleted);
                service.GetAllLocationsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(Location location)
        {
            try
            {
                service = new ServiceClient();
                service.AddLocationCompleted += new EventHandler<AddLocationCompletedEventArgs>(service_AddLocationCompleted);
                service.AddLocationAsync(location);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(Location location)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyLocationCompleted += new EventHandler<ModifyLocationCompletedEventArgs>(service_ModifyLocationCompleted);
                service.ModifyLocationAsync(location);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(Location location)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveLocationCompleted += new EventHandler<RemoveLocationCompletedEventArgs>(service_RemoveLocationCompleted);
                service.RemoveLocationAsync(location);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveLocationCompleted(object sender, RemoveLocationCompletedEventArgs e)
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
        private void service_AddLocationCompleted(object sender, AddLocationCompletedEventArgs e)
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
        private void service_ModifyLocationCompleted(object sender, ModifyLocationCompletedEventArgs e)
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
        private void service_GetAllLocationsCompleted(object sender, GetAllLocationsCompletedEventArgs e)
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
