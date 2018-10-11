using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.DataAccess;
using System.Data;

namespace Mango.Data.Interfaces
{
    public interface IInpsDb
    {
        string FIELD_INPS_ID { get; }
        string FIELD_METRIC_PERSPECTIVE_ID { get; }
        string FIELD_STAFF_ID { get; }
        string FIELD_KPI { get; }
        string FIELD_MEASURE { get; }
        string FIELD_DATA_SOURCE { get; }
        string FIELD_RESPONSIBLE_DEPARTMENT_ID { get; }
        string FIELD_TARGET { get; }
        string FIELD_SCORE { get; }
        string FIELD_RATING { get; }
        string FIELD_PERIOD_ID { get; }

        long InsertInps(int metricPerspectiveId, string staffId, string kpi, string measure, string dataSource, string responsibleDepartmentId, decimal target, decimal score, int periodId, Transaction transaction);
        bool UpdateInpsScoreByNpsId(long npsId, decimal score, Transaction transaction);
        DataSet SelectDefaultInpsByStaffIDAndPeriodID(string staffId, int periodId);

    }


}
