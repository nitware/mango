//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 24/07/2011 11:18:31

//Description: This Class represents the data tier layer class for Pace table.
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
    public class PaceDb : DataAccess.DataAccess, IPaceDb
    {
        private const string CLASS_NAME = "PaceDb";

        //==========================================================================================
        //Db Stored Procedures declaration
        //==========================================================================================
        #region  Pace Stored Procedure declaration

        private const string STP_PACE_INSERTPACE = "STP_PACE_INSERTPACE";
        private const string STP_PACE_DELETEPACEBYPACE_ID = "STP_PACE_DELETEPACEBYPACE_ID";
        private const string STP_PACE_DELETEPACEBYPACE_AREA_ID = "STP_PACE_DELETEPACEBYPACE_AREA_ID";
        private const string STP_PACE_DELETEPACEBYAPPRAISAL_HEADER_ID = "STP_PACE_DELETEPACEBYAPPRAISAL_HEADER_ID";
        private const string STP_PACE_DELETEPACEBYPace_IDANDPace_Area_IDANDAppraisal_Header_ID = "STP_PACE_DELETEPACEBYPace_IDANDPace_Area_IDANDAppraisal_Header_ID";
        private const string STP_PACE_UPDATEPACEBYPACE_ID = "STP_PACE_UPDATEPACEBYPACE_ID";
        private const string STP_PACE_UPDATEPACEBYPACE_AREA_ID = "STP_PACE_UPDATEPACEBYPACE_AREA_ID";
        private const string STP_PACE_UPDATEPACEBYAPPRAISAL_HEADER_ID = "STP_PACE_UPDATEPACEBYAPPRAISAL_HEADER_ID";
        private const string STP_PACE_UPDATEPACEBYPace_IDANDPace_Area_IDANDAppraisal_Header_ID = "STP_PACE_UPDATEPACEBYPace_IDANDPace_Area_IDANDAppraisal_Header_ID";
        private const string STP_PACE_SELECTALLPACE = "STP_PACE_SELECTALLPACE";
        private const string STP_PACE_SELECTPACEBYPACE_ID = "STP_PACE_SELECTPACEBYPACE_ID";
        private const string STP_PACE_SELECTPACEBYPACE_AREA_ID = "STP_PACE_SELECTPACEBYPACE_AREA_ID";
        private const string STP_PACE_SELECTPACEBYAPPRAISAL_HEADER_ID = "STP_PACE_SELECTPACEBYAPPRAISAL_HEADER_ID";
        private const string STP_PACE_SELECTPACEBYPace_IDANDPace_Area_IDANDAppraisal_Header_ID = "STP_PACE_SELECTPACEBYPace_IDANDPace_Area_IDANDAppraisal_Header_ID";


        private const string STP_PACE_SELECTPACEBYSTAFF_IDANDPERIOD_ID = "STP_PACE_SELECTPACEBYSTAFF_IDANDPERIOD_ID";
        private const string STP_PACE_SELECTDEFAULTPACE = "STP_PACE_SELECTDEFAULTPACE";

        #endregion

        //==========================================================================================
        //Db Configuration properties
        //==========================================================================================
        #region Pace Parameter declaration

        //Parameter decleration for PACE_ID
        private const string PARAM_PACE_ID_NAME = "@PaceID";
        private const SqlDbType PARAM_PACE_ID_TYPE = SqlDbType.Int;
        private const int PARAM_PACE_ID_SIZE = 4;

        //Parameter decleration for PACE_AREA_ID
        private const string PARAM_PACE_AREA_ID_NAME = "@PaceAreaID";
        private const SqlDbType PARAM_PACE_AREA_ID_TYPE = SqlDbType.Int;
        private const int PARAM_PACE_AREA_ID_SIZE = 4;

        //Parameter decleration for APPRAISAL_HEADER_ID
        private const string PARAM_APPRAISAL_HEADER_ID_NAME = "@AppraisalHeaderID";
        private const SqlDbType PARAM_APPRAISAL_HEADER_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_APPRAISAL_HEADER_ID_SIZE = 8;

        //Parameter decleration for WEIGHT
        private const string PARAM_WEIGHT_NAME = "@Weight";
        private const SqlDbType PARAM_WEIGHT_TYPE = SqlDbType.Decimal;
        private const int PARAM_WEIGHT_SIZE = 3;

        //Parameter decleration for SCORE
        private const string PARAM_SCORE_NAME = "@Score";
        private const SqlDbType PARAM_SCORE_TYPE = SqlDbType.Decimal;
        private const int PARAM_SCORE_SIZE = 3;

        //Parameter decleration for JUSTIFICATION
        private const string PARAM_JUSTIFICATION_NAME = "@Justification";
        private const SqlDbType PARAM_JUSTIFICATION_TYPE = SqlDbType.VarChar;
        private const int PARAM_JUSTIFICATION_SIZE = 2000;



        //Parameter decleration for STAFF_ID
        private const string PARAM_STAFF_ID_NAME = "@StaffID";
        private const SqlDbType PARAM_STAFF_ID_TYPE = SqlDbType.NChar;
        private const int PARAM_STAFF_ID_SIZE = 10;

        //Parameter decleration for PERIOD_ID
        private const string PARAM_PERIOD_ID_NAME = "@PeriodID";
        private const SqlDbType PARAM_PERIOD_ID_TYPE = SqlDbType.Int;
        private const int PARAM_PERIOD_ID_SIZE = 4;

        #endregion

        //==========================================================================================
        //Pace Table Field Name Declaration
        //==========================================================================================
        #region Pace Field Name declaration

        public string FIELD_PACE_ID { get { return "Pace_ID"; } }
        public string FIELD_PACE_AREA_ID { get { return "Pace_Area_ID"; } }
        public string FIELD_APPRAISAL_HEADER_ID { get { return "Appraisal_Header_ID"; } }
        public string FIELD_WEIGHT { get { return "Weight"; } }
        public string FIELD_SCORE { get { return "Score"; } }
        public string FIELD_JUSTIFICATION { get { return "Justification"; } }

        #endregion

        //Table name declarations for Pace in the database, this will be used for dataset reference
        public string PACE_TABLE_NAME = "PACE";

        //==========================================================================================
        //public PaceDb Class Method declarations that will be called from the Biz Tier
        //==========================================================================================
        #region PaceDb Class Methods

        public bool InsertPace(int paceId, int paceAreaId, long appraisalHeaderId, decimal weight, decimal score, string justification, Transaction transaction)
        {
            //const string METHOD_NAME  = "InsertPace";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                //param.Add(MakeParam(PARAM_PACE_ID_NAME, PARAM_PACE_ID_TYPE, PARAM_PACE_ID_SIZE, paceId));
                param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_WEIGHT_NAME, PARAM_WEIGHT_TYPE, PARAM_WEIGHT_SIZE, weight));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));
                param.Add(MakeParam(PARAM_JUSTIFICATION_NAME, PARAM_JUSTIFICATION_TYPE, PARAM_JUSTIFICATION_SIZE, justification));

                //Execute Stored Procedure
                if (ExecuteProc(STP_PACE_INSERTPACE, param, transaction) == 0)
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

        public bool DeletePaceByPaceId(int paceId, Transaction transaction)
        {
            //const string METHOD_NAME  = "DeletePaceByPaceId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_PACE_ID_NAME, PARAM_PACE_ID_TYPE, PARAM_PACE_ID_SIZE, paceId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_PACE_DELETEPACEBYPACE_ID, param, transaction) == 0)
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

        public bool DeletePaceByPaceAreaId(int paceAreaId, Transaction transaction)
        {
            //const string METHOD_NAME  = "DeletePaceByPaceAreaId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_PACE_DELETEPACEBYPACE_AREA_ID, param, transaction) == 0)
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

        public bool DeletePaceByAppraisalHeaderId(int appraisalHeaderId, Transaction transaction)
        {
            //const string METHOD_NAME  = "DeletePaceByAppraisalHeaderId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_PACE_DELETEPACEBYAPPRAISAL_HEADER_ID, param, transaction) == 0)
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

        public bool DeletePaceByPace_IDAndPace_Area_IDAndAppraisal_Header_ID(int paceId, int paceAreaId, int appraisalHeaderId, Transaction transaction)
        {
            //const string METHOD_NAME  = "DeletePaceByPace_IDAndPace_Area_IDAndAppraisal_Header_ID";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_PACE_ID_NAME, PARAM_PACE_ID_TYPE, PARAM_PACE_ID_SIZE, paceId));
                param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

                //Execute Stored Procedure
                if (ExecuteProc(STP_PACE_DELETEPACEBYPace_IDANDPace_Area_IDANDAppraisal_Header_ID, param, transaction) == 0)
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

        public bool UpdatePaceByPaceId(int paceId, int paceAreaId, int appraisalHeaderId, decimal weight, decimal score, string justification, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdatePaceByPaceId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_PACE_ID_NAME, PARAM_PACE_ID_TYPE, PARAM_PACE_ID_SIZE, paceId));
                param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_WEIGHT_NAME, PARAM_WEIGHT_TYPE, PARAM_WEIGHT_SIZE, weight));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));
                param.Add(MakeParam(PARAM_JUSTIFICATION_NAME, PARAM_JUSTIFICATION_TYPE, PARAM_JUSTIFICATION_SIZE, justification));

                //Execute Stored Procedure
                if (ExecuteProc(STP_PACE_UPDATEPACEBYPACE_ID, param, transaction) == 0)
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

        public bool UpdatePaceByPaceAreaId(int paceId, int paceAreaId, int appraisalHeaderId, decimal weight, decimal score, string justification, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdatePaceByPaceAreaId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_PACE_ID_NAME, PARAM_PACE_ID_TYPE, PARAM_PACE_ID_SIZE, paceId));
                param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_WEIGHT_NAME, PARAM_WEIGHT_TYPE, PARAM_WEIGHT_SIZE, weight));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));
                param.Add(MakeParam(PARAM_JUSTIFICATION_NAME, PARAM_JUSTIFICATION_TYPE, PARAM_JUSTIFICATION_SIZE, justification));

                //Execute Stored Procedure
                if (ExecuteProc(STP_PACE_UPDATEPACEBYPACE_AREA_ID, param, transaction) == 0)
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

        public bool UpdatePaceByAppraisalHeaderId(int paceId, int paceAreaId, int appraisalHeaderId, decimal weight, decimal score, string justification, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdatePaceByAppraisalHeaderId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_PACE_ID_NAME, PARAM_PACE_ID_TYPE, PARAM_PACE_ID_SIZE, paceId));
                param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_WEIGHT_NAME, PARAM_WEIGHT_TYPE, PARAM_WEIGHT_SIZE, weight));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));
                param.Add(MakeParam(PARAM_JUSTIFICATION_NAME, PARAM_JUSTIFICATION_TYPE, PARAM_JUSTIFICATION_SIZE, justification));

                //Execute Stored Procedure
                if (ExecuteProc(STP_PACE_UPDATEPACEBYAPPRAISAL_HEADER_ID, param, transaction) == 0)
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

        public bool UpdatePaceByPace_IDAndPace_Area_IDAndAppraisal_Header_ID(int paceId, int paceAreaId, int appraisalHeaderId, decimal weight, decimal score, string justification, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdatePaceByPace_IDAndPace_Area_IDAndAppraisal_Header_ID";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_PACE_ID_NAME, PARAM_PACE_ID_TYPE, PARAM_PACE_ID_SIZE, paceId));
                param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_WEIGHT_NAME, PARAM_WEIGHT_TYPE, PARAM_WEIGHT_SIZE, weight));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));
                param.Add(MakeParam(PARAM_JUSTIFICATION_NAME, PARAM_JUSTIFICATION_TYPE, PARAM_JUSTIFICATION_SIZE, justification));

                //Execute Stored Procedure
                if (ExecuteProc(STP_PACE_UPDATEPACEBYPace_IDANDPace_Area_IDANDAppraisal_Header_ID, param, transaction) == 0)
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

        public DataSet SelectAllPace()
        {
            //const string METHOD_NAME  = "SelectAllPace";

            try
            {
                //Execute Stored Procedure
                return ExecuteDataset(STP_PACE_SELECTALLPACE, null, PACE_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectPaceByPaceId(int paceId)
        {
            //const string METHOD_NAME  = "SelectPaceByPaceId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_PACE_ID_NAME, PARAM_PACE_ID_TYPE, PARAM_PACE_ID_SIZE, paceId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_PACE_SELECTPACEBYPACE_ID, param, PACE_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectPaceByPaceAreaId(int paceAreaId)
        {
            //const string METHOD_NAME  = "SelectPaceByPaceAreaId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_PACE_SELECTPACEBYPACE_AREA_ID, param, PACE_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectPaceByAppraisalHeaderId(int appraisalHeaderId)
        {
            //const string METHOD_NAME  = "SelectPaceByAppraisalHeaderId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_PACE_SELECTPACEBYAPPRAISAL_HEADER_ID, param, PACE_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectPaceByPace_IDAndPace_Area_IDAndAppraisal_Header_ID(int paceId, int paceAreaId, int appraisalHeaderId)
        {
            //const string METHOD_NAME  = "SelectPaceByPace_IDAndPace_Area_IDAndAppraisal_Header_ID";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_PACE_ID_NAME, PARAM_PACE_ID_TYPE, PARAM_PACE_ID_SIZE, paceId));
                param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_PACE_SELECTPACEBYPace_IDANDPace_Area_IDANDAppraisal_Header_ID, param, PACE_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectPaceByStaffIDAndPeriodID(string staffId, int periodId)
        {
            //const string METHOD_NAME  = "SelectPaceByStaffIDAndPeriodID";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_PACE_SELECTPACEBYSTAFF_IDANDPERIOD_ID, param, PACE_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectDefaultPace(int periodId)
        {
            //const string METHOD_NAME  = "SelectDefaultPace";

            try
            {
                
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_PACE_SELECTDEFAULTPACE, param, PACE_TABLE_NAME);
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

