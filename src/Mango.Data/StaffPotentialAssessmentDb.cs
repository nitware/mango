using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data.Interfaces;
using System.Data;
using System.Collections;

namespace Mango.Data
{
    public class StaffPotentialAssessmentDb : DataAccess.DataAccess, IStaffPotentialAssessmentDb
    {
        private const string STP_STAFF_POTENTIAL_ASSESSMENT_INSERTSTAFF_POTENTIAL_ASSESSMENT = "STP_STAFF_POTENTIAL_ASSESSMENT_INSERTSTAFF_POTENTIAL_ASSESSMENT";
        private const string STP_STAFF_POTENTIAL_ASSESSMENT_UPDATESTAFF_POTENTIAL_ASSESSMENTBYSTAFF_POTENTIAL_ASSESSMENT_ID = "STP_STAFF_POTENTIAL_ASSESSMENT_UPDATESTAFF_POTENTIAL_ASSESSMENTBYSTAFF_POTENTIAL_ASSESSMENT_ID";


        //Parameter decleration for STAFF_POTENTIAL_ASSESSMENT_ID
        private const string PARAM_STAFF_POTENTIAL_ASSESSMENT_ID_NAME = "@Staff_Potential_Assessment_Id";
        private const SqlDbType PARAM_STAFF_POTENTIAL_ASSESSMENT_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_STAFF_POTENTIAL_ASSESSMENT_ID_SIZE = 8;

        //Parameter decleration for APPRAISAL_HEADER_ID
        private const string PARAM_APPRAISAL_HEADER_ID_NAME = "@Appraisal_Header_ID";
        private const SqlDbType PARAM_APPRAISAL_HEADER_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_APPRAISAL_HEADER_ID_SIZE = 8;

        //Parameter decleration for POTENTIAL_ASSESSMENT_PERIOD_ID
        private const string PARAM_POTENTIAL_ASSESSMENT_PERIOD_ID_NAME = "@Potential_Assessment_Period_Id";
        private const SqlDbType PARAM_POTENTIAL_ASSESSMENT_PERIOD_ID_TYPE = SqlDbType.Int;
        private const int PARAM_POTENTIAL_ASSESSMENT_PERIOD_ID_SIZE = 4;

        //Parameter decleration for SCORE
        private const string PARAM_SCORE_NAME = "@Score";
        private const SqlDbType PARAM_SCORE_TYPE = SqlDbType.SmallInt;
        private const int PARAM_SCORE_SIZE = 2;

        public string FIELD_STAFF_POTENTIAL_ASSESSMENT_ID { get { return "Staff_Potential_Assessment_ID"; } }
        public string FIELD_APPRAISAL_HEADER_ID { get { return "Appraisal_Header_ID"; } }
        public string FIELD_POTENTIAL_ASSESSMENT_PERIOD_ID { get { return "Potential_Assessment_Period_ID"; } }
        public string FIELD_SCORE { get { return "Score"; } }
        
        public bool InsertStaffPotentialAssessment(long appraisalHeaderId, int potentialAssessmentPeriodId, short score, DataAccess.Transaction transaction)
        {
            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_POTENTIAL_ASSESSMENT_PERIOD_ID_NAME, PARAM_POTENTIAL_ASSESSMENT_PERIOD_ID_TYPE, PARAM_POTENTIAL_ASSESSMENT_PERIOD_ID_SIZE, potentialAssessmentPeriodId));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));

                //return Convert.ToInt32(ExecuteProcWithOutputParam(STP_STAFF_POTENTIAL_ASSESSMENT_UPDATESTAFF_POTENTIAL_ASSESSMENTBYSTAFF_POTENTIAL_ASSESSMENT_ID, param, transaction));

                //Execute Stored Procedure
                if (ExecuteProc(STP_STAFF_POTENTIAL_ASSESSMENT_INSERTSTAFF_POTENTIAL_ASSESSMENT, param, transaction) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateStaffPotentialAssessmentByAppraisalHeaderId(long staffPotentialAssessmentId, long appraisalHeaderId, int potentialAssessmentPeriodId, short score, DataAccess.Transaction transaction)
        {
            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_POTENTIAL_ASSESSMENT_ID_NAME, PARAM_STAFF_POTENTIAL_ASSESSMENT_ID_TYPE, PARAM_STAFF_POTENTIAL_ASSESSMENT_ID_SIZE, staffPotentialAssessmentId));
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_POTENTIAL_ASSESSMENT_PERIOD_ID_NAME, PARAM_POTENTIAL_ASSESSMENT_PERIOD_ID_TYPE, PARAM_POTENTIAL_ASSESSMENT_PERIOD_ID_SIZE, potentialAssessmentPeriodId));
                param.Add(MakeParam(PARAM_SCORE_NAME, PARAM_SCORE_TYPE, PARAM_SCORE_SIZE, score));

                //Execute Stored Procedure
                if (ExecuteProc(STP_STAFF_POTENTIAL_ASSESSMENT_UPDATESTAFF_POTENTIAL_ASSESSMENTBYSTAFF_POTENTIAL_ASSESSMENT_ID, param, transaction) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



}
