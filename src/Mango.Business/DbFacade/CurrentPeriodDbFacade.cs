using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections.ObjectModel;
using System.Data;
using Mango.Data;
using Mango.Model;
using Mango.Data.Interfaces;
using Mango.DataAccess;

namespace Mango.Business.DbFacade
{
    public class CurrentPeriodDbFacade
    {
         private PeriodLogic periodLogic;
         private ICurrentPeriodDb currentPeriodD;
         private PeriodDbFacade periodDbFacade;

         public CurrentPeriodDbFacade()
         {
             currentPeriodD = new CurrentPeriodDb();
             periodLogic = new PeriodLogic();
         }

         //public Period GetCurrentPeriod()
         //{
         //    try
         //    {
         //        Period period = new Period();
         //        DataSet dsPeriod = currentPeriodD.SelectAllCurrentPeriod();
         //        if (dsPeriod != null)
         //        {
         //            if (dsPeriod.Tables[0].Rows.Count > 0)
         //            {
         //                periodDbFacade = new PeriodDbFacade();
         //                period.Id = Convert.ToInt32(dsPeriod.Tables[0].Rows[0][currentPeriodD.FIELD_PERIOD_ID]);
         //                period = periodLogic.GetCurrentPeriod(period.Id);

         //                //period = periodDbFacade.LoadCurrentAppraisalDetail(period.Id);

         //                return period;
         //            }
         //        }

         //        return null;
         //    }
         //    catch (Exception ex)
         //    {
         //        throw ex;
         //    }
         //}

         public Period GetCurrentPeriod()
         {
             try
             {
                 Period period = null;
                 DataSet dsPeriod = currentPeriodD.SelectAllCurrentPeriod();
                 if (dsPeriod != null)
                 {
                     if (dsPeriod.Tables[0].Rows.Count > 0)
                     {
                         period = new Period();
                         periodDbFacade = new PeriodDbFacade();
                         period.Id = Convert.ToInt32(dsPeriod.Tables[0].Rows[0][currentPeriodD.FIELD_PERIOD_ID]);
                         period = periodLogic.GetCurrentPeriod(period.Id);
                     }
                 }

                 return period;
             }
             catch (Exception)
             {
                 throw;
             }
         }

         public bool Add(int periodId, Transaction transaction)
         {
             try
             {
                 if (Remove(transaction))
                 {
                     return currentPeriodD.InsertCurrentPeriod(periodId, transaction);
                 }

                 return false;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public bool Remove(Transaction transaction)
         {
             try
             {
                 return currentPeriodD.DeleteAllCurrentPeriod(transaction);
             }
             catch (Exception)
             {
                 throw;
             }
         }


        
    }
}