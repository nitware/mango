//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 22/07/2011 09:12:13

//Description: This Class represents the data tier layer class for Option table.
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
    public class OptionDb : DataAccess.DataAccess, IOptionDb
	{
		private const string CLASS_NAME = "OptionDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  Option Stored Procedure declaration

		private const string STP_OPTION_INSERTOPTION = "STP_OPTION_INSERTOPTION";
		private const string STP_OPTION_DELETEOPTIONBYOPTION_ID = "STP_OPTION_DELETEOPTIONBYOPTION_ID";
		private const string STP_OPTION_UPDATEOPTIONBYOPTION_ID = "STP_OPTION_UPDATEOPTIONBYOPTION_ID";
		private const string STP_OPTION_SELECTALLOPTION = "STP_OPTION_SELECTALLOPTION";
		private const string STP_OPTION_SELECTOPTIONBYOPTION_ID = "STP_OPTION_SELECTOPTIONBYOPTION_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region Option Parameter declaration 

		//Parameter decleration for OPTION_ID
		private const string PARAM_OPTION_ID_NAME = "@OptionID";
		private const SqlDbType PARAM_OPTION_ID_TYPE = SqlDbType.TinyInt;
		private const int PARAM_OPTION_ID_SIZE = 1;

		//Parameter decleration for OPTION_NAME
		private const string PARAM_OPTION_NAME_NAME = "@OptionName";
		private const SqlDbType PARAM_OPTION_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_OPTION_NAME_SIZE = 50;

		#endregion

		//==========================================================================================
		//Option Table Field Name Declaration
		//==========================================================================================
		#region Option Field Name declaration 

		public string FIELD_OPTION_ID { get { return "Option_ID"; } }
		public string FIELD_OPTION_NAME { get { return "Option_Name"; } }

		#endregion

		//Table name declarations for Option in the database, this will be used for dataset reference
		public string OPTION_TABLE_NAME  = "OPTION";

		//==========================================================================================
		//public OptionDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region OptionDb Class Methods 

		public bool InsertOption(byte optionId, string optionName, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertOption";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_OPTION_ID_NAME, PARAM_OPTION_ID_TYPE, PARAM_OPTION_ID_SIZE, optionId));
				param.Add(MakeParam(PARAM_OPTION_NAME_NAME, PARAM_OPTION_NAME_TYPE, PARAM_OPTION_NAME_SIZE, optionName));

				//Execute Stored Procedure
				if (ExecuteProc(STP_OPTION_INSERTOPTION, param, transaction) == 0)
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

		public bool DeleteOptionByOptionId(byte optionId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteOptionByOptionId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_OPTION_ID_NAME, PARAM_OPTION_ID_TYPE, PARAM_OPTION_ID_SIZE, optionId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_OPTION_DELETEOPTIONBYOPTION_ID, param, transaction) == 0)
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

		public bool UpdateOptionByOptionId(byte optionId, string optionName, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateOptionByOptionId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_OPTION_ID_NAME, PARAM_OPTION_ID_TYPE, PARAM_OPTION_ID_SIZE, optionId));
				param.Add(MakeParam(PARAM_OPTION_NAME_NAME, PARAM_OPTION_NAME_TYPE, PARAM_OPTION_NAME_SIZE, optionName));

				//Execute Stored Procedure
				if (ExecuteProc(STP_OPTION_UPDATEOPTIONBYOPTION_ID, param, transaction) == 0)
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

		public DataSet SelectAllOption()
		{
			//const string METHOD_NAME  = "SelectAllOption";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_OPTION_SELECTALLOPTION, null, OPTION_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectOptionByOptionId(byte optionId)
		{
			//const string METHOD_NAME  = "SelectOptionByOptionId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_OPTION_ID_NAME, PARAM_OPTION_ID_TYPE, PARAM_OPTION_ID_SIZE, optionId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_OPTION_SELECTOPTIONBYOPTION_ID, param, OPTION_TABLE_NAME);
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


