using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Mango.Data;
using Mango.DataAccess;
using Mango.Model;
using Mango.Data.Interfaces;


namespace Mango.Business.DbFacade
{
    public class MetricRatingDbFacade
    {
        //private Transaction transaction;
        private IMetricRatingDb metricRatingDb;

        public MetricRatingDbFacade()
        {
            metricRatingDb = new MetricRatingDb();
        }

        public bool Add(List<MetricRating> metricRatings, Transaction transaction)
        {
            try
            {
                foreach (MetricRating metricRating in metricRatings)
                {
                    if (!metricRatingDb.InsertMetricRating(metricRating.Metrics.Id, metricRating.Rating.Id, metricRating.From, metricRating.To, metricRating.RatingType.Id, metricRating.Period.Id, transaction))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MetricRating> GetMetricRatingByPeriodId(int periodId)
        {
            try
            {
                //List<MetricRating> metricRatings = new List<MetricRating>();
                DataSet dsMetricRating = metricRatingDb.SelectMetricRatingByPeriodID(periodId);

                return ConvertToModel(dsMetricRating);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<MetricRating> ConvertToModel(DataSet dsMetricRating)
        {
            try
            {
                List<MetricRating> metricRatings = new List<MetricRating>();

                if (dsMetricRating != null)
                {
                    if (dsMetricRating.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsMetricRating.Tables[0].Rows.Count; i++)
                        {
                            MetricRating metricRating = new MetricRating();
                            metricRating.RatingType = new RatingType();
                            metricRating.Period = new Period();

                            metricRating.Metrics = new Model.Model.Metrics();
                            metricRating.Rating = new Rating();

                            metricRating.Metrics.Id = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt64(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_METRIC_ID]);
                            metricRating.Rating.Id = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_RATING_ID] == DBNull.Value ? (byte)0 : Convert.ToByte(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_RATING_ID]);
                            metricRating.From = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_FROM] == DBNull.Value ? 0 : Convert.ToDecimal(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_FROM]);
                            metricRating.To = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_TO] == DBNull.Value ? 0 : Convert.ToDecimal(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_TO]);
                            metricRating.RatingType.Id = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_RATING_TYPE_ID] == DBNull.Value ? (byte)0 : Convert.ToByte(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_RATING_TYPE_ID]);
                            metricRating.Period.Id = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_PERIOD_ID] == DBNull.Value ? 0 : Convert.ToInt32(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_PERIOD_ID]);

                            metricRatings.Add(metricRating);
                        }
                    }
                }

                return metricRatings;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetBy(Metric metric)
        {
            try
            {
                DataSet dsMetricRating = metricRatingDb.SelectMetricRatingByMetricIdAndPeriodID(metric.Id, metric.Period.Id);

                List<MetricRating> metricRatings = ConvertToModel(dsMetricRating);
                return metricRatings.Max(mr => mr.Rating.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<MetricRating> GetMetricRatingByMetricIdAndPeriodId(long metricId, int periodId, Transaction transaction)
        //{
        //    try
        //    {
        //        List<MetricRating> metricRatings = new List<MetricRating>();
        //        DataSet dsMetricRating = metricRatingDb.SelectMetricRatingByMetricIdAndPeriodID(metricId, periodId);

        //        if (dsMetricRating != null)
        //        {
        //            if (dsMetricRating.Tables[0].Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dsMetricRating.Tables[0].Rows.Count; i++)
        //                {
        //                    MetricRating metricRating = new MetricRating();
        //                    metricRating.RatingType = new RatingType();
        //                    metricRating.Period = new Period();

        //                    metricRating.Id = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt64(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_METRIC_ID]);
        //                    metricRating.Rating = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_RATING_ID] == DBNull.Value ? (byte)0 : Convert.ToByte(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_RATING_ID]);
        //                    metricRating.From = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_FROM] == DBNull.Value ? 0 : Convert.ToDouble(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_FROM]);
        //                    metricRating.To = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_TO] == DBNull.Value ? 0 : Convert.ToDouble(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_TO]);
        //                    metricRating.RatingType.Id = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_RATING_TYPE_ID] == DBNull.Value ? (byte)0 : Convert.ToByte(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_RATING_TYPE_ID]);
        //                    metricRating.Period.Id = dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_PERIOD_ID] == DBNull.Value ? 0 : Convert.ToInt32(dsMetricRating.Tables[0].Rows[i][metricRatingDb.FIELD_PERIOD_ID]);

        //                    metricRatings.Add(metricRating);
        //                }
        //            }
        //        }

        //        return metricRatings;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

       



    }



}