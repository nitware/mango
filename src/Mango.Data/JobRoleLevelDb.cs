//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:21

//Description: This Class represents the data tier layer class for JobRoleLevel table.
//It contains all data access methods and static constants representing the
//Stored Procedures, field names and SQL parameters required by this entity.

//No man can cover the moon with his bare hands. You will shine when the time is ripe.
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;
using Mango.DataAccess;

namespace Mango.Data
{
    public class JobRoleLevelDb : DataAccess.DataAccess
	{
		private const string CLASS_NAME = "JobRoleLevelDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  JobRoleLevel Stored Procedure declaration

		private const string STP_JOB_ROLE_LEVEL_INSERTJOB_ROLE_LEVEL = "STP_JOB_ROLE_LEVEL_INSERTJOB_ROLE_LEVEL";
		private const string STP_JOB_ROLE_LEVEL_DELETEJOB_ROLE_LEVELBYJOB_ROLE_LEVEL_ID = "STP_JOB_ROLE_LEVEL_DELETEJOB_ROLE_LEVELBYJOB_ROLE_LEVEL_ID";
		private const string STP_JOB_ROLE_LEVEL_UPDATEJOB_ROLE_LEVELBYJOB_ROLE_LEVEL_ID = "STP_JOB_ROLE_LEVEL_UPDATEJOB_ROLE_LEVELBYJOB_ROLE_LEVEL_ID";
		private const string STP_JOB_ROLE_LEVEL_SELECTALLJOB_ROLE_LEVEL = "STP_JOB_ROLE_LEVEL_SELECTALLJOB_ROLE_LEVEL";
		private const string STP_JOB_ROLE_LEVEL_SELECTJOB_ROLE_LEVELBYJOB_ROLE_LEVEL_ID = "STP_JOB_ROLE_LEVEL_SELECTJOB_ROLE_LEVELBYJOB_ROLE_LEVEL_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region JobRoleLevel Parameter declaration 

		//Parameter decleration for JOB_ROLE_LEVEL_ID
		private const string PARAM_JOB_ROLE_LEVEL_ID_NAME = "@JobRoleLevelID";
		private const SqlDbType PARAM_JOB_ROLE_LEVEL_ID_TYPE = SqlDbType.Char;
		private const int PARAM_JOB_ROLE_LEVEL_ID_SIZE = 10;

		//Parameter decleration for JOB_ROLE_LEVEL_NAME
		private const string PARAM_JOB_ROLE_LEVEL_NAME_NAME = "@JobRoleLevelName";
		private const SqlDbType PARAM_JOB_ROLE_LEVEL_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_JOB_ROLE_LEVEL_NAME_SIZE = 50;

		#endregion

		//==========================================================================================
		//JobRoleLevel Table Field Name Declaration
		//==========================================================================================
		#region JobRoleLevel Field Name declaration 

		public string FIELD_JOB_ROLE_LEVEL_ID { get { return "Job_Role_Level_ID"; } }
		public string FIELD_JOB_ROLE_LEVEL_NAME { get { return "Job_Role_Level_Name"; } }

		#endregion

		//Table name declarations for JobRoleLevel in the database, this will be used for dataset reference
		public string JOBROLELEVEL_TABLE_NAME  = "JOBROLELEVEL";

		//==========================================================================================
		//public JobRoleLevelDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region JobRoleLevelDb Class Methods 

		public bool InsertJobRoleLevel(DataSet dsAuditItem, string jobRoleLevelId, string jobRoleLevelName, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertJobRoleLevel";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_NAME_NAME, PARAM_JOB_ROLE_LEVEL_NAME_TYPE, PARAM_JOB_ROLE_LEVEL_NAME_SIZE, jobRoleLevelName));

				//Execute Stored Procedure
				if (ExecuteProc(STP_JOB_ROLE_LEVEL_INSERTJOB_ROLE_LEVEL, param, transaction) == 0)
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

		public bool DeleteJobRoleLevelByJobRoleLevelId(DataSet dsAuditItem, string jobRoleLevelId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteJobRoleLevelByJobRoleLevelId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_JOB_ROLE_LEVEL_DELETEJOB_ROLE_LEVELBYJOB_ROLE_LEVEL_ID, param, transaction) == 0)
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

		public bool UpdateJobRoleLevelByJobRoleLevelId(DataSet dsAuditItem, string jobRoleLevelId, string jobRoleLevelName, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateJobRoleLevelByJobRoleLevelId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_NAME_NAME, PARAM_JOB_ROLE_LEVEL_NAME_TYPE, PARAM_JOB_ROLE_LEVEL_NAME_SIZE, jobRoleLevelName));

				//Execute Stored Procedure
				if (ExecuteProc(STP_JOB_ROLE_LEVEL_UPDATEJOB_ROLE_LEVELBYJOB_ROLE_LEVEL_ID, param, transaction) == 0)
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

		public DataSet SelectAllJobRoleLevel()
		{
			//const string METHOD_NAME  = "SelectAllJobRoleLevel";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_JOB_ROLE_LEVEL_SELECTALLJOB_ROLE_LEVEL, null, JOBROLELEVEL_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectJobRoleLevelByJobRoleLevelId(string jobRoleLevelId)
		{
			//const string METHOD_NAME  = "SelectJobRoleLevelByJobRoleLevelId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_JOB_ROLE_LEVEL_SELECTJOB_ROLE_LEVELBYJOB_ROLE_LEVEL_ID, param, JOBROLELEVEL_TABLE_NAME);
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


