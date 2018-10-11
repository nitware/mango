using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class StaffCdjrTranslator : TranslatorBase<StaffCdjr, STAFF_COMPANY_DEPARTMENT_JOB_ROLE>
    {
        private StaffTranslator staffTranslator;
        private CompanyDepartmentJobRoleTranslator companyDepartmentJobRoleTranslator;
        private PeriodTranslator periodTranslator;

        public StaffCdjrTranslator()
        {
            staffTranslator = new StaffTranslator();
            companyDepartmentJobRoleTranslator = new CompanyDepartmentJobRoleTranslator();
            periodTranslator = new PeriodTranslator();
        }

        public override StaffCdjr TranslateToModel(STAFF_COMPANY_DEPARTMENT_JOB_ROLE entity)
        {
            try
            {
                StaffCdjr staffCdjr = null;
                if (entity != null)
                {
                    staffCdjr = new StaffCdjr();
                    staffCdjr.Staff = staffTranslator.Translate(entity.STAFF);
                    staffCdjr.CompanyDepartmentJobRole = companyDepartmentJobRoleTranslator.Translate(entity.COMPANY_DEPARTMENT_JOB_ROLE);
                    staffCdjr.Period = periodTranslator.Translate(entity.PERIOD);
                }

                return staffCdjr;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STAFF_COMPANY_DEPARTMENT_JOB_ROLE TranslateToEntity(StaffCdjr staffCdjr)
        {
            try
            {
                STAFF_COMPANY_DEPARTMENT_JOB_ROLE entity = null;
                if (staffCdjr != null)
                {
                    entity = new STAFF_COMPANY_DEPARTMENT_JOB_ROLE();
                    entity.Staff_ID = staffCdjr.Staff.Id;
                    entity.Company_Department_Job_Role_ID = staffCdjr.CompanyDepartmentJobRole.Id;
                    entity.Period_ID = staffCdjr.Period.Id;
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
