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

namespace Mango.Infrastructure.Interfaces
{
    public interface IMetricBaseService<T>
    {
        event EventHandler ActionCompleted;
        event EventHandler GetModelsCompleted;

        Fault Fault { get; set; }
        ObservableCollection<T> Models { get; set; }
        bool Done { get; set; }
        //bool RemoveAssociatedRatings { get; set; }

        void LoadByPeriod(Period period);
        void Save(ObservableCollection<T> models);
        void Modify(ObservableCollection<T> models);
    }



}
