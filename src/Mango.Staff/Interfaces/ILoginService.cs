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

namespace Mango.Staff.Interfaces
{
    public interface ILoginService
    {
        event EventHandler UserValidationCompleted;

        Fault Fault { get; set; }
        Infrastructure.MangoService.Staff LoggedInUser { get; set; }

        void ValidateUser(string userName, string password);
    }




}
