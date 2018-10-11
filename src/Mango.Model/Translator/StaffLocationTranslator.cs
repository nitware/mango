using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class StaffLocationTranslator : TranslatorBase<StaffLocation, STAFF_LOCATION>
    {
        private StaffTranslator staffTranslator;
        private LocationTranslator locationTranslator;
        private PeriodTranslator periodTranslator;

        public StaffLocationTranslator()
        {
            staffTranslator = new StaffTranslator();
            locationTranslator = new LocationTranslator();
            periodTranslator = new PeriodTranslator();
        }

        public override StaffLocation TranslateToModel(STAFF_LOCATION entity)
        {
            try
            {
                StaffLocation staffLocation = null;
                if (entity != null)
                {
                    staffLocation = new StaffLocation();
                    staffLocation.Staff = staffTranslator.Translate(entity.STAFF);
                    staffLocation.Location = locationTranslator.Translate(entity.LOCATION);
                    staffLocation.Period = periodTranslator.Translate(entity.PERIOD);
                }

                return staffLocation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STAFF_LOCATION TranslateToEntity(StaffLocation staffLocation)
        {
            try
            {
                STAFF_LOCATION entity = null;
                if (staffLocation != null)
                {
                    entity = new STAFF_LOCATION();
                    entity.Staff_ID = staffLocation.Staff.Id;
                    entity.Location_ID = staffLocation.Location.Id;
                    entity.Period_ID = staffLocation.Period.Id;
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
