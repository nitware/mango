using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using System.Data;
using Mango.Model;
using Mango.Business.DbFacade;

namespace Mango.Business
{
    public class StaffLevelLogic : BusinessLogicBase<StaffLevel, STAFF_JOB_ROLE_LEVEL>
    {
        private CurrentPeriodDbFacade currentPeriodDbFacade;

        public StaffLevelLogic()
        {
            base.translator = new StaffLevelTranslator();
            currentPeriodDbFacade = new CurrentPeriodDbFacade();
        }

        public override List<StaffLevel> GetAll()
        {
            try
            {
                Period period = currentPeriodDbFacade.GetCurrentPeriod();
                if (period == null)
                {
                    throw new Exception("No current period found! Please contact your system administrator.");
                }

                //Func<STAFF_JOB_ROLE_LEVEL, bool> predicate = s => s.Period_ID == period.Id;

                Func<STAFF_JOB_ROLE_LEVEL, bool> predicate = s => s.Period_ID == period.Id && s.STAFF.Company_Id == 2 && s.STAFF.Is_Active == true;
                return base.GetModelsBy(predicate).OrderBy(sl => sl.Staff.FullName).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public StaffLevel GetBy(Staff staff, Period period)
        {
            try
            {
                Func<STAFF_JOB_ROLE_LEVEL, bool> predicate = s => s.Staff_ID == staff.Id && s.Period_ID == period.Id && s.STAFF.Company_Id == 2 && s.STAFF.Is_Active == true;
                return base.GetModelBy(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(StaffLevel staffLevel)
        {
            try
            {
                Func<STAFF_JOB_ROLE_LEVEL, bool> predicate = s => s.Staff_ID == staffLevel.Staff.Id && s.Period_ID == staffLevel.Period.Id;
                STAFF_JOB_ROLE_LEVEL entity = GetEntityBy(predicate);

                entity.Staff_ID = staffLevel.Staff.Id;
                entity.Job_Role_Level_ID = staffLevel.Level.Id;
                entity.Period_ID = staffLevel.Period.Id;

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

        public bool Remove(StaffLevel staffLevel)
        {
            try
            {
                Func<STAFF_JOB_ROLE_LEVEL, bool> predicate = s => s.Job_Role_Level_ID == staffLevel.Level.Id && s.Staff_ID == staffLevel.Staff.Id && s.Period_ID == staffLevel.Period.Id;
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
