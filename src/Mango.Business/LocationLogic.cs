using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Model.Translator;
using Mango.Data;
using Mango.Model;
using System.Data;

namespace Mango.Business
{
    public class LocationLogic : BusinessLogicBase<Location, LOCATION>
    {
        public LocationLogic()
        {
            base.translator = new LocationTranslator();
        }

        public bool Modify(Location location)
        {
            try
            {
                Func<LOCATION, bool> selector = l => l.Location_ID == location.Id;
                LOCATION entity = GetEntityBy(selector);

                entity.Location_Name = location.Name;

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

        public bool Remove(Location location)
        {
            try
            {
                Func<LOCATION, bool> selector = l => l.Location_ID == location.Id;
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
