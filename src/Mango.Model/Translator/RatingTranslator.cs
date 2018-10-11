using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;

namespace Mango.Model.Translator
{
    public class RatingTranslator : TranslatorBase<Rating, RATING>
    {
        public override Rating TranslateToModel(RATING ratingEntity)
        {
            try
            {
                Rating rating = null;
                if (ratingEntity != null)
                {
                    rating = new Rating();
                    rating.Id = ratingEntity.Rating_ID;
                    rating.Name = ratingEntity.Rating_Name;
                }

                return rating;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override RATING TranslateToEntity(Rating rating)
        {
            try
            {
                RATING ratingEntity = null;
                if (rating != null)
                {
                    ratingEntity = new RATING();
                    ratingEntity.Rating_ID = rating.Id;
                    ratingEntity.Rating_Name = rating.Name;
                }

                return ratingEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }




}
