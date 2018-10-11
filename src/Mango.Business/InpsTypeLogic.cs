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
    public class InpsTypeLogic : BusinessLogicBase<InpsType, INPS_TYPE>
    {
        public InpsTypeLogic()
        {
            base.translator = new InpsTypeTranslator();
        }

        public bool Modify(InpsType inpsType)
        {
            try
            {
                Func<INPS_TYPE, bool> selector = c => c.Inps_Type_Id == inpsType.Id;
                INPS_TYPE entity = GetEntityBy(selector);

                entity.Inps_Type_Name = inpsType.Name;
                entity.Inps_Type_Description = inpsType.Description;
              
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

        public bool Remove(InpsType inpsType)
        {
            try
            {
                Func<INPS_TYPE, bool> selector = c => c.Inps_Type_Id == inpsType.Id;
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
