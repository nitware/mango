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
    public class RatingService : ISetupService<Infrastructure.MangoService.Rating>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<Infrastructure.MangoService.Rating> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllRatingsCompleted += new EventHandler<GetAllRatingsCompletedEventArgs>(service_GetAllRatingsCompleted);
                service.GetAllRatingsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(Infrastructure.MangoService.Rating rating)
        {
            try
            {
                service = new ServiceClient();
                service.AddRatingCompleted += new EventHandler<AddRatingCompletedEventArgs>(service_AddRatingCompleted);
                service.AddRatingAsync(rating);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(Infrastructure.MangoService.Rating rating)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyRatingCompleted += new EventHandler<ModifyRatingCompletedEventArgs>(service_ModifyRatingCompleted);
                service.ModifyRatingAsync(rating);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(Infrastructure.MangoService.Rating rating)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveRatingCompleted += new EventHandler<RemoveRatingCompletedEventArgs>(service_RemoveRatingCompleted);
                service.RemoveRatingAsync(rating);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveRatingCompleted(object sender, RemoveRatingCompletedEventArgs e)
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
        private void service_AddRatingCompleted(object sender, AddRatingCompletedEventArgs e)
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
        private void service_ModifyRatingCompleted(object sender, ModifyRatingCompletedEventArgs e)
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
        private void service_GetAllRatingsCompleted(object sender, GetAllRatingsCompletedEventArgs e)
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
