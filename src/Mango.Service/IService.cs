using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using Mango.Model;
using System.Collections.ObjectModel;
using Mango.Model.Model;

namespace Mango.Service
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Staff ValidateUserAccessKey(string userName, string password);

        [OperationContract]
        Staff GetStaffById(string staffId, int companyDepartmentJobRoleId, int periodId);
        //Staff GetStaffById(string staffId, int companyDepartmentJobRoleId);

        [OperationContract]
        ObservableCollection<PaceArea> LoadPaceArea();

        [OperationContract]
        Period LoadCurrentAppraisalDetail(int periodId);

        [OperationContract]
        Metrices LoadMetrices(int companyDepartmentJobRoleId, string staffId, int periodId, bool isLoadingAppraisee);

        [OperationContract]
        List<Option> LoadAppraisalOption();

        [OperationContract]
        Comment LoadComment(string staffId, int periodId);

        [OperationContract]
        List<Recommendation> LoadRecommendation();

        [OperationContract]
        List<Staff> LoadSupervisorsAppraisee(int companyDepartmentJobRoleId, int periodId);
        //List<Staff> LoadSupervisorsAppraisee(int companyDepartmentJobRoleId);

        [OperationContract]
        Staff GetStaffByLoginName(string loginName);

        [OperationContract]
        List<Pace> LoadPaceByStaffAndPeriod(string staffId, int companyDepartmentJobRoleId, int periodId, bool isLoadingAppraisee);

        [OperationContract]
        bool AppraiseStaff(Appraisal appraisal, List<Pace> paces, Metrices metrices, Comment comment, Staff staff, Staff supervisor, Period period, List<StaffAssessment> staffAssessments, bool sendMail);

        [OperationContract]
        Appraisal LoadStaffAppraisal(string staffId, int periodId);

        [OperationContract]
        Period GetCurrentPeriod();

        [OperationContract]
        Staff LoadAppraiseeSupervisor(int companyDepartmentJobRoleId, int periodId);
        //Staff LoadAppraiseeSupervisor(int companyDepartmentJobRoleId);

        [OperationContract]
        List<GradeScale> LoadAllGradeScale();

        [OperationContract]
        Staff GetAppraiseeHod(int companyDepartmentJobRoleId, int periodId);
        //Staff GetAppraiseeHod(int companyDepartmentJobRoleId);

        [OperationContract]
        List<PaceRating> LoadAllPaceRating();

        [OperationContract]
        byte GetStaffType(int companyDepartmentJobRoleId, int periodId);

        [OperationContract]
        List<Staff> LoadHodAppraisees(int companyDepartmentJobRoleId, int periodId, byte optionId);

        [OperationContract]
        bool ModifyAppraisal(Staff staff, Staff supervisor, Period period, Appraisal appraisal, Comment comment);

        [OperationContract]
        bool AcceptOrRejectAppraisal(Staff staff, Staff supervisor, Staff hod, Period period, Appraisal appraisal, Comment comment);

        [OperationContract]
        bool CreateNewPeriod(Period newPeriod);
        //bool CreateNewPeriod(Period newPeriod, Period oldPeriod);

        [OperationContract]
        List<Period> GetAllPeriods();

        [OperationContract]
        Learning GetLearningByStaffAndPeriod(string staffId, int periodId);

        [OperationContract]
        List<AppraisalReport> GetAppraisalReportByPeriod(int periodId);

        #region Staff

        [OperationContract]
        List<Staff> GetStaffs(Staff staff, out Fault fault);
        [OperationContract]
        List<Staff> GetAllStaffs(out Fault fault);
        [OperationContract]
        Staff GetStaffByUserName(string userName, out Fault fault);
        [OperationContract]
        bool AddStaff(Staff staff, out Fault fault);
        [OperationContract]
        bool ModifyStaff(Staff staff, out Fault fault);
        [OperationContract]
        LoginDetail ValidateStaff(string userName, string password, out Fault fault);
        [OperationContract]
        bool RemoveStaff(Staff staff, out Fault fault);

        #endregion

        #region Role Region

        [OperationContract]
        List<Role> GetRoles(Staff staff, out Fault fault);
        [OperationContract]
        List<Role> GetAllRoles(out Fault fault);
        [OperationContract]
        bool AddRole(Role role, out Fault fault);
        [OperationContract]
        bool AssignRightToRole(Role role, out Fault fault);
        [OperationContract]
        bool ModifyRole(Role role, out Fault fault);
        [OperationContract]
        bool RemoveRole(Role role, out Fault fault);

        #endregion

        #region Right Region

        [OperationContract]
        List<Right> GetAllRights(out Fault fault);
        [OperationContract]
        bool AddRight(Right right, out Fault fault);
        [OperationContract]
        bool ModifyRight(Right right, out Fault fault);
        [OperationContract]
        bool RemoveRight(Right right, out Fault fault);

        #endregion

        #region Job Role

        [OperationContract]
        bool AddJobRole(JobRole jobRole, out Fault fault);
        [OperationContract]
        List<JobRole> GetAllJobRoles(out Fault fault);
        [OperationContract]
        bool RemoveJobRole(JobRole jobRole, out Fault fault);
        [OperationContract]
        bool ModifyJobRole(JobRole jobRole, out Fault fault);

        #endregion

        #region Job Role Level

        [OperationContract]
        bool AddLevel(Level level, out Fault fault);
        [OperationContract]
        List<Level> GetAllLevels(out Fault fault);
        [OperationContract]
        bool RemoveLevel(Level level, out Fault fault);
        [OperationContract]
        bool ModifyLevel(Level level, out Fault fault);

        #endregion

        #region Department

        [OperationContract]
        bool AddDepartment(Department department, out Fault fault);
        [OperationContract]
        List<Department> GetAllDepartments(out Fault fault);
        [OperationContract]
        bool RemoveDepartment(Department department, out Fault fault);
        [OperationContract]
        bool ModifyDepartment(Department department, out Fault fault);

        #endregion

        #region Staff Level

        [OperationContract]
        bool AddStaffLevel(StaffLevel staffLevel, out Fault fault);
        [OperationContract]
        List<StaffLevel> GetAllStaffLevels(out Fault fault);
        [OperationContract]
        StaffLevel GetByStaffAndPeriod(Staff staff, Period period, out Fault fault);
        [OperationContract]
        bool RemoveStaffLevel(StaffLevel staffLevel, out Fault fault);
        [OperationContract]
        bool ModifyStaffLevel(StaffLevel staffLevel, out Fault fault);

        #endregion

        #region Staff Company Department Job Role

        [OperationContract]
        bool AddStaffCdjr(StaffCdjr staffCdjr, out Fault fault);
        [OperationContract]
        List<StaffCdjr> GetAllStaffCdjrs(out Fault fault);
        [OperationContract]
        StaffCdjr GetStaffCdjrByStaffAndPeriod(Staff staff, Period period, out Fault fault);
        [OperationContract]
        bool RemoveStaffCdjr(StaffCdjr staffCdjr, out Fault fault);
        [OperationContract]
        bool ModifyStaffCdjr(StaffCdjr staffCdjr, out Fault fault);

        #endregion

        #region Company Department Job Role

        [OperationContract]
        bool AddCompanyDepartmentJobRole(CompanyDepartmentJobRole cdjr, out Fault fault);
        [OperationContract]
        List<CompanyDepartmentJobRole> GetAllCompanyDepartmentJobRoles(out Fault fault);
        [OperationContract]
        bool RemoveCompanyDepartmentJobRole(CompanyDepartmentJobRole companyDepartmentJobRole, out Fault fault);
        [OperationContract]
        bool ModifyCompanyDepartmentJobRole(CompanyDepartmentJobRole companyDepartmentJobRole, out Fault fault);

        #endregion

        #region Job Role HOD

        [OperationContract]
        bool AddJobRoleHods(List<JobRoleHod> jobRoleHods, out Fault fault);
        [OperationContract]
        List<JobRoleHod> GetAllJobRoleHods(out Fault fault);
        [OperationContract]
        List<JobRoleHod> GetJobRolesUnderHodByPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, out Fault fault);
        [OperationContract]
        bool RemoveJobRoleHod(JobRoleHod jobRoleHod, Period period, out Fault fault);
        [OperationContract]
        bool ModifyJobRoleHods(List<JobRoleHod> jobRoleHods, out Fault fault);
        [OperationContract]
        List<JobRoleHod> GetAllHodJobRolesByPeriod(Period period, out Fault fault);
        [OperationContract]
        bool RemoveJobRoleHodByHodCompanyDepartmentJobRole(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, out Fault fault);
        #endregion

        #region Metrics

        [OperationContract]
        bool ModifyMetric(Metrics metrics, out Fault fault);
        [OperationContract]
        bool AddMetrics(Metrics metrics, out Fault fault);
        [OperationContract]
        bool AddMetrices(List<Metrics> metrices, out Fault fault);
        [OperationContract]
        List<Metrics> GetAllMetricesByPeriod(Period period, out Fault fault);
        [OperationContract]
        List<Metrics> GetMetricesByCompanyDepartmetJobRoleAndPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, out Fault fault);
        [OperationContract]
        List<Metrics> GetMetricesByPeriod(Period period, out Fault fault);
        [OperationContract]
        bool RemoveMetrics(List<Metrics> metrices, out Fault fault);

        //[OperationContract]
        //bool ModifyMetrics(List<Metrics> metrices, out Fault fault);

        [OperationContract]
        bool ModifyMetrics(List<Metrics> metrices, bool removeAssociatedRatings, out Fault fault);

        [OperationContract]
        bool RemoveMetricsByCompanyDepartmentJobRoleAndPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, bool removeMetrics, out Fault fault);
        [OperationContract]
        List<Metrics> GetAllNpsByPeriod(Period period, out Fault fault);
        [OperationContract]
        bool ModifyNps(List<Metrics> metrics, out Fault fault);
        [OperationContract]
        List<Metrics> GetAllMetricesByPeriodAndPerspective(Period period, MetricsPerspective perspective, out Fault fault);

        [OperationContract]
        bool RemoveMetricAndAssociatedRatings(Metrics metrics, out Fault fault);

        #endregion

        #region Metric Rating

        [OperationContract]
        bool AddMetricRatings(List<MetricRating> metricRatings, out Fault fault);
        [OperationContract]
        List<MetricRating> GetAllMetricRatingsByPeriod(Period period, out Fault fault);
        [OperationContract]
        bool RemoveMetricRatings(List<MetricRating> metricRatings, out Fault fault);
        [OperationContract]
        bool ModifyMetricRatings(List<MetricRating> metricRatings, out Fault fault);
        [OperationContract]
        List<MetricRating> GetMetricRatingsByMetrics(Metrics metrics, out Fault fault);
        [OperationContract]
        bool RemoveMetricRatingByMetrics(Metrics metrics, out Fault fault);

        #endregion

        #region Rating

        [OperationContract]
        bool AddRating(Rating rating, out Fault fault);
        [OperationContract]
        List<Rating> GetAllRatings(out Fault fault);
        [OperationContract]
        bool RemoveRating(Rating rating, out Fault fault);
        [OperationContract]
        bool ModifyRating(Rating rating, out Fault fault);

        #endregion

        #region Rating Type

        [OperationContract]
        bool AddRatingType(RatingType ratingType, out Fault fault);
        [OperationContract]
        List<RatingType> GetAllRatingTypes(out Fault fault);
        [OperationContract]
        bool RemoveRatingType(RatingType ratingType, out Fault fault);
        [OperationContract]
        bool ModifyRatingType(RatingType ratingType, out Fault fault);

        #endregion

        #region Department

        [OperationContract]
        bool AddMetricsPerspective(MetricsPerspective metricsPerspective, out Fault fault);
        [OperationContract]
        List<MetricsPerspective> GetAllMetricsPerspectives(out Fault fault);
        [OperationContract]
        bool RemoveMetricsPerspective(MetricsPerspective metricsPerspective, out Fault fault);
        [OperationContract]
        bool ModifyMetricsPerspective(MetricsPerspective metricsPerspective, out Fault fault);

        #endregion

        #region Period

        [OperationContract]
        bool ModifyPeriod(Period period, out Fault fault);
        [OperationContract]
        bool RemovePeriod(Period period, out Fault fault);
        [OperationContract]
        bool SetNewCurrentPeriod(CurrentPeriod period, out Fault fault);

        #endregion

        #region Status

        [OperationContract]
        List<Status> GetAllStatus(out Fault fault);

        #endregion

        #region Job Role Supervisor

        [OperationContract]
        bool AddJobRoleSupervisors(List<JobRoleSupervisor> jobRoleSupervisors, out Fault fault);
        [OperationContract]
        List<JobRoleSupervisor> GetJobRolesUnderSupervisorByPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, out Fault fault);
        [OperationContract]
        List<JobRoleSupervisor> GetAllSupervisorJobRolesByPeriod(Period period, out Fault fault);
        [OperationContract]
        bool RemoveJobRoleSupervisor(JobRoleSupervisor jobRoleSupervisor, Period period, out Fault fault);
        [OperationContract]
        bool RemoveJobRoleSupervisorBySupervisorCompanyDepartmentJobRole(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, out Fault fault);
        [OperationContract]
        bool ModifyJobRoleSupervisors(List<JobRoleSupervisor> jobRoleSupervisors, out Fault fault);

        #endregion

        #region Period Type Region

        [OperationContract]
        List<PeriodType> GetAllPeriodTypes(out Fault fault);
        [OperationContract]
        bool AddPeriodType(PeriodType periodType, out Fault fault);
        [OperationContract]
        bool RemovePeriodType(PeriodType periodType, out Fault fault);
        [OperationContract]
        bool ModifyPeriodType(PeriodType periodType, out Fault fault);

        #endregion

        #region Company

        [OperationContract]
        bool AddCompany(Company company, out Fault fault);
        [OperationContract]
        List<Company> GetAllCompanies(out Fault fault);
        [OperationContract]
        bool RemoveCompany(Company company, out Fault fault);
        [OperationContract]
        bool ModifyCompany(Company company, out Fault fault);

        #endregion

        #region Company Department

        [OperationContract]
        List<CompanyDepartment> GetCompanyDepartmentByCompany(Company company, out Fault fault);

        #endregion

        #region Staff Learning

        [OperationContract]
        bool AddStaffLearning(StaffLearning staffLearning, out Fault fault);
        [OperationContract]
        List<StaffLearning> GetAllStaffLearning(out Fault fault);
        [OperationContract]
        bool RemoveStaffLearning(StaffLearning staffLearning, out Fault fault);
        [OperationContract]
        bool ModifyStaffLearning(StaffLearning staffLearning, out Fault fault);

        #endregion

        #region Location Region

        [OperationContract]
        List<Location> GetAllLocations(out Fault fault);
        [OperationContract]
        bool AddLocation(Location location, out Fault fault);
        [OperationContract]
        bool ModifyLocation(Location location, out Fault fault);
        [OperationContract]
        bool RemoveLocation(Location location, out Fault fault);

        #endregion

        #region Staff Location Region

        [OperationContract]
        List<StaffLocation> GetAllStaffLocations(out Fault fault);
        [OperationContract]
        bool AddStaffLocation(StaffLocation staffLocation, out Fault fault);
        [OperationContract]
        bool ModifyStaffLocation(StaffLocation staffLocation, out Fault fault);
        [OperationContract]
        bool RemoveStaffLocation(StaffLocation staffLocation, out Fault fault);

        #endregion

        #region Login Detail Region

        [OperationContract]
        List<LoginDetail> GetAllLoginDetails(out Fault fault);
        [OperationContract]
        bool ResetLoginDetailPassword(LoginDetail loginDetail, out Fault fault);
        [OperationContract]
        bool ModifyLoginDetail(LoginDetail loginDetail, out Fault fault);
        [OperationContract]
        LoginDetail ChangePassword(Staff staff, string password, out Fault fault);

        #endregion

        #region Potential Assessment

        [OperationContract]
        List<StaffAssessment> GetStaffAssessments(string staffId, int periodId, bool isLoadingAppraisee, out Fault fault);
        [OperationContract]
        bool IsStaffLevelExistInAssessment(string levelId, int periodId, out Fault fault);

        #endregion

        #region INPS Excel

        [OperationContract]
        List<Inps> ReadInpsExcel(string fileName, byte[] bytes, Period period, InpsType inpsType, out Fault fault);
        [OperationContract]
        bool SaveInps(List<Inps> inpss, out Fault fault);
        [OperationContract]
        List<Inps> GetAllInpsByPeriod(Period period, out Fault fault);

        #endregion

        #region INPS Setup

        [OperationContract]
        bool AddInps(Inps inps, out Fault fault);
        [OperationContract]
        List<Inps> GetAllInps(out Fault fault);
        [OperationContract]
        bool RemoveInps(Inps inps, out Fault fault);
        [OperationContract]
        bool ModifyInps(Inps inps, out Fault fault);
        [OperationContract]
        List<Inps> GetInpsByPeriod(Period period, out Fault fault);
        [OperationContract]
        List<Inps> GetInpsByPeriodAndType(Period period, InpsType inpsType, out Fault fault);

        #endregion

        #region INPS Rating Setup

        [OperationContract]
        bool AddInpsRating(List<InpsRating> inpsRatings, out Fault fault);
        [OperationContract]
        List<InpsRating> GetAllInpsRating(Period period, out Fault fault);
        [OperationContract]
        bool RemoveInpsRating(List<InpsRating> inpsRatings, out Fault fault);
        [OperationContract]
        bool ModifyInpsRating(List<InpsRating> inpsRatings, out Fault fault);
        [OperationContract]
        bool RemoveInpsRatingByPeriod(Period period, out Fault fault);
        [OperationContract]
        List<InpsRating> GetInpsRatingByPeriodAndType(Period period, InpsType inpsType, out Fault fault);

        #endregion

        #region INPS TYPE Setup

        [OperationContract]
        List<InpsType> GetAllInpsType(out Fault fault);
        [OperationContract]
        bool AddInpsType(InpsType inpsType, out Fault fault);
        [OperationContract]
        bool ModifyInpsType(InpsType inpsType, out Fault fault);
        [OperationContract]
        bool RemoveInpsType(InpsType inpsType, out Fault fault);

        #endregion


    }





}
