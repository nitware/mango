using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model;
using Mango.Model.Translator;
using System.Data;

namespace Mango.Business
{
    public class CurrentPeriodLogic : BusinessLogicBase<CurrentPeriod, CURRENT_PERIOD>
    {
        public CurrentPeriodLogic()
        {
            base.translator = new CurrentPeriodTranslator();
        }

        public Period Get()
        {
            try
            {
                CurrentPeriod currentPeriod = base.GetAll().FirstOrDefault();
                return currentPeriod.Period;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public Period Add(Period period)
        //{
        //    try
        //    {
        //        CurrentPeriod currentPeriod = new CurrentPeriod();
        //        currentPeriod.Period = period;
        //        base.Add(
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public bool Modify(CurrentPeriod currentPeriod)
        {
            try
            {
                List<CurrentPeriod> currentPeriods = base.GetAll();

                if (currentPeriods != null && currentPeriods.Count > 0)
                {
                    Remove(currentPeriods);
                }

                return base.Add(currentPeriod) != null ? true : false;
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

        public bool Remove(List<CurrentPeriod> currentPeriods)
        {
            try
            {
                if (currentPeriods != null)
                {
                    foreach (CurrentPeriod currentPeriod in currentPeriods)
                    {
                        Func<CURRENT_PERIOD, bool> selector = cp => cp.Period_ID == currentPeriod.Period.Id;
                        bool suceeded = base.Remove(selector);
                    }
                }

                return repository.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }
}
