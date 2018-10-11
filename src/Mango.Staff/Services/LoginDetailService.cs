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

using System.Collections.ObjectModel;
using Mango.Infrastructure.MangoService;
using Mango.Staff.Interfaces;

namespace Mango.Staff.Services
{
    public class LoginDetailService : ILoginDetailService
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllLoginDetailsCompleted;
        public event EventHandler ChangePasswordCompleted;
                
        public bool Done { get; set; }
        public ObservableCollection<LoginDetail> LoginDetails { get; set; }
        public LoginDetail LoginDetail { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllLoginDetailsCompleted += new EventHandler<GetAllLoginDetailsCompletedEventArgs>(service_GetAllLoginDetailsCompleted);
                service.GetAllLoginDetailsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Reset(LoginDetail loginDetail)
        {
            try
            {
                service = new ServiceClient();
                service.ResetLoginDetailPasswordCompleted += new EventHandler<ResetLoginDetailPasswordCompletedEventArgs>(service_ResetLoginDetailPasswordCompleted);
                service.ResetLoginDetailPasswordAsync(loginDetail);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(LoginDetail loginDetail)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyLoginDetailCompleted += new EventHandler<ModifyLoginDetailCompletedEventArgs>(service_ModifyLoginDetailCompleted);
                service.ModifyLoginDetailAsync(loginDetail);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ChangePassword(Infrastructure.MangoService.Staff staff, string password)
        {
            try
            {
                service = new ServiceClient();
                service.ChangePasswordCompleted += new EventHandler<ChangePasswordCompletedEventArgs>(service_ChangePasswordCompleted);
                service.ChangePasswordAsync(staff, password);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void service_ResetLoginDetailPasswordCompleted(object sender, ResetLoginDetailPasswordCompletedEventArgs e)
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
        private void service_ModifyLoginDetailCompleted(object sender, ModifyLoginDetailCompletedEventArgs e)
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
       
        private void service_GetAllLoginDetailsCompleted(object sender, GetAllLoginDetailsCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                LoginDetails = e.Result;


                if (GetAllLoginDetailsCompleted != null)
                {
                    GetAllLoginDetailsCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_ChangePasswordCompleted(object sender, ChangePasswordCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                LoginDetail = e.Result;
                
                if (ChangePasswordCompleted != null)
                {
                    ChangePasswordCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }





    }
}
