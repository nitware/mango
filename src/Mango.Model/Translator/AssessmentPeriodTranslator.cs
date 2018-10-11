using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class AssessmentPeriodTranslator : TranslatorBase<AssessmentPeriod, POTENTIAL_ASSESSMENT_PERIOD>
    {
        private PeriodTranslator periodTranslator;
        private AssessmentTranslator assessmentTranslator;

        public AssessmentPeriodTranslator()
        {
            periodTranslator = new PeriodTranslator();
            assessmentTranslator = new AssessmentTranslator();
        }

        public override AssessmentPeriod TranslateToModel(POTENTIAL_ASSESSMENT_PERIOD entity)
        {
            try
            {
                AssessmentPeriod model = null;
                if (entity != null)
                {
                    model = new AssessmentPeriod();
                    model.Id = entity.Potential_Assessment_Id;
                    model.Assessment = assessmentTranslator.Translate(entity.POTENTIAL_ASSESSMENT);
                    model.Period = periodTranslator.Translate(entity.PERIOD);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override POTENTIAL_ASSESSMENT_PERIOD TranslateToEntity(AssessmentPeriod model)
        {
            try
            {
                POTENTIAL_ASSESSMENT_PERIOD entity = null;
                if (model != null)
                {
                    entity = new POTENTIAL_ASSESSMENT_PERIOD();
                    entity.Potential_Assessment_Period_Id = model.Id;
                    entity.Potential_Assessment_Id = model.Assessment.Id ;
                    entity.Period_Id = model.Period.Id;
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
