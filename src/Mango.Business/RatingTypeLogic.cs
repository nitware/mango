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
    public class RatingTypeLogic : BusinessLogicBase<RatingType, RATING_TYPE>
    {
        public RatingTypeLogic()
        {
            base.translator = new RatingTypeTranslator();
        }

        public bool Modify(RatingType ratingType)
        {
            try
            {
                Func<RATING_TYPE, bool> selector = r => r.Rating_Type_ID == ratingType.Id;
                RATING_TYPE entity = GetEntityBy(selector);

                entity.Rating_Type_ID = ratingType.Id;
                entity.Rating_Name = ratingType.Name;

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

        public bool Remove(RatingType ratingType)
        {
            try
            {
                Func<RATING_TYPE, bool> selector = r => r.Rating_Type_ID == ratingType.Id;
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
