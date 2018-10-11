//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:27

//Description: This Class represents the data tier layer class for Metric table.
//It contains all data access methods and static constants representing the
//Stored Procedures, field names and SQL parameters required by this entity.

//No man can cover the moon with his bare hands. You will shine when the time is ripe.
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;
using Mango.Data.Interfaces;
using Mango.DataAccess;

namespace Mango.Data
{
    public class MetricDb : DataAccess.DataAccess, IMetricDb
	{
		private const string CLASS_NAME = "MetricDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  Metric Stored Procedure declaration
                
        private const string STP_METRIC_INSERTMETRIC = "STP_METRIC_INSERTMETRIC";
        private const string STP_METRIC_SELECTMETRIC_BY_PERIOD_ID = "STP_METRIC_SELECTMETRIC_BY_PERIOD_ID";
        private const string STP_METRIC_SELECTMETRICBYMETRIC_PERSECTIVE_IDANDCOMPANY_DEPARTMENT_JOB_ROLE_ID_AND_PERIOD_ID = "STP_METRIC_SELECTMETRICBYMETRIC_PERSECTIVE_IDANDCOMPANY_DEPARTMENT_JOB_ROLE_ID_AND_PERIOD_ID";
        private const string STP_METRIC_RATINGSELECTMETRIC_RATINGBYMETRIC_PERSECTIVE_IDANDCOMPANY_DEPARTMENT_JOB_ROLE_ID_AND_PERIOD_ID = "STP_METRIC_RATINGSELECTMETRIC_RATINGBYMETRIC_PERSECTIVE_IDANDCOMPANY_DEPARTMENT_JOB_ROLE_ID_AND_PERIOD_ID";
        private const string STP_VW_STAFF_METRICS_SELECTVW_STAFF_METRICSBYMETRIC_PERSECTIVE_IDANDCOMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_VW_STAFF_METRICS_SELECTVW_STAFF_METRICSBYMETRIC_PERSECTIVE_IDANDCOMPANY_DEPARTMENT_JOB_ROLE_ID";
        private const string STP_METRIC_SELECTMETRIC_TOTAL_COUNTBYCOMPANY_DEPARTMENT_JOB_ROLE_ID_AND_PERIOD_ID = "STP_METRIC_SELECTMETRIC_TOTAL_COUNTBYCOMPANY_DEPARTMENT_JOB_ROLE_ID_AND_PERIOD_ID";

        private const string STP_APPRAISAL_CREATE_NEW_APPRAISAL = "STP_APPRAISAL_CREATE_NEW_APPRAISAL";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region Metric Parameter declaration 

        //Parameter decleration for STAFF_METRIC_ID
        private const string PARAM_STAFF_METRIC_ID_NAME = "@StaffMetricID";
        private const SqlDbType PARAM_STAFF_METRIC_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_STAFF_METRIC_ID_SIZE = 8;

		//Parameter decleration for METRIC_ID
		private const string PARAM_METRIC_ID_NAME = "@MetricID";
		private const SqlDbType PARAM_METRIC_ID_TYPE = SqlDbType.BigInt;
		private const int PARAM_METRIC_ID_SIZE = 8;

		//Parameter decleration for METRIC_PERSPECTIVE_ID
		private const string PARAM_METRIC_PERSPECTIVE_ID_NAME = "@MetricPerspectiveID";
		private const SqlDbType PARAM_METRIC_PERSPECTIVE_ID_TYPE = SqlDbType.Int;
		private const int PARAM_METRIC_PERSPECTIVE_ID_SIZE = 4;

		//Parameter decleration for COMPANY_DEPARTMENT_JOB_ROLE_ID
		private const string PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME = "@CompanyDepartmentJobRoleID";
		private const SqlDbType PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE = SqlDbType.Int;
		private const int PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE = 4;

		//Parameter decleration for METRIC1
		private const string PARAM_METRIC1_NAME = "@Metric1";
		private const SqlDbType PARAM_METRIC1_TYPE = SqlDbType.VarChar;
		private const int PARAM_METRIC1_SIZE = 50;

		//Parameter decleration for METRIC2
		private const string PARAM_METRIC2_NAME = "@Metric2";
		private const SqlDbType PARAM_METRIC2_TYPE = SqlDbType.VarChar;
		private const int PARAM_METRIC2_SIZE = 50;

		//Parameter decleration for WEIGHT
		private const string PARAM_WEIGHT_NAME = "@Weight";
		private const SqlDbType PARAM_WEIGHT_TYPE = SqlDbType.Int;
		private const int PARAM_WEIGHT_SIZE = 4;

		//Parameter decleration for BUDGET
		private const string PARAM_BUDGET_NAME = "@Budget";
		private const SqlDbType PARAM_BUDGET_TYPE = SqlDbType.Decimal;
		private const int PARAM_BUDGET_SIZE = 3;

