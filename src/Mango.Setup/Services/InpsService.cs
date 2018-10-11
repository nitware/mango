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
using Mango.Infrastructure.Models;

namespace Mango.Setup.Services
{
    public class InpsService : ISetupService<Inps>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<Inps> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetInpsByPeriodCompleted += new EventHandler<GetInpsByPeriodCompletedEventArgs>(service_GetInpsByPeriodCompleted);
                service.GetInpsByPeriodAsync(Utility.Period);
                service.CloseAsync();


                //service = new ServiceClient();
                //service.GetAllInpsCompleted += new EventHandler<GetAllInpsCompletedEventArgs>(service_GetAllInpsCompleted);
                //service.GetAllInpsAsync();
                //service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(Inps inps)
        {
            try
            {
                service = new ServiceClient();
                service.AddInpsCompleted += new EventHandler<AddInpsCompletedEventArgs>(service_AddInpsCompleted);
                service.AddInpsAsync(inps);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(Inps inps)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyInpsCompleted += new EventHandler<ModifyInpsCompletedEventArgs>(service_ModifyInpsCompleted);
                service.ModifyInpsAsync(inps);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(Inps inps)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveInpsCompleted += new EventHandler<RemoveInpsCompletedEventArgs>(service_RemoveInpsCompleted);
                service.RemoveInpsAsync(inps);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveInpsCompleted(object sender, RemoveInpsCompletedEventArgs e)
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
        private void service_AddInpsCompleted(object sender, AddInpsCompletedEventArgs e)
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
        private void service_ModifyInpsCompleted(object sender, ModifyInpsCompletedEventArgs e)
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


        private void service_GetInpsByPeriodCompleted(object sender, GetInpsByPeriodCompletedEventArgs e)
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


        //private void service_GetAllInpsCompleted(object sender, GetAllInpsCompletedEventArgs e)
        //{
        //    try
        //    {
        //        Fault = e.fault;
        //        Models = e.Result;

        //        if (GetAllModelsCompleted != null)
        //        {
        //            GetAllModelsCompleted(this, e);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}




    }
}
