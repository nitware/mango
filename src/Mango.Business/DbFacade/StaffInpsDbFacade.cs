using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Mango.Data.Interfaces;
using Mango.Data;
using Mango.DataAccess;
using Mango.Model;


namespace Mango.Business.DbFacade
{
    public class StaffInpsDbFacade
    {
        private IStaffInpsDb staffInpsDb;

        public StaffInpsDbFacade()
        {
            staffInpsDb = new StaffInpsDb();
        }

        public int CreateStaffInps(Metric metric, long appraisalHeaderId, Transaction transaction)
        {
            try
            {
                if (metric != null)
                {
                    return staffInpsDb.InsertStaffInps(appraisalHeaderId, metric.Id, metric.Score, transaction);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return -1;
        }

        //public bool ModifyStaffMetric(StaffMetric staffMetric, Transaction transaction)
        //{
        //    try
        //    {
        //        if (staffMetric != null)
        //        {
        //            return staffMetricDb.UpdateStaffMetricByStaffMetricId(staffMetric.Id, staffMetric.AppraisalHeaderId, staffMetric.MetricId, staffMetric.Score, transaction);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return false;
        //}



    }

}