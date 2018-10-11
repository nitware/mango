using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Collections.ObjectModel;
using System.Collections.Generic;
using Mango.Infrastructure.MangoService;

namespace Mango.Appraisal.Services
{
    public class AppraisalService : IAppraisalService
    {
        //private string val;
        ServiceClient svc;

        public event EventHandler AppraiseeSupervisorLoadCompleted;
        public event EventHandler AppraiseStaffCompleted;
        public event EventHandler SupervisorAppraiseeListLoadCompleted;
        public event EventHandler StaffDetailLoadCompleted;
        public event EventHandler CurrentAppraisalDetailsLoadCompleted;
        public event EventHandler GradeScaleLoadCompleted;
        public event EventHandler PaceAreaLoadCompleted;
        public event EventHandler MetricesLoadCompleted;
        public event EventHandler OptionLoadCompleted;
        public event EventHandler RecommendationLoadCompleted;
        public event EventHandler CommentLoadCompleted;
        public event EventHandler PaceLoadCompleted;
        public event EventHandler StaffAppraisalLoadCompleted;
        public event EventHandler AppraiseeHodGetCompleted;
        public event EventHandler PaceRatingLoadCompleted;
        //public event EventHandler ResponseToAppraisalCompleted;
        public event EventHandler HodAppraiseesLoadCompleted;

        public event EventHandler AppraisalModificationCompleted;
        public event EventHandler AppraisalAcceptOrRejectCompleted;

        public event EventHandler GetStaffAssessmentCompleted;
        public event EventHandler IsStaffLevelExistInAssessmentCompleted;

        //public AppraisalService() { }

        public Infrastructure.MangoService.Appraisal Appraisal { get; set; }
        public Staff Staff { get; set; }
        public Staff StaffSupervisor { get; set; }
        public Staff StaffHod { get; set; }
        public ObservableCollection<Staff> Staffs { get; set; }
        //public ObservableCollection<AppraisalSvc.Staff> HodAppraisees { get; set; }
        public ObservableCollection<PaceArea> PaceAreas { get; set; }
        public Metrices Metrices { get; set; }
        public Period Period { get; set; }
        public ObservableCollection<Option> Options { get; set; }
        public ObservableCollection<Recommendation> Recommendations { get; set; }
        public ObservableCollection<Pace> Paces { get; set; }
        public bool Happy { get; set; }
        public bool IsAppraisalModified { get; set; }
        public bool IsAppraisalAcceptedOrRejected { get; set; }
        public Comment Comment { get; set; }
        public decimal TotalPaceScore { get; set; }
        public ObservableCollection<GradeScale> GradeScales { get; set; }
        public ObservableCollection<PaceRating> PaceRatings { get; set; }

        public ObservableCollection<StaffAssessment> StaffAssessments { get; set; }
        public bool StaffLevelExistInAssessment { get; set; }

