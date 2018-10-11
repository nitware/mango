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
    public class PaceRatingDbFacade
    {
        private IPaceRatingDb paceRatingDb;

        public PaceRatingDbFacade()
        {
            paceRatingDb = new PaceRatingDb();
        }

        public PaceRating GetGrade(int score)
        {
            try
            {
                PaceRating paceRating = new PaceRating();
                DataSet dsPaceRating = paceRatingDb.SelectPaceRatingByFromAndTo(score);
                if (dsPaceRating != null)
                {
                    if (dsPaceRating.Tables[0].Rows.Count > 0)
                    {
                        paceRating.Rating = Convert.ToString(dsPaceRating.Tables[0].Rows[0][paceRatingDb.FIELD_RATING]);
                    }
                }

                return paceRating;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaceRating> Load()
        {
            try
            {
                List<PaceRating> paceRatings = new List<PaceRating>();
                DataSet dsPaceRating = paceRatingDb.SelectAllPaceRating();
                if (dsPaceRating != null)
                {
                    if (dsPaceRating.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsPaceRating.Tables[0].Rows.Count; i++)
                        {
                            PaceRating paceRating = new PaceRating();
                            paceRating.Rating = Convert.ToString(dsPaceRating.Tables[0].Rows[i][paceRatingDb.FIELD_RATING]);
                            paceRating.From = Convert.ToDecimal(dsPaceRating.Tables[0].Rows[i][paceRatingDb.FIELD_FROM]);
                            paceRating.To = Convert.ToDecimal(dsPaceRating.Tables[0].Rows[i][paceRatingDb.FIELD_TO]);

                            paceRatings.Add(paceRating);
                        }
                    }
                }

                return paceRatings;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


}