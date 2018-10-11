using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Collections;

namespace Mango.Data
{
    public class JobRoleHodDb : DataAccess.DataAccess
    {
        //==========================================================================================
        //Db Stored Procedures declaration
        //==========================================================================================
        #region  JobRoleSupervisor Stored Procedure declaration

        //private const string STP_JOB_ROLE_HOD_INSERTJOB_ROLE_HOD = "STP_JOB_ROLE_HOD_INSERTJOB_ROLE_HOD";
        //private const string STP_JOB_ROLE_HOD_DELETEJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_JOB_ROLE_HOD_DELETEJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_ID";
        //private const string STP_JOB_ROLE_HOD_DELETEJOB_ROLE_HODBYJOB_ROLE_ID = "STP_JOB_ROLE_HOD_DELETEJOB_ROLE_HODBYJOB_ROLE_ID";
        //private const string STP_JOB_ROLE_HOD_DELETEJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID = "STP_JOB_ROLE_HOD_DELETEJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID";
        //private const string STP_JOB_ROLE_HOD_UPDATEJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_JOB_ROLE_HOD_UPDATEJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_ID";
        //private const string STP_JOB_ROLE_HOD_UPDATEJOB_ROLE_HODBYJOB_ROLE_ID = "STP_JOB_ROLE_HOD_UPDATEJOB_ROLE_HODBYJOB_ROLE_ID";
        //private const string STP_JOB_ROLE_HOD_UPDATEJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID = "STP_JOB_ROLE_HOD_UPDATEJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID";
        //private const string STP_JOB_ROLE_HOD_SELECTALLJOB_ROLE_HOD = "STP_JOB_ROLE_HOD_SELECTALLJOB_ROLE_HOD";
        private const string STP_JOB_ROLE_HOD_SELECTJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_ID = "STP_JOB_ROLE_HOD_SELECTJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_ID";
        //private const string STP_JOB_ROLE_HOD_SELECTJOB_ROLE_HODBYJOB_ROLE_ID = "STP_JOB_ROLE_HOD_SELECTJOB_ROLE_HODBYJOB_ROLE_ID";
        //private const string STP_JOB_ROLE_HOD_SELECTJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID = "STP_JOB_ROLE_HOD_SELECTJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_IDANDJob_Role_ID";

        #endregion

        //==========================================================================================
        //Db Configuration properties
        //==========================================================================================
        #region JobRoleSupervisor Parameter declaration

        //Parameter decleration for SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID
        private const string PARAM_HOD_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME = "@HodCompanyDepartmentJobRoleID";
        private const SqlDbType PARAM_HOD_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE = SqlDbType.Int;
        private const int PARAM_HOD_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE = 4;

        //Parameter decleration for JOB_ROLE_ID
        private const string PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME = "@StaffCompanyDepartmentJobRoleID";
        private const SqlDbType PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE = SqlDbType.Int;
        private const int PARAM_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE = 4;

        #endregion

        //==========================================================================================
        //JobRoleSupervisor Table Field Name Declaration
        //==========================================================================================
        #region JobRoleSupervisor Field Name declaration

        public string FIELD_HOD_COMPANY_DEPARTMENT_JOB_ROLE_ID { get { return "Hod_Company_Department_Job_Role_ID"; } }
        public string FIELD_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID { get { return "Staff_Company_Department_Job_Role_ID"; } }

        #endregion

        //Table name declarations for JobRoleSupervisor in the database, this will be used for dataset reference
        public string JOBROLEHOD_TABLE_NAME = "JOB_ROLE_HOD";

        //==========================================================================================
        //public JobRoleSupervisorDb Class Method declarations that will be called from the Biz Tier
        //==========================================================================================
        #region JobRoleHodDb Class Methods

        public DataSet SelectJobRoleHodByJobRoleHodId(int jobRoleHodId)
        {
            //const string METHOD_NAME  = "SelectJobRoleHodByJobRoleHodId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();

                param.Add(MakeParam(PARAM_HOD_COMPANY_DEPARTMENT_JOB_ROLE_ID_NAME, PARAM_HOD_COMPANY_DEPARTMENT_JOB_ROLE_ID_TYPE, PARAM_HOD_COMPANY_DEPARTMENT_JOB_ROLE_ID_SIZE, jobRoleHodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_JOB_ROLE_HOD_SELECTJOB_ROLE_HODBYHOD_COMPANY_DEPARTMENT_JOB_ROLE_ID, param, JOBROLEHOD_TABLE_NAME);
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
