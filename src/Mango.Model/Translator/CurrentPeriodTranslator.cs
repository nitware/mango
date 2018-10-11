using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class CurrentPeriodTranslator : TranslatorBase<CurrentPeriod, CURRENT_PERIOD>
    {
        private PeriodTranslator periodTranslator;

        public CurrentPeriodTranslator()
        {
            periodTranslator = new PeriodTranslator();
        }

        public override CurrentPeriod TranslateToModel(CURRENT_PERIOD currentPeriodEntity)
        {
            try
            {
                CurrentPeriod currentPeriod = null;
                if (currentPeriodEntity != null)
                {
                    currentPeriod = new CurrentPeriod();
                    currentPeriod.Period = periodTranslator.Translate(currentPeriodEntity.PERIOD);
                }

                return currentPeriod;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override CURRENT_PERIOD TranslateToEntity(CurrentPeriod currentPeriod)
        {
            try
            {
                CURRENT_PERIOD currentPeriodEntity = null;
                if (currentPeriod != null)
                {
                    currentPeriodEntity = new CURRENT_PERIOD();
                    currentPeriodEntity.Period_ID = currentPeriod.Period.Id;
                }

                return currentPeriodEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }


}
