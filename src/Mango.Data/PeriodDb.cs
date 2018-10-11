//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:31

//Description: This Class represents the data tier layer class for Period table.
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
    public class PeriodDb : DataAccess.DataAccess, IPeriodDb
	{
		private const string CLASS_NAME = "PeriodDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  Period Stored Procedure declaration

        private const string STP_PERIOD_UPDATEPERIODBYPERIOD_ID = "STP_PERIOD_UPDATEPERIODBYPERIOD_ID";
        private const string STP_PERIOD_SELECTALLPERIOD = "STP_PERIOD_SELECTALLPERIOD";
		private const string STP_PERIOD_INSERTPERIOD = "STP_PERIOD_INSERTPERIOD";
		private const string STP_PERIOD_SELECTPERIODBYPERIOD_ID = "STP_PERIOD_SELECTPERIODBYPERIOD_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region Period Parameter declaration 

		//Parameter decleration for PERIOD_ID
        public const string PARAM_PERIOD_ID_NAME = "@PeriodID";
        public const SqlDbType PARAM_PERIOD_ID_TYPE = SqlDbType.Int;
        public const int PARAM_PERIOD_ID_SIZE = 4;

		//Parameter decleration for PERIOD_NAME
		private const string PARAM_PERIOD_NAME_NAME = "@PeriodName";
		private const SqlDbType PARAM_PERIOD_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_PERIOD_NAME_SIZE = 50;

		//Parameter decleration for STATUS
        private const string PARAM_STATUS_ID_NAME = "@StatusID";
        private const SqlDbType PARAM_STATUS_ID_TYPE = SqlDbType.TinyInt;
        private const int PARAM_STATUS_ID_SIZE = 1;

        private const string PARAM_PERIOD_TYPE_ID_NAME = "@PeriodTypeId";
        private const SqlDbType PARAM_PERIOD_TYPE_ID_TYPE = SqlDbType.SmallInt;
        private const int PARAM_PERIOD_TYPE_ID_SIZE = 2;

        ////Parameter decleration for TYPE
        //private const string PARAM_TYPE_NAME = "@Type";
        //private const SqlDbType PARAM_TYPE_TYPE = SqlDbType.VarChar;
        //private const int PARAM_TYPE_SIZE = 20;

		//Parameter decleration for SPAN
		private const string PARAM_SPAN_NAME = "@Span";
		private const SqlDbType PARAM_SPAN_TYPE = SqlDbType.TinyInt;
		private const int PARAM_SPAN_SIZE = 1;

		//Parameter decleration for START_DATE
		private const string PARAM_START_DATE_NAME = "@StartDate";
		private const SqlDbType PARAM_START_DATE_TYPE = SqlDbType.DateTime;
		private const int PARAM_START_DATE_SIZE = 8;

        //Parameter decleration for END_DATE
        private const string PARAM_END_DATE_NAME = "@EndDate";
        private const SqlDbType PARAM_END_DATE_TYPE = SqlDbType.DateTime;
        private const int PARAM_END_DATE_SIZE = 8;

        private const string PARAM_YEAR_NAME = "@Year";
        private const SqlDbType PARAM_YEAR_TYPE = SqlDbType.Int;
        private const int PARAM_YEAR_SIZE = 4;

		#endregion

		//==========================================================================================
		//Period Table Field Name Declaration
		//==========================================================================================
		#region Period Field Name declaration 

		public string FIELD_PERIOD_ID { get { return "Period_ID"; } }
		public string FIELD_PERIOD_NAME { get { return "Period_Name"; } }
		public string FIELD_STATUS_ID { get { return "Status_ID"; } }
        public string FIELD_STATUS_NAME { get { return "Status_Name"; } }
		public string FIELD_PERIOD_TYPE_ID { get { return "Period_Type_Id"; } }
		public string FIELD_SPAN { get { return "Span"; } }
		public string FIELD_START_DATE { get { return "Start_Date"; } }
        public string FIELD_END_DATE { get { return "End_Date"; } }
        public string FIELD_YEAR { get { return "Year"; } }

		#endregion

		//Table name declarations for Period in the database, this will be used for dataset reference
		private string PERIOD_TABLE_NAME  = "PERIOD";

		//==========================================================================================
		//public PeriodDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region PeriodDb Class Methods 

        public int InsertPeriod(string periodName, byte statusId, int periodTypeId, byte span, DateTime startDate, DateTime endDate, int year, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertPeriod";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeOutputParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE));
                param.Add(MakeParam(PARAM_STATUS_ID_NAME, PARAM_STATUS_ID_TYPE, PARAM_STATUS_ID_SIZE, statusId));
				param.Add(MakeParam(PARAM_PERIOD_NAME_NAME, PARAM_PERIOD_NAME_TYPE, PARAM_PERIOD_NAME_SIZE, periodName));
				param.Add(MakeParam(PARAM_SPAN_NAME, PARAM_SPAN_TYPE, PARAM_SPAN_SIZE, span));
				param.Add(MakeParam(PARAM_START_DATE_NAME, PARAM_START_DATE_TYPE, PARAM_START_DATE_SIZE, startDate));
                param.Add(MakeParam(PARAM_END_DATE_NAME, PARAM_END_DATE_TYPE, PARAM_END_DATE_SIZE, endDate));
                param.Add(MakeParam(PARAM_PERIOD_TYPE_ID_NAME, PARAM_PERIOD_TYPE_ID_TYPE, PARAM_PERIOD_TYPE_ID_SIZE, periodTypeId));
                param.Add(MakeParam(PARAM_YEAR_NAME, PARAM_YEAR_TYPE, PARAM_YEAR_SIZE, year));

                return Convert.ToInt32(ExecuteProcWithOutputParam(STP_PERIOD_INSERTPERIOD, param, transaction));
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

        public bool UpdatePeriodByPeriodId(int periodId, string periodName, byte statusId, string type, byte span, DateTime startDate, DateTime endDate, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdatePeriodByPeriodId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
                param.Add(MakeParam(PARAM_STATUS_ID_NAME, PARAM_STATUS_ID_TYPE, PARAM_STATUS_ID_SIZE, statusId));
                param.Add(MakeParam(PARAM_PERIOD_NAME_NAME, PARAM_PERIOD_NAME_TYPE, PARAM_PERIOD_NAME_SIZE, periodName));
                param.Add(MakeParam(PARAM_PERIOD_TYPE_ID_NAME, PARAM_PERIOD_TYPE_ID_TYPE, PARAM_PERIOD_TYPE_ID_SIZE, type));
                param.Add(MakeParam(PARAM_SPAN_NAME, PARAM_SPAN_TYPE, PARAM_SPAN_SIZE, span));
                param.Add(MakeParam(PARAM_START_DATE_NAME, PARAM_START_DATE_TYPE, PARAM_START_DATE_SIZE, startDate));
                param.Add(MakeParam(PARAM_END_DATE_NAME, PARAM_END_DATE_TYPE, PARAM_END_DATE_SIZE, endDate));

                //Execute Stored Procedure
                if (ExecuteProc(STP_PERIOD_UPDATEPERIODBYPERIOD_ID, param, transaction) == 0)
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

		public DataSet SelectPeriodByPeriodId(int periodId)
		{
			//const string METHOD_NAME  = "SelectPeriodByPeriodId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_PERIOD_SELECTPERIODBYPERIOD_ID, param, PERIOD_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

        public DataSet SelectAllPeriod()
        {
            //const string METHOD_NAME  = "SelectAllPeriod";

            try
            {
                //Execute Stored Procedure
                return ExecuteDataset(STP_PERIOD_SELECTALLPERIOD, null, PERIOD_TABLE_NAME);
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


