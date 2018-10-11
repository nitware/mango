using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class StaffAssessmentTranslator : TranslatorBase<StaffAssessment, STAFF_POTENTIAL_ASSESSMENT>
    {
        private AppraisalTranslator appraisalTranslator;
        private AssessmentPeriodTranslator assessmentPeriodTranslator;

        public StaffAssessmentTranslator()
        {
            appraisalTranslator = new AppraisalTranslator();
            assessmentPeriodTranslator = new AssessmentPeriodTranslator();
        }

        public override StaffAssessment TranslateToModel(STAFF_POTENTIAL_ASSESSMENT entity)
        {
            try
            {
                StaffAssessment model = null;
                if (entity != null)
                {
                    model = new StaffAssessment();
                    model.Id = entity.Staff_Potential_Assessment_Id;
                    model.Appraisal = appraisalTranslator.Translate(entity.APPRAISAL_HEADER);
                    model.Period = assessmentPeriodTranslator.Translate(entity.POTENTIAL_ASSESSMENT_PERIOD);
                    model.Score = entity.Score;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STAFF_POTENTIAL_ASSESSMENT TranslateToEntity(StaffAssessment model)
        {
            try
            {
                STAFF_POTENTIAL_ASSESSMENT entity = null;
                if (model != null)
                {
                    entity = new STAFF_POTENTIAL_ASSESSMENT();
                    entity.Staff_Potential_Assessment_Id = model.Id;
                    entity.Appraisal_Header_ID = model.Appraisal.Id;
                    entity.Potential_Assessment_Period_Id = model.Period.Id;
                    entity.Score = model.Score;
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
