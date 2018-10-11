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

namespace Mango.Infrastructure.Services
{
    public class CurrentPeriodService : ICurrentPeriodService
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler CurrentPeriodLoadCompleted;

        public Period Period { get; set; }
        public Fault Fault { get; set; }
        public bool Done { get; set; }

        public void GetCurrentPeriod()
        {
            service = new ServiceClient();
            service.GetCurrentPeriodCompleted += new EventHandler<GetCurrentPeriodCompletedEventArgs>(svc_GetCurrentPeriodCompleted);
            service.GetCurrentPeriodAsync();
            service.CloseAsync();
        }
        public void SetCurrent(CurrentPeriod period)
        {
            try
            {
                service = new ServiceClient();
                service.SetNewCurrentPeriodCompleted += new EventHandler<SetNewCurrentPeriodCompletedEventArgs>(service_SetNewCurrentPeriodCompleted);
                service.SetNewCurrentPeriodAsync(period);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void svc_GetCurrentPeriodCompleted(object sender, GetCurrentPeriodCompletedEventArgs e)
        {
            Period = e.Result;
            if (CurrentPeriodLoadCompleted != null)
            {
                CurrentPeriodLoadCompleted(this, null);
            }
        }
        private void service_SetNewCurrentPeriodCompleted(object sender, SetNewCurrentPeriodCompletedEventArgs e)
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



    }
}
