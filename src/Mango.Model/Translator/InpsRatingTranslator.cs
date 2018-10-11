using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class InpsRatingTranslator : TranslatorBase<InpsRating, INPS_RATING>
    {
        private InpsTypeTranslator inpsTypeTranslator;
        private RatingTranslator ratingTranslator;
        private InpsTranslator npsTranslator;
        private RatingTypeTranslator ratingTypeTranslator;
        private PeriodTranslator periodTranslator;

        public InpsRatingTranslator()
        {
            ratingTranslator = new RatingTranslator();
            npsTranslator = new InpsTranslator();
            ratingTypeTranslator = new RatingTypeTranslator();
            periodTranslator = new PeriodTranslator();
            inpsTypeTranslator = new InpsTypeTranslator();
        }

        public override InpsRating TranslateToModel(INPS_RATING entity)
        {
            try
            {
                InpsRating npsRating = null;
                if (entity != null)
                {
                    npsRating = new InpsRating();
                    npsRating.Type = inpsTypeTranslator.Translate(entity.INPS_TYPE);
                    npsRating.Rating = ratingTranslator.Translate(entity.RATING);
                    npsRating.From = entity.From;
                    npsRating.To = entity.To;
                    npsRating.RatingType = ratingTypeTranslator.Translate(entity.RATING_TYPE);
                    npsRating.Period = periodTranslator.Translate(entity.PERIOD);
                }

                return npsRating;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override INPS_RATING TranslateToEntity(InpsRating npsRating)
        {
            try
            {
                INPS_RATING entity = null;
                if (npsRating != null)
                {
                    entity = new INPS_RATING();
                    entity.Inps_Type_Id = npsRating.Type.Id;
                    entity.Rating_ID = npsRating.Rating.Id;
                    entity.From = npsRating.From;
                    entity.To = npsRating.To;
                    entity.Rating_Type_ID = npsRating.RatingType.Id;
                    entity.Period_ID = npsRating.Period.Id;
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
