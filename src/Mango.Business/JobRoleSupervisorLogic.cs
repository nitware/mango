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
    public class JobRoleSupervisorLogic : BusinessLogicBase<JobRoleSupervisor, JOB_ROLE_SUPERVISOR>
    {
        public JobRoleSupervisorLogic()
        {
            base.translator = new JobRoleSupervisorTranslator();
        }

        public override int Add(List<JobRoleSupervisor> jobRoleSupervisors)
        {
            try
            {
                if (jobRoleSupervisors == null || jobRoleSupervisors.Count <= 0)
                {
                    throw new Exception("Supervisor Job Role cannot be empty!");
                }

                return base.Add(jobRoleSupervisors);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(List<JobRoleSupervisor> jobRoleSupervisors)
        {
            try
            {
                int rowsAffected = 0;
                if (jobRoleSupervisors != null && jobRoleSupervisors.Count > 0)
                {
                    bool removed = Remove(jobRoleSupervisors[0].SupervisorCompanyDepartmentJobRole, jobRoleSupervisors[0].Period);
                    if (removed)
                    {
                        rowsAffected = base.Add(jobRoleSupervisors);
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
                Func<JOB_ROLE_SUPERVISOR, bool> selector = jrh => jrh.Supervisor_Company_Department_Job_Role_ID == companyDepartmentJobRole.Id && jrh.Period_Id == period.Id && jrh.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<JobRoleSupervisor> GetBy(CompanyDepartmentJobRole companyDepartmentJobRole, Period period)
        {
            try
            {
                Func<JOB_ROLE_SUPERVISOR, bool> selector = jrh => jrh.Supervisor_Company_Department_Job_Role_ID == companyDepartmentJobRole.Id && jrh.Period_Id == period.Id && jrh.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2;
                return GetModelsBy(selector).OrderBy(jrh => jrh.StaffCompanyDepartmentJobRole.JobRole.Name).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<JobRoleSupervisor> GetBy(Period period)
        {
            try
            {
                Func<JOB_ROLE_SUPERVISOR, bool> selector = jrh => jrh.Period_Id == period.Id && jrh.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2;
                return GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        






    }
}
