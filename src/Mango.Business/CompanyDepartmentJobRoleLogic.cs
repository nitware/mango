using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Model.Model;
using Mango.Data;
using Mango.Model.Translator;
using System.Data;

namespace Mango.Business
{
    public class CompanyDepartmentJobRoleLogic : BusinessLogicBase<CompanyDepartmentJobRole, COMPANY_DEPARTMENT_JOB_ROLE>
    {
        public CompanyDepartmentJobRoleLogic()
        {
            base.translator = new CompanyDepartmentJobRoleTranslator();
        }

        public override List<CompanyDepartmentJobRole> GetAll()
        {
            try
            {
                List<CompanyDepartmentJobRole> companyDepartmentJobRoles = base.GetAll().Where(c => c.Company.Id == 2).ToList();
                return companyDepartmentJobRoles.OrderBy(c => c.JobRole.Name).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(CompanyDepartmentJobRole companyDepartmentJobRole)
        {
            try
            {
                Func<COMPANY_DEPARTMENT_JOB_ROLE, bool> predicate = c => c.Company_Department_Job_Role_ID == companyDepartmentJobRole.Id;
                COMPANY_DEPARTMENT_JOB_ROLE entity = GetEntityBy(predicate);

                entity.Company_ID = companyDepartmentJobRole.Company.Id;
                entity.Department_ID = companyDepartmentJobRole.Department.Id;
                entity.Job_Role_ID = companyDepartmentJobRole.JobRole.Id;
                entity.Description = companyDepartmentJobRole.Description;

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

        public bool Remove(CompanyDepartmentJobRole companyDepartmentJobRole)
        {
            try
            {
                Func<COMPANY_DEPARTMENT_JOB_ROLE, bool> selector = d => d.Company_Department_Job_Role_ID == companyDepartmentJobRole.Id;
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