        public void LoadHodAppraisees(int companyDepartmentJobRoleId, int periodId, byte optionId)
        {
            svc = new ServiceClient();
            svc.LoadHodAppraiseesCompleted += new EventHandler<LoadHodAppraiseesCompletedEventArgs>(svc_LoadHodAppraiseesCompleted);
            svc.LoadHodAppraiseesAsync(companyDepartmentJobRoleId, periodId, optionId);
            svc.CloseAsync();
        }
        //public void RespondToAppraisal(AppraisalSvc.Staff staff, AppraisalSvc.Staff supervisor, AppraisalSvc.Staff hod, AppraisalSvc.Period period, AppraisalSvc.Appraisal appraisal, AppraisalSvc.Comment comment)
        //{
        //    svc = new DataServiceClient();
        //    svc.RespondToAppraisalCompleted += new EventHandler<RespondToAppraisalCompletedEventArgs>(svc_RespondToAppraisalCompleted);
        //    svc.RespondToAppraisalAsync(staff, supervisor, hod, period, appraisal, comment);
        //    svc.CloseAsync();
        //}
        public void AcceptOrRejectAppraisal(Staff staff, Staff supervisor, Staff hod, Period period, Infrastructure.MangoService.Appraisal appraisal, Comment comment)
        {
            svc = new ServiceClient();
            svc.AcceptOrRejectAppraisalCompleted += new EventHandler<AcceptOrRejectAppraisalCompletedEventArgs>(svc_AcceptOrRejectAppraisalCompleted);
            svc.AcceptOrRejectAppraisalAsync(staff, supervisor, hod, period, appraisal, comment);
            svc.CloseAsync();
        }
        public void ModifyAppraisal(Staff staff, Staff supervisor, Period period, Infrastructure.MangoService.Appraisal appraisal, Comment comment)
        {
            svc = new ServiceClient();
            svc.ModifyAppraisalCompleted += new EventHandler<ModifyAppraisalCompletedEventArgs>(svc_ModifyAppraisalCompleted);
            svc.ModifyAppraisalAsync(staff, supervisor, period, appraisal, comment);
            svc.CloseAsync();
        }
        public void GetStaffHod(int companyDepartmentJobRoleId, int periodId)
        {
            svc = new ServiceClient();
            svc.GetAppraiseeHodCompleted += new EventHandler<GetAppraiseeHodCompletedEventArgs>(svc_GetAppraiseeHodCompleted);
            svc.GetAppraiseeHodAsync(companyDepartmentJobRoleId, periodId);
            svc.CloseAsync();
        }
        public void LoadStaffSupervisor(int companyDepartmentJobRoleId, int periodId)
        {
            svc = new ServiceClient();
            svc.LoadAppraiseeSupervisorCompleted += new EventHandler<LoadAppraiseeSupervisorCompletedEventArgs>(svc_LoadAppraiseeSupervisorCompleted);
            svc.LoadAppraiseeSupervisorAsync(companyDepartmentJobRoleId, periodId);
            svc.CloseAsync();
        }
        public void LoadStaffAppraisal(string staffId, int periodId)
        {
            svc = new ServiceClient();
            svc.LoadStaffAppraisalCompleted += new EventHandler<LoadStaffAppraisalCompletedEventArgs>(svc_LoadStaffAppraisalCompleted);
            svc.LoadStaffAppraisalAsync(staffId, periodId);
            svc.CloseAsync();
        }
        public void AppraiseStaff(Infrastructure.MangoService.Appraisal appraisal, ObservableCollection<Pace> paces, Metrices metrices, Comment comment, Staff staff, Staff supervisor, Period period, ObservableCollection<StaffAssessment> staffAssessments, bool sendMail)
        {
            svc = new ServiceClient();
            svc.AppraiseStaffCompleted += new EventHandler<AppraiseStaffCompletedEventArgs>(svc_AppraiseStaffCompleted);
            svc.AppraiseStaffAsync(appraisal, paces, metrices, comment, staff, supervisor, period, staffAssessments, sendMail);
            svc.CloseAsync();
        }
        public void LoadSupervisorsAppraisees(int companyDepartmentJobRoleId, int periodId)
        {
            svc = new ServiceClient();
            svc.LoadSupervisorsAppraiseeCompleted += new EventHandler<LoadSupervisorsAppraiseeCompletedEventArgs>(svc_LoadSupervisorsAppraiseeCompleted);
            svc.LoadSupervisorsAppraiseeAsync(companyDepartmentJobRoleId, periodId);
            svc.CloseAsync();
        }
        public void IsStaffLevelExistInAssessment(int periodId, string levelId)
        {
            svc = new ServiceClient();
            svc.IsStaffLevelExistInAssessmentCompleted += new EventHandler<IsStaffLevelExistInAssessmentCompletedEventArgs>(svc_IsStaffLevelExistInAssessmentCompleted);
            svc.IsStaffLevelExistInAssessmentAsync(levelId, periodId);
            svc.CloseAsync();
        }
        public void LoadPace(string staffId, int companyDepartmentJobRoleId, int periodId, bool isLoadingAppraisee)
        {
            svc = new ServiceClient();
            svc.LoadPaceByStaffAndPeriodCompleted += new EventHandler<LoadPaceByStaffAndPeriodCompletedEventArgs>(svc_LoadPaceByStaffAndPeriodCompleted);
            svc.LoadPaceByStaffAndPeriodAsync(staffId, companyDepartmentJobRoleId, periodId, isLoadingAppraisee);
            svc.CloseAsync();
        }
        public void LoadStaffAssessment(string staffId, int periodId, bool isLoadingAppraisee)
        {
            svc = new ServiceClient();
            svc.GetStaffAssessmentsCompleted += new EventHandler<GetStaffAssessmentsCompletedEventArgs>(svc_GetStaffAssessmentsCompleted);
            svc.GetStaffAssessmentsAsync(staffId, periodId, isLoadingAppraisee);
            svc.CloseAsync();
        }
        public void LoadPaceArea()
        {
            svc = new ServiceClient();
            svc.LoadPaceAreaCompleted += new EventHandler<LoadPaceAreaCompletedEventArgs>(svc_LoadPaceAreaCompleted);
            svc.LoadPaceAreaAsync();
            svc.CloseAsync();
        }
        public void LoadMetrices(int companyDepartmentJobRoleId, string staffId, int periodId, bool isLoadingAppraisee)
        {
            svc = new ServiceClient();
            svc.LoadMetricesCompleted += new EventHandler<LoadMetricesCompletedEventArgs>(svc_LoadMetricesCompleted);
            svc.LoadMetricesAsync(companyDepartmentJobRoleId, staffId, periodId, isLoadingAppraisee);
            svc.CloseAsync();
        }
        public void LoadCurrentAppraisalDetails(int periodId)
        {
            svc = new ServiceClient();
            svc.LoadCurrentAppraisalDetailCompleted += new EventHandler<LoadCurrentAppraisalDetailCompletedEventArgs>(svc_LoadCurrentAppraisalDetailCompleted);
            svc.LoadCurrentAppraisalDetailAsync(periodId);
            svc.CloseAsync();
        }
        public void GetStaffById(string staffID, int companyDepartmentJobRoleId, int periodId)
        {
            svc = new ServiceClient();
            svc.GetStaffByIdCompleted += new EventHandler<GetStaffByIdCompletedEventArgs>(svc_GetStaffByIdCompleted);
            svc.GetStaffByIdAsync(staffID, companyDepartmentJobRoleId, periodId);
            svc.CloseAsync();
        }
        //public void GetStaffById(string staffID, int companyDepartmentJobRoleId)
        //{
        //    svc = new DataServiceClient();
        //    svc.GetStaffByIdCompleted += new EventHandler<GetStaffByIdCompletedEventArgs>(svc_GetStaffByIdCompleted);
        //    svc.GetStaffByIdAsync(staffID, companyDepartmentJobRoleId);
        //    svc.CloseAsync();
        //}
        public void LoadAppraisalOption()
        {
            svc = new ServiceClient();
            svc.LoadAppraisalOptionCompleted += new EventHandler<LoadAppraisalOptionCompletedEventArgs>(svc_LoadAppraisalOptionCompleted);
            svc.LoadAppraisalOptionAsync();
            svc.CloseAsync();
        }
        public void LoadRecommendation()
        {
            svc = new ServiceClient();
            svc.LoadRecommendationCompleted += new EventHandler<LoadRecommendationCompletedEventArgs>(svc_LoadRecommendationCompleted);
            svc.LoadRecommendationAsync();
            svc.CloseAsync();
        }
        public void LoadComment(string staffId, int periodId)
        {
            svc = new ServiceClient();
            svc.LoadCommentCompleted += new EventHandler<LoadCommentCompletedEventArgs>(svc_LoadCommentCompleted);
            svc.LoadCommentAsync(staffId, periodId);
            svc.CloseAsync();
        }

