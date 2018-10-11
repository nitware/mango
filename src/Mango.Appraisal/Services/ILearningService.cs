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

namespace Mango.Appraisal.Services
{
    public interface ILearningService
    {
        //event EventHandler ActionCompleted;
        event EventHandler GetLearnigByStaffAndPeriodCompleted;

        Learning Learning { get; set; }
        bool Done { get; set; }

        void GetByStaffAndPeriod(string staffId, int periodId);
    }


}
