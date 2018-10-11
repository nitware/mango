using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Mango.Model;
using Mango.Data;



namespace Mango.Business.DbFacade
{
    public class AppraisalReportFacade
    {
        protected IRepository repository = new Repository();

        public List<AppraisalReport> GetBy(int periodId)
        {
            try
            {
                Func<VW_APPRAISAL_REPORT, bool> selector = var => var.Period_ID == periodId;

                Repository repository = new Repository();
                //List<AppraisalReport> appraisalReports = (from ar in repository.Find(selector)
                
                List<AppraisalReport> appraisalReports = (from ar in repository.Fetch<VW_APPRAISAL_REPORT>() where ar.Period_ID == periodId
                                                          select new AppraisalReport
                                                                 {
                                                                     CompanyName = ar.Company_Name,
                                                                     DepartmentName = ar.Department_Name,
                                                                     StaffId = ar.Staff_ID,
                                                                     StaffName = ar.NAME,
                                                                     SupervisorId = ar.Supervisor_Staff_ID,
                                                                     SupervisorName = ar.Supervisor,
                                                                     JobRoleLevelName = ar.Job_Role_Level_Name,
                                                                     JobRoleName = ar.Job_Role_Name,
                                                                     PaceScore = ar.Pace_Score.HasValue ? ar.Pace_Score.Value : (decimal)0,
                                                                     PaceGrade = ar.Pace_Grade,
                                                                     MetricScore = ar.Metric_Score.HasValue ? ar.Metric_Score.Value : (double)0,
                                                                     MetricRating = ar.Metric_Rating.HasValue ? ar.Metric_Rating.Value : (byte)0,
                                                                     Recommendation = ar.Recommendation_Name,
                                                                     PeriodName = ar.Type,

                                                                     Strength = ar.Strength,
                                                                     Weakness = ar.Weakness,
                                                                     TrainingNeeds = ar.Training_Need,
                                                                     SupervisorComment = ar.Supervisor_Comment,
                                                                     AppraiseeComment = ar.Appraisee_Comment,
                                                                     HodComment = ar.Hod_Comment
                                                                 }).ToList();

                return appraisalReports;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}