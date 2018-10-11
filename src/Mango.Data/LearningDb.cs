using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Collections;
using Mango.Data.Interfaces;

namespace Mango.Data
{
    public class LearningDb : DataAccess.DataAccess, ILearningDb
    {
        private const string CLASS_NAME = "LearningDb";

        //==========================================================================================
        //Db Stored Procedures declaration
        //==========================================================================================
        #region  Company Stored Procedure declaration

        private const string STP_STAFF_LEARNING_BY_STAFF_IDANDPERIOD_ID = "STP_STAFF_LEARNING_BY_STAFF_IDANDPERIOD_ID";
      
        #endregion

        //==========================================================================================
        //Db Configuration properties
        //==========================================================================================
        #region Company Parameter declaration

        //Parameter decleration for STAFF_ID
        private const string PARAM_STAFF_ID_NAME = "@StaffID";
        private const SqlDbType PARAM_STAFF_ID_TYPE = SqlDbType.NChar;
        private const int PARAM_STAFF_ID_SIZE = 10;

        //Parameter decleration for PERIOD_ID
        public const string PARAM_PERIOD_ID_NAME = "@PeriodID";
        public const SqlDbType PARAM_PERIOD_ID_TYPE = SqlDbType.Int;
        public const int PARAM_PERIOD_ID_SIZE = 4;

        //Parameter decleration for TRAINING_SCORE
        private const string PARAM_TRAINING_SCORE_NAME = "@TrainigScore";
        private const SqlDbType PARAM_TRAINING_SCORE_TYPE = SqlDbType.Decimal;
        private const int PARAM_TRAINING_SCORE_SIZE = 8;

        //Parameter decleration for PERCENTAGE_SCORE
        private const string PARAM_PERCENTAGE_SCORE_NAME = "@PercentageScore";
        private const SqlDbType PARAM_PERCENTAGE_SCORE_TYPE = SqlDbType.Decimal;
        private const int PARAM_PERCENTAGE_SCORE_SIZE = 8;

        #endregion

        //==========================================================================================
        //Company Table Field Name Declaration
        //==========================================================================================
        #region Company Field Name declaration

        public string FIELD_STAFF_ID { get { return "Staff_ID"; } }
        public string FIELD_PERIOD_ID { get { return "Period_ID"; } }
        public string FIELD_TRAINING_SCORE { get { return "Training_Score"; } }
        public string FIELD_PERCENTAGE_SCORE { get { return "Percentage_Score"; } }

        #endregion

        //Table name declarations for Company in the database, this will be used for dataset reference
        public string STAFF_LEARNING_TABLE_NAME = "STAFF_LEARNING";

        //==========================================================================================
        //public CompanyDb Class Method declarations that will be called from the Biz Tier
        //==========================================================================================
        #region LearningDb Class Methods

        public DataSet SelectStaffLearningByStaffIdAndPeriodId(string staffId, int periodId)
        {
            //const string METHOD_NAME  = "SelectStaffLearningByStaffIdAndPeriodId";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
                param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_STAFF_LEARNING_BY_STAFF_IDANDPERIOD_ID, param, STAFF_LEARNING_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        #endregion
    }


}
