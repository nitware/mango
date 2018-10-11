//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 22/07/2011 09:11:57

//Description: This Class represents the data tier layer class for Comment table.
//It contains all data access methods and static constants representing the
//Stored Procedures, field names and SQL parameters required by this entity.

//No man can cover the moon with his bare hands. You will shine when the time is ripe.
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;
//using wire.AppraisalDataAccess;
using Mango.Data.Interfaces;
using Mango.DataAccess;

namespace Mango.Data
{
    public class CommentDb : DataAccess.DataAccess, ICommentDb
	{
		private const string CLASS_NAME = "CommentDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  Comment Stored Procedure declaration

		private const string STP_COMMENT_INSERTCOMMENT = "STP_COMMENT_INSERTCOMMENT";
		private const string STP_COMMENT_DELETECOMMENTBYCOMMENT_ID = "STP_COMMENT_DELETECOMMENTBYCOMMENT_ID";
		private const string STP_COMMENT_DELETECOMMENTBYSTAFF_ID = "STP_COMMENT_DELETECOMMENTBYSTAFF_ID";
		private const string STP_COMMENT_DELETECOMMENTBYPERIOD_ID = "STP_COMMENT_DELETECOMMENTBYPERIOD_ID";
		private const string STP_COMMENT_DELETECOMMENTBYRECOMMENDATION_ID = "STP_COMMENT_DELETECOMMENTBYRECOMMENDATION_ID";
		private const string STP_COMMENT_DELETECOMMENTBYOPTION_ID = "STP_COMMENT_DELETECOMMENTBYOPTION_ID";
		private const string STP_COMMENT_DELETECOMMENTBYComment_IDANDStaff_IDANDPeriod_IDANDRecommendation_IDANDOption_ID = "STP_COMMENT_DELETECOMMENTBYComment_IDANDStaff_IDANDPeriod_IDANDRecommendation_IDANDOption_ID";
		private const string STP_COMMENT_UPDATECOMMENTBYCOMMENT_ID = "STP_COMMENT_UPDATECOMMENTBYCOMMENT_ID";
		private const string STP_COMMENT_UPDATECOMMENTBYSTAFF_ID = "STP_COMMENT_UPDATECOMMENTBYSTAFF_ID";
		private const string STP_COMMENT_UPDATECOMMENTBYPERIOD_ID = "STP_COMMENT_UPDATECOMMENTBYPERIOD_ID";
		private const string STP_COMMENT_UPDATECOMMENTBYRECOMMENDATION_ID = "STP_COMMENT_UPDATECOMMENTBYRECOMMENDATION_ID";
		private const string STP_COMMENT_UPDATECOMMENTBYOPTION_ID = "STP_COMMENT_UPDATECOMMENTBYOPTION_ID";
		private const string STP_COMMENT_UPDATECOMMENTBYComment_IDANDStaff_IDANDPeriod_IDANDRecommendation_IDANDOption_ID = "STP_COMMENT_UPDATECOMMENTBYComment_IDANDStaff_IDANDPeriod_IDANDRecommendation_IDANDOption_ID";
		private const string STP_COMMENT_SELECTALLCOMMENT = "STP_COMMENT_SELECTALLCOMMENT";
		private const string STP_COMMENT_SELECTCOMMENTBYCOMMENT_ID = "STP_COMMENT_SELECTCOMMENTBYCOMMENT_ID";
		private const string STP_COMMENT_SELECTCOMMENTBYSTAFF_ID = "STP_COMMENT_SELECTCOMMENTBYSTAFF_ID";
		private const string STP_COMMENT_SELECTCOMMENTBYPERIOD_ID = "STP_COMMENT_SELECTCOMMENTBYPERIOD_ID";
		private const string STP_COMMENT_SELECTCOMMENTBYRECOMMENDATION_ID = "STP_COMMENT_SELECTCOMMENTBYRECOMMENDATION_ID";
		private const string STP_COMMENT_SELECTCOMMENTBYOPTION_ID = "STP_COMMENT_SELECTCOMMENTBYOPTION_ID";
		private const string STP_COMMENT_SELECTCOMMENTBYComment_IDANDStaff_IDANDPeriod_IDANDRecommendation_IDANDOption_ID = "STP_COMMENT_SELECTCOMMENTBYComment_IDANDStaff_IDANDPeriod_IDANDRecommendation_IDANDOption_ID";

