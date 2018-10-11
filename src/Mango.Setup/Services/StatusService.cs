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
using Mango.Setup.Interfaces;

namespace Mango.Setup.Services
{
    public class StatusService : IStatusService
    {
        private ServiceClient service;

        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<Status> Models { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllStatusCompleted += new EventHandler<GetAllStatusCompletedEventArgs>(service_GetAllStatusCompleted);
                service.GetAllStatusAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_GetAllStatusCompleted(object sender, GetAllStatusCompletedEventArgs e)
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
