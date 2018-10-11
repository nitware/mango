using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class CompanyDepartmentTranslator : TranslatorBase<CompanyDepartment, COMPANY_DEPARTMENT>
    {
        private CompanyTranslator companyTranslator;
        private DepartmentTranslator departmentTranslator;

        public CompanyDepartmentTranslator()
        {
            companyTranslator = new CompanyTranslator();
            departmentTranslator = new DepartmentTranslator();
        }

        public override CompanyDepartment TranslateToModel(COMPANY_DEPARTMENT entity)
        {
            try
            {
                CompanyDepartment companyDepartment = null;
                if (entity != null)
                {
                    companyDepartment = new CompanyDepartment();
                    companyDepartment.Company = companyTranslator.Translate(entity.COMPANY);
                    companyDepartment.Department = departmentTranslator.Translate(entity.DEPARTMENT);
                    companyDepartment.Description = entity.Description;
                }

                return companyDepartment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override COMPANY_DEPARTMENT TranslateToEntity(CompanyDepartment companyDepartment)
        {
            try
            {
                COMPANY_DEPARTMENT entity = null;
                if (companyDepartment != null)
                {
                    entity = new COMPANY_DEPARTMENT();
                    entity.Company_ID = companyDepartment.Company.Id;
                    entity.Department_ID = companyDepartment.Department.Id;
                    entity.Description = companyDepartment.Description;
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
