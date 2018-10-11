//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:31

//Description: This Class represents the data tier layer class for PaceArea table.
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
    public class PaceAreaDb : DataAccess.DataAccess, IPaceAreaDb
	{
		private const string CLASS_NAME = "PaceAreaDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  PaceArea Stored Procedure declaration

		private const string STP_PACE_AREA_INSERTPACE_AREA = "STP_PACE_AREA_INSERTPACE_AREA";
		private const string STP_PACE_AREA_DELETEPACE_AREABYPACE_AREA_ID = "STP_PACE_AREA_DELETEPACE_AREABYPACE_AREA_ID";
		private const string STP_PACE_AREA_UPDATEPACE_AREABYPACE_AREA_ID = "STP_PACE_AREA_UPDATEPACE_AREABYPACE_AREA_ID";
		private const string STP_PACE_AREA_SELECTALLPACE_AREA = "STP_PACE_AREA_SELECTALLPACE_AREA";
		private const string STP_PACE_AREA_SELECTPACE_AREABYPACE_AREA_ID = "STP_PACE_AREA_SELECTPACE_AREABYPACE_AREA_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region PaceArea Parameter declaration 

		//Parameter decleration for PACE_AREA_ID
		private const string PARAM_PACE_AREA_ID_NAME = "@PaceAreaID";
		private const SqlDbType PARAM_PACE_AREA_ID_TYPE = SqlDbType.Int;
		private const int PARAM_PACE_AREA_ID_SIZE = 4;

		//Parameter decleration for PACE_NAME
		private const string PARAM_PACE_NAME_NAME = "@PaceName";
		private const SqlDbType PARAM_PACE_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_PACE_NAME_SIZE = 20;

		//Parameter decleration for PACE_DESCRIPTION
		private const string PARAM_PACE_DESCRIPTION_NAME = "@PaceDescription";
		private const SqlDbType PARAM_PACE_DESCRIPTION_TYPE = SqlDbType.VarChar;
		private const int PARAM_PACE_DESCRIPTION_SIZE = 1000;

		#endregion

		//==========================================================================================
		//PaceArea Table Field Name Declaration
		//==========================================================================================
		#region PaceArea Field Name declaration 

		public string FIELD_PACE_AREA_ID { get { return "Pace_Area_ID"; } }
		public string FIELD_PACE_NAME { get { return "Pace_Name"; } }
		public string FIELD_PACE_DESCRIPTION { get { return "Pace_Description"; } }

		#endregion

		//Table name declarations for PaceArea in the database, this will be used for dataset reference
		public string PACEAREA_TABLE_NAME  = "PACEAREA";

		//==========================================================================================
		//public PaceAreaDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region PaceAreaDb Class Methods 

		public bool InsertPaceArea(int paceAreaId, string paceName, string paceDescription, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertPaceArea";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));
				param.Add(MakeParam(PARAM_PACE_NAME_NAME, PARAM_PACE_NAME_TYPE, PARAM_PACE_NAME_SIZE, paceName));
				param.Add(MakeParam(PARAM_PACE_DESCRIPTION_NAME, PARAM_PACE_DESCRIPTION_TYPE, PARAM_PACE_DESCRIPTION_SIZE, paceDescription));

				//Execute Stored Procedure
				if (ExecuteProc(STP_PACE_AREA_INSERTPACE_AREA, param, transaction) == 0)
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

		public bool DeletePaceAreaByPaceAreaId(int paceAreaId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeletePaceAreaByPaceAreaId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_PACE_AREA_DELETEPACE_AREABYPACE_AREA_ID, param, transaction) == 0)
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

		public bool UpdatePaceAreaByPaceAreaId(int paceAreaId, string paceName, string paceDescription, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdatePaceAreaByPaceAreaId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));
				param.Add(MakeParam(PARAM_PACE_NAME_NAME, PARAM_PACE_NAME_TYPE, PARAM_PACE_NAME_SIZE, paceName));
				param.Add(MakeParam(PARAM_PACE_DESCRIPTION_NAME, PARAM_PACE_DESCRIPTION_TYPE, PARAM_PACE_DESCRIPTION_SIZE, paceDescription));

				//Execute Stored Procedure
				if (ExecuteProc(STP_PACE_AREA_UPDATEPACE_AREABYPACE_AREA_ID, param, transaction) == 0)
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

		public DataSet SelectAllPaceArea()
		{
			//const string METHOD_NAME  = "SelectAllPaceArea";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_PACE_AREA_SELECTALLPACE_AREA, null, PACEAREA_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectPaceAreaByPaceAreaId(int paceAreaId)
		{
			//const string METHOD_NAME  = "SelectPaceAreaByPaceAreaId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_PACE_AREA_ID_NAME, PARAM_PACE_AREA_ID_TYPE, PARAM_PACE_AREA_ID_SIZE, paceAreaId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_PACE_AREA_SELECTPACE_AREABYPACE_AREA_ID, param, PACEAREA_TABLE_NAME);
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