        public void LoadGrade()
        {
            svc = new ServiceClient();
            svc.LoadAllGradeScaleCompleted += new EventHandler<LoadAllGradeScaleCompletedEventArgs>(svc_LoadAllGradeScaleCompleted);
            svc.LoadAllGradeScaleAsync();
            svc.CloseAsync();
        }
        public void LoadPaceRating()
        {
            svc = new ServiceClient();
            svc.LoadAllPaceRatingCompleted += new EventHandler<LoadAllPaceRatingCompletedEventArgs>(svc_LoadAllPaceRatingCompleted);
            svc.LoadAllPaceRatingAsync();
            svc.CloseAsync();
        }

       

        private void svc_LoadHodAppraiseesCompleted(object sender, LoadHodAppraiseesCompletedEventArgs e)
        {
            Staffs = e.Result;
            if (HodAppraiseesLoadCompleted != null)
            {
                HodAppraiseesLoadCompleted(this, null);
            }
        }
        //private void svc_RespondToAppraisalCompleted(object sender, RespondToAppraisalCompletedEventArgs e)
        //{
        //    Happy = e.Result;
        //    if (ResponseToAppraisalCompleted != null)
        //    {
        //        ResponseToAppraisalCompleted(this, null);
        //    }
        //}
        private void svc_AcceptOrRejectAppraisalCompleted(object sender, AcceptOrRejectAppraisalCompletedEventArgs e)
        {
            IsAppraisalAcceptedOrRejected = e.Result;
            if (AppraisalAcceptOrRejectCompleted != null)
            {
                AppraisalAcceptOrRejectCompleted(this, null);
            }
        }
        private void svc_ModifyAppraisalCompleted(object sender, ModifyAppraisalCompletedEventArgs e)
        {
            IsAppraisalModified = e.Result;
            if (AppraisalModificationCompleted != null)
            {
                AppraisalModificationCompleted(this, null);
            }
        }
        private void svc_GetAppraiseeHodCompleted(object sender, GetAppraiseeHodCompletedEventArgs e)
        {
            StaffHod = e.Result;
            if (AppraiseeHodGetCompleted != null)
            {
                AppraiseeHodGetCompleted(this, null);
            }
        }
        private void svc_LoadAppraiseeSupervisorCompleted(object sender, LoadAppraiseeSupervisorCompletedEventArgs e)
        {
            StaffSupervisor = e.Result;
            if (AppraiseeSupervisorLoadCompleted != null)
            {
                AppraiseeSupervisorLoadCompleted(this, null);
            }
        }

