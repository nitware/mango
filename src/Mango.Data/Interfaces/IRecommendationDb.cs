using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.DataAccess;
using System.Data;

namespace Mango.Data.Interfaces
{
    public interface IRecommendationDb
    {
        string FIELD_RECOMMENDATION_ID { get; }
        string FIELD_RECOMMENDATION_NAME { get; }
               
        bool InsertRecommendation(byte recommendationId, string recommendationName, Transaction transaction);
        bool DeleteRecommendationByRecommendationId(byte recommendationId, Transaction transaction);
        bool UpdateRecommendationByRecommendationId(byte recommendationId, string recommendationName, Transaction transaction);
        DataSet SelectAllRecommendation();
        DataSet SelectRecommendationByRecommendationId(byte recommendationId);
       
    }


}
