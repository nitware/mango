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
    public class MetricRatingDb : DataAccess.DataAccess, IMetricRatingDb
    {
        private const string CLASS_NAME = "MetricRatingDb";
        private const string METRIC_RATING_TABLE_NAME = "METRIC_RATING";

        //==========================================================================================
        //Db Stored Procedures declaration
        //==========================================================================================
        #region  Metric_Rating Stored Procedure declaration

        private const string STP_METRIC_RATING_INSERTMETRIC_RATING = "STP_METRIC_RATING_INSERTMETRIC_RATING";
        private const string STP_METRIC_RATING_SELECTMETRIC_RATINGBYMETRIC_ID_AND_PERIOD_ID = "STP_METRIC_RATING_SELECTMETRIC_RATINGBYMETRIC_ID_AND_PERIOD_ID";
        private const string STP_METRIC_RATING_SELECTMETRIC_RATINGBYPERIOD_ID = "STP_METRIC_RATING_SELECTMETRIC_RATINGBYPERIOD_ID";

        #endregion

        //==========================================================================================
        //Db Configuration properties
        //==========================================================================================
        #region Metric_Rating Parameter declaration

        //Parameter decleration for METRIC
        private const string PARAM_METRIC_ID_NAME = "@MetricID";
        private const SqlDbType PARAM_METRIC_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_METRIC_ID_SIZE = 8;

        //Parameter decleration for RATING_ID
        private const string PARAM_RATING_ID_NAME = "@RatingID";
        private const SqlDbType PARAM_RATING_ID_TYPE = SqlDbType.TinyInt;
        private const int PARAM_RATING_ID_SIZE = 1;

        //Parameter decleration for FROM
        private const string PARAM_FROM_NAME = "@From";
        private const SqlDbType PARAM_FROM_TYPE = SqlDbType.Decimal;
        private const int PARAM_FROM_SIZE = 8;

        //Parameter decleration for TO
        private const string PARAM_TO_NAME = "@To";
        private const SqlDbType PARAM_TO_TYPE = SqlDbType.Decimal;
        private const int PARAM_TO_SIZE = 8;

        //Parameter decleration for RATING_TYPE_ID
        private const string PARAM_RATING_TYPE_ID_NAME = "@RatingTypeID";
        private const SqlDbType PARAM_RATING_TYPE_ID_TYPE = SqlDbType.TinyInt;
        private const int PARAM_RATING_TYPE_ID_SIZE = 1;

        //Parameter decleration for SCORE
        private const string PARAM_PERIOD_ID_NAME = "@PeriodID";
        private const SqlDbType PARAM_PERIOD_ID_TYPE = SqlDbType.Int;
        private const int PARAM_PERIOD_ID_SIZE = 4;
            
        #endregion

        //==========================================================================================
        //Metric_Rating Table Field Name Declaration
        //==========================================================================================
        #region Metric Field Name declaration

        public string FIELD_METRIC_ID { get { return "Metric_ID"; } }
        public string FIELD_RATING_ID { get { return "Rating_ID"; } }
        public string FIELD_FROM { get { return "From"; } }
        public string FIELD_TO { get { return "To"; } }
        public string FIELD_RATING_TYPE_ID { get { return "Rating_Type_ID"; } }
        public string FIELD_PERIOD_ID { get { return "Period_ID"; } }
       
        #endregion

        //==========================================================================================
        //public MetricRatingDb Class Method declarations that will be called from the Biz Tier
        //==========================================================================================
        #region MetricRatingDb Class Methods

        public bool InsertMetricRating(long metricId, byte ratingId, decimal from, decimal to, byte ratingTypeId, int periodId, Transaction transaction)
        {
            //const string METHOD_NAME  = "InsertMetricRating";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));
                param.Add(MakeParam(PARAM_RATING_ID_NAME, PARAM_RATING_ID_TYPE, PARAM_RATING_ID_SIZE, ratingId));
                param.Add(MakeParam(PARAM_FROM_NAME, PARAM_FROM_TYPE, PARAM_FROM_SIZE, from));
                param.Add(MakeParam(PARAM_TO_NAME, PARAM_TO_TYPE, PARAM_TO_SIZE, to));
                param.Add(MakeParam(PARAM_RATING_TYPE_ID_NAME, PARAM_RATING_TYPE_ID_TYPE, PARAM_RATING_TYPE_ID_SIZE, ratingTypeId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_METRIC_RATING_INSERTMETRIC_RATING, param, transaction) == 0)
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
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectMetricRatingByMetricIdAndPeriodID(long metricId, int periodId)
        {
            //const string METHOD_NAME  = "SelectMetricRatingByMetricIdAndPeriodID";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
                
                //Execute Stored Procedure
                return ExecuteDataset(STP_METRIC_RATING_SELECTMETRIC_RATINGBYMETRIC_ID_AND_PERIOD_ID, param, METRIC_RATING_TABLE_NAME);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SelectMetricRatingByPeriodID(int periodId)
        {
            //const string METHOD_NAME  = "SelectMetricRatingByPeriodID";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_METRIC_RATING_SELECTMETRIC_RATINGBYPERIOD_ID, param, METRIC_RATING_TABLE_NAME);

            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        #endregion
    }


}
