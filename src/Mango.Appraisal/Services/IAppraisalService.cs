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
using Mango.Infrastructure.MangoService;

namespace Mango.Appraisal.Services
{
    public interface IAppraisalService
    {
        event EventHandler GradeScaleLoadCompleted;
        event EventHandler PaceAreaLoadCompleted;
        event EventHandler AppraiseStaffCompleted;
        event EventHandler StaffDetailLoadCompleted;
        event EventHandler CurrentAppraisalDetailsLoadCompleted;
        event EventHandler SupervisorAppraiseeListLoadCompleted;
        event EventHandler RecommendationLoadCompleted;
        event EventHandler MetricesLoadCompleted;
        event EventHandler CommentLoadCompleted;
        event EventHandler OptionLoadCompleted;
        event EventHandler PaceLoadCompleted;
        event EventHandler StaffAppraisalLoadCompleted;
        event EventHandler AppraiseeSupervisorLoadCompleted;
        event EventHandler AppraiseeHodGetCompleted;
        event EventHandler PaceRatingLoadCompleted;
        //event EventHandler ResponseToAppraisalCompleted;
        event EventHandler HodAppraiseesLoadCompleted;
        event EventHandler AppraisalModificationCompleted;
        event EventHandler AppraisalAcceptOrRejectCompleted;

        event EventHandler GetStaffAssessmentCompleted;
        event EventHandler IsStaffLevelExistInAssessmentCompleted;

        Infrastructure.MangoService.Appraisal Appraisal { get; set; }
        Staff Staff { get; set; }
        ObservableCollection<Staff> Staffs { get; set; }
        ObservableCollection<PaceArea> PaceAreas { get; set; }
        ObservableCollection<Pace> Paces { get; set; }
        Period Period { get; set; }
        Metrices Metrices { get; set; }
        ObservableCollection<Option> Options { get; set; }
        ObservableCollection<Recommendation> Recommendations { get; set; }
        Comment Comment { get; set; }
        bool Happy { get; set; }
        decimal TotalPaceScore { get; set; }
        Staff StaffSupervisor { get; set; }
        ObservableCollection<GradeScale> GradeScales { get; set; }
        Staff StaffHod { get; set; }
        ObservableCollection<PaceRating> PaceRatings { get; set; }
        ObservableCollection<StaffAssessment> StaffAssessments { get; set; }
        bool StaffLevelExistInAssessment { get; set; }

        bool IsAppraisalModified { get; set; }
        bool IsAppraisalAcceptedOrRejected { get; set; }

        void LoadGrade();
        void LoadPaceArea();
        void LoadCurrentAppraisalDetails(int periodId);

        //void GetStaffById(string staffID, int companyDepartmentJobRoleId);
        void GetStaffById(string staffID, int companyDepartmentJobRoleId, int periodId);
        void LoadMetrices(int companyDepartmentJobRoleId, string staffId, int periodId, bool isLoadingAppraisee);
        void LoadAppraisalOption();
        void LoadRecommendation();
        void LoadComment(string staffId, int periodId);

        //void LoadSupervisorsAppraisees(int companyDepartmentJobRoleId);
        void LoadSupervisorsAppraisees(int companyDepartmentJobRoleId, int periodId);
        void LoadPace(string staffId, int companyDepartmentJobRoleId, int periodId, bool isLoadingAppraisee);
        void AppraiseStaff(Infrastructure.MangoService.Appraisal appraisal, ObservableCollection<Pace> paces, Metrices metrices, Comment comment, Staff staff, Staff supervisor, Period period, ObservableCollection<StaffAssessment> staffAssessments, bool sendMail);
        void LoadStaffAppraisal(string staffId, int periodId);
        void LoadStaffSupervisor(int companyDepartmentJobRoleId, int periodId);
        void GetStaffHod(int companyDepartmentJobRoleId, int periodId);
        void LoadPaceRating();

        //void RespondToAppraisal(AppraisalSvc.Staff staff, AppraisalSvc.Staff supervisor, AppraisalSvc.Staff hod, AppraisalSvc.Period period, AppraisalSvc.Appraisal appraisal, AppraisalSvc.Comment comment);
        void LoadHodAppraisees(int companyDepartmentJobRoleId, int periodId, byte optionId);
        void AcceptOrRejectAppraisal(Staff staff, Staff supervisor, Staff hod, Period period, Infrastructure.MangoService.Appraisal appraisal, Comment comment);
        void ModifyAppraisal(Staff staff, Staff supervisor, Period period, Infrastructure.MangoService.Appraisal appraisal, Comment comment);

        void LoadStaffAssessment(string staffId, int periodId, bool isLoadingAppraisee);
        void IsStaffLevelExistInAssessment(int periodId, string levelId);

    }



}
