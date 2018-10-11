//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:32

//Description: This Class represents the data tier layer class for StaffInps table.
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
    public class StaffInpsDb : DataAccess.DataAccess, IStaffInpsDb
    {
        private const string CLASS_NAME = "StaffInpsDb";

        //==========================================================================================
        //Db Stored Procedures declaration
        //==========================================================================================
        #region  StaffInps Stored Procedure declaration

        private const string STP_STAFF_INPS_INSERTSTAFF_INPS = "STP_STAFF_INPS_INSERTSTAFF_INPS";

        //private const string STP_STAFF_INPS_DELETESTAFF_INPSBYSTAFF_INPS_ID = "STP_STAFF_INPS_DELETESTAFF_INPSBYSTAFF_INPS_ID";
        //private const string STP_STAFF_INPS_DELETESTAFF_INPSBYAPPRAISAL_HEADER_ID = "STP_STAFF_INPS_DELETESTAFF_INPSBYAPPRAISAL_HEADER_ID";
        //private const string STP_STAFF_INPS_DELETESTAFF_INPSBYINPS_ID = "STP_STAFF_INPS_DELETESTAFF_INPSBYINPS_ID";
        //private const string STP_STAFF_INPS_DELETESTAFF_INPSBYStaff_Inps_IDANDAppraisal_Header_IDANDInps_ID = "STP_STAFF_INPS_DELETESTAFF_INPSBYStaff_Inps_IDANDAppraisal_Header_IDANDInps_ID";
        //private const string STP_STAFF_INPS_UPDATESTAFF_INPSBYSTAFF_INPS_ID = "STP_STAFF_INPS_UPDATESTAFF_INPSBYSTAFF_INPS_ID";
        //private const string STP_STAFF_INPS_UPDATESTAFF_INPSBYAPPRAISAL_HEADER_ID = "STP_STAFF_INPS_UPDATESTAFF_INPSBYAPPRAISAL_HEADER_ID";
        //private const string STP_STAFF_INPS_UPDATESTAFF_INPSBYINPS_ID = "STP_STAFF_INPS_UPDATESTAFF_INPSBYINPS_ID";
        //private const string STP_STAFF_INPS_UPDATESTAFF_INPSBYStaff_Inps_IDANDAppraisal_Header_IDANDInps_ID = "STP_STAFF_INPS_UPDATESTAFF_INPSBYStaff_Inps_IDANDAppraisal_Header_IDANDInps_ID";
        //private const string STP_STAFF_INPS_SELECTALLSTAFF_INPS = "STP_STAFF_INPS_SELECTALLSTAFF_INPS";
        //private const string STP_STAFF_INPS_SELECTSTAFF_INPSBYSTAFF_INPS_ID = "STP_STAFF_INPS_SELECTSTAFF_INPSBYSTAFF_INPS_ID";
        //private const string STP_STAFF_INPS_SELECTSTAFF_INPSBYAPPRAISAL_HEADER_ID = "STP_STAFF_INPS_SELECTSTAFF_INPSBYAPPRAISAL_HEADER_ID";
        //private const string STP_STAFF_INPS_SELECTSTAFF_INPSBYINPS_ID = "STP_STAFF_INPS_SELECTSTAFF_INPSBYINPS_ID";
        //private const string STP_STAFF_INPS_SELECTSTAFF_INPSBYStaff_Inps_IDANDAppraisal_Header_IDANDInps_ID = "STP_STAFF_INPS_SELECTSTAFF_INPSBYStaff_Inps_IDANDAppraisal_Header_IDANDInps_ID";

        #endregion

        //==========================================================================================
        //Db Configuration properties
        //==========================================================================================
        #region StaffInps Parameter declaration

        //Parameter decleration for STAFF_INPS_ID
        private const string PARAM_STAFF_INPS_ID_NAME = "@StaffInpsID";
        private const SqlDbType PARAM_STAFF_INPS_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_STAFF_INPS_ID_SIZE = 8;

        //Parameter decleration for STAFF_METRIC_ID
        private const string PARAM_STAFF_METRIC_ID_NAME = "@StaffMetricID";
        private const SqlDbType PARAM_STAFF_METRIC_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_STAFF_METRIC_ID_SIZE = 8;

        //Parameter decleration for APPRAISAL_HEADER_ID
        private const string PARAM_APPRAISAL_HEADER_ID_NAME = "@AppraisalHeaderID";
        private const SqlDbType PARAM_APPRAISAL_HEADER_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_APPRAISAL_HEADER_ID_SIZE = 8;

        //Parameter decleration for INPS_ID
        private const string PARAM_INPS_ID_NAME = "@InpsID";
        private const SqlDbType PARAM_INPS_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_INPS_ID_SIZE = 8;

        //Parameter decleration for SCORE
        private const string PARAM_SCORE_NAME = "@Score";
        private const SqlDbType PARAM_SCORE_TYPE = SqlDbType.Decimal;
        private const int PARAM_SCORE_SIZE = 10;

        #endregion

        //==========================================================================================
        //StaffInps Table Field Name Declaration
        //==========================================================================================
        #region StaffInps Field Name declaration

        public string FIELD_STAFF_INPS_ID { get { return "Staff_Inps_ID"; } }
        public string FIELD_STAFF_METRIC_ID { get { return "Staff_Metric_ID"; } }
        public string FIELD_APPRAISAL_HEADER_ID { get { return "Appraisal_Header_ID"; } }
        public string FIELD_INPS_ID { get { return "Inps_ID"; } }
        public string FIELD_SCORE { get { return "Score"; } }

        #endregion

        //Table name declarations for StaffInps in the database, this will be used for dataset reference
        public string STAFFINPS_TABLE_NAME = "STAFFINPS";

        //==========================================================================================
        //public StaffInpsDb Class Method declarations that will be called from the Biz Tier
        //==========================================================================================
        #region StaffInpsDb Class Methods

        public int InsertStaffInps(long appraisalHeaderId, long InpsId, decimal score, Transaction transaction)
        {
            //const string METHOD_NAME  = "InsertStaffInps";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeOutputParam(PARAM_STAFF_INPS_ID_NAME, PARAM_STAFF_INPS_ID_TYPE, PARAM_STAFF_INPS_ID_SIZE));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE, InpsId));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));

                //Execute Stored Procedure
                return Convert.ToInt32(ExecuteProcWithOutputParam(STP_STAFF_INPS_INSERTSTAFF_INPS, param, transaction));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool DeleteStaffInpsByStaffInpsId(long staffInpsId, Transaction transaction)
        //{
        //    //const string METHOD_NAME  = "DeleteStaffInpsByStaffInpsId";

        //    try
        //    {
        //        //Make parameter(s)
        //        ArrayList param = new ArrayList();
        //        param.Add(MakeParam(PARAM_STAFF_INPS_ID_NAME, PARAM_STAFF_INPS_ID_TYPE, PARAM_STAFF_INPS_ID_SIZE, staffInpsId));

        //        //Execute Stored Procedure
        //        if (ExecuteProc(STP_STAFF_INPS_DELETESTAFF_INPSBYSTAFF_INPS_ID, param, transaction) == 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public bool DeleteStaffInpsByAppraisalHeaderId(long appraisalHeaderId, Transaction transaction)
        //{
        //    //const string METHOD_NAME  = "DeleteStaffInpsByAppraisalHeaderId";

        //    try
        //    {
        //        //Make parameter(s)
        //        ArrayList param = new ArrayList();
        //        param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

        //        //Execute Stored Procedure
        //        if (ExecuteProc(STP_STAFF_INPS_DELETESTAFF_INPSBYAPPRAISAL_HEADER_ID, param, transaction) == 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public bool DeleteStaffInpsByInpsId(long InpsId, Transaction transaction)
        //{
        //    //const string METHOD_NAME  = "DeleteStaffInpsByInpsId";

        //    try
        //    {
        //        //Make parameter(s)
        //        ArrayList param = new ArrayList();
        //        param.Add(MakeParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE, InpsId));

        //        //Execute Stored Procedure
        //        if (ExecuteProc(STP_STAFF_INPS_DELETESTAFF_INPSBYINPS_ID, param, transaction) == 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public bool DeleteStaffInpsByStaff_Inps_IDAndAppraisal_Header_IDAndInps_ID(long staffInpsId, long appraisalHeaderId, long InpsId, Transaction transaction)
        //{
        //    //const string METHOD_NAME  = "DeleteStaffInpsByStaff_Inps_IDAndAppraisal_Header_IDAndInps_ID";

        //    try
        //    {
        //        //Make parameter(s)
        //        ArrayList param = new ArrayList();
        //        param.Add(MakeParam(PARAM_STAFF_INPS_ID_NAME, PARAM_STAFF_INPS_ID_TYPE, PARAM_STAFF_INPS_ID_SIZE, staffInpsId));
        //        param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
        //        param.Add(MakeParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE, InpsId));

        //        //Execute Stored Procedure
        //        if (ExecuteProc(STP_STAFF_INPS_DELETESTAFF_INPSBYStaff_Inps_IDANDAppraisal_Header_IDANDInps_ID, param, transaction) == 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public bool UpdateStaffInpsByStaffInpsId(long staffInpsId, long appraisalHeaderId, long InpsId, decimal score, Transaction transaction)
        //{
        //    //const string METHOD_NAME  = "UpdateStaffInpsByStaffInpsId";

        //    try
        //    {
        //        //Make parameter(s)
        //        ArrayList param = new ArrayList();
        //        param.Add(MakeParam(PARAM_STAFF_INPS_ID_NAME, PARAM_STAFF_INPS_ID_TYPE, PARAM_STAFF_INPS_ID_SIZE, staffInpsId));
        //        param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
        //        param.Add(MakeParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE, InpsId));
        //        param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));

        //        //Execute Stored Procedure
        //        if (ExecuteProc(STP_STAFF_INPS_UPDATESTAFF_INPSBYSTAFF_INPS_ID, param, transaction) == 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public bool UpdateStaffInpsByAppraisalHeaderId(long staffInpsId, long appraisalHeaderId, long InpsId, Transaction transaction)
        //{
        //    //const string METHOD_NAME  = "UpdateStaffInpsByAppraisalHeaderId";

        //    try
        //    {
        //        //Make parameter(s)
        //        ArrayList param = new ArrayList();
        //        param.Add(MakeParam(PARAM_STAFF_INPS_ID_NAME, PARAM_STAFF_INPS_ID_TYPE, PARAM_STAFF_INPS_ID_SIZE, staffInpsId));
        //        param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
        //        param.Add(MakeParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE, InpsId));

        //        //Execute Stored Procedure
        //        if (ExecuteProc(STP_STAFF_INPS_UPDATESTAFF_INPSBYAPPRAISAL_HEADER_ID, param, transaction) == 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public bool UpdateStaffInpsByInpsId(long staffInpsId, long appraisalHeaderId, long InpsId, Transaction transaction)
        //{
        //    //const string METHOD_NAME  = "UpdateStaffInpsByInpsId";

        //    try
        //    {
        //        //Make parameter(s)
        //        ArrayList param = new ArrayList();
        //        param.Add(MakeParam(PARAM_STAFF_INPS_ID_NAME, PARAM_STAFF_INPS_ID_TYPE, PARAM_STAFF_INPS_ID_SIZE, staffInpsId));
        //        param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
        //        param.Add(MakeParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE, InpsId));

        //        //Execute Stored Procedure
        //        if (ExecuteProc(STP_STAFF_INPS_UPDATESTAFF_INPSBYINPS_ID, param, transaction) == 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public bool UpdateStaffInpsByStaff_Inps_IDAndAppraisal_Header_IDAndInps_ID(long staffInpsId, long appraisalHeaderId, long InpsId, Transaction transaction)
        //{
        //    //const string METHOD_NAME  = "UpdateStaffInpsByStaff_Inps_IDAndAppraisal_Header_IDAndInps_ID";

        //    try
        //    {
        //        //Make parameter(s)
        //        ArrayList param = new ArrayList();
        //        param.Add(MakeParam(PARAM_STAFF_INPS_ID_NAME, PARAM_STAFF_INPS_ID_TYPE, PARAM_STAFF_INPS_ID_SIZE, staffInpsId));
        //        param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
        //        param.Add(MakeParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE, InpsId));

        //        //Execute Stored Procedure
        //        if (ExecuteProc(STP_STAFF_INPS_UPDATESTAFF_INPSBYStaff_Inps_IDANDAppraisal_Header_IDANDInps_ID, param, transaction) == 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public DataSet SelectAllStaffInps()
        //{
        //    //const string METHOD_NAME  = "SelectAllStaffInps";

        //    try
        //    {
        //        //Execute Stored Procedure
        //        return ExecuteDataset(STP_STAFF_INPS_SELECTALLSTAFF_INPS, null, STAFFINPS_TABLE_NAME);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public DataSet SelectStaffInpsByStaffInpsId(long staffInpsId)
        //{
        //    //const string METHOD_NAME  = "SelectStaffInpsByStaffInpsId";

        //    try
        //    {
        //        //Method parameter declaration
        //        ArrayList param = new ArrayList();

        //        param.Add(MakeParam(PARAM_STAFF_INPS_ID_NAME, PARAM_STAFF_INPS_ID_TYPE, PARAM_STAFF_INPS_ID_SIZE, staffInpsId));

        //        //Execute Stored Procedure
        //        return ExecuteDataset(STP_STAFF_INPS_SELECTSTAFF_INPSBYSTAFF_INPS_ID, param, STAFFINPS_TABLE_NAME);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public DataSet SelectStaffInpsByAppraisalHeaderId(long appraisalHeaderId)
        //{
        //    //const string METHOD_NAME  = "SelectStaffInpsByAppraisalHeaderId";

        //    try
        //    {
        //        //Method parameter declaration
        //        ArrayList param = new ArrayList();

        //        param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

        //        //Execute Stored Procedure
        //        return ExecuteDataset(STP_STAFF_INPS_SELECTSTAFF_INPSBYAPPRAISAL_HEADER_ID, param, STAFFINPS_TABLE_NAME);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public DataSet SelectStaffInpsByInpsId(long InpsId)
        //{
        //    //const string METHOD_NAME  = "SelectStaffInpsByInpsId";

        //    try
        //    {
        //        //Method parameter declaration
        //        ArrayList param = new ArrayList();

        //        param.Add(MakeParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE, InpsId));

        //        //Execute Stored Procedure
        //        return ExecuteDataset(STP_STAFF_INPS_SELECTSTAFF_INPSBYINPS_ID, param, STAFFINPS_TABLE_NAME);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public DataSet SelectStaffInpsByStaff_Inps_IDAndAppraisal_Header_IDAndInps_ID(long staffInpsId, long appraisalHeaderId, long InpsId)
        //{
        //    //const string METHOD_NAME  = "SelectStaffInpsByStaff_Inps_IDAndAppraisal_Header_IDAndInps_ID";

        //    try
        //    {
        //        //Method parameter declaration
        //        ArrayList param = new ArrayList();

        //        param.Add(MakeParam(PARAM_STAFF_INPS_ID_NAME, PARAM_STAFF_INPS_ID_TYPE, PARAM_STAFF_INPS_ID_SIZE, staffInpsId));
        //        param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
        //        param.Add(MakeParam(PARAM_INPS_ID_NAME, PARAM_INPS_ID_TYPE, PARAM_INPS_ID_SIZE, InpsId));

        //        //Execute Stored Procedure
        //        return ExecuteDataset(STP_STAFF_INPS_SELECTSTAFF_INPSBYStaff_Inps_IDANDAppraisal_Header_IDANDInps_ID, param, STAFFINPS_TABLE_NAME);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        #endregion


    }
}


