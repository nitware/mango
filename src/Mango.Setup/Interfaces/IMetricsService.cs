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
    public interface IMetricsService
    {
        event EventHandler ActionCompleted;
        event EventHandler GetModelsCompleted;
        event EventHandler GetAllModelsCompleted;
        

        Fault Fault { get; set; }
        ObservableCollection<Metrics> Models { get; set; }
        bool Done { get; set; }

        void LoadByPeriod(Period period);
        void Save(ObservableCollection<Metrics> models);
        void Modify(ObservableCollection<Metrics> models);
        void LoadByCompanyDepartmentJobRoleAndPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period);
       
    }



}
