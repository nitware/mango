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

using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.MangoService;
using System.Collections.ObjectModel;

namespace Mango.Setup.Services
{
    public class MetricsPerspectiveService : ISetupService<MetricsPerspective>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<MetricsPerspective> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllMetricsPerspectivesCompleted += new EventHandler<GetAllMetricsPerspectivesCompletedEventArgs>(service_GetAllMetricsPerspectivesCompleted);
                service.GetAllMetricsPerspectivesAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(MetricsPerspective metricsPerspective)
        {
            try
            {
                service = new ServiceClient();
                service.AddMetricsPerspectiveCompleted += new EventHandler<AddMetricsPerspectiveCompletedEventArgs>(service_AddMetricsPerspectiveCompleted);
                service.AddMetricsPerspectiveAsync(metricsPerspective);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(MetricsPerspective metricsPerspective)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyMetricsPerspectiveCompleted += new EventHandler<ModifyMetricsPerspectiveCompletedEventArgs>(service_ModifyMetricsPerspectiveCompleted);
                service.ModifyMetricsPerspectiveAsync(metricsPerspective);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(MetricsPerspective metricsPerspective)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveMetricsPerspectiveCompleted += new EventHandler<RemoveMetricsPerspectiveCompletedEventArgs>(service_RemoveMetricsPerspectiveCompleted);
                service.RemoveMetricsPerspectiveAsync(metricsPerspective);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveMetricsPerspectiveCompleted(object sender, RemoveMetricsPerspectiveCompletedEventArgs e)
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
        private void service_AddMetricsPerspectiveCompleted(object sender, AddMetricsPerspectiveCompletedEventArgs e)
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
        private void service_ModifyMetricsPerspectiveCompleted(object sender, ModifyMetricsPerspectiveCompletedEventArgs e)
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
        private void service_GetAllMetricsPerspectivesCompleted(object sender, GetAllMetricsPerspectivesCompletedEventArgs e)
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
