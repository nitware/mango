using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;
using System.Data;
using Mango.Model.Model;
using System.Transactions;

namespace Mango.Business
{
    public class JobRoleLogic : BusinessLogicBase<JobRole, JOB_ROLE>
    {
        private CompanyJobRoleLogic companyJobRoleLogic;

        public JobRoleLogic()
        {
            base.translator = new JobRoleTranslator();
            companyJobRoleLogic = new CompanyJobRoleLogic();
        }

        public override JobRole Add(JobRole jobRole)
        {
            try
            {
                if (jobRole == null)
                {
                    throw new Exception("Job Role to be added cannot be null! Please try again");
                }

                JobRole newJobRole = null;
                using (TransactionScope transaction = new TransactionScope())
                {
                    newJobRole = base.Add(jobRole);
                    if (newJobRole == null || newJobRole.Id <= 0)
                    {
                        throw new Exception("Critical error occurred during Job Role creation! Please try again.");
                    }

                    CompanyJobRole companyJobRole = new CompanyJobRole();
                    companyJobRole.Company = new Company() { Id = 2 };
                    companyJobRole.JobRole = newJobRole;

                    CompanyJobRole newCompanyJobRole = companyJobRoleLogic.Add(companyJobRole);
                    if (newCompanyJobRole == null || newCompanyJobRole.Id <= 0)
                    {
                        throw new Exception("Job Role (" + jobRole.Name + ") could not be successfully added!");
                    }

                    transaction.Complete();
                }
                
                return newJobRole;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public override List<JobRole> GetAll()
        {
            try
            {
                List<JobRole> jobRoles = null;
                List<CompanyJobRole> companyJobRoles = companyJobRoleLogic.GetAll();
                                
                if (companyJobRoles != null && companyJobRoles.Count > 0)
                {
                    jobRoles = new List<JobRole>();
                    companyJobRoles = companyJobRoles.OrderBy(jr => jr.JobRole.Name).ToList();
                                        
                    foreach(CompanyJobRole companyJobRole in companyJobRoles)
                    {
                        jobRoles.Add(companyJobRole.JobRole);
                    }
                }

                return jobRoles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(JobRole jobRole)
        {
            try
            {
                Func<JOB_ROLE, bool> predicate = jr => jr.Job_Role_ID == jobRole.Id;
                JOB_ROLE entity = GetEntityBy(predicate);

                entity.Job_Role_Name = jobRole.Name;
                            
                int rowsAffected = repository.SaveChanges();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception(NoItemModified);
                }
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(ArgumentNullException);
            }
            catch (UpdateException)
            {
                throw new UpdateException(UpdateException);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(JobRole jobRole)
        {
            try
            {
                Func<JOB_ROLE, bool> selector = jr => jr.Job_Role_ID == jobRole.Id;
                Func<COMPANY_JOB_ROLE, bool> predicate = cjr => cjr.Job_Role_ID == jobRole.Id && cjr.Company_ID == 2;

                CompanyJobRole companyJobRole = companyJobRoleLogic.GetModelBy(predicate);
                if (companyJobRole == null || companyJobRole.Id <= 0)
                {
                    throw new Exception("Corresponding object not found! Please try again.");
                }

                bool removed = false;
                using (TransactionScope transaction = new TransactionScope())
                {
                    if (companyJobRoleLogic.Remove(companyJobRole))
                    {
                        repository.SaveChanges();
                        removed = base.Remove(selector);
                    }

                    repository.SaveChanges();
                    transaction.Complete();
                }

                return removed;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }




}
