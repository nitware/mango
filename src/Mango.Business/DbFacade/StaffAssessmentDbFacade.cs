using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Model.Model;
using Mango.DataAccess;
using Mango.Data.Interfaces;
using Mango.Data;

namespace Mango.Business.DbFacade
{
    public class StaffAssessmentDbFacade
    {
        private IStaffPotentialAssessmentDb staffAssessmentDb;

         public StaffAssessmentDbFacade()
         {
             staffAssessmentDb = new StaffPotentialAssessmentDb();
         }

         public bool CreateStaffAssessment(long appraisalHeaderId, StaffAssessment staffAssessment, Transaction transaction)
        {
            try
            {
                if (staffAssessment != null)
                {
                    return staffAssessmentDb.InsertStaffPotentialAssessment(appraisalHeaderId, staffAssessment.Period.Id, staffAssessment.Score, transaction);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public bool ModifyStaffAssessment(StaffAssessment staffAssessment, Transaction transaction)
        {
            try
            {
                if (staffAssessment != null)
                {
                    if (staffAssessmentDb.UpdateStaffPotentialAssessmentByAppraisalHeaderId(staffAssessment.Id, staffAssessment.Appraisal.Id, staffAssessment.Period.Id, staffAssessment.Score, transaction))
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
