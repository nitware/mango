using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using Mango.Model;

namespace Mango.Business
{
    public class InpsRatingLogic : BusinessLogicBase<InpsRating, INPS_RATING>
    {
        public InpsRatingLogic()
        {
            base.translator = new InpsRatingTranslator();
        }

        public int GetBy(Period period)
        {
            try
            {
                Func<INPS_RATING, bool> selector = nr => nr.Period_ID == period.Id;
                List<InpsRating> npsRatings = base.GetModelsBy(selector);

                return npsRatings.Max(nr => nr.Rating.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<InpsRating> GetAllBy(Period period)
        {
            try
            {
                Func<INPS_RATING, bool> selector = nr => nr.Period_ID == period.Id;
                return base.GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<InpsRating> GetBy(Period period, InpsType inpsType)
        {
            try
            {
                Func<INPS_RATING, bool> selector = nr => nr.Period_ID == period.Id && nr.Inps_Type_Id == inpsType.Id;
                return base.GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(List<InpsRating> inpsRatings)
        {
            try
            {
                int added = 0;
                if (inpsRatings != null && inpsRatings.Count > 0)
                {
                    bool removed = Remove(inpsRatings);
                    if (removed)
                    {
                        added = base.Add(inpsRatings);
                    }
                }
              
                return added > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(List<InpsRating> inpsRatings)
        {
            try
            {
                Func<INPS_RATING, bool> selector = d => d.Period_ID == inpsRatings[0].Period.Id;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(Period period)
        {
            try
            {
                Func<INPS_RATING, bool> selector = d => d.Period_ID == period.Id;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }






    }

}
