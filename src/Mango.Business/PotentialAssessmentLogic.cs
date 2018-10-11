using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using Mango.Model;

namespace Mango.Business
{
    public class PotentialAssessmentLogic : BusinessLogicBase<Assessment, POTENTIAL_ASSESSMENT>
    {
        private AssessmentPeriodLogic assessmentPeriodLogic;
        private StaffAssessmentLogic staffAssessmentLogic;

        public PotentialAssessmentLogic()
        {
            base.translator = new AssessmentTranslator();
            assessmentPeriodLogic = new AssessmentPeriodLogic();
            staffAssessmentLogic = new StaffAssessmentLogic();
        }

        public List<StaffAssessment> GetDefault(Period period, bool enable)
        {
            try
            {
                List<StaffAssessment> staffAssessments = new List<StaffAssessment>();
                List<AssessmentPeriod> assessmentPeriods = assessmentPeriodLogic.GetBy(period);
                if (assessmentPeriods != null && assessmentPeriods.Count > 0)
                {
                    foreach (AssessmentPeriod assessmentPeriod in assessmentPeriods)
                    {
                        StaffAssessment staffAssessment = new StaffAssessment();
                        staffAssessment.Period = new AssessmentPeriod();
                        staffAssessment.Period = assessmentPeriod;
                        staffAssessment.Enable = enable;

                        //staffAssessment.Period.Assessment.Indicator = UtilityLogic.JumbbleText(staffAssessment.Period.Assessment.Indicator);

                        staffAssessments.Add(staffAssessment);
                    }
                }

                return staffAssessments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<StaffAssessment> GetBy(Appraisal appraisal, bool enable)
        {
            try
            {
                List<StaffAssessment> staffAssessments = staffAssessmentLogic.GetBy(appraisal);
                
                if (staffAssessments != null && staffAssessments.Count > 0)
                {
                    foreach (StaffAssessment staffAssessment in staffAssessments)
                    {
                        staffAssessment.Enable = enable;
                    }
                }

                return staffAssessments;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }



}