		//Parameter decleration for VARIANCE
		private const string PARAM_VARIANCE_NAME = "@Variance";
		private const SqlDbType PARAM_VARIANCE_TYPE = SqlDbType.Decimal;
		private const int PARAM_VARIANCE_SIZE = 3;
        
        //Parameter decleration for APPRAISAL_HEADER_ID
        private const string PARAM_APPRAISAL_HEADER_ID_NAME = "@AppraisalHeaderID";
        private const SqlDbType PARAM_APPRAISAL_HEADER_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_APPRAISAL_HEADER_ID_SIZE = 8;

        //Parameter decleration for PERIOD_ID
        private const string PARAM_PERIOD_ID_NAME = "@PeriodID";
        private const SqlDbType PARAM_PERIOD_ID_TYPE = SqlDbType.Int;
        private const int PARAM_PERIOD_ID_SIZE = 4;

        //Parameter decleration for PERIOD_ID
        private const string PARAM_OLD_PERIOD_ID_NAME = "@OldPeriodID";
        private const SqlDbType PARAM_OLD_PERIOD_ID_TYPE = SqlDbType.Int;
        private const int PARAM_OLD_PERIOD_ID_SIZE = 4;



        //Parameter decleration for KPI
        private const string PARAM_KPI_NAME = "@Kpi";
        private const SqlDbType PARAM_KPI_TYPE = SqlDbType.VarChar;
        private const int PARAM_KPI_SIZE = 250;

        //Parameter decleration for MEASURE
        private const string PARAM_MEASURE_NAME = "@Measure";
        private const SqlDbType PARAM_MEASURE_TYPE = SqlDbType.VarChar;
        private const int PARAM_MEASURE_SIZE = 250;

        //Parameter decleration for DATA_SOURCE
        private const string PARAM_DATA_SOURCE_NAME = "@DataSource";
        private const SqlDbType PARAM_DATA_SOURCE_TYPE = SqlDbType.VarChar;
        private const int PARAM_DATA_SOURCE_SIZE = 150;

        //Parameter decleration for RSPONSIBLE_DEPARTMENT_ID
        private const string PARAM_RSPONSIBLE_DEPARTMENT_ID_NAME = "@RsponsibleDepartmentID";
        private const SqlDbType PARAM_RSPONSIBLE_DEPARTMENT_ID_TYPE = SqlDbType.NChar;
        private const int PARAM_RSPONSIBLE_DEPARTMENT_ID_SIZE = 3;

        //Parameter decleration for TARGET
        private const string PARAM_TARGET_NAME = "@Target";
        private const SqlDbType PARAM_TARGET_TYPE = SqlDbType.Decimal;
        private const int PARAM_TARGET_SIZE = 10;

        //Parameter decleration for SCORE
        private const string PARAM_SCORE_NAME = "@Score";
        private const SqlDbType PARAM_SCORE_TYPE = SqlDbType.Decimal;
        private const int PARAM_SCORE_SIZE = 10;


		#endregion

		//==========================================================================================
		//Metric Table Field Name Declaration
		//==========================================================================================
		#region Metric Field Name declaration 

        public string FIELD_STAFF_METRIC_ID { get { return "Staff_Metric_ID"; } }
		public string FIELD_METRIC_ID { get { return "Metric_ID"; } }
		public string FIELD_METRIC_PERSPECTIVE_ID { get { return "Metric_Perspective_ID"; } }
		public string FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID { get { return "Company_Department_Job_Role_ID"; } }
		public string FIELD_KPI { get { return "Kpi"; } }
		public string FIELD_MEASURE { get { return "Measure"; } }
        public string FIELD_DATA_SOURCE { get { return "Data_Source"; } }
        public string FIELD_RESPONSIBLE_DEPARTMENT_ID { get { return "Rsponsible_Department_ID"; } }
        public string FIELD_TARGET { get { return "Target"; } }
        public string FIELD_SCORE { get { return "Score"; } }
        public string FIELD_RATING { get { return "Rating"; } }
        public string FIELD_PERIOD_ID { get { return "Period_Id"; } }
        
      
        //[Data_Source] [nchar](10) NULL,
        //[Rsponsible_Department_ID] [nchar](3) NULL,
        //[Target] [decimal](3, 2) NULL,
        //[Score] 

		#endregion

		//Table name declarations for Metric in the database, this will be used for dataset reference
		public string METRIC_TABLE_NAME  = "METRIC";

		//==========================================================================================
		//public MetricDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region MetricDb Class Methods 

