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

namespace Mango.Setup.Interfaces
{
    public interface IPeriodSetupService
    {
        event EventHandler ActionCompleted;
        event EventHandler GetAllModelsCompleted;

        ObservableCollection<Period> Periods { get; set; }
        bool Done { get; set; }

        void LoadAll();
        void Save(Period newPeriod);

        //void Modify(Period period);
        //void Remove(Period period);
    }


}
