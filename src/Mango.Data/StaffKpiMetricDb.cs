//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 24/07/2011 11:18:38

//Description: This Class represents the data tier layer class for StaffKpiMetric table.
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
    public class StaffKpiMetricDb : DataAccess.DataAccess, IStaffKpiMetricDb
	{
		private const string CLASS_NAME = "StaffKpiMetricDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  StaffKpiMetric Stored Procedure declaration

		private const string STP_STAFF_KPI_METRIC_INSERTSTAFF_KPI_METRIC = "STP_STAFF_KPI_METRIC_INSERTSTAFF_KPI_METRIC";
		private const string STP_STAFF_KPI_METRIC_DELETESTAFF_KPI_METRICBYSTAFF_METRIC_ID = "STP_STAFF_KPI_METRIC_DELETESTAFF_KPI_METRICBYSTAFF_METRIC_ID";
		private const string STP_STAFF_KPI_METRIC_UPDATESTAFF_KPI_METRICBYSTAFF_METRIC_ID = "STP_STAFF_KPI_METRIC_UPDATESTAFF_KPI_METRICBYSTAFF_METRIC_ID";
		private const string STP_STAFF_KPI_METRIC_SELECTALLSTAFF_KPI_METRIC = "STP_STAFF_KPI_METRIC_SELECTALLSTAFF_KPI_METRIC";
		private const string STP_STAFF_KPI_METRIC_SELECTSTAFF_KPI_METRICBYSTAFF_METRIC_ID = "STP_STAFF_KPI_METRIC_SELECTSTAFF_KPI_METRICBYSTAFF_METRIC_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region StaffKpiMetric Parameter declaration 

		//Parameter decleration for STAFF_METRIC_ID
		private const string PARAM_STAFF_METRIC_ID_NAME = "@StaffMetricID";
		private const SqlDbType PARAM_STAFF_METRIC_ID_TYPE = SqlDbType.Int;
		private const int PARAM_STAFF_METRIC_ID_SIZE = 4;

		//Parameter decleration for KPI_SCORE
		private const string PARAM_KPI_SCORE_NAME = "@KpiScore";
		private const SqlDbType PARAM_KPI_SCORE_TYPE = SqlDbType.Decimal;
		private const int PARAM_KPI_SCORE_SIZE = 3;

		#endregion

		//==========================================================================================
		//StaffKpiMetric Table Field Name Declaration
		//==========================================================================================
		#region StaffKpiMetric Field Name declaration 

		public string FIELD_STAFF_METRIC_ID { get { return "Staff_Metric_ID"; } }
		public string FIELD_KPI_SCORE { get { return "Kpi_Score"; } }

		#endregion

		//Table name declarations for StaffKpiMetric in the database, this will be used for dataset reference
		public string STAFFKPIMETRIC_TABLE_NAME  = "STAFFKPIMETRIC";

		//==========================================================================================
		//public StaffKpiMetricDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region StaffKpiMetricDb Class Methods 

		public bool InsertStaffKpiMetric(int staffMetricId, decimal kpiScore, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertStaffKpiMetric";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));
				param.Add(MakeParam(PARAM_KPI_SCORE_NAME, PARAM_KPI_SCORE_TYPE, PARAM_KPI_SCORE_SIZE, kpiScore));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_KPI_METRIC_INSERTSTAFF_KPI_METRIC, param, transaction) == 0)
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

		public bool DeleteStaffKpiMetricByStaffMetricId(int staffMetricId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteStaffKpiMetricByStaffMetricId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_KPI_METRIC_DELETESTAFF_KPI_METRICBYSTAFF_METRIC_ID, param, transaction) == 0)
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

		public bool UpdateStaffKpiMetricByStaffMetricId(int staffMetricId, decimal kpiScore, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateStaffKpiMetricByStaffMetricId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));
				param.Add(MakeParam(PARAM_KPI_SCORE_NAME, PARAM_KPI_SCORE_TYPE, PARAM_KPI_SCORE_SIZE, kpiScore));

				//Execute Stored Procedure
				if (ExecuteProc(STP_STAFF_KPI_METRIC_UPDATESTAFF_KPI_METRICBYSTAFF_METRIC_ID, param, transaction) == 0)
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

		public DataSet SelectAllStaffKpiMetric()
		{
			//const string METHOD_NAME  = "SelectAllStaffKpiMetric";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_STAFF_KPI_METRIC_SELECTALLSTAFF_KPI_METRIC, null, STAFFKPIMETRIC_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectStaffKpiMetricByStaffMetricId(int staffMetricId)
		{
			//const string METHOD_NAME  = "SelectStaffKpiMetricByStaffMetricId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_METRIC_ID_NAME, PARAM_STAFF_METRIC_ID_TYPE, PARAM_STAFF_METRIC_ID_SIZE, staffMetricId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_STAFF_KPI_METRIC_SELECTSTAFF_KPI_METRICBYSTAFF_METRIC_ID, param, STAFFKPIMETRIC_TABLE_NAME);
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


