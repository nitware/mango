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

namespace Mango.Infrastructure.Interfaces
{
    public interface ISetupService<T>
    {
        event EventHandler ActionCompleted;
        event EventHandler GetAllModelsCompleted;

        Fault Fault { get; set; }
        ObservableCollection<T> Models { get; set; }
        bool Done { get; set; }

        void LoadAll();
        void Save(T model);
        void Modify(T model);
        void Remove(T model);
    }
    

}
