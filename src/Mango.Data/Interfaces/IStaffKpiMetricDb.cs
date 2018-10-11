using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.DataAccess;

namespace Mango.Data.Interfaces
{
    public interface IStaffKpiMetricDb
    {
        string FIELD_STAFF_METRIC_ID { get; }
        string FIELD_KPI_SCORE { get; }
        
        bool InsertStaffKpiMetric(int staffMetricId, decimal kpiScore, Transaction transaction);
        bool DeleteStaffKpiMetricByStaffMetricId(int staffMetricId, Transaction transaction);
        bool UpdateStaffKpiMetricByStaffMetricId(int staffMetricId, decimal kpiScore, Transaction transaction);
        DataSet SelectAllStaffKpiMetric();
        DataSet SelectStaffKpiMetricByStaffMetricId(int staffMetricId);
      
    }





}
