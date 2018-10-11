using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;

namespace Mango.Model.Translator
{
    public class MetricRatingTranslator : TranslatorBase<MetricRating, METRIC_RATING>
    {
        private RatingTranslator ratingTranslator;
        private MetricsTranslator metricsTranslator;
        private RatingTypeTranslator ratingTypeTranslator;
        private PeriodTranslator periodTranslator;

        public MetricRatingTranslator()
        {
            ratingTranslator = new RatingTranslator();
            metricsTranslator = new MetricsTranslator();
            ratingTypeTranslator = new RatingTypeTranslator();
            periodTranslator = new PeriodTranslator();
        }

        public override MetricRating TranslateToModel(METRIC_RATING entity)
        {
            try
            {
                MetricRating metricRating = null;
                if (entity != null)
                {
                    metricRating = new MetricRating();
                    metricRating.Metrics = metricsTranslator.Translate(entity.METRIC);
                    metricRating.Rating = ratingTranslator.Translate(entity.RATING);
                    metricRating.From = entity.From;
                    metricRating.To = entity.To;
                    metricRating.RatingType = ratingTypeTranslator.Translate(entity.RATING_TYPE);
                    metricRating.Period = periodTranslator.Translate(entity.PERIOD);
                }

                return metricRating;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override METRIC_RATING TranslateToEntity(MetricRating metricRating)
        {
            try
            {
                METRIC_RATING entity = null;
                if (metricRating != null)
                {
                    entity = new METRIC_RATING();
                    entity.Metric_ID = metricRating.Metrics.Id;
                    entity.Rating_ID = metricRating.Rating.Id;
                    entity.From = metricRating.From;
                    entity.To = metricRating.To;
                    entity.Rating_Type_ID = metricRating.RatingType.Id;
                    entity.Period_ID = metricRating.Period.Id;
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
