using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;

namespace Mango.Model.Translator
{
    public class LocationTranslator : TranslatorBase<Location, LOCATION>
    {
        public override Location TranslateToModel(LOCATION entity)
        {
            try
            {
                Location location = null;
                if (entity != null)
                {
                    location = new Location();
                    location.Id = entity.Location_ID;
                    location.Name = entity.Location_Name;
                }

                return location;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override LOCATION TranslateToEntity(Location location)
        {
            try
            {
                LOCATION entity = null;
                if (location != null)
                {
                    entity = new LOCATION();
                    entity.Location_ID = location.Id;
                    entity.Location_Name = location.Name;
                }

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
