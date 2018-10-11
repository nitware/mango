using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class StaffLevelTranslator : TranslatorBase<StaffLevel, STAFF_JOB_ROLE_LEVEL>
    {
        private StaffTranslator staffTranslator;
        private LevelTranslator levelTranslator;
        private PeriodTranslator periodTranslator;

        public StaffLevelTranslator()
        {
            staffTranslator = new StaffTranslator();
            levelTranslator = new LevelTranslator();
            periodTranslator = new PeriodTranslator();
        }

        public override StaffLevel TranslateToModel(STAFF_JOB_ROLE_LEVEL entity)
        {
            try
            {
                StaffLevel staffLevel = null;
                if (entity != null)
                {
                    staffLevel = new StaffLevel();
                    staffLevel.Staff = staffTranslator.Translate(entity.STAFF);
                    staffLevel.Level = levelTranslator.Translate(entity.JOB_ROLE_LEVEL);
                    staffLevel.Period = periodTranslator.Translate(entity.PERIOD);
                }

                return staffLevel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STAFF_JOB_ROLE_LEVEL TranslateToEntity(StaffLevel staffLevel)
        {
            try
            {
                STAFF_JOB_ROLE_LEVEL entity = null;
                if (staffLevel != null)
                {
                    entity = new STAFF_JOB_ROLE_LEVEL();
                    entity.Staff_ID = staffLevel.Staff.Id;
                    entity.Job_Role_Level_ID = staffLevel.Level.Id;
                    entity.Period_ID = staffLevel.Period.Id;
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
