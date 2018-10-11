using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Mango.Data;
using Mango.Model;
using Mango.Data.Interfaces;

namespace Mango.Business.DbFacade
{
    public class LearningDbFacade
    {
        private ILearningDb learningDb;

        public LearningDbFacade()
        {
            learningDb = new LearningDb();
        }

        public Learning GetByStaffAndPeriod(string staffId, int periodId)
        {
            try
            {
                DataSet dsLearning = learningDb.SelectStaffLearningByStaffIdAndPeriodId(staffId, periodId);
                if (dsLearning != null)
                {
                    if (dsLearning.Tables[0].Rows.Count > 0)
                    {
                        Learning learning = new Learning();
                        learning.StaffId = Convert.ToString(dsLearning.Tables[0].Rows[0][learningDb.FIELD_STAFF_ID]);
                        learning.PeriodId = Convert.ToInt32(dsLearning.Tables[0].Rows[0][learningDb.FIELD_PERIOD_ID]);
                        learning.TrainingScore = Convert.ToDecimal(dsLearning.Tables[0].Rows[0][learningDb.FIELD_TRAINING_SCORE]);
                        learning.PercentageScore = Convert.ToDecimal(dsLearning.Tables[0].Rows[0][learningDb.FIELD_PERCENTAGE_SCORE]);

                        return learning;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }



}