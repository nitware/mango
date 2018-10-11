using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class MetricsPerspectiveTranslator : TranslatorBase<MetricsPerspective, METRIC_PERSPECTIVE>
    {
        public override MetricsPerspective TranslateToModel(METRIC_PERSPECTIVE entity)
        {
            try
            {
                MetricsPerspective metricsPerspective = null;
                if (entity != null)
                {
                    metricsPerspective = new MetricsPerspective();
                    metricsPerspective.Id = entity.Metric_Perspective_ID;
                    metricsPerspective.Name = entity.Metric_Perspective_Name;
                }

                return metricsPerspective;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override METRIC_PERSPECTIVE TranslateToEntity(MetricsPerspective metricsPerspective)
        {
            try
            {
                METRIC_PERSPECTIVE entity = null;
                if (metricsPerspective != null)
                {
                    entity = new METRIC_PERSPECTIVE();
                    entity.Metric_Perspective_ID = metricsPerspective.Id;
                    entity.Metric_Perspective_Name = metricsPerspective.Name;
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
