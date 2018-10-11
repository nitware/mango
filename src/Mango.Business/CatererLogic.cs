using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Model.Translator;
using Mango.Model;
using Mango.Data;

namespace Mango.Business
{
    public class CatererLogic //: BusinessLogicBase<Caterer, CATERER>
    {
        //private ScoreLogic scoreLogic;

        //public CatererLogic() 
        //{
        //    base.translator = new CatererTranslator();
        //    scoreLogic = new ScoreLogic();
        //    //ratingLogic = new RatingLogic();
        //}

        //public List<Caterer> GetAll(User user)
        //{
        //    try
        //    {
        //        List<Caterer> caterers = base.GetAll();
        //        foreach (Caterer caterer in caterers)
        //        {
        //            caterer.ScoreTables = scoreLogic.GetBy(user, caterer);
        //        }

        //        return caterers;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<Caterer> GetAliases()
        //{
        //    try
        //    {
        //        var caterers = (from c in base.repository.Fetch<VW_CATERER>()
        //                        select new Caterer
        //                       {
        //                           Alias = c.Alias,
        //                           Name = c.Name
        //                       }).OrderBy(c => c.Alias).ToList();

        //        return caterers;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

       
      
       
    }




}
