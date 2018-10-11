using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using Mango.Model;

namespace Mango.Business
{
    public class JobRoleHodLogic : BusinessLogicBase<JobRoleHod, JOB_ROLE_HOD>
    {
        public JobRoleHodLogic()
        {
            base.translator = new JobRoleHodTranslator();
        }

        public override int Add(List<JobRoleHod> jobRoleHods)
        {
            try
            {
                if (jobRoleHods == null || jobRoleHods.Count <= 0)
                {
                    throw new Exception("Job Role Hods cannot be empty!");
                }

                return base.Add(jobRoleHods);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(List<JobRoleHod> jobRoleHods)
        {
            try
            {
                int rowsAffected = 0;
                if (jobRoleHods != null && jobRoleHods.Count > 0)
                {
                    bool removed = Remove(jobRoleHods[0].HodCompanyDepartmentJobRole, jobRoleHods[0].Period);
                    if (removed)
                    {
                        rowsAffected = base.Add(jobRoleHods);
                    }
                }

                return rowsAffected > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(CompanyDepartmentJobRole companyDepartmentJobRole, Period period)
        {
            try
            {
                Func<JOB_ROLE_HOD, bool> selector = jrh => jrh.Hod_Company_Department_Job_Role_ID == companyDepartmentJobRole.Id && jrh.Period_Id == period.Id;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<JobRoleHod> GetBy(CompanyDepartmentJobRole companyDepartmentJobRole, Period period)
        {
            try
            {
                //Func<JOB_ROLE_HOD, bool> selector = jrh => jrh.Hod_Company_Department_Job_Role_ID == companyDepartmentJobRole.Id && jrh.Period_Id == period.Id;

                Func<JOB_ROLE_HOD, bool> selector = jrh => jrh.Hod_Company_Department_Job_Role_ID == companyDepartmentJobRole.Id && jrh.Period_Id == period.Id && jrh.COMPANY_DEPARTMENT_JOB_ROLE1.Company_ID == 2;
                return GetModelsBy(selector).OrderBy(jrh => jrh.StaffCompanyDepartmentJobRole.JobRole.Name).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<JobRoleHod> GetBy(Period period)
        {
            try
            {
                Func<JOB_ROLE_HOD, bool> selector = jrh => jrh.Period_Id == period.Id && jrh.COMPANY_DEPARTMENT_JOB_ROLE1.Company_ID == 2;
                //int count = GetModelsBy(selector).Count;
                return GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }
       



    }
}