        //public long InsertMetric(DataSet dsAuditItem, int metricId, int metricPerspectiveId, int companyDepartmentJobRoleId, string kpi, string measure, string dataSource, string rsponsibleDepartmentId, decimal target, decimal score, int periodId, Transaction transaction)
        public long InsertMetric(int metricPerspectiveId, int companyDepartmentJobRoleId, string kpi, string measure, string dataSource, string rsponsibleDepartmentId, decimal target, decimal score, int periodId, Transaction transaction)
        {
            //const string METHOD_NAME  = "InsertMetric";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeOutputParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE));
                param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_ID_NAME, PARAM_METRIC_PERSPECTIVE_ID_TYPE, PARAM_METRIC_PERSPECTIVE_ID_SIZE, metricPerspectiveId));
                param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
                param.Add(MakeParam(PARAM_KPI_NAME, PARAM_KPI_TYPE, PARAM_KPI_SIZE, kpi));
                param.Add(MakeParam(PARAM_MEASURE_NAME, PARAM_MEASURE_TYPE, PARAM_MEASURE_SIZE, measure));
                param.Add(MakeParam(PARAM_DATA_SOURCE_NAME, PARAM_DATA_SOURCE_TYPE, PARAM_DATA_SOURCE_SIZE, dataSource));
                param.Add(MakeParam(PARAM_RSPONSIBLE_DEPARTMENT_ID_NAME, PARAM_RSPONSIBLE_DEPARTMENT_ID_TYPE, PARAM_RSPONSIBLE_DEPARTMENT_ID_SIZE, rsponsibleDepartmentId));
                param.Add(MakeParam(PARAM_TARGET_NAME, PARAM_TARGET_TYPE, PARAM_TARGET_SIZE, target));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                return Convert.ToInt64(ExecuteProcWithOutputParam(STP_METRIC_INSERTMETRIC, param, transaction));
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID(int metricPerspectiveId, int companyDepartmentJobRoleId, int periodId)
        {
            //const string METHOD_NAME  = "SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                //param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metiricId));
                param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_ID_NAME, PARAM_METRIC_PERSPECTIVE_ID_TYPE, PARAM_METRIC_PERSPECTIVE_ID_SIZE, metricPerspectiveId));
                param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
                
                //Execute Stored Procedure
                return ExecuteDataset(STP_METRIC_SELECTMETRICBYMETRIC_PERSECTIVE_IDANDCOMPANY_DEPARTMENT_JOB_ROLE_ID_AND_PERIOD_ID, param, METRIC_TABLE_NAME);
                //return ExecuteDataset(STP_METRIC_SELECTMETRICBYMETRIC_PERSECTIVE_IDANDCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, METRIC_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectMetricRatingByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID(long metricId, int metricPerspectiveId, int companyDepartmentJobRoleId, int periodId)
        {
            //const string METHOD_NAME  = "SelectMetricRatingByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));
                param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_ID_NAME, PARAM_METRIC_PERSPECTIVE_ID_TYPE, PARAM_METRIC_PERSPECTIVE_ID_SIZE, metricPerspectiveId));
                param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_METRIC_RATINGSELECTMETRIC_RATINGBYMETRIC_PERSECTIVE_IDANDCOMPANY_DEPARTMENT_JOB_ROLE_ID_AND_PERIOD_ID, param, METRIC_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndAppraisalHeaderId(int metricPerspectiveId, int companyDepartmentJobRoleId, long appraisalHeaderId)
        {
            //const string METHOD_NAME  = "SelectMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndAppraisalHeaderId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                //param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metiricId));
                param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_ID_NAME, PARAM_METRIC_PERSPECTIVE_ID_TYPE, PARAM_METRIC_PERSPECTIVE_ID_SIZE, metricPerspectiveId));
                param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_VW_STAFF_METRICS_SELECTVW_STAFF_METRICSBYMETRIC_PERSECTIVE_IDANDCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, METRIC_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }
        
        public DataSet SelectMetricTotalCountByCompanyDepartmentJobRoleIdAndPeriodID(int companyDepartmentJobRoleId, int periodId)
        {
            //const string METHOD_NAME  = "SelectMetricTotalCountByCompanyDepartmentJobRoleIdAndPeriod";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_METRIC_SELECTMETRIC_TOTAL_COUNTBYCOMPANY_DEPARTMENT_JOB_ROLE_ID_AND_PERIOD_ID, param, METRIC_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectMetricByPeriodID(int periodId)
        {
            //const string METHOD_NAME  = "SelectMetricByPeriodID";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_METRIC_SELECTMETRIC_BY_PERIOD_ID, param, METRIC_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public bool InsertOtherEntitiesForNewAppraisal(int oldPeriodId, int newPeriodId, Transaction transaction)
        {
            //const string METHOD_NAME  = "InsertOtherEntitiesForNewAppraisal";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_OLD_PERIOD_ID_NAME, PARAM_OLD_PERIOD_ID_TYPE, PARAM_OLD_PERIOD_ID_SIZE, oldPeriodId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, newPeriodId));
                
                //return ExecuteProc(STP_APPRAISAL_CREATE_NEW_APPRAISAL, param, transaction);

                //Execute Stored Procedure
                if (ExecuteProc(STP_APPRAISAL_CREATE_NEW_APPRAISAL, param, transaction) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
		#endregion

	}
}