        private void svc_LoadStaffAppraisalCompleted(object sender, LoadStaffAppraisalCompletedEventArgs e)
        {
            Appraisal = e.Result;
            if (StaffAppraisalLoadCompleted != null)
            {
                StaffAppraisalLoadCompleted(this, null);
            }
        }

        public void svc_AppraiseStaffCompleted(object sender, AppraiseStaffCompletedEventArgs e)
        {
            Happy = e.Result;
            if (AppraiseStaffCompleted != null)
            {
                AppraiseStaffCompleted(this, null);
            }
        }
        private void svc_LoadCommentCompleted(object sender, LoadCommentCompletedEventArgs e)
        {
            Comment = e.Result;
            if (CommentLoadCompleted != null)
            {
                CommentLoadCompleted(this, null);
            }
        }
        private void svc_LoadRecommendationCompleted(object sender, LoadRecommendationCompletedEventArgs e)
        {
            Recommendations = e.Result;
            if (RecommendationLoadCompleted != null)
            {
                RecommendationLoadCompleted(this, null);
            }
        }
        private void svc_LoadAppraisalOptionCompleted(object sender, LoadAppraisalOptionCompletedEventArgs e)
        {
            Options = e.Result;
            if (OptionLoadCompleted != null)
            {
                OptionLoadCompleted(this, null);
            }
        }
        private void svc_GetStaffByIdCompleted(object sender, GetStaffByIdCompletedEventArgs e)
        {
            Staff = e.Result;
            if (StaffDetailLoadCompleted != null)
            {
                StaffDetailLoadCompleted(this, null);
            }
        }
        private void svc_LoadMetricesCompleted(object sender, LoadMetricesCompletedEventArgs e)
        {
            Metrices = e.Result;
            if (MetricesLoadCompleted != null)
            {
                MetricesLoadCompleted(this, null);
            }
        }

