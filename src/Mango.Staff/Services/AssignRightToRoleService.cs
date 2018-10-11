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
using Mango.Users.Interfaces;

namespace Mango.Users.Services
{
    public class AssignRightToRoleService : IAssignRightToRoleService
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;

        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void AssignRightToRole(Role role)
        {
            try
            {
                service = new ServiceClient();
                service.AssignRightToRoleCompleted += new EventHandler<AssignRightToRoleCompletedEventArgs>(service_AssignRightToRoleCompleted);
                service.AssignRightToRoleAsync(role);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_AssignRightToRoleCompleted(object sender, AssignRightToRoleCompletedEventArgs e)
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
