using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class AssessmentTranslator : TranslatorBase<Assessment, POTENTIAL_ASSESSMENT>
    {
        public override Assessment TranslateToModel(POTENTIAL_ASSESSMENT entity)
        {
            try
            {
                Assessment assessment = null;
                if (entity != null)
                {
                    assessment = new Assessment();
                    assessment.Id = entity.Potential_Assessment_Id;
                    assessment.Indicator = entity.Potential_Indicator;
                    assessment.Skill = entity.Skill;
                }

                return assessment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override POTENTIAL_ASSESSMENT TranslateToEntity(Assessment assessment)
        {
            try
            {
                POTENTIAL_ASSESSMENT entity = null;
                if (assessment != null)
                {
                    entity = new POTENTIAL_ASSESSMENT();
                    entity.Potential_Assessment_Id = assessment.Id;
                    entity.Potential_Indicator = assessment.Indicator;
                    entity.Skill = assessment.Skill;
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
