using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;
using Mango.DataAccess;

namespace Mango.Business
{
    public class AppraisalLogic : BusinessLogicBase<Appraisal, APPRAISAL_HEADER>
    {
        public AppraisalLogic()
        {
            base.translator = new AppraisalTranslator();
        }

        public Appraisal LoadByPeriodAndStaff(string staffId, int periodId)
        {
            try
            {
                Func<APPRAISAL_HEADER, bool> selector = a => a.Staff_ID == staffId && a.Period_ID == periodId;
                Appraisal appraisal = base.GetModelBy(selector);

                return appraisal != null ? appraisal : new Appraisal();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long CreateAppraisalHeader(Appraisal appraisal, Transaction transaction)
        {
            try
            {
                //if (appraisal != null)
                //{
                //    return appraisalHeaderDb.InsertAppraisalHeader(appraisal.PeriodId, appraisal.StaffId, appraisal.SupervisorId, appraisal.SupervisorAppraisalDate, appraisal.StaffResponseDate, appraisal.HodId, appraisal.HodAppraisalDate, appraisal.StatusId, transaction);
                //}
                               

                Appraisal newAppraisal = null;
                if (appraisal != null)
                {
                   newAppraisal = base.Add(appraisal);
                }

                return newAppraisal != null ? newAppraisal.Id : -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifyAppraisalHeader(Appraisal appraisal, Transaction transaction)
        {
            try
            {
                //if (appraisal != null)
                //{
                //    if (appraisalHeaderDb.UpdateAppraisalHeaderByAppraisalHeaderId(appraisal.Id, appraisal.PeriodId, appraisal.StaffId, appraisal.SupervisorId, appraisal.SupervisorAppraisalDate, appraisal.StaffResponseDate, appraisal.HodId, appraisal.HodAppraisalDate, appraisal.StatusId, transaction))
                //    {
                //        return true;
                //    }
                //}

                Func<APPRAISAL_HEADER, bool> selector = ah => ah.Appraisal_Header_ID == appraisal.Id;
                APPRAISAL_HEADER entity = base.GetEntityBy(selector);

                if (appraisal != null)
                {
                    entity = new APPRAISAL_HEADER();
                    entity.Appraisal_Header_ID = appraisal.Id;
                    entity.Period_ID = appraisal.Period.Id;
                    entity.Staff_ID = appraisal.Staff.Id;
                    entity.Supervisor_ID = appraisal.Supervisor.Id;
                    entity.Appraisal_Date = appraisal.AppraisalDate;
                    entity.Staff_Response_Date = appraisal.ResponseDate;
                    entity.Hod_ID = appraisal.Hod.Id;
                    entity.Hod_Appraisal_Date = appraisal.HodAppraisalDate;
                    entity.Status_ID = appraisal.Status.Id;

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
                    throw new Exception("Required object for Appraisal not set! Please contact your administrator.");
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }




}
