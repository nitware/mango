using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using System.Data;
using Mango.Model;
using Mango.Model.Translator;
using Mango.Model.Model;

namespace Mango.Business
{
    public class PeriodLogic : BusinessLogicBase<Period, PERIOD>
    {
        private CurrentPeriodLogic currentPeriodLogic;

        public PeriodLogic()
        {
            base.translator = new PeriodTranslator();
            currentPeriodLogic = new CurrentPeriodLogic();
        }

        public Period GetLatest()
        {
            try
            {
                List<Period> periods = GetAll().OrderBy(p => p.Id).ToList();
                return periods.LastOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Period GetCurrentPeriod(int id)
        {
            try
            {
                return currentPeriodLogic.Get();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int Add(Period period)
        {
            try
            {

                Period newPeriod = base.Add(period);
                if (newPeriod != null && newPeriod.Id > 0)
                {
                    CurrentPeriod currentPeriod = new CurrentPeriod();
                    currentPeriod.Period = newPeriod;
                    currentPeriodLogic.Modify(currentPeriod);
                }

                //int newPeriodId = periodDb.InsertPeriod(period.Name, period.Status.Id, period.Type, period.Span, period.StartDate, period.EndDate, transaction);
                //if (newPeriodId > 0)
                //{
                //    if (currentPeriodDbFacade.Add(newPeriodId, transaction))
                //    {
                //        return newPeriodId;
                //    }
                //}

                return newPeriod.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public int Add(Period period)
        //{
        //    try
        //    {

        //        Period newPeriod = base.Add(period);
        //        if (newPeriod != null && newPeriod.Id > 0)
        //        {
        //            currentPeriodLogic.Add();
        //        }
                
        //        int newPeriodId = periodDb.InsertPeriod(period.Name, period.Status.Id, period.Type, period.Span, period.StartDate, period.EndDate, transaction);
        //        if (newPeriodId > 0)
        //        {
        //            if (currentPeriodDbFacade.Add(newPeriodId, transaction))
        //            {
        //                //transaction.Commit();
        //                return newPeriodId;
        //            }
        //        }

        //        //transaction.Abort();
        //        return newPeriodId;
        //    }
        //    catch (Exception)
        //    {
        //        //transaction.Abort();
        //        throw;
        //    }
        //}

        public bool Modify(Period period)
        {
            try
            {
                Func<PERIOD, bool> predicate = p => p.Period_ID == period.Id;
                PERIOD entity = GetEntityBy(predicate);

                //entity.Type = period.Type;
                entity.Status_ID = period.Status.Id;
                entity.Period_Name = period.Name;
                entity.Period_Type_Id = period.Type.Id;
                entity.Span = period.Span;
                entity.Start_Date = period.StartDate;
                entity.End_Date = period.EndDate;
                entity.Year = period.Year;

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

        public bool Remove(Period period)
        {
            try
            {
                Func<PERIOD, bool> predicate = p => p.Period_ID == period.Id;
                bool suceeded = base.Remove(predicate);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public bool Remove(Period periodToDelete, Period currentPeriod)
        //{
        //    try
        //    {
        //        MangoEntities mango = new MangoEntities();
        //        System.Data.Objects.ObjectResult<int?> errors = mango.RemovePeriodBy(periodToDelete.Id, currentPeriod.Id);


        //        //if (errors != null)
        //        //{
        //        //    if (errors[0] != 0)
        //        //    {

        //        //    }
        //        //}

        //        return true;


        //        //Func<PERIOD, bool> predicate = p => p.Period_ID == period.Id;
        //        //bool suceeded = base.Remove(predicate);
        //        //repository.SaveChanges();
        //        //return suceeded;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}




    }
}
