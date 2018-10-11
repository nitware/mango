using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;

namespace Mango.Model.Translator
{
    public class StaffMetricTranslator : TranslatorBase<StaffMetric, STAFF_METRIC>
    {
        private MetricsTranslator metricsTranslator;
        private AppraisalTranslator appraisalTranslator;

        public StaffMetricTranslator()
        {
            metricsTranslator = new MetricsTranslator();
            appraisalTranslator = new AppraisalTranslator();
        }

        public override StaffMetric TranslateToModel(STAFF_METRIC entity)
        {
            try
            {
                StaffMetric model = null;
                if (entity != null)
                {
                    model = new StaffMetric();
                    model.Id = entity.Staff_Metric_ID;
                    model.AppraisalHeaderId = entity.Appraisal_Header_ID;
                    model.MetricId = entity.Metric_ID;
                    model.Score = entity.Score.GetValueOrDefault();
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STAFF_METRIC TranslateToEntity(StaffMetric model)
        {
            try
            {
                STAFF_METRIC entity = null;
                if (model != null)
                {
                    entity = new STAFF_METRIC();
                    entity.Staff_Metric_ID = model.Id;
                    entity.Appraisal_Header_ID = model.AppraisalHeaderId;
                    entity.Metric_ID = model.MetricId;
                    entity.Score = model.Score;
                }

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }


}
