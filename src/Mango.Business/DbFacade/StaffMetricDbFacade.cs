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
    public class StaffMetricDbFacade
    {
        private IStaffMetricDb staffMetricDb;
        //private IStaffKpiMetricDb staffKpiMetricDb;

        public StaffMetricDbFacade()
        {
            staffMetricDb = new StaffMetricDb();
        }

        public int CreateStaffMetric(Metric metric, long appraisalHeaderId, Transaction transaction)
        {
            try
            {
                if (metric != null)
                {
                    return staffMetricDb.InsertStaffMetric(appraisalHeaderId, metric.Id, metric.Score, transaction);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return -1;
        }

        //public bool CreateStaffKpiMetric(Metric metric, int staffMetricId, Transaction transaction)
        //{
        //    try
        //    {
        //        if (metric != null)
        //        {
        //            staffKpiMetricDb = new StaffKpiMetricDb();
        //            return staffKpiMetricDb.InsertStaffKpiMetric(staffMetricId, metric.Score, transaction);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return false;
        //}

        public bool ModifyStaffMetric(StaffMetric staffMetric, Transaction transaction)
        {
            try
            {
                if (staffMetric != null)
                {
                    return staffMetricDb.UpdateStaffMetricByStaffMetricId(staffMetric.Id, staffMetric.AppraisalHeaderId, staffMetric.MetricId, staffMetric.Score, transaction);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }



    }

}