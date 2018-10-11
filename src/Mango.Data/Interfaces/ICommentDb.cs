using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.DataAccess;
using System.Data;

namespace Mango.Data.Interfaces
{
    public interface ICommentDb
    {
        //string FIELD_COMMENT_ID { get; }
        string FIELD_APPRAISAL_HEADER_ID { get; }
        string FIELD_RECOMMENDATION_ID { get; }
        string FIELD_OPTION_ID { get; }
        string FIELD_STRENGTH { get; }
        string FIELD_WEAKNESS { get; }
        string FIELD_TRAINING_NEED { get; }
        string FIELD_SUPERVISOR_COMMENT { get; }
        string FIELD_APPRAISEE_COMMENT { get; }
        string FIELD_HOD_COMMENT { get; }

        bool InsertComment(long appraisalHeaderId, byte recommendationId, byte optionId, string strength, string weakness, string trainingNeed, string supervisorComment, string appraiseeComment, string hodComment, Transaction transaction);
      
        bool DeleteCommentByRecommendationId(byte recommendationId, Transaction transaction);
        bool DeleteCommentByOptionId(byte optionId, Transaction transaction);
        DataSet SelectAllComment();
       
        DataSet SelectCommentByRecommendationId(byte recommendationId);
        DataSet SelectCommentByOptionId(byte optionId);
        DataSet SelectCommentByAppraisalHeaderId(long appraisalHeaderId);

        bool UpdateCommentByAppraisalHeaderId(long appraisalHeaderId, byte recommendationId, byte optionId, string strength, string weakness, string trainingNeed, string supervisorComment, string appraiseeComment, string hodComment, Transaction transaction);

    }




}