        //private const string STP_COMMENT_SELECTCOMMENTBYSTAFFIDANDPERIODID = "STP_COMMENT_SELECTCOMMENTBYSTAFFIDANDPERIODID";
        private const string STP_COMMENT_SELECTCOMMENTBYAPPRAISAL_HEADER_ID = "STP_COMMENT_SELECTCOMMENTBYAPPRAISAL_HEADER_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region Comment Parameter declaration 

        ////Parameter decleration for COMMENT_ID
        //private const string PARAM_COMMENT_ID_NAME = "@CommentID";
        //private const SqlDbType PARAM_COMMENT_ID_TYPE = SqlDbType.Int;
        //private const int PARAM_COMMENT_ID_SIZE = 4;

        //Parameter decleration for APPRAISAL_HEADER_ID
        private const string PARAM_APPRAISAL_HEADER_ID_NAME = "@AppraisalHeaderID";
        private const SqlDbType PARAM_APPRAISAL_HEADER_ID_TYPE = SqlDbType.BigInt;
        private const int PARAM_APPRAISAL_HEADER_ID_SIZE = 8;

		//Parameter decleration for RECOMMENDATION_ID
		private const string PARAM_RECOMMENDATION_ID_NAME = "@RecommendationID";
		private const SqlDbType PARAM_RECOMMENDATION_ID_TYPE = SqlDbType.TinyInt;
		private const int PARAM_RECOMMENDATION_ID_SIZE = 1;

		//Parameter decleration for OPTION_ID
		private const string PARAM_OPTION_ID_NAME = "@OptionID";
		private const SqlDbType PARAM_OPTION_ID_TYPE = SqlDbType.TinyInt;
		private const int PARAM_OPTION_ID_SIZE = 1;

		//Parameter decleration for STRENGTH
		private const string PARAM_STRENGTH_NAME = "@Strength";
		private const SqlDbType PARAM_STRENGTH_TYPE = SqlDbType.VarChar;
		private const int PARAM_STRENGTH_SIZE = 4000;

		//Parameter decleration for WEAKNESS
		private const string PARAM_WEAKNESS_NAME = "@Weakness";
		private const SqlDbType PARAM_WEAKNESS_TYPE = SqlDbType.VarChar;
		private const int PARAM_WEAKNESS_SIZE = 4000;

		//Parameter decleration for TRAINING_NEED
		private const string PARAM_TRAINING_NEED_NAME = "@TrainingNeed";
		private const SqlDbType PARAM_TRAINING_NEED_TYPE = SqlDbType.VarChar;
		private const int PARAM_TRAINING_NEED_SIZE = 4000;

		//Parameter decleration for SUPERVISOR_COMMENT
		private const string PARAM_SUPERVISOR_COMMENT_NAME = "@SupervisorComment";
		private const SqlDbType PARAM_SUPERVISOR_COMMENT_TYPE = SqlDbType.VarChar;
		private const int PARAM_SUPERVISOR_COMMENT_SIZE = 4000;

		//Parameter decleration for APPRAISEE_COMMENT
		private const string PARAM_APPRAISEE_COMMENT_NAME = "@AppraiseeComment";
		private const SqlDbType PARAM_APPRAISEE_COMMENT_TYPE = SqlDbType.VarChar;
		private const int PARAM_APPRAISEE_COMMENT_SIZE = 400;

		//Parameter decleration for HOD_COMMENT
		private const string PARAM_HOD_COMMENT_NAME = "@HodComment";
		private const SqlDbType PARAM_HOD_COMMENT_TYPE = SqlDbType.VarChar;
		private const int PARAM_HOD_COMMENT_SIZE = 4000;

		#endregion

		//==========================================================================================
		//Comment Table Field Name Declaration
		//==========================================================================================
		#region Comment Field Name declaration 

        //public string FIELD_COMMENT_ID { get { return "Comment_ID"; } }
        public string FIELD_APPRAISAL_HEADER_ID { get { return "Appraisal_Header_ID"; } }
		public string FIELD_RECOMMENDATION_ID { get { return "Recommendation_ID"; } }
		public string FIELD_OPTION_ID { get { return "Option_ID"; } }
		public string FIELD_STRENGTH { get { return "Strength"; } }
		public string FIELD_WEAKNESS { get { return "Weakness"; } }
		public string FIELD_TRAINING_NEED { get { return "Training_Need"; } }
		public string FIELD_SUPERVISOR_COMMENT { get { return "Supervisor_Comment"; } }
		public string FIELD_APPRAISEE_COMMENT { get { return "Appraisee_Comment"; } }
		public string FIELD_HOD_COMMENT { get { return "Hod_Comment"; } }

