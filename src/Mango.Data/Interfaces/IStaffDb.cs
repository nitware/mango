using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.DataAccess;

namespace Mango.Data.Interfaces
{
    public interface IStaffDb
    {
        string FIELD_STAFF_ID { get; }
        string FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID { get; }
        string FIELD_JOB_ROLE_LEVEL_ID { get; }
        string FIELD_LAST_NAME { get; }
        string FIELD_FIRST_NAME { get; }
        string FIELD_OTHER_NAME { get; }
        string FIELD_LOGIN_NAME { get; }
        string FIELD_EMAIL { get; }
        string FIELD_LOCATION_NAME { get; }
        string FIELD_LOCATION_ID { get; }
        
        bool InsertStaff(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, string lastName, string firstName, string otherName, string loginName, string email, Transaction transaction);
        bool DeleteStaffByStaffId(string staffId, Transaction transaction);
        bool DeleteStaffByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId, Transaction transaction);
        bool DeleteStaffByJobRoleLevelId(string jobRoleLevelId, Transaction transaction);
        bool DeleteStaffByStaff_IDAndCompany_Department_Job_Role_IDAndJob_Role_Level_ID(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, Transaction transaction);
        bool UpdateStaffByStaffId(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, string lastName, string firstName, string otherName, string loginName, string email, Transaction transaction);
        bool UpdateStaffByCompanyDepartmentJobRoleId(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, string lastName, string firstName, string otherName, string loginName, string email, Transaction transaction);
        bool UpdateStaffByJobRoleLevelId(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, string lastName, string firstName, string otherName, string loginName, string email, Transaction transaction);
        bool UpdateStaffByStaff_IDAndCompany_Department_Job_Role_IDAndJob_Role_Level_ID(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId, string lastName, string firstName, string otherName, string loginName, string email, Transaction transaction);
        DataSet SelectAllStaff();
        DataSet SelectStaffByStaffId(string staffId, int periodId);
        //DataSet SelectStaffByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId);
        DataSet SelectStaffByCompanyDepartmentJobRoleIdAndPeriodId(int companyDepartmentJobRoleId, int periodId);
        DataSet SelectStaffByJobRoleLevelId(string jobRoleLevelId);
        DataSet SelectStaffByStaff_IDAndCompany_Department_Job_Role_IDAndJob_Role_Level_ID(string staffId, int companyDepartmentJobRoleId, string jobRoleLevelId);
        DataSet SelectStaffByLoginName(string loginName, int periodId);
        DataSet SelectStaffSupervisorByCompanyDepartmentJobRoleIdAndPeriodId(int companyDepartmentJobRoleId, int periodId);
        //DataSet SelectStaffSupervisorByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId);

        //DataSet SelectStaffHodByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId);
        DataSet SelectStaffHodByCompanyDepartmentJobRoleIdAndPeriodId(int companyDepartmentJobRoleId, int periodId);

        //DataSet SelectStaffTypeByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId);
        DataSet SelectStaffTypeByCompanyDepartmentJobRoleId(int companyDepartmentJobRoleId, int periodId);
        DataSet SelectHodAppraiseesByCompanyDepartmentJobRoleIdAndPeriodIdAndOptionId(int companyDepartmentJobRoleId, int periodId, byte optionId);

       
       
    }
}
