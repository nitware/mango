using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;

namespace Mango.Model.Translator
{
    public class CompanyTranslator : TranslatorBase<Company, COMPANY>
    {
        public override Company TranslateToModel(COMPANY entity)
        {
            try
            {
                Company company = null;
                if (entity != null)
                {
                    company = new Company();
                    company.Id = entity.Company_ID;
                    company.Name = entity.Company_Name;
                    company.Symbol = entity.Company_Symbol;
                    company.Description = entity.Company_Description;
                }

                return company;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override COMPANY TranslateToEntity(Company company)
        {
            try
            {
                COMPANY entity = null;
                if (company != null)
                {
                    entity = new COMPANY();
                    entity.Company_ID = company.Id;
                    entity.Company_Name = company.Name;
                    entity.Company_Symbol = company.Symbol;
                    entity.Company_Description = company.Description;
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
