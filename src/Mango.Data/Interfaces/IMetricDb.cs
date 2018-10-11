using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.DataAccess;
using System.Data;

namespace Mango.Data.Interfaces
{
    public interface IMetricDb
    {
        string FIELD_STAFF_METRIC_ID { get; }
        string FIELD_METRIC_ID { get; }
        string FIELD_METRIC_PERSPECTIVE_ID { get; }
        string FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID { get; }
        string FIELD_KPI { get; }
        string FIELD_MEASURE { get; }
        string FIELD_DATA_SOURCE { get; }
        string FIELD_RESPONSIBLE_DEPARTMENT_ID { get; }
        string FIELD_TARGET { get; }
        string FIELD_SCORE { get; }
        string FIELD_RATING { get; }
        string FIELD_PERIOD_ID { get; }

        long InsertMetric(int metricPerspectiveId, int companyDepartmentJobRoleId, string kpi, string measure, string dataSource, string rsponsibleDepartmentId, decimal target, decimal score, int periodId, Transaction transaction);
        DataSet SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID(int metricPerspectiveId, int companyDepartmentJobRoleId, int periodId);
        DataSet SelectMetricRatingByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID(long metricId, int metricPerspectiveId, int companyDepartmentJobRoleId, int periodId);
        DataSet SelectMetricTotalCountByCompanyDepartmentJobRoleIdAndPeriodID(int companyDepartmentJobRoleId, int periodId);
        DataSet SelectMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndAppraisalHeaderId(int metricPerspectiveId, int companyDepartmentJobRoleId, long appraisalHeaderId);
        DataSet SelectMetricByPeriodID(int periodId);

        bool InsertOtherEntitiesForNewAppraisal(int oldPeriodId, int newPeriodId, Transaction transaction);
    }


}
