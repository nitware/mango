using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;
using System.Data;

namespace Mango.Business
{
    public class RatingLogic : BusinessLogicBase<Rating, RATING>
    {
        public RatingLogic()
        {
            base.translator = new RatingTranslator();
        }

        public bool Modify(Rating rating)
        {
            try
            {
                Func<RATING, bool> predicate = r => r.Rating_ID == rating.Id;
                RATING entity = GetEntityBy(predicate);

                entity.Rating_ID = rating.Id;
                entity.Rating_Name = rating.Name;

                int rowsAffected = repository.SaveChanges();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception(NoItemModified);
                }
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(ArgumentNullException);
            }
            catch (UpdateException)
            {
                throw new UpdateException(UpdateException);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(Rating rating)
        {
            try
            {
                Func<RATING, bool> selector = r => r.Rating_ID == rating.Id;
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
