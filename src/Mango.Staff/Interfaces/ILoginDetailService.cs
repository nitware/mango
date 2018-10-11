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

namespace Mango.Staff.Interfaces
{
    public interface ILoginDetailService
    {
        event EventHandler ActionCompleted;
        event EventHandler GetAllLoginDetailsCompleted;
        event EventHandler ChangePasswordCompleted;

        bool Done { get; set; }
        ObservableCollection<LoginDetail> LoginDetails { get; set; }
        LoginDetail LoginDetail { get; set; }
        Fault Fault { get; set; }

        void LoadAll();
        void Reset(LoginDetail loginDetail);
        void Modify(LoginDetail loginDetail);
        void ChangePassword(Infrastructure.MangoService.Staff staff, string password);
    }
}
