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
    public class AssessmentLevelLogic : BusinessLogicBase<AssessmentLevel, POTENTIAL_ASSESSMENT_JOB_ROLE_LEVEL>
    {
        public AssessmentLevelLogic()
        {
            base.translator = new AssessmentLevelTranslator();
        }

        public AssessmentLevel GetBy(Period period, Level level)
        {
            try
            {
                Func<POTENTIAL_ASSESSMENT_JOB_ROLE_LEVEL, bool> selector = al => al.Job_Role_Level_Id == level.Id && al.Period_Id == period.Id;
                return base.GetModelBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsLevelExist(Period period, Level level)
        {
            try
            {
                bool levelExist = false;
                AssessmentLevel assessmentLevel = GetBy(period, level);
                if (assessmentLevel != null && assessmentLevel.Id > 0)
                {
                    levelExist = true;
                }

                return levelExist;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }





}
