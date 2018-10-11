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
    public class AppraisalHeaderDbFacade
    {
         private IAppraisalHeaderDb appraisalHeaderDb;

         public AppraisalHeaderDbFacade()
         {
             appraisalHeaderDb = new AppraisalHeaderDb();
         }

         //public Appraisal LoadByPeriodAndStaff(string staffId, int periodId)
         //{
         //    try
         //    {
         //        Appraisal appraisal = new Appraisal();
         //        DataSet dsAppraisal = appraisalHeaderDb.SelectAppraisalHeaderByPeriodIDAndStaffID(periodId, staffId);

         //        if (dsAppraisal != null)
         //        {
         //            if (dsAppraisal.Tables[0].Rows.Count > 0)
         //            {
         //                appraisal.Id = dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_APPRAISAL_HEADER_ID] == DBNull.Value ? 0 : Convert.ToInt32(dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_APPRAISAL_HEADER_ID]);
         //                appraisal.StaffId = dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_STAFF_ID] == DBNull.Value ? null : Convert.ToString(dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_STAFF_ID]);
         //                appraisal.PeriodId = dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_PERIOD_ID] == DBNull.Value ? 0 : Convert.ToInt32(dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_PERIOD_ID]);
         //                appraisal.SupervisorId = dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_SUPERVISOR_ID] == DBNull.Value ? null : Convert.ToString(dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_SUPERVISOR_ID]);
         //                appraisal.SupervisorAppraisalDate = dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_APPRAISAL_DATE] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_APPRAISAL_DATE]);
         //                appraisal.StaffResponseDate = dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_STAFF_RESPONSE_DATE] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_STAFF_RESPONSE_DATE]);
         //                appraisal.HodId = dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_HOD_ID] == DBNull.Value ? null : Convert.ToString(dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_HOD_ID]);
         //                appraisal.HodAppraisalDate = dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_HOD_APPRAISAL_DATE] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dsAppraisal.Tables[0].Rows[0][appraisalHeaderDb.FIELD_HOD_APPRAISAL_DATE]);
         //                appraisal.StatusId = Convert.ToByte(dsAppraisal.Tables[0].Rows[0]["Status_ID"]);
         //                appraisal.Status = dsAppraisal.Tables[0].Rows[0]["Status_Name"] == DBNull.Value ? null : Convert.ToString(dsAppraisal.Tables[0].Rows[0]["Status_Name"]);
         //            }
         //        }

         //        return appraisal;
         //    }
         //    catch (Exception ex)
         //    {
         //        throw ex;
         //    }
         //}

         public int CreateAppraisalHeader(Appraisal appraisal, Transaction transaction)
         {
             try
             {
                 if (appraisal != null)
                 {
                     return appraisalHeaderDb.InsertAppraisalHeader(appraisal.Period.Id, appraisal.Staff.Id, appraisal.Supervisor.Id, appraisal.AppraisalDate, appraisal.ResponseDate, appraisal.Hod.Id, appraisal.HodAppraisalDate, appraisal.Status.Id, transaction);
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }

             return -1;
         }

         public bool ModifyAppraisalHeader(Appraisal appraisal, Transaction transaction)
         {
             try
             {
                 if (appraisal != null)
                 {
                     if (appraisalHeaderDb.UpdateAppraisalHeaderByAppraisalHeaderId(appraisal.Id, appraisal.Period.Id, appraisal.Staff.Id, appraisal.Supervisor.Id, appraisal.AppraisalDate, appraisal.ResponseDate, appraisal.Hod.Id, appraisal.HodAppraisalDate, appraisal.Status.Id, transaction))
                     {
                         return true;
                     }
                 }

                 return false;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }





    }
}