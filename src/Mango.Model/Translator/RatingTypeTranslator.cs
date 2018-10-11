using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;

namespace Mango.Model.Translator
{
    public class RatingTypeTranslator : TranslatorBase<RatingType, RATING_TYPE>
    {
        public override RatingType TranslateToModel(RATING_TYPE entity)
        {
            try
            {
                RatingType ratingType = null;
                if (entity != null)
                {
                    ratingType = new RatingType();
                    ratingType.Id = entity.Rating_Type_ID;
                    ratingType.Name = entity.Rating_Name;
                }

                return ratingType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override RATING_TYPE TranslateToEntity(RatingType ratingType)
        {
            try
            {
                RATING_TYPE entity = null;
                if (ratingType != null)
                {
                    entity = new RATING_TYPE();
                    entity.Rating_Type_ID = ratingType.Id;
                    entity.Rating_Name = ratingType.Name;
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
