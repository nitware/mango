using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Business.DbFacade;
using Mango.Model.Translator;
using Mango.Model;
using System.Data;

namespace Mango.Business
{
    public class StaffLocationLogic : BusinessLogicBase<StaffLocation, STAFF_LOCATION>
    {
        private CurrentPeriodDbFacade currentPeriodDbFacade;

        public StaffLocationLogic()
        {
            base.translator = new StaffLocationTranslator();
            currentPeriodDbFacade = new CurrentPeriodDbFacade();
        }

        public override List<StaffLocation> GetAll()
        {
            try
            {
                Period period = currentPeriodDbFacade.GetCurrentPeriod();
                if (period == null)
                {
                    throw new Exception("No current period found! Please contact your system administrator.");
                }

                Func<STAFF_LOCATION, bool> predicate = s => s.Period_ID == period.Id && s.STAFF.Company_Id == 2 && s.STAFF.Is_Active == true;
                return base.GetModelsBy(predicate).OrderBy(sl => sl.Staff.FullName).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(StaffLocation staffLocation)
        {
            try
            {
                Func<STAFF_LOCATION, bool> predicate = s => s.Staff_ID == staffLocation.Staff.Id && s.Period_ID == staffLocation.Period.Id;
                STAFF_LOCATION entity = GetEntityBy(predicate);

                entity.Location_ID = staffLocation.Location.Id;

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

        public bool Remove(StaffLocation staffLocation)
        {
            try
            {
                Func<STAFF_LOCATION, bool> predicate = s => s.Staff_ID == staffLocation.Staff.Id && s.Period_ID == staffLocation.Period.Id && s.Location_ID == staffLocation.Location.Id;
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
