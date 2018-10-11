using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;

namespace Mango.Business
{
    public class RightLogic : BusinessLogicBase<Right, RIGHT>
    {
        public RightLogic()
        {
            base.translator = new RightTranslator();
        }

        public bool Modify(Right right)
        {
            try
            {
                Func<RIGHT, bool> selector = r => r.Right_Id == right.Id;
                RIGHT rightEntity = GetEntityBy(selector);
                rightEntity.Right_Name = right.Name;
                rightEntity.Right_Description = right.Description;

                int rowsAffected = repository.SaveChanges();

                //int rowsAffected = base.ContextManager.SummitChanges();
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

        public bool Remove(Right right)
        {
            try
            {
                Func<RIGHT, bool> selector = r => r.Right_Id == right.Id;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();

                //base.ContextManager.SummitChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }

       


    }


}