        private void svc_LoadPaceByStaffAndPeriodCompleted(object sender, LoadPaceByStaffAndPeriodCompletedEventArgs e)
        {
            Paces = e.Result;

            if (Paces != null)
            {
                if (Paces.Count > 0)
                {
                    
                    int i = 0;
                    TotalPaceScore = 0;
                    decimal totalPaceScoreaAverage = 0;

                    for (i = 0; i < Paces.Count; i++)
                    {
                        totalPaceScoreaAverage += Paces[i].Score;
                    }
                    if (totalPaceScoreaAverage > 0)
                    {
                        //TotalPaceScore = Math.Round(((totalPaceScoreaAverage / Convert.ToDecimal(i)) / 105) * 100, 2);
                        TotalPaceScore = Math.Round(totalPaceScoreaAverage / Convert.ToDecimal(i), 2);
                    }
                }
            }

            if (PaceLoadCompleted != null)
            {
                PaceLoadCompleted(this, null);
            }
        }
        private void svc_LoadPaceAreaCompleted(object sender, LoadPaceAreaCompletedEventArgs e)
        {
            PaceAreas = e.Result;
            if (PaceAreaLoadCompleted != null)
            {
                PaceAreaLoadCompleted(this, null);
            }
        }
        private void svc_LoadCurrentAppraisalDetailCompleted(object sender, LoadCurrentAppraisalDetailCompletedEventArgs e)
        {
            Period = e.Result;
            if (CurrentAppraisalDetailsLoadCompleted != null)
            {
                CurrentAppraisalDetailsLoadCompleted(this, null);
            }
        }
        private void svc_LoadSupervisorsAppraiseeCompleted(object sender, LoadSupervisorsAppraiseeCompletedEventArgs e)
        {
            Staffs = e.Result;
            if (SupervisorAppraiseeListLoadCompleted != null)
            {
                SupervisorAppraiseeListLoadCompleted(this, null);
            }
        }

        private void svc_LoadAllGradeScaleCompleted(object sender, LoadAllGradeScaleCompletedEventArgs e)
        {
            GradeScales = e.Result;
            if (GradeScaleLoadCompleted != null)
            {
                GradeScaleLoadCompleted(this, null);
            }
        }

        private void svc_LoadAllPaceRatingCompleted(object sender, LoadAllPaceRatingCompletedEventArgs e)
        {
            PaceRatings = e.Result;
            if (PaceRatingLoadCompleted != null)
            {
                PaceRatingLoadCompleted(this, null);
            }
        }

        private void svc_GetStaffAssessmentsCompleted(object sender, GetStaffAssessmentsCompletedEventArgs e)
        {
            try
            {
                StaffAssessments = e.Result;
                if (GetStaffAssessmentCompleted != null)
                {
                    GetStaffAssessmentCompleted(this, null);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void svc_IsStaffLevelExistInAssessmentCompleted(object sender, IsStaffLevelExistInAssessmentCompletedEventArgs e)
        {
            try
            {
                StaffLevelExistInAssessment = e.Result;
                if (IsStaffLevelExistInAssessmentCompleted != null)
                {
                    IsStaffLevelExistInAssessmentCompleted(this, null);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }





    }
}
