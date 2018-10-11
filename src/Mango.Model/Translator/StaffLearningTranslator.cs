using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class StaffLearningTranslator : TranslatorBase<StaffLearning, STAFF_LEARNING>
    {
        private StaffTranslator staffTranslator;
        private PeriodTranslator periodTranslator;

        public StaffLearningTranslator()
        {
            staffTranslator = new StaffTranslator();
            periodTranslator = new PeriodTranslator();
        }

        public override StaffLearning TranslateToModel(STAFF_LEARNING entity)
        {
            try
            {
                StaffLearning staffLearning = null;
                if (entity != null)
                {
                    staffLearning = new StaffLearning();
                    staffLearning.Staff = staffTranslator.Translate(entity.STAFF);
                    staffLearning.Period = periodTranslator.Translate(entity.PERIOD);
                    staffLearning.TrainingScore = entity.Training_Score;
                    staffLearning.PercentScore = entity.Percentage_Score;
                }

                return staffLearning;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STAFF_LEARNING TranslateToEntity(StaffLearning staffLearning)
        {
            try
            {
                STAFF_LEARNING entity = null;
                if (staffLearning != null)
                {
                    entity = new STAFF_LEARNING();
                    entity.Staff_ID = staffLearning.Staff.Id;
                    entity.Period_ID = staffLearning.Period.Id;
                    entity.Training_Score = staffLearning.TrainingScore;
                    entity.Percentage_Score = staffLearning.PercentScore;
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
