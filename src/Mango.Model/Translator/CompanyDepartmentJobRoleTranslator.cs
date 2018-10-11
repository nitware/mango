using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class CompanyDepartmentJobRoleTranslator : TranslatorBase<CompanyDepartmentJobRole, COMPANY_DEPARTMENT_JOB_ROLE>
    {
        private CompanyTranslator companyTranslator;
        private DepartmentTranslator departmentTranslator;
        private JobRoleTranslator jobRoleTranslator;

        public CompanyDepartmentJobRoleTranslator()
        {
            companyTranslator = new CompanyTranslator();
            departmentTranslator = new DepartmentTranslator();
            jobRoleTranslator = new JobRoleTranslator();
        }

        public override CompanyDepartmentJobRole TranslateToModel(COMPANY_DEPARTMENT_JOB_ROLE entity)
        {
            try
            {
                CompanyDepartmentJobRole companyDepartmentJobRole = null;
                if (entity != null)
                {
                    companyDepartmentJobRole = new CompanyDepartmentJobRole();
                    companyDepartmentJobRole.Id = entity.Company_Department_Job_Role_ID;
                    companyDepartmentJobRole.Company = companyTranslator.Translate(entity.COMPANY_DEPARTMENT.COMPANY);
                    companyDepartmentJobRole.Department = departmentTranslator.Translate(entity.COMPANY_DEPARTMENT.DEPARTMENT);
                    companyDepartmentJobRole.JobRole = jobRoleTranslator.Translate(entity.JOB_ROLE);
                    companyDepartmentJobRole.Description = entity.Description;
                }

                return companyDepartmentJobRole;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override COMPANY_DEPARTMENT_JOB_ROLE TranslateToEntity(CompanyDepartmentJobRole companyDepartmentJobRole)
        {
            try
            {
                COMPANY_DEPARTMENT_JOB_ROLE entity = null;
                if (companyDepartmentJobRole != null)
                {
                    entity = new COMPANY_DEPARTMENT_JOB_ROLE();
                    entity.Company_Department_Job_Role_ID = companyDepartmentJobRole.Id;
                    entity.Company_ID = companyDepartmentJobRole.Company.Id;
                    entity.Department_ID = companyDepartmentJobRole.Department.Id;
                    entity.Job_Role_ID = companyDepartmentJobRole.JobRole.Id;
                    entity.Description = companyDepartmentJobRole.Description;
                }

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }
}
