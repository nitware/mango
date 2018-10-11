using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using Mango.Business.DbFacade;
using Mango.Model;
using System.Data;

namespace Mango.Business
{
    public class StaffCdjrLogic : BusinessLogicBase<StaffCdjr, STAFF_COMPANY_DEPARTMENT_JOB_ROLE>
    {
        private CurrentPeriodDbFacade currentPeriodDbFacade;

        public StaffCdjrLogic()
        {
            base.translator = new StaffCdjrTranslator();
            currentPeriodDbFacade = new CurrentPeriodDbFacade();
        }

        public override List<StaffCdjr> GetAll()
        {
            try
            {
                Period period = currentPeriodDbFacade.GetCurrentPeriod();
                if (period == null)
                {
                    throw new Exception("No current period found! Please contact your system administrator.");
                }

                //Func<STAFF_COMPANY_DEPARTMENT_JOB_ROLE, bool> predicate = s => s.Period_ID == period.Id;

                Func<STAFF_COMPANY_DEPARTMENT_JOB_ROLE, bool> predicate = s => s.Period_ID == period.Id && s.STAFF.Company_Id == 2 && s.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2 && s.STAFF.Is_Active == true;
                return base.GetModelsBy(predicate).OrderBy(sc => sc.Staff.FullName).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public StaffCdjr GetBy(Staff staff, Period period)
        {
            try
            {
                //Func<STAFF_COMPANY_DEPARTMENT_JOB_ROLE, bool> predicate = s => s.Staff_ID == staff.Id && s.Period_ID == period.Id;

                Func<STAFF_COMPANY_DEPARTMENT_JOB_ROLE, bool> predicate = s => s.Staff_ID == staff.Id && s.Period_ID == period.Id && s.STAFF.Company_Id == 2 && s.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2 && s.STAFF.Is_Active == true;
                return base.GetModelBy(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(StaffCdjr staffCdjr)
        {
            try
            {
                Func<STAFF_COMPANY_DEPARTMENT_JOB_ROLE, bool> predicate = s => s.Staff_ID == staffCdjr.Staff.Id && s.Period_ID == staffCdjr.Period.Id;
                STAFF_COMPANY_DEPARTMENT_JOB_ROLE entity = GetEntityBy(predicate);

                entity.Staff_ID = staffCdjr.Staff.Id;
                entity.Company_Department_Job_Role_ID = staffCdjr.CompanyDepartmentJobRole.Id;
                entity.Period_ID = staffCdjr.Period.Id;

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

        public bool Remove(StaffCdjr staffCdjr)
        {
            try
            {
                Func<STAFF_COMPANY_DEPARTMENT_JOB_ROLE, bool> predicate = s => s.Company_Department_Job_Role_ID == staffCdjr.CompanyDepartmentJobRole.Id && s.Staff_ID == staffCdjr.Staff.Id && s.Period_ID == staffCdjr.Period.Id;
                bool suceeded = base.Remove(predicate);
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
