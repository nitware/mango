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
    public class PeriodDbFacade
    {
        private IPeriodDb periodDb;
        private CurrentPeriodDbFacade currentPeriodDbFacade;

        public PeriodDbFacade()
        {
            periodDb = new PeriodDb();
            currentPeriodDbFacade = new CurrentPeriodDbFacade();
        }

        //public List<Period> GetAll()
        //{
        //    try
        //    {
        //        List<Period> periods = new List<Period>();

        //        DataSet dsPeriod = periodDb.SelectAllPeriod();
        //        if (dsPeriod != null)
        //        {
        //            if (dsPeriod.Tables[0].Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dsPeriod.Tables[0].Rows.Count; i++)
        //                {
        //                    Period period = new Period();
        //                    period.Status = new Model.Model.Status();
        //                    period.Id = Convert.ToInt32(dsPeriod.Tables[0].Rows[i][periodDb.FIELD_PERIOD_ID]);
        //                    period.Name = Convert.ToString(dsPeriod.Tables[0].Rows[i][periodDb.FIELD_PERIOD_NAME]);
        //                    period.Status.Id = Convert.ToByte(dsPeriod.Tables[0].Rows[i][periodDb.FIELD_STATUS_ID]);
        //                    period.Status.Name = Convert.ToString(dsPeriod.Tables[0].Rows[i][periodDb.FIELD_STATUS_NAME]);
        //                    period.Type = Convert.ToString(dsPeriod.Tables[0].Rows[i][periodDb.FIELD_TYPE]);
        //                    period.Span = Convert.ToByte(dsPeriod.Tables[0].Rows[i][periodDb.FIELD_SPAN]);
        //                    period.StartDate = Convert.ToDateTime(dsPeriod.Tables[0].Rows[i][periodDb.FIELD_START_DATE]);
        //                    period.EndDate = Convert.ToDateTime(dsPeriod.Tables[0].Rows[i][periodDb.FIELD_END_DATE]);

        //                    periods.Add(period);
        //                }

        //                return periods;
        //            }
        //        }

        //        return null;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int GetLatestID()
        //{
        //    try
        //    {
        //        List<Period> periods = GetAll();
        //        return periods.Max(p => p.Id);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //public Period LoadCurrentAppraisalDetail(int periodId)
        //{
        //    try
        //    {
        //        DataSet dsPeriod = periodDb.SelectPeriodByPeriodId(periodId);
        //        if (dsPeriod != null)
        //        {
        //            if (dsPeriod.Tables[0].Rows.Count > 0)
        //            {
        //                Period period = new Period();
        //                period.Status = new Model.Model.Status();
        //                period.Id = Convert.ToInt32(dsPeriod.Tables[0].Rows[0][periodDb.FIELD_PERIOD_ID]);
        //                period.Name = Convert.ToString(dsPeriod.Tables[0].Rows[0][periodDb.FIELD_PERIOD_NAME]);
        //                period.Status.Name = Convert.ToString(dsPeriod.Tables[0].Rows[0][periodDb.FIELD_STATUS_NAME]);
        //                period.Type = Convert.ToString(dsPeriod.Tables[0].Rows[0][periodDb.FIELD_TYPE]);
        //                period.Span = Convert.ToByte(dsPeriod.Tables[0].Rows[0][periodDb.FIELD_SPAN]);
        //                period.StartDate = Convert.ToDateTime(dsPeriod.Tables[0].Rows[0][periodDb.FIELD_START_DATE]);
        //                period.EndDate = Convert.ToDateTime(dsPeriod.Tables[0].Rows[0][periodDb.FIELD_END_DATE]);

        //                return period;
        //            }
        //        }

        //        return null;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public int Add(Period period, Transaction transaction)
        {
            try
            {
                 int newPeriodId = periodDb.InsertPeriod(period.Name, period.Status.Id, period.Type.Id, period.Span, period.StartDate, period.EndDate, period.Year, transaction);
                if (newPeriodId > 0)
                {
                    if (currentPeriodDbFacade.Add(newPeriodId, transaction))
                    {
                        //transaction.Commit();
                        return newPeriodId;
                    }
                }

                //transaction.Abort();
                return newPeriodId;
            }
            catch (Exception)
            {
                //transaction.Abort();
                throw;
            }
        }


        
    }


}