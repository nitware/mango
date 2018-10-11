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
    public interface IMetricsSetupService
    {
         event EventHandler ActionCompleted;
        event EventHandler GetAllModelsCompleted;
        event EventHandler GetModelsByCdjrAndPeriodCompleted;
        event EventHandler ModifyNpsCompleted;
        event EventHandler LoadNpsCompleted;
        event EventHandler GetAllMetricesByPeriodAndPerspectiveCompleted;

        ObservableCollection<Metrics> Models { get; set; }
        bool Done { get; set; }
        Fault Fault { get; set; }

        void LoadAll();
        void LoadByCompanyDepartmentJobRoleAndPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period);
        void Modify(Metrics metrics);
        void GetAllNpsByPeriod(Period period);
        void ModifyNps(ObservableCollection<Metrics> models);
        void GetAllMetricsByPeriodAndPerspective(Period period, MetricsPerspective perspective);
    }




}
