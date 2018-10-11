using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using Mango.Model;
using System.Data;

namespace Mango.Business
{
    public class StaffAssessmentLogic : BusinessLogicBase<StaffAssessment, STAFF_POTENTIAL_ASSESSMENT>
    {
        public StaffAssessmentLogic()
        {
            base.translator = new StaffAssessmentTranslator();
        }

        public List<StaffAssessment> GetBy(Appraisal appraisal)
        {
            try
            {
                Func<STAFF_POTENTIAL_ASSESSMENT, bool> selector = sps => sps.Appraisal_Header_ID == appraisal.Id;
                return base.GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(List<StaffAssessment> assessments)
        {
            try
            {
                if (assessments != null && assessments.Count > 0)
                {
                    foreach (StaffAssessment assessment in assessments)
                    {
                        Func<STAFF_POTENTIAL_ASSESSMENT, bool> predicate = spa => spa.Staff_Potential_Assessment_Id == assessment.Id;
                        STAFF_POTENTIAL_ASSESSMENT entity = GetEntityBy(predicate);

                        entity.Appraisal_Header_ID = assessment.Appraisal.Id;
                        entity.Potential_Assessment_Period_Id = assessment.Period.Id;
                        entity.Score = assessment.Score;
                    }

                    int rowsAffected = repository.SaveChanges();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception(NoItemModified);
                    }
                }
                else
                {
                    throw new Exception("Object to modify not set! Please contact your system administrator.");
                }
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(ArgumentNullException);
            }
            catch (UpdateException)
            {
                throw new UpdateException(UpdateException);
            }
            catch (Exception)
            {
                throw;
            }
        }




    }



}
