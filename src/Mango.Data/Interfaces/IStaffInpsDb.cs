﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.DataAccess;


namespace Mango.Data.Interfaces
{
    public interface IStaffInpsDb
    {
        //string FIELD_STAFF_METRIC_ID { get; }
        //string FIELD_APPRAISAL_HEADER_ID { get; }
        //string FIELD_METRIC_ID { get; }

        string FIELD_STAFF_INPS_ID { get; }
        string FIELD_STAFF_METRIC_ID { get; }
        string FIELD_APPRAISAL_HEADER_ID { get; }
        string FIELD_INPS_ID { get; }
        string FIELD_SCORE { get; }

        int InsertStaffInps(long appraisalHeaderId, long InpsId, decimal score, Transaction transaction);

        //int InsertStaffInps(long appraisalHeaderId, long InpsId, long metricId, decimal score, Transaction transaction);

        //bool DeleteStaffMetricByStaffMetricId(long staffMetricId, Transaction transaction);
        //bool DeleteStaffMetricByAppraisalHeaderId(long appraisalHeaderId, Transaction transaction);
        //bool DeleteStaffMetricByMetricId(long metricId, Transaction transaction);
        //bool DeleteStaffMetricByStaff_Metric_IDAndAppraisal_Header_IDAndMetric_ID(long staffMetricId, long appraisalHeaderId, long metricId, Transaction transaction);
        //bool UpdateStaffMetricByStaffMetricId(long staffMetricId, long appraisalHeaderId, long metricId, decimal score, Transaction transaction);
        //bool UpdateStaffMetricByAppraisalHeaderId(long staffMetricId, long appraisalHeaderId, long metricId, Transaction transaction);
        //bool UpdateStaffMetricByMetricId(long staffMetricId, long appraisalHeaderId, long metricId, Transaction transaction);
        //bool UpdateStaffMetricByStaff_Metric_IDAndAppraisal_Header_IDAndMetric_ID(long staffMetricId, long appraisalHeaderId, long metricId, Transaction transaction);
        //DataSet SelectAllStaffMetric();
        //DataSet SelectStaffMetricByStaffMetricId(long staffMetricId);
        //DataSet SelectStaffMetricByAppraisalHeaderId(long appraisalHeaderId);
        //DataSet SelectStaffMetricByMetricId(long metricId);
        //DataSet SelectStaffMetricByStaff_Metric_IDAndAppraisal_Header_IDAndMetric_ID(long staffMetricId, long appraisalHeaderId, long metricId);
       

    }
}
