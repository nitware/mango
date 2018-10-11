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


namespace Mango.Users.Interfaces
{
    public interface IAssignRightToRoleService
    {
        event EventHandler ActionCompleted;

        bool Done { get; set; }
        Fault Fault { get; set; }

        void AssignRightToRole(Role role);
    }


}
