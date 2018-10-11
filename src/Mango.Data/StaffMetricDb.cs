//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:32

//Description: This Class represents the data tier layer class for StaffMetric table.
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
    public class StaffMetricDb : DataAccess.DataAccess, IStaffMetricDb
    {
        private const string CLASS_NAME = "StaffMetricDb";

        //==========================================================================================
        //Db Stored Procedures declaration
        //==========================================================================================
        #region  StaffMetric Stored Procedure declaration

        private const string STP_STAFF_METRIC_INSERTSTAFF_METRIC = "STP_STAFF_METRIC_INSERTSTAFF_METRIC";
        private const string STP_STAFF_METRIC_DELETESTAFF_METRICBYSTAFF_METRIC_ID = "STP_STAFF_METRIC_DELETESTAFF_METRICBYSTAFF_METRIC_ID";
        private const string STP_STAFF_METRIC_DELETESTAFF_METRICBYAPPRAISAL_HEADER_ID = "STP_STAFF_METRIC_DELETESTAFF_METRICBYAPPRAISAL_HEADER_ID";
        private const string STP_STAFF_METRIC_DELETESTAFF_METRICBYMETRIC_ID = "STP_STAFF_METRIC_DELETESTAFF_METRICBYMETRIC_ID";
        private const string STP_STAFF_METRIC_DELETESTAFF_METRICBYStaff_Metric_IDANDAppraisal_Header_IDANDMetric_ID = "STP_STAFF_METRIC_DELETESTAFF_METRICBYStaff_Metric_IDANDAppraisal_Header_IDANDMetric_ID";
        private const string STP_STAFF_METRIC_UPDATESTAFF_METRICBYSTAFF_METRIC_ID = "STP_STAFF_METRIC_UPDATESTAFF_METRICBYSTAFF_METRIC_ID";
        private const string STP_STAFF_METRIC_UPDATESTAFF_METRICBYAPPRAISAL_HEADER_ID = "STP_STAFF_METRIC_UPDATESTAFF_METRICBYAPPRAISAL_HEADER_ID";
        private const string STP_STAFF_METRIC_UPDATESTAFF_METRICBYMETRIC_ID = "STP_STAFF_METRIC_UPDATESTAFF_METRICBYMETRIC_ID";
        private const string STP_STAFF_METRIC_UPDATESTAFF_METRICBYStaff_Metric_IDANDAppraisal_Header_IDANDMetric_ID = "STP_STAFF_METRIC_UPDATESTAFF_METRICBYStaff_Metric_IDANDAppraisal_Header_IDANDMetric_ID";
        private const string STP_STAFF_METRIC_SELECTALLSTAFF_METRIC = "STP_STAFF_METRIC_SELECTALLSTAFF_METRIC";
        private const string STP_STAFF_METRIC_SELECTSTAFF_METRICBYSTAFF_METRIC_ID = "STP_STAFF_METRIC_SELECTSTAFF_METRICBYSTAFF_METRIC_ID";
        private const string STP_STAFF_METRIC_SELECTSTAFF_METRICBYAPPRAISAL_HEADER_ID = "STP_STAFF_METRIC_SELECTSTAFF_METRICBYAPPRAISAL_HEADER_ID";
        private const string STP_STAFF_METRIC_SELECTSTAFF_METRICBYMETRIC_ID = "STP_STAFF_METRIC_SELECTSTAFF_METRICBYMETRIC_ID";
        private const string STP_STAFF_METRIC_SELECTSTAFF_METRICBYStaff_Metric_IDANDAppraisal_Header_IDANDMetric_ID = "STP_STAFF_METRIC_SELECTSTAFF_METRICBYStaff_Metric_IDANDAppraisal_Header_IDANDMetric_ID";

        #endregion

        //==========================================================================================
        //Db Configuration properties
        //==========================================================================================
        #region StaffMetric Parameter declaration

        //Parameter decleration for STAFF_METRIC_ID
        private const string PARAM_STAFF_METRIC_ID_NAME = "@StaffMetricID";
        private const SqlDbType PARAM_STAFF_METRIC_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_STAFF_METRIC_ID_SIZE = 8;

        //Parameter decleration for APPRAISAL_HEADER_ID
        private const string PARAM_APPRAISAL_HEADER_ID_NAME = "@AppraisalHeaderID";
        private const SqlDbType PARAM_APPRAISAL_HEADER_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_APPRAISAL_HEADER_ID_SIZE = 8;

        //Parameter decleration for METRIC_ID
        private const string PARAM_METRIC_ID_NAME = "@MetricID";
        private const SqlDbType PARAM_METRIC_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_METRIC_ID_SIZE = 8;

        //Parameter decleration for SCORE
        private const string PARAM_SCORE_NAME = "@Score";
        private const SqlDbType PARAM_SCORE_TYPE = SqlDbType.Decimal;
        private const int PARAM_SCORE_SIZE = 10;

        #endregion

        //==========================================================================================
        //StaffMetric Table Field Name Declaration
        //==========================================================================================
        #region StaffMetric Field Name declaration

        public string FIELD_STAFF_METRIC_ID { get { return "Staff_Metric_ID"; } }
        public string FIELD_APPRAISAL_HEADER_ID { get { return "Appraisal_Header_ID"; } }
        public string FIELD_METRIC_ID { get { return "Metric_ID"; } }
        public string FIELD_SCORE { get { return "Score"; } }

        #endregion

        //Table name declarations for StaffMetric in the database, this will be used for dataset reference
        public string STAFFMETRIC_TABLE_NAME = "STAFFMETRIC";

        //==========================================================================================
        //public StaffMetricDb Class Method declarations that will be called from the Biz Tier
        //==========================================================================================
        #region StaffMetricDb Class Methods

        public int InsertStaffMetric(long appraisalHeaderId, long metricId, decimal score, Transaction transaction)
        {
            //const string METHOD_NAME  = "InsertStaffMetric";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeOutputParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));

                //Execute Stored Procedure
                return Convert.ToInt32(ExecuteProcWithOutputParam(STP_STAFF_METRIC_INSERTSTAFF_METRIC, param, transaction));

                ////Execute Stored Procedure
                //if (ExecuteProc(STP_STAFF_METRIC_INSERTSTAFF_METRIC, param, transaction) == 0)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public bool DeleteStaffMetricByStaffMetricId(long staffMetricId, Transaction transaction)
        {
            //const string METHOD_NAME  = "DeleteStaffMetricByStaffMetricId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_STAFF_METRIC_DELETESTAFF_METRICBYSTAFF_METRIC_ID, param, transaction) == 0)
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

        public bool DeleteStaffMetricByAppraisalHeaderId(long appraisalHeaderId, Transaction transaction)
        {
            //const string METHOD_NAME  = "DeleteStaffMetricByAppraisalHeaderId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_STAFF_METRIC_DELETESTAFF_METRICBYAPPRAISAL_HEADER_ID, param, transaction) == 0)
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

        public bool DeleteStaffMetricByMetricId(long metricId, Transaction transaction)
        {
            //const string METHOD_NAME  = "DeleteStaffMetricByMetricId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_STAFF_METRIC_DELETESTAFF_METRICBYMETRIC_ID, param, transaction) == 0)
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

        public bool DeleteStaffMetricByStaff_Metric_IDAndAppraisal_Header_IDAndMetric_ID(long staffMetricId, long appraisalHeaderId, long metricId, Transaction transaction)
        {
            //const string METHOD_NAME  = "DeleteStaffMetricByStaff_Metric_IDAndAppraisal_Header_IDAndMetric_ID";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_STAFF_METRIC_DELETESTAFF_METRICBYStaff_Metric_IDANDAppraisal_Header_IDANDMetric_ID, param, transaction) == 0)
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

        public bool UpdateStaffMetricByStaffMetricId(long staffMetricId, long appraisalHeaderId, long metricId, decimal score, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdateStaffMetricByStaffMetricId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));

                //Execute Stored Procedure
                if (ExecuteProc(STP_STAFF_METRIC_UPDATESTAFF_METRICBYSTAFF_METRIC_ID, param, transaction) == 0)
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

        public bool UpdateStaffMetricByAppraisalHeaderId(long staffMetricId, long appraisalHeaderId, long metricId, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdateStaffMetricByAppraisalHeaderId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_STAFF_METRIC_UPDATESTAFF_METRICBYAPPRAISAL_HEADER_ID, param, transaction) == 0)
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

        public bool UpdateStaffMetricByMetricId(long staffMetricId, long appraisalHeaderId, long metricId, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdateStaffMetricByMetricId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_STAFF_METRIC_UPDATESTAFF_METRICBYMETRIC_ID, param, transaction) == 0)
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

        public bool UpdateStaffMetricByStaff_Metric_IDAndAppraisal_Header_IDAndMetric_ID(long staffMetricId, long appraisalHeaderId, long metricId, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdateStaffMetricByStaff_Metric_IDAndAppraisal_Header_IDAndMetric_ID";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_STAFF_METRIC_UPDATESTAFF_METRICBYStaff_Metric_IDANDAppraisal_Header_IDANDMetric_ID, param, transaction) == 0)
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

        public DataSet SelectAllStaffMetric()
        {
            //const string METHOD_NAME  = "SelectAllStaffMetric";

            try
            {
                //Execute Stored Procedure
                return ExecuteDataset(STP_STAFF_METRIC_SELECTALLSTAFF_METRIC, null, STAFFMETRIC_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectStaffMetricByStaffMetricId(long staffMetricId)
        {
            //const string METHOD_NAME  = "SelectStaffMetricByStaffMetricId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_STAFF_METRIC_SELECTSTAFF_METRICBYSTAFF_METRIC_ID, param, STAFFMETRIC_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectStaffMetricByAppraisalHeaderId(long appraisalHeaderId)
        {
            //const string METHOD_NAME  = "SelectStaffMetricByAppraisalHeaderId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_STAFF_METRIC_SELECTSTAFF_METRICBYAPPRAISAL_HEADER_ID, param, STAFFMETRIC_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectStaffMetricByMetricId(long metricId)
        {
            //const string METHOD_NAME  = "SelectStaffMetricByMetricId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_STAFF_METRIC_SELECTSTAFF_METRICBYMETRIC_ID, param, STAFFMETRIC_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectStaffMetricByStaff_Metric_IDAndAppraisal_Header_IDAndMetric_ID(long staffMetricId, long appraisalHeaderId, long metricId)
        {
            //const string METHOD_NAME  = "SelectStaffMetricByStaff_Metric_IDAndAppraisal_Header_IDAndMetric_ID";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_METRIC_ID_NAME, PARAM_METRIC_ID_TYPE, PARAM_METRIC_ID_SIZE, metricId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_STAFF_METRIC_SELECTSTAFF_METRICBYStaff_Metric_IDANDAppraisal_Header_IDANDMetric_ID, param, STAFFMETRIC_TABLE_NAME);
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


