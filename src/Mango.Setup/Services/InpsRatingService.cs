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
    public class InpsRatingService : IMetricBaseService<InpsRating>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetModelsCompleted;
        public event EventHandler GetInpsRatingByPeriodAndTypeCompleted;

        public Fault Fault { get; set; }
        public ObservableCollection<InpsRating> Models { get; set; }
        public bool Done { get; set; }

        public void LoadByPeriod(Period period)
        {
            try
            {
                service = new ServiceClient();
                service.GetAllInpsRatingCompleted += new EventHandler<GetAllInpsRatingCompletedEventArgs>(service_GetAllInpsRatingCompleted);
                service.GetAllInpsRatingAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LoadByPeriodAndType(Period period, InpsType inpsType)
        {
            try
            {
                service = new ServiceClient();
                service.GetInpsRatingByPeriodAndTypeCompleted += new EventHandler<GetInpsRatingByPeriodAndTypeCompletedEventArgs>(service_GetInpsRatingByPeriodAndTypeCompleted);
                service.GetInpsRatingByPeriodAndTypeAsync(period, inpsType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(ObservableCollection<InpsRating> models)
        {
            try
            {
                service = new ServiceClient();
                service.AddInpsRatingCompleted += new EventHandler<AddInpsRatingCompletedEventArgs>(service_AddInpsRatingCompleted);
                service.AddInpsRatingAsync(models);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RemoveBy(Period period)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveInpsRatingByPeriodCompleted += new EventHandler<RemoveInpsRatingByPeriodCompletedEventArgs>(service_RemoveInpsRatingByPeriodCompleted);
                service.RemoveInpsRatingByPeriodAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(ObservableCollection<InpsRating> models)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyInpsRatingCompleted += new EventHandler<ModifyInpsRatingCompletedEventArgs>(service_ModifyInpsRatingCompleted);
                service.ModifyInpsRatingAsync(models);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_GetAllInpsRatingCompleted(object sender, GetAllInpsRatingCompletedEventArgs e)
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
        private void service_AddInpsRatingCompleted(object sender, AddInpsRatingCompletedEventArgs e)
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

        private void service_ModifyInpsRatingCompleted(object sender, ModifyInpsRatingCompletedEventArgs e)
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

        private void service_RemoveInpsRatingByPeriodCompleted(object sender, RemoveInpsRatingByPeriodCompletedEventArgs e)
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

        private void service_GetInpsRatingByPeriodAndTypeCompleted(object sender, GetInpsRatingByPeriodAndTypeCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Models = e.Result;

                if (GetInpsRatingByPeriodAndTypeCompleted != null)
                {
                    GetInpsRatingByPeriodAndTypeCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }



}
