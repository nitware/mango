using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;

namespace Mango.Business
{
    public class ScoreLogic //: BusinessLogicBase<Score, SCORE>
    {
        //private RatingLogic ratingLogic;

        //public ScoreLogic() 
        //{
        //    base.translator = new ScoreTranslator();
        //    ratingLogic = new RatingLogic();
        //}

        //public List<ScoreTable> GetBy(User user, Caterer caterer)
        //{
        //    try
        //    {
        //        List<ScoreTable> scoreTables = null;

        //        if (ScoreAlreadyExist(user, caterer))
        //        {
        //            scoreTables = (from sc in base.repository.Fetch<VW_SCORE>()
        //                           where sc.Caterer_Id == caterer.Id && sc.User_Id == user.Id
        //                           select new ScoreTable
        //                                 {
        //                                     CatererId = sc.Caterer_Id,
        //                                     UserId = sc.User_Id,
        //                                     RatingCriteriaId = sc.Rating_Criteria_Id,
        //                                     RatingCriteriaName = sc.Rating_Criteria_Name,
        //                                     Excellent = sc.Excellent.HasValue ? true : false,
        //                                     VeryGood = sc.Very_Good.HasValue ? true : false,
        //                                     Good = sc.Good.HasValue ? true : false,
        //                                     Fair = sc.Fair.HasValue ? true : false,
        //                                     Poor = sc.Poor.HasValue ? true : false,
        //                                     Score = sc.Score
        //                                 }).OrderBy(sc => sc.RatingCriteriaId).ToList();
        //        }
        //        else
        //        {
        //            scoreTables = (from sc in base.repository.Fetch<VW_NO_SCORE>()
        //                           where sc.Caterer_Id == caterer.Id && sc.User_Id == user.Id
        //                           select new ScoreTable
        //                           {
        //                               CatererId = sc.Caterer_Id,
        //                               UserId = sc.User_Id,
        //                               RatingCriteriaId = sc.Rating_Criteria_Id,
        //                               RatingCriteriaName = sc.Rating_Criteria_Name,
        //                               Excellent = sc.Excellent.HasValue ? true : false,
        //                               VeryGood = sc.Very_Good.HasValue ? true : false,
        //                               Good = sc.Good.HasValue ? true : false,
        //                               Fair = sc.Fair.HasValue ? true : false,
        //                               Poor = sc.Poor.HasValue ? true : false,
        //                               Score = sc.Score
        //                           }).OrderBy(sc => sc.RatingCriteriaId).ToList();
        //        }

        //        return scoreTables;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private bool ScoreAlreadyExist(User user, Caterer caterer)
        //{
        //    try
        //    {
        //        Func<SCORE, bool> selector = sc => (sc.USER.User_Id == user.Id) && (sc.CATERER.Caterer_Id >= caterer.Id);
                
        //        List<Score> scores = GetModelsBy(selector);
        //        if (scores != null && scores.Count > 0)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int Add(List<Caterer> caterers, User user)
        //{
        //    try
        //    {
        //        if (caterers != null && caterers.Count > 0)
        //        {
        //            List<Score> scores = new List<Score>();
        //            foreach (Caterer caterer in caterers)
        //            {
        //                List<ScoreTable> scoreTables = caterer.ScoreTables;
        //                if (scoreTables != null && scoreTables.Count > 0)
        //                {

        //                    foreach (ScoreTable scoreTable in scoreTables)
        //                    {
        //                        Score score = new Score();
        //                        score.Rating = new Rating();
        //                        score.RatingCriteria = new RatingCriteria();

        //                        score.Caterer = caterer;
        //                        score.RatingCriteria.Id = scoreTable.RatingCriteriaId;
        //                        score.Rating = ratingLogic.GetBy(scoreTable.Score);
        //                        score.User = user;
        //                        score.Date = DateTime.Now;

        //                        scores.Add(score);
        //                    }
        //                }
        //            }
                    
        //            return base.Add(scores);
        //        }

        //        return -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ScoreSummary> Get()
        //{
        //    try
        //    {
        //        List<ScoreSummary> scoreSummary = null;


        //        scoreSummary = (from ss in base.repository.Fetch<VW_SCORE_SUMMARY>()
        //                        select new ScoreSummary
        //                           {
        //                               StaffId = ss.Staff_Id,
        //                               StaffName = ss.Staff,
        //                               Caterer1 = ss.Caterer_1.HasValue ? ss.Caterer_1.Value : (double)0,
        //                               Caterer2 = ss.Caterer_2.HasValue ? ss.Caterer_2.Value : (double)0
                                      
        //                           }).OrderBy(sc => sc.StaffName).ToList();



        //        return scoreSummary;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

       



    }

}
