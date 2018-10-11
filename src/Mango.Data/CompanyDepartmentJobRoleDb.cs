//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:02

//Description: This Class represents the data tier layer class for CompanyDepartmentJobRole table.
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
    public class CompanyDepartmentJobRoleDb : DataAccess.DataAccess
	{
		private const string CLASS_NAME = "CompanyDepartmentJobRoleDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  CompanyDepartmentJobRole Stored Procedure declaration

		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_INSERTCOMPANY_DEPARTMENT_JOB_ROLE = "STP_COMPANY_DEPARTMENT_JOB_ROLE_INSERTCOMPANY_DEPARTMENT_JOB_ROLE";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_DEPARTMENT_JOB_ROLE_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYDEPARTMENT_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYDEPARTMENT_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYJOB_ROLE_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYJOB_ROLE_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYKEYFIELDS = "STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYKEYFIELDS";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_DEPARTMENT_JOB_ROLE_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYDEPARTMENT_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYDEPARTMENT_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYJOB_ROLE_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYJOB_ROLE_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYKEYFIELDS = "STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYKEYFIELDS";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTALLCOMPANY_DEPARTMENT_JOB_ROLE = "STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTALLCOMPANY_DEPARTMENT_JOB_ROLE";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_DEPARTMENT_JOB_ROLE_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYDEPARTMENT_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYDEPARTMENT_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYJOB_ROLE_ID = "STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYJOB_ROLE_ID";
		private const string STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYKEYFIELDS = "STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYKEYFIELDS";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region CompanyDepartmentJobRole Parameter declaration 

		//Parameter decleration for COMPANY_DEPARTMENT_JOB_ROLE_ID
		private const string PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME = "@CompanyDepartmentJobRoleID";
		private const SqlDbType PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE = SqlDbType.Int;
		private const int PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE = 4;

		//Parameter decleration for COMPANY_ID
		private const string PARAM_COMPANY_ID_NAME = "@CompanyID";
		private const SqlDbType PARAM_COMPANY_ID_TYPE = SqlDbType.TinyInt;
		private const int PARAM_COMPANY_ID_SIZE = 1;

		//Parameter decleration for DEPARTMENT_ID
		private const string PARAM_DEPARTMENT_ID_NAME = "@DepartmentID";
		private const SqlDbType PARAM_DEPARTMENT_ID_TYPE = SqlDbType.NChar;
		private const int PARAM_DEPARTMENT_ID_SIZE = 3;

		//Parameter decleration for JOB_ROLE_ID
		private const string PARAM_JOB_ROLE_ID_NAME = "@JobRoleID";
		private const SqlDbType PARAM_JOB_ROLE_ID_TYPE = SqlDbType.SmallInt;
		private const int PARAM_JOB_ROLE_ID_SIZE = 2;

		//Parameter decleration for DESCRIPTION
		private const string PARAM_DESCRIPTION_NAME = "@Description";
		private const SqlDbType PARAM_DESCRIPTION_TYPE = SqlDbType.VarChar;
		private const int PARAM_DESCRIPTION_SIZE = 100;

		#endregion

		//==========================================================================================
		//CompanyDepartmentJobRole Table Field Name Declaration
		//==========================================================================================
		#region CompanyDepartmentJobRole Field Name declaration 

		public string FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID { get { return "Company_Department_Job_Role_ID"; } }
		public string FIELD_COMPANY_ID { get { return "Company_ID"; } }
		public string FIELD_DEPARTMENT_ID { get { return "Department_ID"; } }
		public string FIELD_JOB_ROLE_ID { get { return "Job_Role_ID"; } }
		public string FIELD_DESCRIPTION { get { return "Description"; } }

		#endregion

		//Table name declarations for CompanyDepartmentJobRole in the database, this will be used for dataset reference
		public string COMPANYDEPARTMENTJOBROLE_TABLE_NAME  = "COMPANYDEPARTMENTJOBROLE";

		//==========================================================================================
		//public CompanyDepartmentJobRoleDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region CompanyDepartmentJobRoleDb Class Methods 

		public bool InsertCompanyDepartmentJobRole(DataSet dsAuditItem, int companyDepartmentJobRoleId, byte companyId, string departmentId, Int16 jobRoleId, string description, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertCompanyDepartmentJobRole";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));
				param.Add(MakeParam(PARAM_DESCRIPTION_NAME, PARAM_DESCRIPTION_TYPE, PARAM_DESCRIPTION_SIZE, description));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_INSERTCOMPANY_DEPARTMENT_JOB_ROLE, param, transaction) == 0)
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

		public bool DeleteCompanyDepartmentJobRoleByCompanyDepartmentJobRoleId(DataSet dsAuditItem, int companyDepartmentJobRoleId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCompanyDepartmentJobRoleByCompanyDepartmentJobRoleId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, transaction) == 0)
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

		public bool DeleteCompanyDepartmentJobRoleByCompanyId(DataSet dsAuditItem, byte companyId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCompanyDepartmentJobRoleByCompanyId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_ID, param, transaction) == 0)
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

		public bool DeleteCompanyDepartmentJobRoleByDepartmentId(DataSet dsAuditItem, string departmentId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCompanyDepartmentJobRoleByDepartmentId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYDEPARTMENT_ID, param, transaction) == 0)
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

		public bool DeleteCompanyDepartmentJobRoleByJobRoleId(DataSet dsAuditItem, Int16 jobRoleId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCompanyDepartmentJobRoleByJobRoleId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYJOB_ROLE_ID, param, transaction) == 0)
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

		public bool DeleteCompanyDepartmentJobRoleByCompany_Department_Job_Role_IDAndCompany_IDAndDepartment_IDAndJob_Role_ID(DataSet dsAuditItem, int companyDepartmentJobRoleId, byte companyId, string departmentId, Int16 jobRoleId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCompanyDepartmentJobRoleByCompany_Department_Job_Role_IDAndCompany_IDAndDepartment_IDAndJob_Role_ID";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_DELETECOMPANY_DEPARTMENT_JOB_ROLEBYKEYFIELDS, param, transaction) == 0)
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

		public bool UpdateCompanyDepartmentJobRoleByCompanyDepartmentJobRoleId(DataSet dsAuditItem, int companyDepartmentJobRoleId, byte companyId, string departmentId, Int16 jobRoleId, string description, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateCompanyDepartmentJobRoleByCompanyDepartmentJobRoleId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));
				param.Add(MakeParam(PARAM_DESCRIPTION_NAME, PARAM_DESCRIPTION_TYPE, PARAM_DESCRIPTION_SIZE, description));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, transaction) == 0)
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

		public bool UpdateCompanyDepartmentJobRoleByCompanyId(DataSet dsAuditItem, int companyDepartmentJobRoleId, byte companyId, string departmentId, Int16 jobRoleId, string description, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateCompanyDepartmentJobRoleByCompanyId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));
				param.Add(MakeParam(PARAM_DESCRIPTION_NAME, PARAM_DESCRIPTION_TYPE, PARAM_DESCRIPTION_SIZE, description));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_ID, param, transaction) == 0)
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

		public bool UpdateCompanyDepartmentJobRoleByDepartmentId(DataSet dsAuditItem, int companyDepartmentJobRoleId, byte companyId, string departmentId, Int16 jobRoleId, string description, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateCompanyDepartmentJobRoleByDepartmentId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));
				param.Add(MakeParam(PARAM_DESCRIPTION_NAME, PARAM_DESCRIPTION_TYPE, PARAM_DESCRIPTION_SIZE, description));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYDEPARTMENT_ID, param, transaction) == 0)
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

		public bool UpdateCompanyDepartmentJobRoleByJobRoleId(DataSet dsAuditItem, int companyDepartmentJobRoleId, byte companyId, string departmentId, Int16 jobRoleId, string description, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateCompanyDepartmentJobRoleByJobRoleId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));
				param.Add(MakeParam(PARAM_DESCRIPTION_NAME, PARAM_DESCRIPTION_TYPE, PARAM_DESCRIPTION_SIZE, description));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYJOB_ROLE_ID, param, transaction) == 0)
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

		public bool UpdateCompanyDepartmentJobRoleByCompany_Department_Job_Role_IDAndCompany_IDAndDepartment_IDAndJob_Role_ID(DataSet dsAuditItem, int companyDepartmentJobRoleId, byte companyId, string departmentId, Int16 jobRoleId, string description, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateCompanyDepartmentJobRoleByCompany_Department_Job_Role_IDAndCompany_IDAndDepartment_IDAndJob_Role_ID";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));
				param.Add(MakeParam(PARAM_DESCRIPTION_NAME, PARAM_DESCRIPTION_TYPE, PARAM_DESCRIPTION_SIZE, description));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_JOB_ROLE_UPDATECOMPANY_DEPARTMENT_JOB_ROLEBYKEYFIELDS, param, transaction) == 0)
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

		public DataSet SelectAllCompanyDepartmentJobRole()
		{
			//const string METHOD_NAME  = "SelectAllCompanyDepartmentJobRole";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTALLCOMPANY_DEPARTMENT_JOB_ROLE, null, COMPANYDEPARTMENTJOBROLE_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectCompanyDepartmentJobRoleByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId)
		{
			//const string METHOD_NAME  = "SelectCompanyDepartmentJobRoleByCompanyDepartmentJobRoleId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_DEPARTMENT_JOB_ROLE_ID, param, COMPANYDEPARTMENTJOBROLE_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectCompanyDepartmentJobRoleByCompanyId(byte companyId)
		{
			//const string METHOD_NAME  = "SelectCompanyDepartmentJobRoleByCompanyId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYCOMPANY_ID, param, COMPANYDEPARTMENTJOBROLE_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectCompanyDepartmentJobRoleByDepartmentId(string departmentId)
		{
			//const string METHOD_NAME  = "SelectCompanyDepartmentJobRoleByDepartmentId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYDEPARTMENT_ID, param, COMPANYDEPARTMENTJOBROLE_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectCompanyDepartmentJobRoleByJobRoleId(Int16 jobRoleId)
		{
			//const string METHOD_NAME  = "SelectCompanyDepartmentJobRoleByJobRoleId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYJOB_ROLE_ID, param, COMPANYDEPARTMENTJOBROLE_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectCompanyDepartmentJobRoleByCompany_Department_Job_Role_IDAndCompany_IDAndDepartment_IDAndJob_Role_ID(DataSet dsAuditItem, int companyDepartmentJobRoleId, byte companyId, string departmentId, Int16 jobRoleId)
		{
			//const string METHOD_NAME  = "SelectCompanyDepartmentJobRoleByCompany_Department_Job_Role_IDAndCompany_IDAndDepartment_IDAndJob_Role_ID";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, companyDepartmentJobRoleId));
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_JOB_ROLE_ID_NAME, PARAM_JOB_ROLE_ID_TYPE, PARAM_JOB_ROLE_ID_SIZE, jobRoleId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_DEPARTMENT_JOB_ROLE_SELECTCOMPANY_DEPARTMENT_JOB_ROLEBYKEYFIELDS, param, COMPANYDEPARTMENTJOBROLE_TABLE_NAME);
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


