using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class CompanyJobRoleTranslator : TranslatorBase<CompanyJobRole, COMPANY_JOB_ROLE>
    {
        private CompanyTranslator companyTranslator;
        private JobRoleTranslator jobRoleTranslator;

        public CompanyJobRoleTranslator()
        {
            companyTranslator = new CompanyTranslator();
            jobRoleTranslator = new JobRoleTranslator();
        }

        public override CompanyJobRole TranslateToModel(COMPANY_JOB_ROLE entity)
        {
            try
            {
                CompanyJobRole model = null;
                if (entity != null)
                {
                    model = new CompanyJobRole();
                    model.Id = entity.Company_Job_Role_Id;
                    model.Company = companyTranslator.Translate(entity.COMPANY);
                    model.JobRole = jobRoleTranslator.Translate(entity.JOB_ROLE);
                    model.Description = entity.Description;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override COMPANY_JOB_ROLE TranslateToEntity(CompanyJobRole model)
        {
            try
            {
                COMPANY_JOB_ROLE entity = null;
                if (model != null)
                {
                    entity = new COMPANY_JOB_ROLE();
                    entity.Company_Job_Role_Id = model.Id;
                    entity.Company_ID = model.Company.Id;
                    entity.Job_Role_ID = model.JobRole.Id;
                    entity.Description = model.Description;
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
