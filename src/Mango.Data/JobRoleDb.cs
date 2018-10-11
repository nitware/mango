//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:21

//Description: This Class represents the data tier layer class for JobRole table.
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
    public class JobRoleDb : DataAccess.DataAccess
	{
		private const string CLASS_NAME = "JobRoleDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  JobRole Stored Procedure declaration

		private const string STP_JOB_ROLE_INSERTJOB_ROLE = "STP_JOB_ROLE_INSERTJOB_ROLE";
		private const string STP_JOB_ROLE_DELETEJOB_ROLEBYJOB_ROLE_ID = "STP_JOB_ROLE_DELETEJOB_ROLEBYJOB_ROLE_ID";
		private const string STP_JOB_ROLE_UPDATEJOB_ROLEBYJOB_ROLE_ID = "STP_JOB_ROLE_UPDATEJOB_ROLEBYJOB_ROLE_ID";
		private const string STP_JOB_ROLE_SELECTALLJOB_ROLE = "STP_JOB_ROLE_SELECTALLJOB_ROLE";
		private const string STP_JOB_ROLE_SELECTJOB_ROLEBYJOB_ROLE_ID = "STP_JOB_ROLE_SELECTJOB_ROLEBYJOB_ROLE_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region JobRole Parameter declaration 

		//Parameter decleration for JOB_ROLE_ID
		private const string PARAM_JOB_ROLE_ID_NAME = "@JobRoleID";
		private const SqlDbType PARAM_JOB_ROLE_ID_TYPE = SqlDbType.SmallInt;
		private const int PARAM_JOB_ROLE_ID_SIZE = 2;

		//Parameter decleration for JOB_ROLE_NAME
		private const string PARAM_JOB_ROLE_NAME_NAME = "@JobRoleName";
		private const SqlDbType PARAM_JOB_ROLE_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_JOB_ROLE_NAME_SIZE = 50;

		#endregion

		//==========================================================================================
		//JobRole Table Field Name Declaration
		//==========================================================================================
		#region JobRole Field Name declaration 

		public string FIELD_JOB_ROLE_ID { get { return "Job_Role_ID"; } }
		public string FIELD_JOB_ROLE_NAME { get { return "Job_Role_Name"; } }

		#endregion

		//Table name declarations for JobRole in the database, this will be used for dataset reference
		public string JOBROLE_TABLE_NAME  = "JOBROLE";

		//==========================================================================================
		//public JobRoleDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region JobRoleDb Class Methods 

		public bool InsertJobRole(DataSet dsAuditItem, Int16 jobRoleId, string jobRoleName, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertJobRole";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));
				param.Add(MakeParam(PARAM_JOB_ROLE_NAME_NAME, PARAM_JOB_ROLE_NAME_TYPE, PARAM_JOB_ROLE_NAME_SIZE, jobRoleName));

				//Execute Stored Procedure
				if (ExecuteProc(STP_JOB_ROLE_INSERTJOB_ROLE, param, transaction) == 0)
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

		public bool DeleteJobRoleByJobRoleId(DataSet dsAuditItem, Int16 jobRoleId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteJobRoleByJobRoleId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_JOB_ROLE_DELETEJOB_ROLEBYJOB_ROLE_ID, param, transaction) == 0)
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

		public bool UpdateJobRoleByJobRoleId(DataSet dsAuditItem, Int16 jobRoleId, string jobRoleName, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateJobRoleByJobRoleId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));
				param.Add(MakeParam(PARAM_JOB_ROLE_NAME_NAME, PARAM_JOB_ROLE_NAME_TYPE, PARAM_JOB_ROLE_NAME_SIZE, jobRoleName));

				//Execute Stored Procedure
				if (ExecuteProc(STP_JOB_ROLE_UPDATEJOB_ROLEBYJOB_ROLE_ID, param, transaction) == 0)
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

		public DataSet SelectAllJobRole()
		{
			//const string METHOD_NAME  = "SelectAllJobRole";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_JOB_ROLE_SELECTALLJOB_ROLE, null, JOBROLE_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectJobRoleByJobRoleId(Int16 jobRoleId)
		{
			//const string METHOD_NAME  = "SelectJobRoleByJobRoleId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_JOB_ROLE_SELECTJOB_ROLEBYJOB_ROLE_ID, param, JOBROLE_TABLE_NAME);
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


