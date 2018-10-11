using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class JobRoleHodTranslator : TranslatorBase<JobRoleHod, JOB_ROLE_HOD>
    {
        private PeriodTranslator periodTranslator;
        private CompanyDepartmentJobRoleTranslator companyDepartmentJobRoleTranslator;

        public JobRoleHodTranslator()
        {
            periodTranslator = new PeriodTranslator();
            companyDepartmentJobRoleTranslator = new CompanyDepartmentJobRoleTranslator();
        }

        public override JobRoleHod TranslateToModel(JOB_ROLE_HOD entity)
        {
            try
            {
                JobRoleHod jobRoleHod = null;
                if (entity != null)
                {
                    jobRoleHod = new JobRoleHod();
                    jobRoleHod.HodCompanyDepartmentJobRole = companyDepartmentJobRoleTranslator.Translate(entity.COMPANY_DEPARTMENT_JOB_ROLE);
                    jobRoleHod.StaffCompanyDepartmentJobRole = companyDepartmentJobRoleTranslator.Translate(entity.COMPANY_DEPARTMENT_JOB_ROLE1);
                    jobRoleHod.Period = periodTranslator.Translate(entity.PERIOD);
                    //jobRoleHod.Period = new Period() { Id = entity.Period_Id };
                }

                return jobRoleHod;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override JOB_ROLE_HOD TranslateToEntity(JobRoleHod jobRoleHod)
        {
            try
            {
                JOB_ROLE_HOD entity = null;
                if (jobRoleHod != null)
                {
                    entity = new JOB_ROLE_HOD();
                    entity.Hod_Company_Department_Job_Role_ID = jobRoleHod.HodCompanyDepartmentJobRole.Id;
                    entity.Staff_Company_Department_Job_Role_ID = jobRoleHod.StaffCompanyDepartmentJobRole.Id;
                    entity.Period_Id = jobRoleHod.Period.Id;
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
