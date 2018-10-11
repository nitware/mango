using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using System.Data;

namespace Mango.Business
{
    public class PeriodTypeLogic : BusinessLogicBase<PeriodType, PERIOD_TYPE>
    {
        public PeriodTypeLogic()
        {
            translator = new PeriodTypeTranslator();
        }

        public bool Modify(PeriodType periodType)
        {
            try
            {
                Func<PERIOD_TYPE, bool> predicate = pt => pt.Period_Type_Id == periodType.Id;
                PERIOD_TYPE entity = GetEntityBy(predicate);

                entity.Period_Type_Name = periodType.Name;
                entity.Period_Type_Description = periodType.Description;

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

        public bool Remove(PeriodType periodType)
        {
            try
            {
                Func<PERIOD_TYPE, bool> selector = p => p.Period_Type_Id == periodType.Id;
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
