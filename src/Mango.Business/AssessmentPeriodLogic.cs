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
    public class AssessmentPeriodLogic : BusinessLogicBase<AssessmentPeriod, POTENTIAL_ASSESSMENT_PERIOD>
    {
        public AssessmentPeriodLogic()
        {
            base.translator = new AssessmentPeriodTranslator();
        }

        public List<AssessmentPeriod> GetBy(Period period)
        {
            try
            {
                Func<POTENTIAL_ASSESSMENT_PERIOD, bool> selector = pap => pap.Period_Id == period.Id;
                return base.GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }



    }



}
