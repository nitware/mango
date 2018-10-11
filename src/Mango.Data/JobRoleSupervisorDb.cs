//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:22

//Description: This Class represents the data tier layer class for JobRoleSupervisor table.
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
    public class JobRoleSupervisorDb : DataAccess.DataAccess, IJobRoleSupervisorDb
	{
		private const string CLASS_NAME = "JobRoleSupervisorDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  JobRoleSupervisor Stored Procedure declaration

		private const string STP_JOB_ROLE_SUPERVISOR_INSERTJOB_ROLE_SUPERVISOR = "STP_JOB_ROLE_SUPERVISOR_INSERTJOB_ROLE_SUPERVISOR";
		private const string STP_JOB_ROLE_SUPERVISOR_DELETEJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_JOB_ROLE_SUPERVISOR_DELETEJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID";
		private const string STP_JOB_ROLE_SUPERVISOR_DELETEJOB_ROLE_SUPERVISORBYJOB_ROLE_ID = "STP_JOB_ROLE_SUPERVISOR_DELETEJOB_ROLE_SUPERVISORBYJOB_ROLE_ID";
		private const string STP_JOB_ROLE_SUPERVISOR_DELETEJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID = "STP_JOB_ROLE_SUPERVISOR_DELETEJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID";
		private const string STP_JOB_ROLE_SUPERVISOR_UPDATEJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_JOB_ROLE_SUPERVISOR_UPDATEJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID";
		private const string STP_JOB_ROLE_SUPERVISOR_UPDATEJOB_ROLE_SUPERVISORBYJOB_ROLE_ID = "STP_JOB_ROLE_SUPERVISOR_UPDATEJOB_ROLE_SUPERVISORBYJOB_ROLE_ID";
		private const string STP_JOB_ROLE_SUPERVISOR_UPDATEJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID = "STP_JOB_ROLE_SUPERVISOR_UPDATEJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID";
		private const string STP_JOB_ROLE_SUPERVISOR_SELECTALLJOB_ROLE_SUPERVISOR = "STP_JOB_ROLE_SUPERVISOR_SELECTALLJOB_ROLE_SUPERVISOR";
		private const string STP_JOB_ROLE_SUPERVISOR_SELECTJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_JOB_ROLE_SUPERVISOR_SELECTJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID";
		private const string STP_JOB_ROLE_SUPERVISOR_SELECTJOB_ROLE_SUPERVISORBYJOB_ROLE_ID = "STP_JOB_ROLE_SUPERVISOR_SELECTJOB_ROLE_SUPERVISORBYJOB_ROLE_ID";
		private const string STP_JOB_ROLE_SUPERVISOR_SELECTJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID = "STP_JOB_ROLE_SUPERVISOR_SELECTJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region JobRoleSupervisor Parameter declaration 

		//Parameter decleration for SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID
        private const string PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME = "@SupervisorCompanyDepartmentJobRoleID";
        private const SqlDbType PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE = SqlDbType.Int;
        private const int PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE = 4;

		//Parameter decleration for JOB_ROLE_ID
        private const string PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME = "@StaffCompanyDepartmentJobRoleID";
        private const SqlDbType PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE = SqlDbType.Int;
        private const int PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE = 4;

		#endregion

		//==========================================================================================
		//JobRoleSupervisor Table Field Name Declaration
		//==========================================================================================
		#region JobRoleSupervisor Field Name declaration 
        
        public string FIELD_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID { get { return "Supervisor_Company_Department_Job_Role_ID"; } }
		public string FIELD_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID { get { return "Staff_Company_Department_Job_Role_ID"; } }

		#endregion

		//Table name declarations for JobRoleSupervisor in the database, this will be used for dataset reference
		public string JOBROLESUPERVISOR_TABLE_NAME  = "JOBROLESUPERVISOR";

		//==========================================================================================
		//public JobRoleSupervisorDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region JobRoleSupervisorDb Class Methods 

        public bool InsertJobRoleSupervisor(int supervisorCompanyDepartmentJobRoleID, int staffCompanyDepartmentJobRoleID, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertJobRoleSupervisor";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, supervisorCompanyDepartmentJobRoleID));
                param.Add(MakeParam(PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, staffCompanyDepartmentJobRoleID));

				//Execute Stored Procedure
				if (ExecuteProc(STP_JOB_ROLE_SUPERVISOR_INSERTJOB_ROLE_SUPERVISOR, param, transaction) == 0)
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

		public bool DeleteJobRoleSupervisorByJobRoleSupervisorId(int jobRoleSupervisorId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteJobRoleSupervisorByJobRoleSupervisorId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, jobRoleSupervisorId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_JOB_ROLE_SUPERVISOR_DELETEJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID, param, transaction) == 0)
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

		public bool DeleteJobRoleSupervisorByJobRoleId(int jobRoleId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteJobRoleSupervisorByJobRoleId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, jobRoleId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_JOB_ROLE_SUPERVISOR_DELETEJOB_ROLE_SUPERVISORBYJOB_ROLE_ID, param, transaction) == 0)
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

		public bool DeleteJobRoleSupervisorBySUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDAndJob_Role_ID(int jobRoleSupervisorId, int jobRoleId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteJobRoleSupervisorBySUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDAndJob_Role_ID";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, jobRoleSupervisorId));
                param.Add(MakeParam(PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, jobRoleId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_JOB_ROLE_SUPERVISOR_DELETEJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID, param, transaction) == 0)
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

		public DataSet SelectAllJobRoleSupervisor()
		{
			//const string METHOD_NAME  = "SelectAllJobRoleSupervisor";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_JOB_ROLE_SUPERVISOR_SELECTALLJOB_ROLE_SUPERVISOR, null, JOBROLESUPERVISOR_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectJobRoleSupervisorByJobRoleSupervisorId(int jobRoleSupervisorId)
		{
			//const string METHOD_NAME  = "SelectJobRoleSupervisorByJobRoleSupervisorId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, jobRoleSupervisorId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_JOB_ROLE_SUPERVISOR_SELECTJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID, param, JOBROLESUPERVISOR_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectJobRoleSupervisorByJobRoleId(int jobRoleId)
		{
			//const string METHOD_NAME  = "SelectJobRoleSupervisorByJobRoleId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, jobRoleId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_JOB_ROLE_SUPERVISOR_SELECTJOB_ROLE_SUPERVISORBYJOB_ROLE_ID, param, JOBROLESUPERVISOR_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectJobRoleSupervisorBySUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDAndJob_Role_ID(int jobRoleSupervisorId, int jobRoleId)
		{
			//const string METHOD_NAME  = "SelectJobRoleSupervisorBySUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDAndJob_Role_ID";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, jobRoleSupervisorId));
                param.Add(MakeParam(PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, jobRoleId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_JOB_ROLE_SUPERVISOR_SELECTJOB_ROLE_SUPERVISORBYSUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID, param, JOBROLESUPERVISOR_TABLE_NAME);
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


