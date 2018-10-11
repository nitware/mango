//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 24/07/2011 11:17:26

//Description: This Class represents the data tier layer class for AppraisalHeader table.
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
using Mango.Data.Interfaces;

namespace Mango.Data
{
    public class AppraisalHeaderDb : DataAccess.DataAccess, IAppraisalHeaderDb
	{
		private const string CLASS_NAME = "AppraisalHeaderDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  AppraisalHeader Stored Procedure declaration

		private const string STP_APPRAISAL_HEADER_INSERTAPPRAISAL_HEADER = "STP_APPRAISAL_HEADER_INSERTAPPRAISAL_HEADER";
		private const string STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYAPPRAISAL_HEADER_ID = "STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYAPPRAISAL_HEADER_ID";
		private const string STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYPERIOD_ID = "STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYPERIOD_ID";
		private const string STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYSTAFF_ID = "STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYSTAFF_ID";
		private const string STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYSUPERVISOR_ID = "STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYSUPERVISOR_ID";
		private const string STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYHOD_ID = "STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYHOD_ID";
		private const string STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYAppraisal_Header_IDANDPeriod_IDANDStaff_IDANDSupervisor_IDANDHod_ID = "STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYAppraisal_Header_IDANDPeriod_IDANDStaff_IDANDSupervisor_IDANDHod_ID";
		private const string STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYAPPRAISAL_HEADER_ID = "STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYAPPRAISAL_HEADER_ID";
		private const string STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYPERIOD_ID = "STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYPERIOD_ID";
		private const string STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYSTAFF_ID = "STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYSTAFF_ID";
		private const string STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYSUPERVISOR_ID = "STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYSUPERVISOR_ID";
		private const string STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYHOD_ID = "STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYHOD_ID";
		private const string STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYAppraisal_Header_IDANDPeriod_IDANDStaff_IDANDSupervisor_IDANDHod_ID = "STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYAppraisal_Header_IDANDPeriod_IDANDStaff_IDANDSupervisor_IDANDHod_ID";
		private const string STP_APPRAISAL_HEADER_SELECTALLAPPRAISAL_HEADER = "STP_APPRAISAL_HEADER_SELECTALLAPPRAISAL_HEADER";
		private const string STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYAPPRAISAL_HEADER_ID = "STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYAPPRAISAL_HEADER_ID";
		private const string STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYPERIOD_ID = "STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYPERIOD_ID";
		private const string STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYSTAFF_ID = "STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYSTAFF_ID";
		private const string STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYSUPERVISOR_ID = "STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYSUPERVISOR_ID";
		private const string STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYHOD_ID = "STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYHOD_ID";
		private const string STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYAppraisal_Header_IDANDPeriod_IDANDStaff_IDANDSupervisor_IDANDHod_ID = "STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYAppraisal_Header_IDANDPeriod_IDANDStaff_IDANDSupervisor_IDANDHod_ID";


        private const string STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYPERIOD_IDANDSTAFF_ID = "STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYPERIOD_IDANDSTAFF_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region AppraisalHeader Parameter declaration 

		//Parameter decleration for APPRAISAL_HEADER_ID
		private const string PARAM_APPRAISAL_HEADER_ID_NAME = "@AppraisalHeaderID";
		private const SqlDbType PARAM_APPRAISAL_HEADER_ID_TYPE = SqlDbType.BigInt;
		private const int PARAM_APPRAISAL_HEADER_ID_SIZE = 8;

		//Parameter decleration for PERIOD_ID
		private const string PARAM_PERIOD_ID_NAME = "@PeriodID";
		private const SqlDbType PARAM_PERIOD_ID_TYPE = SqlDbType.Int;
		private const int PARAM_PERIOD_ID_SIZE = 4;

		//Parameter decleration for STAFF_ID
		private const string PARAM_STAFF_ID_NAME = "@StaffID";
		private const SqlDbType PARAM_STAFF_ID_TYPE = SqlDbType.NChar;
		private const int PARAM_STAFF_ID_SIZE = 10;

		//Parameter decleration for SUPERVISOR_ID
		private const string PARAM_SUPERVISOR_ID_NAME = "@SupervisorID";
		private const SqlDbType PARAM_SUPERVISOR_ID_TYPE = SqlDbType.NChar;
		private const int PARAM_SUPERVISOR_ID_SIZE = 10;