		#endregion

		//Table name declarations for Comment in the database, this will be used for dataset reference
		public string COMMENT_TABLE_NAME  = "COMMENT";

		//==========================================================================================
		//public CommentDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region CommentDb Class Methods 

        public bool InsertComment(long appraisalHeaderId, byte recommendationId, byte optionId, string strength, string weakness, string trainingNeed, string supervisorComment, string appraiseeComment, string hodComment, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertComment";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
				param.Add(MakeParam(PARAM_RECOMMENDATION_ID_NAME, PARAM_RECOMMENDATION_ID_TYPE, PARAM_RECOMMENDATION_ID_SIZE, recommendationId));
				param.Add(MakeParam(PARAM_OPTION_ID_NAME, PARAM_OPTION_ID_TYPE, PARAM_OPTION_ID_SIZE, optionId));
				param.Add(MakeParam(PARAM_STRENGTH_NAME, PARAM_STRENGTH_TYPE, PARAM_STRENGTH_SIZE, strength));
				param.Add(MakeParam(PARAM_WEAKNESS_NAME, PARAM_WEAKNESS_TYPE, PARAM_WEAKNESS_SIZE, weakness));
				param.Add(MakeParam(PARAM_TRAINING_NEED_NAME, PARAM_TRAINING_NEED_TYPE, PARAM_TRAINING_NEED_SIZE, trainingNeed));
				param.Add(MakeParam(PARAM_SUPERVISOR_COMMENT_NAME, PARAM_SUPERVISOR_COMMENT_TYPE, PARAM_SUPERVISOR_COMMENT_SIZE, supervisorComment));
				param.Add(MakeParam(PARAM_APPRAISEE_COMMENT_NAME, PARAM_APPRAISEE_COMMENT_TYPE, PARAM_APPRAISEE_COMMENT_SIZE, appraiseeComment));
				param.Add(MakeParam(PARAM_HOD_COMMENT_NAME, PARAM_HOD_COMMENT_TYPE, PARAM_HOD_COMMENT_SIZE, hodComment));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMMENT_INSERTCOMMENT, param, transaction) == 0)
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
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}


		public bool DeleteCommentByRecommendationId(byte recommendationId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCommentByRecommendationId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_RECOMMENDATION_ID_NAME, PARAM_RECOMMENDATION_ID_TYPE, PARAM_RECOMMENDATION_ID_SIZE, recommendationId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMMENT_DELETECOMMENTBYRECOMMENDATION_ID, param, transaction) == 0)
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
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public bool DeleteCommentByOptionId(byte optionId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteCommentByOptionId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_OPTION_ID_NAME, PARAM_OPTION_ID_TYPE, PARAM_OPTION_ID_SIZE, optionId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_COMMENT_DELETECOMMENTBYOPTION_ID, param, transaction) == 0)
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
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectAllComment()
		{
			//const string METHOD_NAME  = "SelectAllComment";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_COMMENT_SELECTALLCOMMENT, null, COMMENT_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}


		public DataSet SelectCommentByRecommendationId(byte recommendationId)
		{
			//const string METHOD_NAME  = "SelectCommentByRecommendationId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_RECOMMENDATION_ID_NAME, PARAM_RECOMMENDATION_ID_TYPE, PARAM_RECOMMENDATION_ID_SIZE, recommendationId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMMENT_SELECTCOMMENTBYRECOMMENDATION_ID, param, COMMENT_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectCommentByOptionId(byte optionId)
		{
			//const string METHOD_NAME  = "SelectCommentByOptionId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_OPTION_ID_NAME, PARAM_OPTION_ID_TYPE, PARAM_OPTION_ID_SIZE, optionId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_COMMENT_SELECTCOMMENTBYOPTION_ID, param, COMMENT_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

        public DataSet SelectCommentByAppraisalHeaderId(long appraisalHeaderId)
        {
            //const string METHOD_NAME  = "SelectCommentByAppraisalHeaderID";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));

                //Execute Stored Procedure
                return ExecuteDataset(STP_COMMENT_SELECTCOMMENTBYAPPRAISAL_HEADER_ID, param, COMMENT_TABLE_NAME);
            }
            catch (Exception ex)
            {
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }

        public bool UpdateCommentByAppraisalHeaderId(long appraisalHeaderId, byte recommendationId, byte optionId, string strength, string weakness, string trainingNeed, string supervisorComment, string appraiseeComment, string hodComment, Transaction transaction)
        {
            //const string METHOD_NAME  = "UpdateCommentByAppraisalHeaderId";

            try
            {
                //Make parameter(s)
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_APPRAISAL_HEADER_ID_NAME, PARAM_APPRAISAL_HEADER_ID_TYPE, PARAM_APPRAISAL_HEADER_ID_SIZE, appraisalHeaderId));
                param.Add(MakeParam(PARAM_RECOMMENDATION_ID_NAME, PARAM_RECOMMENDATION_ID_TYPE, PARAM_RECOMMENDATION_ID_SIZE, recommendationId));
                param.Add(MakeParam(PARAM_OPTION_ID_NAME, PARAM_OPTION_ID_TYPE, PARAM_OPTION_ID_SIZE, optionId));
                param.Add(MakeParam(PARAM_STRENGTH_NAME, PARAM_STRENGTH_TYPE, PARAM_STRENGTH_SIZE, strength));
                param.Add(MakeParam(PARAM_WEAKNESS_NAME, PARAM_WEAKNESS_TYPE, PARAM_WEAKNESS_SIZE, weakness));
                param.Add(MakeParam(PARAM_TRAINING_NEED_NAME, PARAM_TRAINING_NEED_TYPE, PARAM_TRAINING_NEED_SIZE, trainingNeed));
                param.Add(MakeParam(PARAM_SUPERVISOR_COMMENT_NAME, PARAM_SUPERVISOR_COMMENT_TYPE, PARAM_SUPERVISOR_COMMENT_SIZE, supervisorComment));
                param.Add(MakeParam(PARAM_APPRAISEE_COMMENT_NAME, PARAM_APPRAISEE_COMMENT_TYPE, PARAM_APPRAISEE_COMMENT_SIZE, appraiseeComment));
                param.Add(MakeParam(PARAM_HOD_COMMENT_NAME, PARAM_HOD_COMMENT_TYPE, PARAM_HOD_COMMENT_SIZE, hodComment));

                //Execute Stored Procedure
                if (ExecuteProc(STP_COMMENT_UPDATECOMMENTBYCOMMENT_ID, param, transaction) == 0)
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
                //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
                throw ex;
            }
        }


        //public DataSet SelectCommentByStaffIDAndPeriodID(string staffId, int periodId)
        //{
        //    //const string METHOD_NAME  = "SelectCommentByStaffIDAndPeriodID";

        //    try
        //    {
        //        //Method parameter declaration
        //        ArrayList param = new ArrayList();
        //        param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
        //        param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));

        //        //Execute Stored Procedure
        //        return ExecuteDataset(STP_COMMENT_SELECTCOMMENTBYSTAFFIDANDPERIODID, param, COMMENT_TABLE_NAME);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

        //public DataSet SelectCommentByComment_IDAndStaff_IDAndPeriod_IDAndRecommendation_IDAndOption_ID(int commentId, string staffId, int periodId, byte recommendationId, byte optionId)
        //{
        //    //const string METHOD_NAME  = "SelectCommentByComment_IDAndStaff_IDAndPeriod_IDAndRecommendation_IDAndOption_ID";

        //    try
        //    {
        //        //Method parameter declaration
        //        ArrayList param = new ArrayList();

        //        param.Add(MakeParam(PARAM_COMMENT_ID_NAME, PARAM_COMMENT_ID_TYPE, PARAM_COMMENT_ID_SIZE, commentId));
        //        param.Add(MakeParam(PARAM_STAFF_ID_NAME, PARAM_STAFF_ID_TYPE, PARAM_STAFF_ID_SIZE, staffId));
        //        param.Add(MakeParam(PARAM_PERIOD_ID_NAME, PARAM_PERIOD_ID_TYPE, PARAM_PERIOD_ID_SIZE, periodId));
        //        param.Add(MakeParam(PARAM_RECOMMENDATION_ID_NAME, PARAM_RECOMMENDATION_ID_TYPE, PARAM_RECOMMENDATION_ID_SIZE, recommendationId));
        //        param.Add(MakeParam(PARAM_OPTION_ID_NAME, PARAM_OPTION_ID_TYPE, PARAM_OPTION_ID_SIZE, optionId));

        //        //Execute Stored Procedure
        //        return ExecuteDataset(STP_COMMENT_SELECTCOMMENTBYComment_IDANDStaff_IDANDPeriod_IDANDRecommendation_IDANDOption_ID, param, COMMENT_TABLE_NAME);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
        //        throw ex;
        //    }
        //}

		#endregion

	}
}


