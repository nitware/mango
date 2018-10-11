using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class AssessmentLevelTranslator : TranslatorBase<AssessmentLevel, POTENTIAL_ASSESSMENT_JOB_ROLE_LEVEL>
    {
        private LevelTranslator levelTranslator;
        private PeriodTranslator periodTranslator;

        public AssessmentLevelTranslator()
        {
            levelTranslator = new LevelTranslator();
            periodTranslator = new PeriodTranslator();
        }

        public override AssessmentLevel TranslateToModel(POTENTIAL_ASSESSMENT_JOB_ROLE_LEVEL entity)
        {
            try
            {
                AssessmentLevel model = null;
                if (entity != null)
                {
                    model = new AssessmentLevel();
                    model.Id = entity.Potential_Assessment_Job_Role_Level_Id;
                    model.Level = levelTranslator.Translate(entity.JOB_ROLE_LEVEL);
                    model.Period = periodTranslator.Translate(entity.PERIOD);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override POTENTIAL_ASSESSMENT_JOB_ROLE_LEVEL TranslateToEntity(AssessmentLevel model)
        {
            try
            {
                POTENTIAL_ASSESSMENT_JOB_ROLE_LEVEL entity = null;
                if (model != null)
                {
                    entity = new POTENTIAL_ASSESSMENT_JOB_ROLE_LEVEL();
                    entity.Potential_Assessment_Job_Role_Level_Id = model.Id;
                    entity.Job_Role_Level_Id = model.Level.Id;
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