		//Parameter decleration for APPRAISAL_DATE
		private const string PARAM_APPRAISAL_DATE_NAME = "@AppraisalDate";
		private const SqlDbType PARAM_APPRAISAL_DATE_TYPE = SqlDbType.DateTime;
		private const int PARAM_APPRAISAL_DATE_SIZE = 8;

		//Parameter decleration for STAFF_RESPONSE_DATE
		private const string PARAM_STAFF_RESPONSE_DATE_NAME = "@StaffResponseDate";
		private const SqlDbType PARAM_STAFF_RESPONSE_DATE_TYPE = SqlDbType.DateTime;
		private const int PARAM_STAFF_RESPONSE_DATE_SIZE = 8;

		//Parameter decleration for HOD_ID
		private const string PARAM_HOD_ID_NAME = "@HodID";
		private const SqlDbType PARAM_HOD_ID_TYPE = SqlDbType.NChar;
		private const int PARAM_HOD_ID_SIZE = 10;

		//Parameter decleration for HOD_APPRAISAL_DATE
		private const string PARAM_HOD_APPRAISAL_DATE_NAME = "@HodAppraisalDate";
		private const SqlDbType PARAM_HOD_APPRAISAL_DATE_TYPE = SqlDbType.DateTime;
		private const int PARAM_HOD_APPRAISAL_DATE_SIZE = 8;

        //Parameter decleration for STATUS_ID
        private const string PARAM_STATUS_ID_NAME = "@StatusID";
        private const SqlDbType PARAM_STATUS_ID_TYPE = SqlDbType.TinyInt;
        private const int PARAM_STATUS_ID_SIZE = 1;

		#endregion

		//==========================================================================================
		//AppraisalHeader Table Field Name Declaration
		//==========================================================================================
		#region AppraisalHeader Field Name declaration 

		public string FIELD_APPRAISAL_HEADER_ID { get { return "Appraisal_Header_ID"; } }
		public string FIELD_PERIOD_ID { get { return "Period_ID"; } }
		public string FIELD_STAFF_ID { get { return "Staff_ID"; } }
		public string FIELD_SUPERVISOR_ID { get { return "Supervisor_ID"; } }
		public string FIELD_APPRAISAL_DATE { get { return "Appraisal_Date"; } }
		public string FIELD_STAFF_RESPONSE_DATE { get { return "Staff_Response_Date"; } }
		public string FIELD_HOD_ID { get { return "Hod_ID"; } }
		public string FIELD_HOD_APPRAISAL_DATE { get { return "Hod_Appraisal_Date"; } }
        public string FIELD_STATUS_ID_DATE { get { return "Status_ID"; } }

		#endregion

		//Table name declarations for AppraisalHeader in the database, this will be used for dataset reference
		public string APPRAISALHEADER_TABLE_NAME  = "APPRAISALHEADER";

		//==========================================================================================
		//public AppraisalHeaderDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region AppraisalHeaderDb Class Methods 

        public int InsertAppraisalHeader(int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime? staffResponseDate, string hodId, DateTime? hodAppraisalDate, byte statusId, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertAppraisalHeader";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeOutputParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE));
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_SUPERVISOR_ID_NAME, PARAM_SUPERVISOR_ID_TYPE, PARAM_SUPERVISOR_ID_SIZE, supervisorId));
				param.Add(MakeParam(PARAM_APPRAISAL_DATE_NAME, PARAM_APPRAISAL_DATE_TYPE, PARAM_APPRAISAL_DATE_SIZE, appraisalDate));
				param.Add(MakeParam(PARAM_STAFF_RESPONSE_DATE_NAME, PARAM_STAFF_RESPONSE_DATE_TYPE, PARAM_STAFF_RESPONSE_DATE_SIZE, staffResponseDate));
				param.Add(MakeParam(PARAM_HOD_ID_NAME, PARAM_HOD_ID_TYPE, PARAM_HOD_ID_SIZE, hodId));
				param.Add(MakeParam(PARAM_HOD_APPRAISAL_DATE_NAME, PARAM_HOD_APPRAISAL_DATE_TYPE, PARAM_HOD_APPRAISAL_DATE_SIZE, hodAppraisalDate));
                param.Add(MakeParam(PARAM_STATUS_ID_NAME, PARAM_STATUS_ID_TYPE, PARAM_STATUS_ID_SIZE, statusId));

