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
    public class CompanyDepartmentLogic : BusinessLogicBase<CompanyDepartment, COMPANY_DEPARTMENT>
    {
        public CompanyDepartmentLogic()
        {
            base.translator = new CompanyDepartmentTranslator();
        }

        public List<CompanyDepartment> GetBy(Company company)
        {
            try
            {
                Func<COMPANY_DEPARTMENT, bool> selector = cd => cd.Company_ID == company.Id;
                return base.GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }




    }



}
