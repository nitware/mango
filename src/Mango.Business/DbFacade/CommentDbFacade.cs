using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data;
using Mango.Data;
using Mango.Model;
using Mango.Data.Interfaces;
using Mango.DataAccess;


namespace Mango.Business.DbFacade
{
    public class CommentDbFacade
    {
         private ICommentDb commentDb;

         public CommentDbFacade()
         {
             commentDb = new CommentDb();
         }

         public Comment Load(long appraisalHeaderId)
         {
             try
             {
                 Comment comment = new Comment();
                 DataSet dsComment = commentDb.SelectCommentByAppraisalHeaderId(appraisalHeaderId);
                 
                 if (dsComment != null)
                 {
                     if (dsComment.Tables[0].Rows.Count > 0)
                     {
                         comment.AppraisalHeaderId = Convert.ToInt32(dsComment.Tables[0].Rows[0][commentDb.FIELD_APPRAISAL_HEADER_ID]);
                         comment.RecommendationId = Convert.ToByte(dsComment.Tables[0].Rows[0][commentDb.FIELD_RECOMMENDATION_ID]);
                         comment.OptionId = Convert.ToByte(dsComment.Tables[0].Rows[0][commentDb.FIELD_OPTION_ID]);
                         comment.Strenght = Convert.ToString(dsComment.Tables[0].Rows[0][commentDb.FIELD_STRENGTH]);
                         comment.Weakness = Convert.ToString(dsComment.Tables[0].Rows[0][commentDb.FIELD_WEAKNESS]);
                         comment.TrainingNeed = Convert.ToString(dsComment.Tables[0].Rows[0][commentDb.FIELD_TRAINING_NEED]);
                         comment.SupervisorComment = Convert.ToString(dsComment.Tables[0].Rows[0][commentDb.FIELD_SUPERVISOR_COMMENT]);
                         comment.AppraiseeComment = Convert.ToString(dsComment.Tables[0].Rows[0][commentDb.FIELD_APPRAISEE_COMMENT]);
                         comment.HodComment = Convert.ToString(dsComment.Tables[0].Rows[0][commentDb.FIELD_HOD_COMMENT]);
                     }
                 }

                 return comment;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public bool CreateComment(Comment comment, Transaction transaction)
         {
             try
             {
                 if (comment != null)
                 {
                     return commentDb.InsertComment(comment.AppraisalHeaderId, comment.RecommendationId, comment.OptionId, comment.Strenght, comment.Weakness, comment.TrainingNeed, comment.SupervisorComment, comment.AppraiseeComment, comment.HodComment, transaction);
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }

             return false;
         }

         public bool ModifyComment(Comment comment, Transaction transaction)
         {
             try
             {
                 if (comment != null)
                 {
                     return commentDb.UpdateCommentByAppraisalHeaderId(comment.AppraisalHeaderId, comment.RecommendationId, comment.OptionId, comment.Strenght, comment.Weakness, comment.TrainingNeed, comment.SupervisorComment, comment.AppraiseeComment, comment.HodComment, transaction);
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }

             return false;
         }






    }
}