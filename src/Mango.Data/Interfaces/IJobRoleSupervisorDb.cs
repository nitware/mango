using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.DataAccess;
using System.Data;

namespace Mango.Data.Interfaces
{
    public interface IJobRoleSupervisorDb
    {
        string FIELD_SUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_ID { get; }
        string FIELD_STAFF_COMPANY_DEPARTMENT_JOB_ROLE_ID { get; }

        bool InsertJobRoleSupervisor(int supervisorCompanyDepartmentJobRoleID, int staffCompanyDepartmentJobRoleID, Transaction transaction);
        bool DeleteJobRoleSupervisorByJobRoleSupervisorId(int jobRoleSupervisorId, Transaction transaction);
        bool DeleteJobRoleSupervisorByJobRoleId(int jobRoleId, Transaction transaction);
        bool DeleteJobRoleSupervisorBySUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDAndJob_Role_ID(int jobRoleSupervisorId, int jobRoleId, Transaction transaction);
        DataSet SelectAllJobRoleSupervisor();
        DataSet SelectJobRoleSupervisorByJobRoleSupervisorId(int jobRoleSupervisorId);
        DataSet SelectJobRoleSupervisorByJobRoleId(int jobRoleId);
        DataSet SelectJobRoleSupervisorBySUPERVISOR_COMPANY_DEPARTMENT_JOB_ROLE_IDAndJob_Role_ID(int jobRoleSupervisorId, int jobRoleId);
       
        
    }
}
