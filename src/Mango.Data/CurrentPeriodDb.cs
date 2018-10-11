//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:17

//Description: This Class represents the data tier layer class for CurrentPeriod table.
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
    public class CurrentPeriodDb : DataAccess.DataAccess, ICurrentPeriodDb
	{
		private const string CLASS_NAME = "CurrentPeriodDb";
        private const string CURRENTPERIOD_TABLE_NAME = "CURRENT_PERIOD";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  CurrentPeriod Stored Procedure declaration

		private const string STP_CURRENT_PERIOD_INSERTCURRENT_PERIOD = "STP_CURRENT_PERIOD_INSERTCURRENT_PERIOD";
        private const string STP_CURRENT_PERIOD_DELETEALLCURRENT_PERIOD = "STP_CURRENT_PERIOD_DELETEALLCURRENT_PERIOD";
		private const string STP_CURRENT_PERIOD_SELECTALLCURRENT_PERIOD = "STP_CURRENT_PERIOD_SELECTALLCURRENT_PERIOD";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region CurrentPeriod Parameter declaration 

		//Parameter decleration for PERIOD_ID
		private const string PARAM_PERIOD_ID_NAME = "@PeriodID";
		private const SqlDbType PARAM_PERIOD_ID_TYPE = SqlDbType.Int;
		private const int PARAM_PERIOD_ID_SIZE = 4;

		#endregion

		//==========================================================================================
		//CurrentPeriod Table Field Name Declaration
		//==========================================================================================
		#region CurrentPeriod Field Name declaration 

		public string FIELD_PERIOD_ID { get { return "Period_ID"; } }

		#endregion	

		//==========================================================================================
		//public CurrentPeriodDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region CurrentPeriodDb Class Methods 

		public bool InsertCurrentPeriod(int periodId, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertCurrentPeriod";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_CURRENT_PERIOD_INSERTCURRENT_PERIOD, param, transaction) == 0)
				{
					return true;
				}
				else 
				{
					return false;
				}
			}
			catch (Exception)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw;
			}
		}

        public bool DeleteAllCurrentPeriod(Transaction transaction)
        {
            //const string METHOD_NAME  = "DeleteAllCurrentPeriod";

            try
            {
                //Execute Stored Procedure
                if (ExecuteProc(STP_CURRENT_PERIOD_DELETEALLCURRENT_PERIOD, null, transaction) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw;
            }
        }

		public DataSet SelectAllCurrentPeriod()
		{
			//const string METHOD_NAME  = "SelectAllCurrentPeriod";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_CURRENT_PERIOD_SELECTALLCURRENT_PERIOD, null, CURRENTPERIOD_TABLE_NAME);
			}
			catch (Exception)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw;
			}
		}

		#endregion

	}
}


