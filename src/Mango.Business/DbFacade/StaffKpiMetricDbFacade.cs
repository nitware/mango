using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Mango.Data;
using Mango.DataAccess;
using Mango.Model;
using Mango.Data.Interfaces;


namespace Mango.Business.DbFacade
{
    public class StaffKpiMetricDbFacade
    {
         private IStaffKpiMetricDb staffKpiMetricDb;

         public StaffKpiMetricDbFacade()
         {
             staffKpiMetricDb = new StaffKpiMetricDb();
         }

         public bool CreateStaffKpiMetric(Metric metric, int staffMetricId, Transaction transaction)
         {
             try
             {
                 if (metric != null)
                 {
                     staffKpiMetricDb = new StaffKpiMetricDb();
                     return staffKpiMetricDb.InsertStaffKpiMetric(staffMetricId, metric.Score, transaction);
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }

             return false;
         }

         public bool ModifyStaffKpiMetric(Metric metric, Transaction transaction)
         {
             try
             {
                 if (metric != null)
                 {
                     return staffKpiMetricDb.UpdateStaffKpiMetricByStaffMetricId(metric.StaffMetricId, metric.Score, transaction);
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