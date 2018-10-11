using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mango.DataAccess;

namespace Mango.Data.Interfaces
{
    public interface IStaffPotentialAssessmentDb
    {
        string FIELD_STAFF_POTENTIAL_ASSESSMENT_ID { get; }
        string FIELD_APPRAISAL_HEADER_ID { get; }
        string FIELD_POTENTIAL_ASSESSMENT_PERIOD_ID { get; }
        string FIELD_SCORE { get; }
        
        bool InsertStaffPotentialAssessment(long appraisalHeaderId, int potentialAssessmentPeriodId, short score, Transaction transaction);
        bool UpdateStaffPotentialAssessmentByAppraisalHeaderId(long staffPotentialAssessmentId, long appraisalHeaderId, int potentialAssessmentPeriodId, short score, Transaction transaction);
    }
}