                return Convert.ToInt32(ExecuteProcWithOutputParam(STP_APPRAISAL_HEADER_INSERTAPPRAISAL_HEADER, param, transaction));

                ////Execute Stored Procedure
                //if (ExecuteProc(STP_APPRAISAL_HEADER_INSERTAPPRAISAL_HEADER, param, transaction) == 0)
                //{
                //    return true;
                //}
                //else 
                //{
                //    return false;
                //}
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

        public bool DeleteAppraisalHeaderByAppraisalHeaderId(long appraisalHeaderId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteAppraisalHeaderByAppraisalHeaderId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYAPPRAISAL_HEADER_ID, param, transaction) == 0)
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

		public bool DeleteAppraisalHeaderByPeriodId(int periodId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteAppraisalHeaderByPeriodId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYPERIOD_ID, param, transaction) == 0)
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

		public bool DeleteAppraisalHeaderByStaffId(string staffId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteAppraisalHeaderByStaffId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYSTAFF_ID, param, transaction) == 0)
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

		public bool DeleteAppraisalHeaderBySupervisorId(string supervisorId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteAppraisalHeaderBySupervisorId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_SUPERVISOR_ID_NAME, PARAM_SUPERVISOR_ID_TYPE, PARAM_SUPERVISOR_ID_SIZE, supervisorId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYSUPERVISOR_ID, param, transaction) == 0)
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

		public bool DeleteAppraisalHeaderByHodId(string hodId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteAppraisalHeaderByHodId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_HOD_ID_NAME, PARAM_HOD_ID_TYPE, PARAM_HOD_ID_SIZE, hodId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYHOD_ID, param, transaction) == 0)
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

        public bool DeleteAppraisalHeaderByAppraisal_Header_IDAndPeriod_IDAndStaff_IDAndSupervisor_IDAndHod_ID(long appraisalHeaderId, int periodId, string staffId, string supervisorId, string hodId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteAppraisalHeaderByAppraisal_Header_IDAndPeriod_IDAndStaff_IDAndSupervisor_IDAndHod_ID";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_SUPERVISOR_ID_NAME, PARAM_SUPERVISOR_ID_TYPE, PARAM_SUPERVISOR_ID_SIZE, supervisorId));
				param.Add(MakeParam(PARAM_HOD_ID_NAME, PARAM_HOD_ID_TYPE, PARAM_HOD_ID_SIZE, hodId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_DELETEAPPRAISAL_HEADERBYAppraisal_Header_IDANDPeriod_IDANDStaff_IDANDSupervisor_IDANDHod_ID, param, transaction) == 0)
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

        public bool UpdateAppraisalHeaderByAppraisalHeaderId(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime? staffResponseDate, string hodId, DateTime? hodAppraisalDate, byte statusId, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateAppraisalHeaderByAppraisalHeaderId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_SUPERVISOR_ID_NAME, PARAM_SUPERVISOR_ID_TYPE, PARAM_SUPERVISOR_ID_SIZE, supervisorId));
				param.Add(MakeParam(PARAM_APPRAISAL_DATE_NAME, PARAM_APPRAISAL_DATE_TYPE, PARAM_APPRAISAL_DATE_SIZE, appraisalDate));
				param.Add(MakeParam(PARAM_STAFF_RESPONSE_DATE_NAME, PARAM_STAFF_RESPONSE_DATE_TYPE, PARAM_STAFF_RESPONSE_DATE_SIZE, staffResponseDate));
				param.Add(MakeParam(PARAM_HOD_ID_NAME, PARAM_HOD_ID_TYPE, PARAM_HOD_ID_SIZE, hodId));
				param.Add(MakeParam(PARAM_HOD_APPRAISAL_DATE_NAME, PARAM_HOD_APPRAISAL_DATE_TYPE, PARAM_HOD_APPRAISAL_DATE_SIZE, hodAppraisalDate));
                param.Add(MakeParam(PARAM_STATUS_ID_NAME, PARAM_STATUS_ID_TYPE, PARAM_STATUS_ID_SIZE, statusId));
                
                //Execute Stored Procedure
                if (ExecuteProc(STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYAPPRAISAL_HEADER_ID, param, transaction) == 0)
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

        public bool UpdateAppraisalHeaderByPeriodId(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime staffResponseDate, string hodId, DateTime hodAppraisalDate, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateAppraisalHeaderByPeriodId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_SUPERVISOR_ID_NAME, PARAM_SUPERVISOR_ID_TYPE, PARAM_SUPERVISOR_ID_SIZE, supervisorId));
				param.Add(MakeParam(PARAM_APPRAISAL_DATE_NAME, PARAM_APPRAISAL_DATE_TYPE, PARAM_APPRAISAL_DATE_SIZE, appraisalDate));
				param.Add(MakeParam(PARAM_STAFF_RESPONSE_DATE_NAME, PARAM_STAFF_RESPONSE_DATE_TYPE, PARAM_STAFF_RESPONSE_DATE_SIZE, staffResponseDate));
				param.Add(MakeParam(PARAM_HOD_ID_NAME, PARAM_HOD_ID_TYPE, PARAM_HOD_ID_SIZE, hodId));
				param.Add(MakeParam(PARAM_HOD_APPRAISAL_DATE_NAME, PARAM_HOD_APPRAISAL_DATE_TYPE, PARAM_HOD_APPRAISAL_DATE_SIZE, hodAppraisalDate));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYPERIOD_ID, param, transaction) == 0)
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

        public bool UpdateAppraisalHeaderByStaffId(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime staffResponseDate, string hodId, DateTime hodAppraisalDate, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateAppraisalHeaderByStaffId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_SUPERVISOR_ID_NAME, PARAM_SUPERVISOR_ID_TYPE, PARAM_SUPERVISOR_ID_SIZE, supervisorId));
				param.Add(MakeParam(PARAM_APPRAISAL_DATE_NAME, PARAM_APPRAISAL_DATE_TYPE, PARAM_APPRAISAL_DATE_SIZE, appraisalDate));
				param.Add(MakeParam(PARAM_STAFF_RESPONSE_DATE_NAME, PARAM_STAFF_RESPONSE_DATE_TYPE, PARAM_STAFF_RESPONSE_DATE_SIZE, staffResponseDate));
				param.Add(MakeParam(PARAM_HOD_ID_NAME, PARAM_HOD_ID_TYPE, PARAM_HOD_ID_SIZE, hodId));
				param.Add(MakeParam(PARAM_HOD_APPRAISAL_DATE_NAME, PARAM_HOD_APPRAISAL_DATE_TYPE, PARAM_HOD_APPRAISAL_DATE_SIZE, hodAppraisalDate));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYSTAFF_ID, param, transaction) == 0)
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

        public bool UpdateAppraisalHeaderBySupervisorId(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime staffResponseDate, string hodId, DateTime hodAppraisalDate, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateAppraisalHeaderBySupervisorId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_SUPERVISOR_ID_NAME, PARAM_SUPERVISOR_ID_TYPE, PARAM_SUPERVISOR_ID_SIZE, supervisorId));
				param.Add(MakeParam(PARAM_APPRAISAL_DATE_NAME, PARAM_APPRAISAL_DATE_TYPE, PARAM_APPRAISAL_DATE_SIZE, appraisalDate));
				param.Add(MakeParam(PARAM_STAFF_RESPONSE_DATE_NAME, PARAM_STAFF_RESPONSE_DATE_TYPE, PARAM_STAFF_RESPONSE_DATE_SIZE, staffResponseDate));
				param.Add(MakeParam(PARAM_HOD_ID_NAME, PARAM_HOD_ID_TYPE, PARAM_HOD_ID_SIZE, hodId));
				param.Add(MakeParam(PARAM_HOD_APPRAISAL_DATE_NAME, PARAM_HOD_APPRAISAL_DATE_TYPE, PARAM_HOD_APPRAISAL_DATE_SIZE, hodAppraisalDate));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYSUPERVISOR_ID, param, transaction) == 0)
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

        public bool UpdateAppraisalHeaderByHodId(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime staffResponseDate, string hodId, DateTime hodAppraisalDate, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateAppraisalHeaderByHodId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_SUPERVISOR_ID_NAME, PARAM_SUPERVISOR_ID_TYPE, PARAM_SUPERVISOR_ID_SIZE, supervisorId));
				param.Add(MakeParam(PARAM_APPRAISAL_DATE_NAME, PARAM_APPRAISAL_DATE_TYPE, PARAM_APPRAISAL_DATE_SIZE, appraisalDate));
				param.Add(MakeParam(PARAM_STAFF_RESPONSE_DATE_NAME, PARAM_STAFF_RESPONSE_DATE_TYPE, PARAM_STAFF_RESPONSE_DATE_SIZE, staffResponseDate));
				param.Add(MakeParam(PARAM_HOD_ID_NAME, PARAM_HOD_ID_TYPE, PARAM_HOD_ID_SIZE, hodId));
				param.Add(MakeParam(PARAM_HOD_APPRAISAL_DATE_NAME, PARAM_HOD_APPRAISAL_DATE_TYPE, PARAM_HOD_APPRAISAL_DATE_SIZE, hodAppraisalDate));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYHOD_ID, param, transaction) == 0)
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

        public bool UpdateAppraisalHeaderByAppraisal_Header_IDAndPeriod_IDAndStaff_IDAndSupervisor_IDAndHod_ID(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime staffResponseDate, string hodId, DateTime hodAppraisalDate, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateAppraisalHeaderByAppraisal_Header_IDAndPeriod_IDAndStaff_IDAndSupervisor_IDAndHod_ID";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
				param.Add(MakeParam(PARAM_SUPERVISOR_ID_NAME, PARAM_SUPERVISOR_ID_TYPE, PARAM_SUPERVISOR_ID_SIZE, supervisorId));
				param.Add(MakeParam(PARAM_APPRAISAL_DATE_NAME, PARAM_APPRAISAL_DATE_TYPE, PARAM_APPRAISAL_DATE_SIZE, appraisalDate));
				param.Add(MakeParam(PARAM_STAFF_RESPONSE_DATE_NAME, PARAM_STAFF_RESPONSE_DATE_TYPE, PARAM_STAFF_RESPONSE_DATE_SIZE, staffResponseDate));
				param.Add(MakeParam(PARAM_HOD_ID_NAME, PARAM_HOD_ID_TYPE, PARAM_HOD_ID_SIZE, hodId));
				param.Add(MakeParam(PARAM_HOD_APPRAISAL_DATE_NAME, PARAM_HOD_APPRAISAL_DATE_TYPE, PARAM_HOD_APPRAISAL_DATE_SIZE, hodAppraisalDate));

				//Execute Stored Procedure
				if (ExecuteProc(STP_APPRAISAL_HEADER_UPDATEAPPRAISAL_HEADERBYAppraisal_Header_IDANDPeriod_IDANDStaff_IDANDSupervisor_IDANDHod_ID, param, transaction) == 0)
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

		public DataSet SelectAllAppraisalHeader()
		{
			//const string METHOD_NAME  = "SelectAllAppraisalHeader";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_APPRAISAL_HEADER_SELECTALLAPPRAISAL_HEADER, null, APPRAISALHEADER_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

        public DataSet SelectAppraisalHeaderByAppraisalHeaderId(long appraisalHeaderId)
		{
			//const string METHOD_NAME  = "SelectAppraisalHeaderByAppraisalHeaderId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYAPPRAISAL_HEADER_ID, param, APPRAISALHEADER_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectAppraisalHeaderByPeriodId(int periodId)
		{
			//const string METHOD_NAME  = "SelectAppraisalHeaderByPeriodId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYPERIOD_ID, param, APPRAISALHEADER_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectAppraisalHeaderByStaffId(string staffId)
		{
			//const string METHOD_NAME  = "SelectAppraisalHeaderByStaffId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYSTAFF_ID, param, APPRAISALHEADER_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectAppraisalHeaderBySupervisorId(string supervisorId)
		{
			//const string METHOD_NAME  = "SelectAppraisalHeaderBySupervisorId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_SUPERVISOR_ID_NAME, PARAM_SUPERVISOR_ID_TYPE, PARAM_SUPERVISOR_ID_SIZE, supervisorId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYSUPERVISOR_ID, param, APPRAISALHEADER_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectAppraisalHeaderByHodId(string hodId)
		{
			//const string METHOD_NAME  = "SelectAppraisalHeaderByHodId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_HOD_ID_NAME, PARAM_HOD_ID_TYPE, PARAM_HOD_ID_SIZE, hodId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYHOD_ID, param, APPRAISALHEADER_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectAppraisalHeaderByPeriodIDAndStaffID(int periodId, string staffId)
		{
            //const string METHOD_NAME  = "SelectAppraisalHeaderByPeriodIDAndStaffID";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
				param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_APPRAISAL_HEADER_SELECTAPPRAISAL_HEADERBYPERIOD_IDANDSTAFF_ID, param, APPRAISALHEADER_TABLE_NAME);
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


