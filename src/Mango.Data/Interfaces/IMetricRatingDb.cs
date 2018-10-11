using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.DataAccess;

namespace Mango.Data.Interfaces
{
    public interface IMetricRatingDb
    {
        string FIELD_METRIC_ID { get; }
        string FIELD_RATING_ID { get; }
        string FIELD_FROM { get; }
        string FIELD_TO { get; }
        string FIELD_RATING_TYPE_ID { get; }
        string FIELD_PERIOD_ID { get; }
        
        //bool InsertMetricRating(DataSet dsAuditItem, long metricId, byte ratingId, decimal from, decimal to, byte ratingTypeId, int periodId, Transaction transaction);
        bool InsertMetricRating(long metricId, byte ratingId, decimal from, decimal to, byte ratingTypeId, int periodId, Transaction transaction);
        DataSet SelectMetricRatingByMetricIdAndPeriodID(long metricId, int periodId);
        DataSet SelectMetricRatingByPeriodID(int periodId);
    }


}
