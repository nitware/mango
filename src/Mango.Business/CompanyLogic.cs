using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;
using System.Data;

namespace Mango.Business
{
    public class CompanyLogic : BusinessLogicBase<Company, COMPANY>
    {
        public CompanyLogic()
        {
            base.translator = new CompanyTranslator();
        }

        public bool Modify(Company company)
        {
            try
            {
                Func<COMPANY, bool> selector = c => c.Company_ID == company.Id;
                COMPANY entity = GetEntityBy(selector);

                entity.Company_Name = company.Name;
                entity.Company_Symbol = company.Symbol;
                entity.Company_Description = company.Description;

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

        public bool Remove(Company company)
        {
            try
            {
                Func<COMPANY, bool> selector = c => c.Company_ID == company.Id;
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
