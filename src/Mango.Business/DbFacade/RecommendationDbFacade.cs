using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data;
using Mango.Data;
using Mango.Model;
using Mango.Data.Interfaces;

namespace Mango.Business.DbFacade
{
    public class RecommendationDbFacade
    {
        private IRecommendationDb recommendationDb;

        public RecommendationDbFacade()
        {
            recommendationDb = new RecommendationDb();
        }

        public List<Recommendation> Load()
        {
            try
            {
                List<Recommendation> recommendations = new List<Recommendation>();
                DataSet dsRecommendation = recommendationDb.SelectAllRecommendation();

                if (dsRecommendation != null)
                {
                    if (dsRecommendation.Tables[0].Rows.Count > 0)
                    {
                        Recommendation rcd = new Recommendation();
                        rcd.Id = 0;
                        rcd.Name = "< Select Recommendation >";
                        recommendations.Add(rcd);

                        for (int i = 0; i < dsRecommendation.Tables[0].Rows.Count; i++)
                        {
                            Recommendation recommendation = new Recommendation();
                            recommendation.Id = Convert.ToByte(dsRecommendation.Tables[0].Rows[i][recommendationDb.FIELD_RECOMMENDATION_ID]);
                            recommendation.Name = Convert.ToString(dsRecommendation.Tables[0].Rows[i][recommendationDb.FIELD_RECOMMENDATION_NAME]);

                            recommendations.Add(recommendation);
                        }
                    }
                }

                return recommendations;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



    }
}