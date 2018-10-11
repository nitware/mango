//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:08:39

//Description: This Class represents the data tier layer class for Company table.
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
    public class CompanyDb : DataAccess.DataAccess
	{
		private const string CLASS_NAME = "CompanyDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  Company Stored Procedure declaration

		private const string STP_COMPANY_INSERTCOMPANY = "STP_COMPANY_INSERTCOMPANY";
		private const string STP_COMPANY_DELETECOMPANYBYCOMPANY_ID = "STP_COMPANY_DELETECOMPANYBYCOMPANY_ID";
		private const string STP_COMPANY_UPDATECOMPANYBYCOMPANY_ID = "STP_COMPANY_UPDATECOMPANYBYCOMPANY_ID";
		private const string STP_COMPANY_SELECTALLCOMPANY = "STP_COMPANY_SELECTALLCOMPANY";
		private const string STP_COMPANY_SELECTCOMPANYBYCOMPANY_ID = "STP_COMPANY_SELECTCOMPANYBYCOMPANY_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region Company Parameter declaration 

		//Parameter decleration for COMPANY_ID
		private const string PARAM_COMPANY_ID_NAME = "@CompanyID";
		private const SqlDbType PARAM_COMPANY_ID_TYPE = SqlDbType.TinyInt;
		private const int PARAM_COMPANY_ID_SIZE = 1;

		//Parameter decleration for COMPANY_NAME
		private const string PARAM_COMPANY_NAME_NAME = "@CompanyName";
		private const SqlDbType PARAM_COMPANY_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_COMPANY_NAME_SIZE = 50;

		//Parameter decleration for COMPANY_DESCRIPTION
		private const string PARAM_COMPANY_DESCRIPTION_NAME = "@CompanyDescription";
		private const SqlDbType PARAM_COMPANY_DESCRIPTION_TYPE = SqlDbType.VarChar;
		private const int PARAM_COMPANY_DESCRIPTION_SIZE = 200;

		#endregion

		//==========================================================================================
		//Company Table Field Name Declaration
		//==========================================================================================
		#region Company Field Name declaration 

		public string FIELD_COMPANY_ID { get { return "Company_ID"; } }
		public string FIELD_COMPANY_NAME { get { return "Company_Name"; } }
		public string FIELD_COMPANY_DESCRIPTION { get { return "Company_Description"; } }

		#endregion

		//Table name declarations for Company in the database, this will be used for dataset reference
		public string COMPANY_TABLE_NAME  = "COMPANY";

		//==========================================================================================
		//public CompanyDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region CompanyDb Class Methods 

		public bool InsertCompany(DataSet dsAuditItem, byte companyId, string companyName, string companyDescription, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertCompany";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_COMPANY_NAME_NAME, PARAM_COMPANY_NAME_TYPE, PARAM_COMPANY_NAME_SIZE, companyName));
				param.Add(MakeParam(PARAM_COMPANY_DESCRIPTION_NAME, PARAM_COMPANY_DESCRIPTION_TYPE, PARAM_COMPANY_DESCRIPTION_SIZE, companyDescription));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_INSERTCOMPANY, param, transaction) == 0)
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

		public bool DeleteCompanyByCompanyId(DataSet dsAuditItem, byte companyId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCompanyByCompanyId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_DELETECOMPANYBYCOMPANY_ID, param, transaction) == 0)
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

		public bool UpdateCompanyByCompanyId(DataSet dsAuditItem, byte companyId, string companyName, string companyDescription, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateCompanyByCompanyId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));
				param.Add(MakeParam(PARAM_COMPANY_NAME_NAME, PARAM_COMPANY_NAME_TYPE, PARAM_COMPANY_NAME_SIZE, companyName));
				param.Add(MakeParam(PARAM_COMPANY_DESCRIPTION_NAME, PARAM_COMPANY_DESCRIPTION_TYPE, PARAM_COMPANY_DESCRIPTION_SIZE, companyDescription));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMPANY_UPDATECOMPANYBYCOMPANY_ID, param, transaction) == 0)
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

		public DataSet SelectAllCompany()
		{
			//const string METHOD_NAME  = "SelectAllCompany";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_SELECTALLCOMPANY, null, COMPANY_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectCompanyByCompanyId(byte companyId)
		{
			//const string METHOD_NAME  = "SelectCompanyByCompanyId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_COMPANY_ID_NAME, PARAM_COMPANY_ID_TYPE, PARAM_COMPANY_ID_SIZE, companyId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMPANY_SELECTCOMPANYBYCOMPANY_ID, param, COMPANY_TABLE_NAME);
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


