using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using System.Data;

namespace Mango.Business
{
    public class CompanyJobRoleLogic : BusinessLogicBase<CompanyJobRole, COMPANY_JOB_ROLE>
    {
        public CompanyJobRoleLogic()
        {
            base.translator = new CompanyJobRoleTranslator();
        }

        public override List<CompanyJobRole> GetAll()
        {
            try
            {
                List<CompanyJobRole> companyJobRoles = base.GetAll().Where(c => c.Company.Id == 2).ToList();
                return companyJobRoles.OrderBy(c => c.JobRole.Name).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(CompanyJobRole companyJobRole)
        {
            try
            {
                Func<COMPANY_JOB_ROLE, bool> predicate = c => c.Company_Job_Role_Id == companyJobRole.Id;
                COMPANY_JOB_ROLE entity = GetEntityBy(predicate);

                entity.Company_ID = companyJobRole.Company.Id;
                entity.Job_Role_ID = companyJobRole.JobRole.Id;
                entity.Description = companyJobRole.Description;

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

        public bool Remove(CompanyJobRole companyJobRole)
        {
            try
            {
                Func<COMPANY_JOB_ROLE, bool> selector = d => d.Company_Job_Role_Id == companyJobRole.Id;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }


}
