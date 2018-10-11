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

namespace Mango.Setup.Interfaces
{
    public interface IStatusService
    {
        event EventHandler GetAllModelsCompleted;

        ObservableCollection<Status> Models { get; set; }
        Fault Fault { get; set; }

        void LoadAll();
    }



}
