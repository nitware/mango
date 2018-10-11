//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:31

//Description: This Class represents the data tier layer class for Staff table.
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
    public class StaffDb : DataAccess.DataAccess, IStaffDb
	{
		private const string CLASS_NAME = "StaffDb";

        //private IPeriodDb periodDb;
        //private IOptionDb optionDb;

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  Staff Stored Procedure declaration

		private const string STP_STAFF_INSERTSTAFF = "STP_STAFF_INSERTSTAFF";
		private const string STP_STAFF_DELETESTAFFBYSTAFF_ID = "STP_STAFF_DELETESTAFFBYSTAFF_ID";
		private const string STP_STAFF_DELETESTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_STAFF_DELETESTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_ID";
		private const string STP_STAFF_DELETESTAFFBYJOB_ROLE_LEVEL_ID = "STP_STAFF_DELETESTAFFBYJOB_ROLE_LEVEL_ID";
		private const string STP_STAFF_DELETESTAFFBYStaff_IDANDCompany_Department_Job_Role_IDANDJob_Role_Level_ID = "STP_STAFF_DELETESTAFFBYStaff_IDANDCompany_Department_Job_Role_IDANDJob_Role_Level_ID";
		private const string STP_STAFF_UPDATESTAFFBYSTAFF_ID = "STP_STAFF_UPDATESTAFFBYSTAFF_ID";
		private const string STP_STAFF_UPDATESTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_STAFF_UPDATESTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_ID";
		private const string STP_STAFF_UPDATESTAFFBYJOB_ROLE_LEVEL_ID = "STP_STAFF_UPDATESTAFFBYJOB_ROLE_LEVEL_ID";
		private const string STP_STAFF_UPDATESTAFFBYStaff_IDANDCompany_Department_Job_Role_IDANDJob_Role_Level_ID = "STP_STAFF_UPDATESTAFFBYStaff_IDANDCompany_Department_Job_Role_IDANDJob_Role_Level_ID";
		private const string STP_STAFF_SELECTALLSTAFF = "STP_STAFF_SELECTALLSTAFF";
		private const string STP_STAFF_SELECTSTAFFBYSTAFF_ID = "STP_STAFF_SELECTSTAFFBYSTAFF_ID";
		private const string STP_STAFF_SELECTSTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_STAFF_SELECTSTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_ID";
		private const string STP_STAFF_SELECTSTAFFBYJOB_ROLE_LEVEL_ID = "STP_STAFF_SELECTSTAFFBYJOB_ROLE_LEVEL_ID";
		private const string STP_STAFF_SELECTSTAFFBYStaff_IDANDCompany_Department_Job_Role_IDANDJob_Role_Level_ID = "STP_STAFF_SELECTSTAFFBYStaff_IDANDCompany_Department_Job_Role_IDANDJob_Role_Level_ID";

        private const string STP_STAFF_BY_LOGIN_NAME = "STP_STAFF_BY_LOGIN_NAME";
        private const string STP_STAFF_SELECTSTAFF_SUPERVISORBYCOMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_STAFF_SELECTSTAFF_SUPERVISORBYCOMPANY_DEPARTMENT_JOB_ROLE_ID";
        private const string STP_STAFF_SELECTSTAFF_HODBYCOMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_STAFF_SELECTSTAFF_HODBYCOMPANY_DEPARTMENT_JOB_ROLE_ID";
        private const string STP_STAFF_CATEGORY_SELECTSTAFF_STAFF_CATEGORYBYCOMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_STAFF_CATEGORY_SELECTSTAFF_STAFF_CATEGORYBYCOMPANY_DEPARTMENT_JOB_ROLE_ID";

        private const string STP_HOD_APPRAISEES_SELECTHOD_APPRAISEESBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_IDANDOPTION_ID = "STP_HOD_APPRAISEES_SELECTHOD_APPRAISEESBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_IDANDOPTION_ID";
        private const string STP_STAFF_SELECTSTAFF_HODBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_ID = "STP_STAFF_SELECTSTAFF_HODBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_ID";
        private const string STP_STAFF_SELECTSTAFF_SUPERVISORBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_ID = "STP_STAFF_SELECTSTAFF_SUPERVISORBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_ID";
        private const string STP_STAFF_SELECTSTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_ID = "STP_STAFF_SELECTSTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region Staff Parameter declaration 

		//Parameter decleration for STAFF_ID
		public const string PARAM_STAFF_ID_NAME = "@StaffID";
        public const SqlDbType PARAM_STAFF_ID_TYPE = SqlDbType.NChar;
        public const int PARAM_STAFF_ID_SIZE = 10;

		//Parameter decleration for COMPANY_DEPARTMENT_JOB_ROLE_ID
		private const string PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME = "@CompanyDepartmentJobRoleID";
		private const SqlDbType PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE = SqlDbType.Int;
		private const int PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE = 4;

		//Parameter decleration for JOB_ROLE_LEVEL_ID
		private const string PARAM_JOB_ROLE_LEVEL_ID_NAME = "@JobRoleLevelID";
		private const SqlDbType PARAM_JOB_ROLE_LEVEL_ID_TYPE = SqlDbType.Char;
		private const int PARAM_JOB_ROLE_LEVEL_ID_SIZE = 10;

		//Parameter decleration for LAST_NAME
		private const string PARAM_LAST_NAME_NAME = "@LastName";
		private const SqlDbType PARAM_LAST_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_LAST_NAME_SIZE = 20;

		//Parameter decleration for FIRST_NAME
		private const string PARAM_FIRST_NAME_NAME = "@FirstName";
		private const SqlDbType PARAM_FIRST_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_FIRST_NAME_SIZE = 20;

		//Parameter decleration for OTHER_NAME
		private const string PARAM_OTHER_NAME_NAME = "@OtherName";
		private const SqlDbType PARAM_OTHER_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_OTHER_NAME_SIZE = 50;

		//Parameter decleration for LOGIN_NAME
		private const string PARAM_LOGIN_NAME_NAME = "@LoginName";
		private const SqlDbType PARAM_LOGIN_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_LOGIN_NAME_SIZE = 30;

		//Parameter decleration for EMAIL
		private const string PARAM_EMAIL_NAME = "@Email";
		private const SqlDbType PARAM_EMAIL_TYPE = SqlDbType.VarChar;
		private const int PARAM_EMAIL_SIZE = 100;

        //Parameter decleration for LOCATION
        private const string PARAM_LOCATION_ID_NAME = "@Location_ID";
        private const SqlDbType PARAM_LOCATION_ID_TYPE = SqlDbType.NChar;
        private const int PARAM_LOCATION_ID_SIZE = 3;

        //Parameter decleration for LOCATION
        private const string PARAM_LOCATION_NAME_NAME = "@Location_Name";
        private const SqlDbType PARAM_LOCATION_NAME_TYPE = SqlDbType.VarChar;
        private const int PARAM_LOCATION_NAME_SIZE = 100;

        //Parameter decleration for PERIOD_ID
        private const string PARAM_PERIOD_ID_NAME = "@PeriodID";
        private const SqlDbType PARAM_PERIOD_ID_TYPE = SqlDbType.Int;
        private const int PARAM_PERIOD_ID_SIZE = 4;

        //Parameter decleration for ROLE_ID
        private const string PARAM_ROLE_ID_NAME = "@RoleId";
        private const SqlDbType PARAM_ROLE_ID_TYPE = SqlDbType.Int;
        private const int PARAM_ROLE_ID_SIZE = 4;

		#endregion

		//==========================================================================================
		//Staff Table Field Name Declaration
		//==========================================================================================
		#region Staff Field Name declaration 

		public string FIELD_STAFF_ID { get { return "Staff_ID"; } }
		public string FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID { get { return "Company_Department_Job_Role_ID"; } }
		public string FIELD_JOB_ROLE_LEVEL_ID { get { return "Job_Role_Level_ID"; } }
		public string FIELD_LAST_NAME { get { return "Last_Name"; } }
		public string FIELD_FIRST_NAME { get { return "First_Name"; } }
		public string FIELD_OTHER_NAME { get { return "Other_Name"; } }
		public string FIELD_LOGIN_NAME { get { return "Login_Name"; } }
		public string FIELD_EMAIL { get { return "Email"; } }

        public string FIELD_LOCATION_NAME { get { return "Location_Name"; } }
        public string FIELD_LOCATION_ID { get { return "Location_ID"; } }
        public string FIELD_ROLE_ID { get { return "Role_Id"; } }

		#endregion

		//Table name declarations for Staff in the database, this will be used for dataset reference
		public string STAFF_TABLE_NAME  = "STAFF";

		//==========================================================================================
		//public StaffDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region StaffDb Class Methods 

		public bool InsertStaff(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, string lastName, string firstName, string otherName, string loginName, string email, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertStaff";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));
				param.Add(MakeParam(PARAM_LAST_NAME_NAME, PARAM_LAST_NAME_TYPE, PARAM_LAST_NAME_SIZE, lastName));
				param.Add(MakeParam(PARAM_FIRST_NAME_NAME, PARAM_FIRST_NAME_TYPE, PARAM_FIRST_NAME_SIZE, firstName));
				param.Add(MakeParam(PARAM_OTHER_NAME_NAME, PARAM_OTHER_NAME_TYPE, PARAM_OTHER_NAME_SIZE, otherName));
				param.Add(MakeParam(PARAM_LOGIN_NAME_NAME, PARAM_LOGIN_NAME_TYPE, PARAM_LOGIN_NAME_SIZE, loginName));
				param.Add(MakeParam(PARAM_EMAIL_NAME, PARAM_EMAIL_TYPE, PARAM_EMAIL_SIZE, email));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_INSERTSTAFF, param, transaction) == 0)
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

		public bool DeleteStaffByStaffId(string staffId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteStaffByStaffId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_DELETESTAFFBYSTAFF_ID, param, transaction) == 0)
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

		public bool DeleteStaffByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteStaffByCompanyDepartmentJobRoleId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_DELETESTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, transaction) == 0)
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

		public bool DeleteStaffByJobRoleLevelId(string jobRoleLevelId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteStaffByJobRoleLevelId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_DELETESTAFFBYJOB_ROLE_LEVEL_ID, param, transaction) == 0)
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

		public bool DeleteStaffByStaff_IDAndCompany_Department_Job_Role_IDAndJob_Role_Level_ID(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteStaffByStaff_IDAndCompany_Department_Job_Role_IDAndJob_Role_Level_ID";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_DELETESTAFFBYStaff_IDANDCompany_Department_Job_Role_IDANDJob_Role_Level_ID, param, transaction) == 0)
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

		public bool UpdateStaffByStaffId(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, string lastName, string firstName, string otherName, string loginName, string email, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateStaffByStaffId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));
				param.Add(MakeParam(PARAM_LAST_NAME_NAME, PARAM_LAST_NAME_TYPE, PARAM_LAST_NAME_SIZE, lastName));
				param.Add(MakeParam(PARAM_FIRST_NAME_NAME, PARAM_FIRST_NAME_TYPE, PARAM_FIRST_NAME_SIZE, firstName));
				param.Add(MakeParam(PARAM_OTHER_NAME_NAME, PARAM_OTHER_NAME_TYPE, PARAM_OTHER_NAME_SIZE, otherName));
				param.Add(MakeParam(PARAM_LOGIN_NAME_NAME, PARAM_LOGIN_NAME_TYPE, PARAM_LOGIN_NAME_SIZE, loginName));
				param.Add(MakeParam(PARAM_EMAIL_NAME, PARAM_EMAIL_TYPE, PARAM_EMAIL_SIZE, email));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_UPDATESTAFFBYSTAFF_ID, param, transaction) == 0)
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

		public bool UpdateStaffByCompanyDepartmentJobRoleId(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, string lastName, string firstName, string otherName, string loginName, string email, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateStaffByCompanyDepartmentJobRoleId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));
				param.Add(MakeParam(PARAM_LAST_NAME_NAME, PARAM_LAST_NAME_TYPE, PARAM_LAST_NAME_SIZE, lastName));
				param.Add(MakeParam(PARAM_FIRST_NAME_NAME, PARAM_FIRST_NAME_TYPE, PARAM_FIRST_NAME_SIZE, firstName));
				param.Add(MakeParam(PARAM_OTHER_NAME_NAME, PARAM_OTHER_NAME_TYPE, PARAM_OTHER_NAME_SIZE, otherName));
				param.Add(MakeParam(PARAM_LOGIN_NAME_NAME, PARAM_LOGIN_NAME_TYPE, PARAM_LOGIN_NAME_SIZE, loginName));
				param.Add(MakeParam(PARAM_EMAIL_NAME, PARAM_EMAIL_TYPE, PARAM_EMAIL_SIZE, email));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_UPDATESTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, transaction) == 0)
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

		public bool UpdateStaffByJobRoleLevelId(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, string lastName, string firstName, string otherName, string loginName, string email, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateStaffByJobRoleLevelId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));
				param.Add(MakeParam(PARAM_LAST_NAME_NAME, PARAM_LAST_NAME_TYPE, PARAM_LAST_NAME_SIZE, lastName));
				param.Add(MakeParam(PARAM_FIRST_NAME_NAME, PARAM_FIRST_NAME_TYPE, PARAM_FIRST_NAME_SIZE, firstName));
				param.Add(MakeParam(PARAM_OTHER_NAME_NAME, PARAM_OTHER_NAME_TYPE, PARAM_OTHER_NAME_SIZE, otherName));
				param.Add(MakeParam(PARAM_LOGIN_NAME_NAME, PARAM_LOGIN_NAME_TYPE, PARAM_LOGIN_NAME_SIZE, loginName));
				param.Add(MakeParam(PARAM_EMAIL_NAME, PARAM_EMAIL_TYPE, PARAM_EMAIL_SIZE, email));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_UPDATESTAFFBYJOB_ROLE_LEVEL_ID, param, transaction) == 0)
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

		public bool UpdateStaffByStaff_IDAndCompany_Department_Job_Role_IDAndJob_Role_Level_ID(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, string lastName, string firstName, string otherName, string loginName, string email, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateStaffByStaff_IDAndCompany_Department_Job_Role_IDAndJob_Role_Level_ID";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));
				param.Add(MakeParam(PARAM_LAST_NAME_NAME, PARAM_LAST_NAME_TYPE, PARAM_LAST_NAME_SIZE, lastName));
				param.Add(MakeParam(PARAM_FIRST_NAME_NAME, PARAM_FIRST_NAME_TYPE, PARAM_FIRST_NAME_SIZE, firstName));
				param.Add(MakeParam(PARAM_OTHER_NAME_NAME, PARAM_OTHER_NAME_TYPE, PARAM_OTHER_NAME_SIZE, otherName));
				param.Add(MakeParam(PARAM_LOGIN_NAME_NAME, PARAM_LOGIN_NAME_TYPE, PARAM_LOGIN_NAME_SIZE, loginName));
				param.Add(MakeParam(PARAM_EMAIL_NAME, PARAM_EMAIL_TYPE, PARAM_EMAIL_SIZE, email));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_UPDATESTAFFBYStaff_IDANDCompany_Department_Job_Role_IDANDJob_Role_Level_ID, param, transaction) == 0)
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

		public DataSet SelectAllStaff()
		{
			//const string METHOD_NAME  = "SelectAllStaff";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_STAFF_SELECTALLSTAFF, null, STAFF_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

        public DataSet SelectStaffByStaffId(string staffId, int periodId)
		{
			//const string METHOD_NAME  = "SelectStaffByStaffId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_STAFF_SELECTSTAFFBYSTAFF_ID, param, STAFF_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

        public DataSet SelectStaffByCompanyDepartmentJobRoleIdAndPeriodId(int companyDepartmentJobRoleId, int periodId)
        {
            //const string METHOD_NAME  = "SelectStaffByCompanyDepartmentJobRoleIdAndPeriodId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_STAFF_SELECTSTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_ID, param, STAFF_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        //public DataSet SelectStaffByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId)
        //{
        //    //const string METHOD_NAME  = "SelectStaffByCompanyDepartmentJobRoleId";

        //    try
        //    {
        //        //Method parameter declaration
        //        ArrayList param = new ArrayList();

        //        param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));

        //        //Execute Stored Procedure
        //        return ExecuteDataset(STP_STAFF_SELECTSTAFFBYCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, STAFF_TABLE_NAME);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        public DataSet SelectStaffSupervisorByCompanyDepartmentJobRoleIdAndPeriodId(int companyDepartmentJobRoleId, int periodId)
        {
            //const string METHOD_NAME  = "SelectStaffSupervisorByCompanyDepartmentJobRoleId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_STAFF_SELECTSTAFF_SUPERVISORBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_ID, param, STAFF_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        //public DataSet SelectStaffSupervisorByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId)
        //{
        //    //const string METHOD_NAME  = "SelectStaffSupervisorByCompanyDepartmentJobRoleId";

        //    try
        //    {
        //        //Method parameter declaration
        //        ArrayList param = new ArrayList();

        //        param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));

        //        //Execute Stored Procedure
        //        return ExecuteDataset(STP_STAFF_SELECTSTAFF_SUPERVISORBYCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, STAFF_TABLE_NAME);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        public DataSet SelectStaffHodByCompanyDepartmentJobRoleIdAndPeriodId(int companyDepartmentJobRoleId, int periodId)
        {
            //const string METHOD_NAME  = "SelectStaffHodByCompanyDepartmentJobRoleId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure

                return ExecuteDataset(STP_STAFF_SELECTSTAFF_HODBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_ID, param, STAFF_TABLE_NAME);
                //return ExecuteDataset(STP_STAFF_SELECTSTAFF_HODBYCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, STAFF_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        //public DataSet SelectStaffHodByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId)
        //{
        //    //const string METHOD_NAME  = "SelectStaffHodByCompanyDepartmentJobRoleId";

        //    try
        //    {
        //        //Method parameter declaration
        //        ArrayList param = new ArrayList();

        //        param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));

        //        //Execute Stored Procedure
        //        return ExecuteDataset(STP_STAFF_SELECTSTAFF_HODBYCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, STAFF_TABLE_NAME);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

		public DataSet SelectStaffByJobRoleLevelId(string jobRoleLevelId)
		{
			//const string METHOD_NAME  = "SelectStaffByJobRoleLevelId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_STAFF_SELECTSTAFFBYJOB_ROLE_LEVEL_ID, param, STAFF_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectStaffByStaff_IDAndCompany_Department_Job_Role_IDAndJob_Role_Level_ID(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId)
		{
			//const string METHOD_NAME  = "SelectStaffByStaff_IDAndCompany_Department_Job_Role_IDAndJob_Role_Level_ID";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_JOB_ROLE_LEVEL_ID_NAME, PARAM_JOB_ROLE_LEVEL_ID_TYPE, PARAM_JOB_ROLE_LEVEL_ID_SIZE, jobRoleLevelId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_STAFF_SELECTSTAFFBYStaff_IDANDCompany_Department_Job_Role_IDANDJob_Role_Level_ID, param, STAFF_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

        public DataSet SelectStaffByLoginName(string loginName, int periodId)
        {
            //const string METHOD_NAME  = "SelectStaffByLoginName";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_LOGIN_NAME_NAME, PARAM_LOGIN_NAME_TYPE, PARAM_LOGIN_NAME_SIZE, loginName));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_STAFF_BY_LOGIN_NAME, param, STAFF_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public DataSet SelectStaffTypeByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId, int periodId)
        {
            //const string METHOD_NAME  = "SelectStaffTypeByCompanyDepartmentJobRoleId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_STAFF_CATEGORY_SELECTSTAFF_STAFF_CATEGORYBYCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, STAFF_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }
                
        public DataSet SelectHodAppraiseesByCompanyDepartmentJobRoleIdAndPeriodIdAndOptionId(int companyDepartmentJobRoleId, int periodId, byte optionId)
        {
            //const string METHOD_NAME  = "SelectHodAppraiseesByCompanyDepartmentJobRoleIdAndPeriodIdAndOptionId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
                param.Add(MakeParam("@PeriodID", SqlDbType.VarChar, 4, periodId));
                param.Add(MakeParam("@OptionID", SqlDbType.VarChar, 1, optionId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_HOD_APPRAISEES_SELECTHOD_APPRAISEESBYCOMPANY_DEPARTMENT_JOB_ROLE_IDANDPERIOD_IDANDOPTION_ID, param, STAFF_TABLE_NAME);
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


