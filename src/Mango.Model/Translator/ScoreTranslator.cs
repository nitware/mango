using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;

namespace Mango.Model.Translator
{
    public class ScoreTranslator // : TranslatorBase<Score, SCORE>
    {
       // private CatererTranslator catererTranslator;
       // private RatingCriteriaTranslator ratingCriteriaTranslator;
       // private RatingTranslator ratingTranslator;
       // private UserTranslator userTranslator;

       //public ScoreTranslator()
       // {
       //     catererTranslator = new CatererTranslator();
       //     ratingCriteriaTranslator = new RatingCriteriaTranslator();
       //     ratingTranslator = new RatingTranslator();
       //     userTranslator = new UserTranslator();
       // }

       // public override Score TranslateToModel(SCORE scoreEntity)
       // {
       //     try
       //     {
       //         Score score = null;
       //         if (scoreEntity != null)
       //         {
       //             score = new Score();
       //             score.Id = scoreEntity.Score_Id;
       //             score.Caterer = catererTranslator.Translate(scoreEntity.CATERER);
       //             score.RatingCriteria = ratingCriteriaTranslator.Translate(scoreEntity.RATING_CRITERIA);
       //             score.Rating = ratingTranslator.Translate(scoreEntity.RATING);
       //             score.User = userTranslator.Translate(scoreEntity.USER);
       //             score.Date = scoreEntity.Date;
       //         }

       //         return score;
       //     }
       //     catch (Exception)
       //     {
       //         throw;
       //     }
       // }

       // public override SCORE TranslateToEntity(Score score)
       // {
       //     try
       //     {
       //         SCORE scoreEntity = null;
       //         if (score != null)
       //         {
       //             scoreEntity = new SCORE();
       //             scoreEntity.Score_Id = score.Id;
       //             scoreEntity.Caterer_Id = score.Caterer.Id;
       //             scoreEntity.Rating_Criteria_Id = score.RatingCriteria.Id;
       //             scoreEntity.Rating_Id = score.Rating.Id;
       //             scoreEntity.User_Id = score.User.Id;
       //             scoreEntity.Date = score.Date;
       //         }

       //         return scoreEntity;
       //     }
       //     catch (Exception)
       //     {
       //         throw;
       //     }
       // }

    }


}
