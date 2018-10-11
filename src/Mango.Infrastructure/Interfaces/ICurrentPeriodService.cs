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

namespace Mango.Infrastructure.Interfaces
{
    public interface ICurrentPeriodService
    {
        event EventHandler CurrentPeriodLoadCompleted;
        event EventHandler ActionCompleted;

        Period Period { get; set; }
        Fault Fault { get; set; }
        bool Done { get; set; }

        void GetCurrentPeriod();
        //void Save(Period period);
        void SetCurrent(CurrentPeriod period);
    }




}
