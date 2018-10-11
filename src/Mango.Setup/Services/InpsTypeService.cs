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
    public class InpsTypeService : ISetupService<InpsType>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<InpsType> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllInpsTypeCompleted += new EventHandler<GetAllInpsTypeCompletedEventArgs>(service_GetAllInpsTypeCompleted);
                service.GetAllInpsTypeAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(InpsType inpsType)
        {
            try
            {
                service = new ServiceClient();
                service.AddInpsTypeCompleted += new EventHandler<AddInpsTypeCompletedEventArgs>(service_AddInpsTypeCompleted);
                service.AddInpsTypeAsync(inpsType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(InpsType inpsType)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyInpsTypeCompleted += new EventHandler<ModifyInpsTypeCompletedEventArgs>(service_ModifyInpsTypeCompleted);
                service.ModifyInpsTypeAsync(inpsType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(InpsType inpsType)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveInpsTypeCompleted += new EventHandler<RemoveInpsTypeCompletedEventArgs>(service_RemoveInpsTypeCompleted);
                service.RemoveInpsTypeAsync(inpsType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveInpsTypeCompleted(object sender, RemoveInpsTypeCompletedEventArgs e)
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
        private void service_AddInpsTypeCompleted(object sender, AddInpsTypeCompletedEventArgs e)
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
        private void service_ModifyInpsTypeCompleted(object sender, ModifyInpsTypeCompletedEventArgs e)
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
        private void service_GetAllInpsTypeCompleted(object sender, GetAllInpsTypeCompletedEventArgs e)
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
