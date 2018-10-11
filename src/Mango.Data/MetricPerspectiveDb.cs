//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 18/07/2011 14:09:29

//Description: This Class represents the data tier layer class for MetricPerspective table.
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
    public class MetricPerspectiveDb : DataAccess.DataAccess
	{
		private const string CLASS_NAME = "MetricPerspectiveDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  MetricPerspective Stored Procedure declaration

		private const string STP_METRIC_PERSPECTIVE_INSERTMETRIC_PERSPECTIVE = "STP_METRIC_PERSPECTIVE_INSERTMETRIC_PERSPECTIVE";
		private const string STP_METRIC_PERSPECTIVE_DELETEMETRIC_PERSPECTIVEBYMETRIC_PERSPECTIVE_ID = "STP_METRIC_PERSPECTIVE_DELETEMETRIC_PERSPECTIVEBYMETRIC_PERSPECTIVE_ID";
		private const string STP_METRIC_PERSPECTIVE_UPDATEMETRIC_PERSPECTIVEBYMETRIC_PERSPECTIVE_ID = "STP_METRIC_PERSPECTIVE_UPDATEMETRIC_PERSPECTIVEBYMETRIC_PERSPECTIVE_ID";
		private const string STP_METRIC_PERSPECTIVE_SELECTALLMETRIC_PERSPECTIVE = "STP_METRIC_PERSPECTIVE_SELECTALLMETRIC_PERSPECTIVE";
		private const string STP_METRIC_PERSPECTIVE_SELECTMETRIC_PERSPECTIVEBYMETRIC_PERSPECTIVE_ID = "STP_METRIC_PERSPECTIVE_SELECTMETRIC_PERSPECTIVEBYMETRIC_PERSPECTIVE_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region MetricPerspective Parameter declaration 

		//Parameter decleration for METRIC_PERSPECTIVE_ID
		private const string PARAM_METRIC_PERSPECTIVE_ID_NAME = "@MetricPerspectiveID";
		private const SqlDbType PARAM_METRIC_PERSPECTIVE_ID_TYPE = SqlDbType.Int;
		private const int PARAM_METRIC_PERSPECTIVE_ID_SIZE = 4;

		//Parameter decleration for METRIC_PERSPECTIVE_NAME
		private const string PARAM_METRIC_PERSPECTIVE_NAME_NAME = "@MetricPerspectiveName";
		private const SqlDbType PARAM_METRIC_PERSPECTIVE_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_METRIC_PERSPECTIVE_NAME_SIZE = 50;

		//Parameter decleration for PERCENTAGE
		private const string PARAM_PERCENTAGE_NAME = "@Percentage";
		private const SqlDbType PARAM_PERCENTAGE_TYPE = SqlDbType.TinyInt;
		private const int PARAM_PERCENTAGE_SIZE = 1;

		#endregion

		//==========================================================================================
		//MetricPerspective Table Field Name Declaration
		//==========================================================================================
		#region MetricPerspective Field Name declaration 

		public string FIELD_METRIC_PERSPECTIVE_ID { get { return "Metric_Perspective_ID"; } }
		public string FIELD_METRIC_PERSPECTIVE_NAME { get { return "Metric_Perspective_Name"; } }
		public string FIELD_PERCENTAGE { get { return "Percentage"; } }

		#endregion

		//Table name declarations for MetricPerspective in the database, this will be used for dataset reference
		public string METRICPERSPECTIVE_TABLE_NAME  = "METRICPERSPECTIVE";

		//==========================================================================================
		//public MetricPerspectiveDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region MetricPerspectiveDb Class Methods 

		public bool InsertMetricPerspective(DataSet dsAuditItem, int metricPerspectiveId, string metricPerspectiveName, byte percentage, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertMetricPerspective";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_ID_NAME, PARAM_METRIC_PERSPECTIVE_ID_TYPE, PARAM_METRIC_PERSPECTIVE_ID_SIZE, metricPerspectiveId));
				param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_NAME_NAME, PARAM_METRIC_PERSPECTIVE_NAME_TYPE, PARAM_METRIC_PERSPECTIVE_NAME_SIZE, metricPerspectiveName));
				param.Add(MakeParam(PARAM_PERCENTAGE_NAME, PARAM_PERCENTAGE_TYPE, PARAM_PERCENTAGE_SIZE, percentage));

				//Execute Stored Procedure
				if (ExecuteProc(STP_METRIC_PERSPECTIVE_INSERTMETRIC_PERSPECTIVE, param, transaction) == 0)
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

		public bool DeleteMetricPerspectiveByMetricPerspectiveId(DataSet dsAuditItem, int metricPerspectiveId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteMetricPerspectiveByMetricPerspectiveId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_ID_NAME, PARAM_METRIC_PERSPECTIVE_ID_TYPE, PARAM_METRIC_PERSPECTIVE_ID_SIZE, metricPerspectiveId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_METRIC_PERSPECTIVE_DELETEMETRIC_PERSPECTIVEBYMETRIC_PERSPECTIVE_ID, param, transaction) == 0)
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

		public bool UpdateMetricPerspectiveByMetricPerspectiveId(DataSet dsAuditItem, int metricPerspectiveId, string metricPerspectiveName, byte percentage, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateMetricPerspectiveByMetricPerspectiveId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_ID_NAME, PARAM_METRIC_PERSPECTIVE_ID_TYPE, PARAM_METRIC_PERSPECTIVE_ID_SIZE, metricPerspectiveId));
				param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_NAME_NAME, PARAM_METRIC_PERSPECTIVE_NAME_TYPE, PARAM_METRIC_PERSPECTIVE_NAME_SIZE, metricPerspectiveName));
				param.Add(MakeParam(PARAM_PERCENTAGE_NAME, PARAM_PERCENTAGE_TYPE, PARAM_PERCENTAGE_SIZE, percentage));

				//Execute Stored Procedure
				if (ExecuteProc(STP_METRIC_PERSPECTIVE_UPDATEMETRIC_PERSPECTIVEBYMETRIC_PERSPECTIVE_ID, param, transaction) == 0)
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

		public DataSet SelectAllMetricPerspective()
		{
			//const string METHOD_NAME  = "SelectAllMetricPerspective";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_METRIC_PERSPECTIVE_SELECTALLMETRIC_PERSPECTIVE, null, METRICPERSPECTIVE_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectMetricPerspectiveByMetricPerspectiveId(int metricPerspectiveId)
		{
			//const string METHOD_NAME  = "SelectMetricPerspectiveByMetricPerspectiveId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_METRIC_PERSPECTIVE_ID_NAME, PARAM_METRIC_PERSPECTIVE_ID_TYPE, PARAM_METRIC_PERSPECTIVE_ID_SIZE, metricPerspectiveId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_METRIC_PERSPECTIVE_SELECTMETRIC_PERSPECTIVEBYMETRIC_PERSPECTIVE_ID, param, METRICPERSPECTIVE_TABLE_NAME);
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


