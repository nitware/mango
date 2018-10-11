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
using Mango.Staff.Interfaces;

namespace Mango.Staff.Services
{
    public class LoginService : ILoginService
    {
        private ServiceClient Service;
        public event EventHandler UserValidationCompleted;

        //public bool Done { get; set; }
        public Fault Fault { get; set; }

        //public bool Validated { get; set; }
        public Infrastructure.MangoService.Staff LoggedInUser { get; set; }

        public void ValidateUser(string userName, string password)
        {
            Service = new ServiceClient();
            Service.ValidateStaffCompleted += new EventHandler<ValidateStaffCompletedEventArgs>(Service_ValidateStaffCompleted);
            Service.ValidateStaffAsync(userName, password);
            Service.CloseAsync();
        }

        private void Service_ValidateStaffCompleted(object sender, ValidateStaffCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                if (e.Result != null)
                {
                    LoggedInUser = e.Result.Staff;
                }

                if (UserValidationCompleted != null)
                {
                    UserValidationCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
