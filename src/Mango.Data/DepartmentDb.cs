//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:20

//Description: This Class represents the data tier layer class for Department table.
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
    public class DepartmentDb : DataAccess.DataAccess
	{
		private const string CLASS_NAME = "DepartmentDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  Department Stored Procedure declaration

		private const string STP_DEPARTMENT_INSERTDEPARTMENT = "STP_DEPARTMENT_INSERTDEPARTMENT";
		private const string STP_DEPARTMENT_DELETEDEPARTMENTBYDEPARTMENT_ID = "STP_DEPARTMENT_DELETEDEPARTMENTBYDEPARTMENT_ID";
		private const string STP_DEPARTMENT_UPDATEDEPARTMENTBYDEPARTMENT_ID = "STP_DEPARTMENT_UPDATEDEPARTMENTBYDEPARTMENT_ID";
		private const string STP_DEPARTMENT_SELECTALLDEPARTMENT = "STP_DEPARTMENT_SELECTALLDEPARTMENT";
		private const string STP_DEPARTMENT_SELECTDEPARTMENTBYDEPARTMENT_ID = "STP_DEPARTMENT_SELECTDEPARTMENTBYDEPARTMENT_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region Department Parameter declaration 

		//Parameter decleration for DEPARTMENT_ID
		private const string PARAM_DEPARTMENT_ID_NAME = "@DepartmentID";
		private const SqlDbType PARAM_DEPARTMENT_ID_TYPE = SqlDbType.NChar;
		private const int PARAM_DEPARTMENT_ID_SIZE = 3;

		//Parameter decleration for DEPARTMENT_NAME
		private const string PARAM_DEPARTMENT_NAME_NAME = "@DepartmentName";
		private const SqlDbType PARAM_DEPARTMENT_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_DEPARTMENT_NAME_SIZE = 30;

		#endregion

		//==========================================================================================
		//Department Table Field Name Declaration
		//==========================================================================================
		#region Department Field Name declaration 

		public string FIELD_DEPARTMENT_ID { get { return "Department_ID"; } }
		public string FIELD_DEPARTMENT_NAME { get { return "Department_Name"; } }

		#endregion

		//Table name declarations for Department in the database, this will be used for dataset reference
		public string DEPARTMENT_TABLE_NAME  = "DEPARTMENT";

		//==========================================================================================
		//public DepartmentDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region DepartmentDb Class Methods 

		public bool InsertDepartment(DataSet dsAuditItem, string departmentId, string departmentName, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertDepartment";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_DEPARTMENT_NAME_NAME, PARAM_DEPARTMENT_NAME_TYPE, PARAM_DEPARTMENT_NAME_SIZE, departmentName));

				//Execute Stored Procedure
				if (ExecuteProc(STP_DEPARTMENT_INSERTDEPARTMENT, param, transaction) == 0)
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

		public bool DeleteDepartmentByDepartmentId(DataSet dsAuditItem, string departmentId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteDepartmentByDepartmentId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_DEPARTMENT_DELETEDEPARTMENTBYDEPARTMENT_ID, param, transaction) == 0)
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

		public bool UpdateDepartmentByDepartmentId(DataSet dsAuditItem, string departmentId, string departmentName, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateDepartmentByDepartmentId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));
				param.Add(MakeParam(PARAM_DEPARTMENT_NAME_NAME, PARAM_DEPARTMENT_NAME_TYPE, PARAM_DEPARTMENT_NAME_SIZE, departmentName));

				//Execute Stored Procedure
				if (ExecuteProc(STP_DEPARTMENT_UPDATEDEPARTMENTBYDEPARTMENT_ID, param, transaction) == 0)
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

		public DataSet SelectAllDepartment()
		{
			//const string METHOD_NAME  = "SelectAllDepartment";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_DEPARTMENT_SELECTALLDEPARTMENT, null, DEPARTMENT_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectDepartmentByDepartmentId(string departmentId)
		{
			//const string METHOD_NAME  = "SelectDepartmentByDepartmentId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_DEPARTMENT_ID_NAME, PARAM_DEPARTMENT_ID_TYPE, PARAM_DEPARTMENT_ID_SIZE, departmentId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_DEPARTMENT_SELECTDEPARTMENTBYDEPARTMENT_ID, param, DEPARTMENT_TABLE_NAME);
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


