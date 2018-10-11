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
    public class RatingTypeService : ISetupService<RatingType>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<RatingType> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllRatingTypesCompleted += new EventHandler<GetAllRatingTypesCompletedEventArgs>(service_GetAllRatingTypesCompleted);
                service.GetAllRatingTypesAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(RatingType ratingType)
        {
            try
            {
                service = new ServiceClient();
                service.AddRatingTypeCompleted += new EventHandler<AddRatingTypeCompletedEventArgs>(service_AddRatingTypeCompleted);
                service.AddRatingTypeAsync(ratingType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(RatingType ratingType)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyRatingTypeCompleted += new EventHandler<ModifyRatingTypeCompletedEventArgs>(service_ModifyRatingTypeCompleted);
                service.ModifyRatingTypeAsync(ratingType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(RatingType ratingType)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveRatingTypeCompleted += new EventHandler<RemoveRatingTypeCompletedEventArgs>(service_RemoveRatingTypeCompleted);
                service.RemoveRatingTypeAsync(ratingType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveRatingTypeCompleted(object sender, RemoveRatingTypeCompletedEventArgs e)
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
        private void service_AddRatingTypeCompleted(object sender, AddRatingTypeCompletedEventArgs e)
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
        private void service_ModifyRatingTypeCompleted(object sender, ModifyRatingTypeCompletedEventArgs e)
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
        private void service_GetAllRatingTypesCompleted(object sender, GetAllRatingTypesCompletedEventArgs e)
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
