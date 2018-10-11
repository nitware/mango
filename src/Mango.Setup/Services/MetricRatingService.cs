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
using System.Collections.ObjectModel;
using Mango.Infrastructure.Interfaces;

namespace Mango.Setup.Services
{
    public class MetricRatingService : IMetricBaseService<MetricRating>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetModelsCompleted;

        
        public Fault Fault { get; set; }
        public ObservableCollection<MetricRating> Models { get; set; }
        public bool Done { get; set; }

        public void LoadByPeriod(Period period)
        {
            try
            {
                service = new ServiceClient();
                service.GetAllMetricRatingsByPeriodCompleted += new EventHandler<GetAllMetricRatingsByPeriodCompletedEventArgs>(service_GetAllMetricRatingsByPeriodCompleted);
                service.GetAllMetricRatingsByPeriodAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LoadByMetrics(Metrics metrics)
        {
            try
            {
                service = new ServiceClient();
                service.GetMetricRatingsByMetricsCompleted += new EventHandler<GetMetricRatingsByMetricsCompletedEventArgs>(service_GetMetricRatingsByMetricsCompleted);
                service.GetMetricRatingsByMetricsAsync(metrics);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(ObservableCollection<MetricRating> models)
        {
            try
            {
                service = new ServiceClient();
                service.AddMetricRatingsCompleted += new EventHandler<AddMetricRatingsCompletedEventArgs>(service_AddMetricRatingsCompleted);
                service.AddMetricRatingsAsync(models);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RemoveBy(Metrics metrics)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveMetricRatingByMetricsCompleted += new EventHandler<RemoveMetricRatingByMetricsCompletedEventArgs>(service_RemoveMetricRatingByMetricsCompleted);
                service.RemoveMetricRatingByMetricsAsync(metrics);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(ObservableCollection<MetricRating> models)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyMetricRatingsCompleted += new EventHandler<ModifyMetricRatingsCompletedEventArgs>(service_ModifyMetricRatingsCompleted);
                service.ModifyMetricRatingsAsync(models);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        private void service_GetAllMetricRatingsByPeriodCompleted(object sender, GetAllMetricRatingsByPeriodCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Models = e.Result;

                if (GetModelsCompleted != null)
                {
                    GetModelsCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_AddMetricRatingsCompleted(object sender, AddMetricRatingsCompletedEventArgs e)
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
       
        private void service_ModifyMetricRatingsCompleted(object sender, ModifyMetricRatingsCompletedEventArgs e)
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

        private void service_RemoveMetricRatingByMetricsCompleted(object sender, RemoveMetricRatingByMetricsCompletedEventArgs e)
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

        private void service_GetMetricRatingsByMetricsCompleted(object sender, GetMetricRatingsByMetricsCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Models = e.Result;

                if (GetModelsCompleted != null)
                {
                    GetModelsCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }



}
