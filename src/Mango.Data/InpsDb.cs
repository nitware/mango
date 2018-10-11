using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Collections;
using Mango.Data.Interfaces;
using Mango.DataAccess;

namespace Mango.Data
{
    public class InpsDb : DataAccess.DataAccess, IInpsDb
    {
        private const string CLASS_NAME = "NpsDb";

        //==========================================================================================
        //Db Stored Procedures declaration
        //==========================================================================================
        #region  Nps Stored Procedure declaration

        private const string STP_NPS_INSERTNPS = "STP_NPS_INSERTNPS";
        private const string STP_METRIC_NPS_UPDATENPS_SCOREBYNPS_ID = "STP_METRIC_NPS_UPDATENPS_SCOREBYNPS_ID";
        private const string STP_INPS_SELECTNPSBYSTAFF_ID_AND_PERIOD_ID = "STP_INPS_SELECTNPSBYSTAFF_ID_AND_PERIOD_ID";

        #endregion

        //==========================================================================================
        //Db Configuration properties
        //==========================================================================================
        #region Nps Parameter declaration

        //Parameter decleration for METRIC_NPS_ID
        private const string PARAM_INPS_ID_NAME = "@InpsID";
        private const SqlDbType PARAM_INPS_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_INPS_ID_SIZE = 8;

        //Parameter decleration for METRIC_PERSPECTIVE_ID
        private const string PARAM_METRIC_PERSPECTIVE_ID_NAME = "@MetricPerspectiveID";
        private const SqlDbType PARAM_METRIC_PERSPECTIVE_ID_TYPE = SqlDbType.Int;
        private const int PARAM_METRIC_PERSPECTIVE_ID_SIZE = 4;

        //Parameter decleration for STAFF_ID
        private const string PARAM_STAFF_ID_NAME = "@StaffID";
        private const SqlDbType PARAM_STAFF_ID_TYPE = SqlDbType.NChar;
        private const int PARAM_STAFF_ID_SIZE = 10;

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

        //Parameter decleration for PERIOD_ID
        private const string PARAM_PERIOD_ID_NAME = "@PeriodID";
        private const SqlDbType PARAM_PERIOD_ID_TYPE = SqlDbType.Int;
        private const int PARAM_PERIOD_ID_SIZE = 4;
        
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

        //Parameter decleration for RESPONSIBLE_DEPARTMENT_ID
        private const string PARAM_RESPONSIBLE_DEPARTMENT_ID_NAME = "@ResponsibleDepartmentID";
        private const SqlDbType PARAM_RESPONSIBLE_DEPARTMENT_ID_TYPE = SqlDbType.NChar;
        private const int PARAM_RESPONSIBLE_DEPARTMENT_ID_SIZE = 3;

        //Parameter decleration for TARGET
        private const string PARAM_TARGET_NAME = "@Target";
        private const SqlDbType PARAM_TARGET_TYPE = SqlDbType.Decimal;
        private const int PARAM_TARGET_SIZE = 5;

        //Parameter decleration for SCORE
        private const string PARAM_SCORE_NAME = "@Score";
        private const SqlDbType PARAM_SCORE_TYPE = SqlDbType.Decimal;
        private const int PARAM_SCORE_SIZE = 5;
        
        #endregion

        //==========================================================================================
        //Metric Table Field Name Declaration
        //==========================================================================================
        #region Nps Field Name declaration

        //public string FIELD_STAFF_METRIC_ID { get { return "Staff_Metric_ID"; } }
        public string FIELD_INPS_ID { get { return "Inps_ID"; } }
        public string FIELD_METRIC_PERSPECTIVE_ID { get { return "Metric_Perspective_ID"; } }
        public string FIELD_STAFF_ID { get { return "Staff_ID"; } }
        public string FIELD_KPI { get { return "Kpi"; } }
        public string FIELD_MEASURE { get { return "Measure"; } }
        public string FIELD_DATA_SOURCE { get { return "Data_Source"; } }
        public string FIELD_RESPONSIBLE_DEPARTMENT_ID { get { return "Responsible_Department_ID"; } }
        public string FIELD_TARGET { get { return "Target"; } }
        public string FIELD_SCORE { get { return "Score"; } }
        public string FIELD_RATING { get { return "Rating"; } }
        public string FIELD_PERIOD_ID { get { return "Period_Id"; } }

        #endregion

        //Table name declarations for Metric in the database, this will be used for dataset reference
        public string TABLE_NAME = "INPS";

        //==========================================================================================
        //public NpsDb Class Method declarations that will be called from the Biz Tier
        //==========================================================================================
        #region NpsDb Class Methods

        public long InsertInps(int metricPerspectiveId, string staffId, string kpi, string measure, string dataSource, string responsibleDepartmentId, decimal target, decimal score, int periodId, Transaction transaction)
        {
            //const string METHOD_NAME  = "InsertNps";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeOutputParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE));
                param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_ID_NAME, PARAM_METRIC_PERSPECTIVE_ID_TYPE, PARAM_METRIC_PERSPECTIVE_ID_SIZE, metricPerspectiveId));
                param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
                param.Add(MakeParam(PARAM_KPI_NAME, PARAM_KPI_TYPE, PARAM_KPI_SIZE, kpi));
                param.Add(MakeParam(PARAM_MEASURE_NAME, PARAM_MEASURE_TYPE, PARAM_MEASURE_SIZE, measure));
                param.Add(MakeParam(PARAM_DATA_SOURCE_NAME, PARAM_DATA_SOURCE_TYPE, PARAM_DATA_SOURCE_SIZE, dataSource));
                param.Add(MakeParam(PARAM_RESPONSIBLE_DEPARTMENT_ID_NAME, PARAM_RESPONSIBLE_DEPARTMENT_ID_TYPE, PARAM_RESPONSIBLE_DEPARTMENT_ID_SIZE, responsibleDepartmentId));
                param.Add(MakeParam(PARAM_TARGET_NAME, PARAM_TARGET_TYPE, PARAM_TARGET_SIZE, target));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                return Convert.ToInt64(ExecuteProcWithOutputParam(STP_NPS_INSERTNPS, param, transaction));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateInpsScoreByNpsId(long npsId, decimal score, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdateInpsScoreByNpsId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE, npsId));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));
                               
                if (ExecuteProc(STP_METRIC_NPS_UPDATENPS_SCOREBYNPS_ID, param, transaction) == 0)
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

        public DataSet SelectDefaultInpsByStaffIDAndPeriodID(string staffId, int periodId)
        {
            //const string METHOD_NAME  = "SelectDefaultNpsByStaffIDAndPeriodID";

            try
            {
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
                
                DataSet ds = ExecuteDataset(STP_INPS_SELECTNPSBYSTAFF_ID_AND_PERIOD_ID, param, TABLE_NAME);
                //return ExecuteDataset(STP_INPS_SELECTNPSBYSTAFF_ID_AND_PERIOD_ID, param, TABLE_NAME);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
               
        #endregion
    }




    

}
