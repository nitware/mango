using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class JobRoleSupervisorTranslator : TranslatorBase<JobRoleSupervisor, JOB_ROLE_SUPERVISOR>
    {
        private PeriodTranslator periodTranslator;
        private CompanyDepartmentJobRoleTranslator companyDepartmentJobRoleTranslator;

        public JobRoleSupervisorTranslator()
        {
            periodTranslator = new PeriodTranslator();
            companyDepartmentJobRoleTranslator = new CompanyDepartmentJobRoleTranslator();
        }

        public override JobRoleSupervisor TranslateToModel(JOB_ROLE_SUPERVISOR entity)
        {
            try
            {
                JobRoleSupervisor jobRoleSupervisor = null;
                if (entity != null)
                {
                    jobRoleSupervisor = new JobRoleSupervisor();
                    jobRoleSupervisor.SupervisorCompanyDepartmentJobRole = companyDepartmentJobRoleTranslator.Translate(entity.COMPANY_DEPARTMENT_JOB_ROLE1);
                    jobRoleSupervisor.StaffCompanyDepartmentJobRole = companyDepartmentJobRoleTranslator.Translate(entity.COMPANY_DEPARTMENT_JOB_ROLE);
                    jobRoleSupervisor.Period = periodTranslator.Translate(entity.PERIOD);

                    //jobRoleSupervisor.Period = new Period() { Id = entity.Period_Id };
                }

                return jobRoleSupervisor;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override JOB_ROLE_SUPERVISOR TranslateToEntity(JobRoleSupervisor jobRoleSupervisor)
        {
            try
            {
                JOB_ROLE_SUPERVISOR entity = null;
                if (jobRoleSupervisor != null)
                {
                    entity = new JOB_ROLE_SUPERVISOR();
                    entity.Supervisor_Company_Department_Job_Role_ID = jobRoleSupervisor.SupervisorCompanyDepartmentJobRole.Id;
                    entity.Staff_Company_Department_Job_Role_ID = jobRoleSupervisor.StaffCompanyDepartmentJobRole.Id;
                    entity.Period_Id = jobRoleSupervisor.Period.Id;
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
