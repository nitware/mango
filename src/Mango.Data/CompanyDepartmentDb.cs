//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:08:52

//Description: This Class represents the data tier layer class for CompanyDepartment table.
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
    public class CompanyDepartmentDb : DataAccess.DataAccess
	{
		private const string CLASS_NAME = "CompanyDepartmentDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  CompanyDepartment Stored Procedure declaration

		private const string STP_COMPANY_DEPARTMENT_INSERTCOMPANY_DEPARTMENT = "STP_COMPANY_DEPARTMENT_INSERTCOMPANY_DEPARTMENT";
		private const string STP_COMPANY_DEPARTMENT_DELETECOMPANY_DEPARTMENTBYCOMPANY_ID = "STP_COMPANY_DEPARTMENT_DELETECOMPANY_DEPARTMENTBYCOMPANY_ID";
		private const string STP_COMPANY_DEPARTMENT_DELETECOMPANY_DEPARTMENTBYDEPARTMENT_ID = "STP_COMPANY_DEPARTMENT_DELETECOMPANY_DEPARTMENTBYDEPARTMENT_ID";
		private const string STP_COMPANY_DEPARTMENT_DELETECOMPANY_DEPARTMENTBYCompany_IDANDDepartment_ID = "STP_COMPANY_DEPARTMENT_DELETECOMPANY_DEPARTMENTBYCompany_IDANDDepartment_ID";
		private const string STP_COMPANY_DEPARTMENT_UPDATECOMPANY_DEPARTMENTBYCOMPANY_ID = "STP_COMPANY_DEPARTMENT_UPDATECOMPANY_DEPARTMENTBYCOMPANY_ID";
		private const string STP_COMPANY_DEPARTMENT_UPDATECOMPANY_DEPARTMENTBYDEPARTMENT_ID = "STP_COMPANY_DEPARTMENT_UPDATECOMPANY_DEPARTMENTBYDEPARTMENT_ID";
		private const string STP_COMPANY_DEPARTMENT_UPDATECOMPANY_DEPARTMENTBYCompany_IDANDDepartment_ID = "STP_COMPANY_DEPARTMENT_UPDATECOMPANY_DEPARTMENTBYCompany_IDANDDepartment_ID";
		private const string STP_COMPANY_DEPARTMENT_SELECTALLCOMPANY_DEPARTMENT = "STP_COMPANY_DEPARTMENT_SELECTALLCOMPANY_DEPARTMENT";
		private const string STP_COMPANY_DEPARTMENT_SELECTCOMPANY_DEPARTMENTBYCOMPANY_ID = "STP_COMPANY_DEPARTMENT_SELECTCOMPANY_DEPARTMENTBYCOMPANY_ID";
		private const string STP_COMPANY_DEPARTMENT_SELECTCOMPANY_DEPARTMENTBYDEPARTMENT_ID = "STP_COMPANY_DEPARTMENT_SELECTCOMPANY_DEPARTMENTBYDEPARTMENT_ID";
		private const string STP_COMPANY_DEPARTMENT_SELECTCOMPANY_DEPARTMENTBYCompany_IDANDDepartment_ID = "STP_COMPANY_DEPARTMENT_SELECTCOMPANY_DEPARTMENTBYCompany_IDANDDepartment_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region CompanyDepartment Parameter declaration 

		//Parameter decleration for COMPANY_ID
		private const string PARAM_COMPANY_ID_NAME = "@CompanyID";
		private const SqlDbType PARAM_COMPANY_ID_TYPE = SqlDbType.TinyInt;
		private const int PARAM_COMPANY_ID_SIZE = 1;

		//Parameter decleration for DEPARTMENT_ID
		private const string PARAM_DEPARTMENT_ID_NAME = "@DepartmentID";
		private const SqlDbType PARAM_DEPARTMENT_ID_TYPE = SqlDbType.NChar;
		private const int PARAM_DEPARTMENT_ID_SIZE = 3;

		//Parameter decleration for DESCRIPTION
		private const string PARAM_DESCRIPTION_NAME = "@Description";
		private const SqlDbType PARAM_DESCRIPTION_TYPE = SqlDbType.VarChar;
		private const int PARAM_DESCRIPTION_SIZE = 150;

		#endregion

		//==========================================================================================
		//CompanyDepartment Table Field Name Declaration
		//==========================================================================================
		#region CompanyDepartment Field Name declaration 

		public string FIELD_COMPANY_ID { get { return "Company_ID"; } }
		public string FIELD_DEPARTMENT_ID { get { return "Department_ID"; } }
		public string FIELD_DESCRIPTION { get { return "Description"; } }

		#endregion

		//Table name declarations for CompanyDepartment in the database, this will be used for dataset reference
		public string COMPANYDEPARTMENT_TABLE_NAME  = "COMPANYDEPARTMENT";

		//==========================================================================================
		//public CompanyDepartmentDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region CompanyDepartmentDb Class Methods 

		public bool InsertCompanyDepartment(DataSet dsAuditItem, byte companyId, string departmentId, string description, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertCompanyDepartment";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_DESCRIPTION_NAME, PARAM_DESCRIPTION_TYPE, PARAM_DESCRIPTION_SIZE, description));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_INSERTCOMPANY_DEPARTMENT, param, transaction) == 0)
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

		public bool DeleteCompanyDepartmentByCompanyId(DataSet dsAuditItem, byte companyId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCompanyDepartmentByCompanyId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_DELETECOMPANY_DEPARTMENTBYCOMPANY_ID, param, transaction) == 0)
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

		public bool DeleteCompanyDepartmentByDepartmentId(DataSet dsAuditItem, string departmentId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCompanyDepartmentByDepartmentId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_DELETECOMPANY_DEPARTMENTBYDEPARTMENT_ID, param, transaction) == 0)
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

		public bool DeleteCompanyDepartmentByCompany_IDAndDepartment_ID(DataSet dsAuditItem, byte companyId, string departmentId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCompanyDepartmentByCompany_IDAndDepartment_ID";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_DELETECOMPANY_DEPARTMENTBYCompany_IDANDDepartment_ID, param, transaction) == 0)
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

		public bool UpdateCompanyDepartmentByCompanyId(DataSet dsAuditItem, byte companyId, string departmentId, string description, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateCompanyDepartmentByCompanyId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_DESCRIPTION_NAME, PARAM_DESCRIPTION_TYPE, PARAM_DESCRIPTION_SIZE, description));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_UPDATECOMPANY_DEPARTMENTBYCOMPANY_ID, param, transaction) == 0)
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

		public bool UpdateCompanyDepartmentByDepartmentId(DataSet dsAuditItem, byte companyId, string departmentId, string description, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateCompanyDepartmentByDepartmentId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_DESCRIPTION_NAME, PARAM_DESCRIPTION_TYPE, PARAM_DESCRIPTION_SIZE, description));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_UPDATECOMPANY_DEPARTMENTBYDEPARTMENT_ID, param, transaction) == 0)
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

		public bool UpdateCompanyDepartmentByCompany_IDAndDepartment_ID(DataSet dsAuditItem, byte companyId, string departmentId, string description, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateCompanyDepartmentByCompany_IDAndDepartment_ID";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_DESCRIPTION_NAME, PARAM_DESCRIPTION_TYPE, PARAM_DESCRIPTION_SIZE, description));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DEPARTMENT_UPDATECOMPANY_DEPARTMENTBYCompany_IDANDDepartment_ID, param, transaction) == 0)
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

		public DataSet SelectAllCompanyDepartment()
		{
			//const string METHOD_NAME  = "SelectAllCompanyDepartment";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_DEPARTMENT_SELECTALLCOMPANY_DEPARTMENT, null, COMPANYDEPARTMENT_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectCompanyDepartmentByCompanyId(byte companyId)
		{
			//const string METHOD_NAME  = "SelectCompanyDepartmentByCompanyId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_DEPARTMENT_SELECTCOMPANY_DEPARTMENTBYCOMPANY_ID, param, COMPANYDEPARTMENT_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectCompanyDepartmentByDepartmentId(string departmentId)
		{
			//const string METHOD_NAME  = "SelectCompanyDepartmentByDepartmentId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_DEPARTMENT_SELECTCOMPANY_DEPARTMENTBYDEPARTMENT_ID, param, COMPANYDEPARTMENT_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectCompanyDepartmentByCompany_IDAndDepartment_ID(DataSet dsAuditItem, byte companyId, string departmentId)
		{
			//const string METHOD_NAME  = "SelectCompanyDepartmentByCompany_IDAndDepartment_ID";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_DEPARTMENT_SELECTCOMPANY_DEPARTMENTBYCompany_IDANDDepartment_ID, param, COMPANYDEPARTMENT_TABLE_NAME);
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


