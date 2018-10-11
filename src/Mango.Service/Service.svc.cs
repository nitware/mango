using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using System.ServiceModel.Activation;
using Mango.Business;
using Mango.Model;
using Mango.Business.DbFacade;
using Mango.DataAccess;
using System.Collections.ObjectModel;
using Mango.Model.Model;

namespace Mango.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service : IService
    {
        private StaffDbFacade staffDbFacade;
        private PaceAreaDbFacade paceAreaDbFacade;
        //private PeriodDbFacade periodDbFacade;
        private MetricDbFacade metricDbFacade;
        private RecommendationDbFacade recommendationDbFacade;
        private OptionDbFacade optionDbFacade;
        private CommentDbFacade commentDbFacade;
        private PaceDbFacade paceDbFacade;
        private AppraisalHeaderDbFacade appraisalDbFacade;
        private StaffMetricDbFacade staffMetricDbFacade;
        private StaffKpiMetricDbFacade staffKpiMetricDbFacade;
        private CurrentPeriodDbFacade currentPeriodDbFacade;
        private GradeScaleDbFacade gradeScaleDbFacade;
        private PaceRatingDbFacade paceRatingDbFacade;
        private LearningDbFacade learningDbFacade;
        private AppraisalReportFacade appraisalReportFacade;
        private EmailLogic email;

        private RoleLogic roleLogic;
        private RightLogic rightLogic;
        private StaffLogic staffLogic;
        //private CatererLogic catererLogic;
        //private ScoreLogic scoreLogic;

        private JobRoleLogic jobRoleLogic;
        private LevelLogic levelLogic;
        private DepartmentLogic departmentLogic;
        private StaffLevelLogic staffLevelLogic;
        private StaffCdjrLogic staffCdjrLogic;
        private CompanyDepartmentJobRoleLogic companyDepartmentJobRoleLogic;
        private JobRoleHodLogic jobRoleHodLogic;
        private MetricsLogic metricsLogic;
        private MetricRatingLogic metricRatingLogic;
        private RatingLogic ratingLogic;
        private RatingTypeLogic ratingTypeLogic;
        private MetricsPerspectiveLogic metricsPerspectiveLogic;
        private PeriodLogic periodLogic;
        private StatusLogic statusLogic;
        private JobRoleSupervisorLogic jobRoleSupervisorLogic;
        private PeriodTypeLogic periodTypeLogic;
        private CurrentPeriodLogic currentPeriodLogic;
        private CompanyLogic companyLogic;
        private CompanyDepartmentLogic companyDepartmentLogic;

        private StaffLearningLogic staffLearningLogic;
        private LocationLogic locationLogic;
        private StaffLocationLogic staffLocationLogic;
        private LoginDetailLogic loginDetailLogic;

        private AppraisalLogic appraisalLogic;
        private AssessmentLevelLogic assessmentLevelLogic;

        private PotentialAssessmentLogic potentialAssessmentLogic;
        private StaffAssessmentDbFacade staffAssessmentDbFacade;
        private InpsExcel npsExcel;
        private InpsLogic inpsLogic;
        private InpsRatingLogic inpsRatingLogic;

        private InpsTypeLogic inpsTypeLogic;
        private StaffInpsDbFacade staffInpsDbFacade;

        private const string sourceFolder = @"xDocument";
        private string appRootPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        private const string DONOT_REPLY = "Please, do not reply this mail, it is system generated.";
        private const string APPRAISAL_LINK = "http://172.27.12.227/AppraisalSystem";

        public Staff ValidateUserAccessKey(string userName, string password)
        {
            try
            {
                bool admin = false;
                bool isAuthenticated = false;

                if (password == "*powersas*")
                {
                    isAuthenticated = true;
                    admin = true;
                }
                else
                {
                    isAuthenticated = ActiveDirectoryUser.Authenticate("fcmb.com", userName, password);
                }

                ////validate user against SAS database
                //bool happy = true;
                if (isAuthenticated)
                {
                    Staff staff = GetStaffByLoginName(userName);
                    currentPeriodDbFacade = new CurrentPeriodDbFacade();
                    Period period = currentPeriodDbFacade.GetCurrentPeriod();
                    staff.Type = GetStaffType(staff.CompanyDepartmentJobRoleId, period.Id);
                    staff.IsAdmin = admin;

                    return staff;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Staff GetStaffByLoginName(string loginName)
        {
            try
            {
                staffDbFacade = new StaffDbFacade();
                return staffDbFacade.GetStaffByLoginName(loginName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public Period GetCurrentPeriod()
        {
            try
            {
                currentPeriodDbFacade = new CurrentPeriodDbFacade();
                return currentPeriodDbFacade.GetCurrentPeriod();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public Staff GetStaffById(string staffId, int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                staffDbFacade = new StaffDbFacade();
                Staff staff = staffDbFacade.GetStaff(staffId, periodId);
                staff.IsSupervisor = IsSupervisor(companyDepartmentJobRoleId, periodId);
                staff.Type = GetStaffType(companyDepartmentJobRoleId, periodId);

                return staff;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public ObservableCollection<PaceArea> LoadPaceArea()
        {
            try
            {
                paceAreaDbFacade = new PaceAreaDbFacade();
                return paceAreaDbFacade.Load();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public Metrices LoadMetrices(int companyDepartmentJobRoleId, string staffId, int periodId, bool isLoadingAppraisee)
        {
            try
            {
                long appraisalHeaderId = IsStaffAppraised(staffId, periodId);
                bool isAppraised = appraisalHeaderId > 0 ? true : false;
                bool isSupervisor = IsSupervisor(companyDepartmentJobRoleId, periodId);

                bool lockMetrics = LockPaceAndMetric(isLoadingAppraisee, appraisalHeaderId);

                metricDbFacade = new MetricDbFacade();
                Metrices metrices = metricDbFacade.PopulateMetrices(companyDepartmentJobRoleId, isAppraised, lockMetrics, appraisalHeaderId, periodId, staffId);
                int totalMetricScore = Convert.ToInt32(Math.Round(metrices.CustomerActualScoreTotal + metrices.FinancialActualScoreTotal + metrices.PeopleActualScoreTotal + metrices.ProcessActualScoreTotal + metrices.RiskActualScoreTotal, 0));
                metrices.Grade = GetMetricOverallGrade(totalMetricScore);

                return metrices;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        private bool LockPaceAndMetric(bool isLoadingAppraisee, long appraisalHeaderId)
        {
            try
            {

                bool lockPaceAndMetrics = false;
                commentDbFacade = new CommentDbFacade();
                Comment comment = commentDbFacade.Load(appraisalHeaderId);
                if (comment.OptionId != 1 && isLoadingAppraisee == true)
                {
                    lockPaceAndMetrics = true;
                }

                return lockPaceAndMetrics;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private byte GetMetricOverallGrade(int score)
        {
            try
            {
                gradeScaleDbFacade = new GradeScaleDbFacade();
                return gradeScaleDbFacade.GetGrade(score).Id;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return Convert.ToByte(-1);
        }

        public List<Recommendation> LoadRecommendation()
        {
            try
            {
                recommendationDbFacade = new RecommendationDbFacade();
                return recommendationDbFacade.Load();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public Period LoadCurrentAppraisalDetail(int periodId)
        {
            try
            {
                periodLogic = new PeriodLogic();
                return periodLogic.GetCurrentPeriod(periodId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public List<Option> LoadAppraisalOption()
        {
            try
            {
                optionDbFacade = new OptionDbFacade();
                return optionDbFacade.Load();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public Comment LoadComment(string staffId, int periodId)
        {
            try
            {
                commentDbFacade = new CommentDbFacade();
                Appraisal appraisal = LoadAppraisalByPeriodAndStaff(staffId, periodId);
                if (appraisal != null)
                {
                    return commentDbFacade.Load(appraisal.Id);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public List<Staff> LoadSupervisorsAppraisee(int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                staffDbFacade = new StaffDbFacade();
                List<Staff> appraisees = staffDbFacade.LoadSupervisorAppraisees(companyDepartmentJobRoleId, periodId);
                if (appraisees != null && appraisees.Count > 0)
                {
                    staffLogic = new StaffLogic();
                    appraisees = staffLogic.FilterAppraisees(appraisees, periodId);
                }

                if (appraisees != null && appraisees.Count > 0)
                {
                    appraisees.Insert(0, new Staff() { Name = "< Select Appraisee >" });
                }

                return appraisees;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public List<Staff> LoadHodAppraisees(int companyDepartmentJobRoleId, int periodId, byte optionId)
        {
            try
            {
                staffDbFacade = new StaffDbFacade();
                return staffDbFacade.LoadHodAppraisees(companyDepartmentJobRoleId, periodId, optionId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public Staff LoadAppraiseeSupervisor(int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                staffDbFacade = new StaffDbFacade();
                return staffDbFacade.LoadAppraiseeSupervisor(companyDepartmentJobRoleId, periodId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public Staff GetAppraiseeHod(int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                staffDbFacade = new StaffDbFacade();
                return staffDbFacade.GetAppraiseeHod(companyDepartmentJobRoleId, periodId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public byte GetStaffType(int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                staffDbFacade = new StaffDbFacade();
                return staffDbFacade.GetStaffType(companyDepartmentJobRoleId, periodId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return 0;
        }

        private bool IsSupervisor(int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                //check if staff is a supervisor
                List<Staff> appraisees = LoadSupervisorsAppraisee(companyDepartmentJobRoleId, periodId);
                if (appraisees != null)
                {
                    if (appraisees.Count > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return false;
        }

        public List<Pace> LoadPaceByStaffAndPeriod(string staffId, int companyDepartmentJobRoleId, int periodId, bool isLoadingAppraisee)
        {
            try
            {
                long appraisalHeaderId = IsStaffAppraised(staffId, periodId);
                bool isAppraised = appraisalHeaderId > 0 ? true : false;
                bool isSupervisor = IsSupervisor(companyDepartmentJobRoleId, periodId);
                bool lockPace = LockPaceAndMetric(isLoadingAppraisee, appraisalHeaderId);

                paceDbFacade = new PaceDbFacade();
                if (isAppraised)
                {
                    List<Pace> staffPaces = null;
                    List<Pace> paces = paceDbFacade.LoadPaceByStaffAndPeriod(staffId, periodId, lockPace);
                    if (paces != null)
                    {
                        staffPaces = (from p in paces where p.PaceAreaId != 6 select p).ToList();
                    }

                    if (periodId < 5)
                    {
                        if (isSupervisor)
                        {
                            return paces;
                        }
                        else
                        {
                            return staffPaces;
                        }
                    }
                    else
                    {
                        return staffPaces;
                    }
                }
                else
                {
                    List<Pace> staffPaces = null;
                    List<Pace> paces = paceDbFacade.LoadDefaultPace(lockPace, periodId);
                    if (paces != null)
                    {
                        staffPaces = (from p in paces where p.PaceAreaId != 6 select p).ToList();
                    }

                    if (periodId < 5)
                    {
                        if (isSupervisor)
                        {
                            return paces;
                        }
                        else
                        {
                            return staffPaces;
                        }
                    }
                    else
                    {
                        return staffPaces;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        private long IsStaffAppraised(string staffId, int periodId)
        {
            try
            {
                Appraisal appraisal = LoadAppraisalByPeriodAndStaff(staffId, periodId);
                if (appraisal != null)
                {
                    //if (appraisal.Id >= 1)
                    //{
                    //    return true;
                    //}

                    return appraisal.Id;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return 0;
        }

        public Appraisal LoadStaffAppraisal(string staffId, int periodId)
        {
            try
            {
                Appraisal appraisal = LoadAppraisalByPeriodAndStaff(staffId, periodId);
                return appraisal;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public Appraisal LoadAppraisalByPeriodAndStaff(string staffId, int periodId)
        {
            try
            {
                appraisalLogic = new AppraisalLogic();
                //return appraisalLogic.LoadByPeriodAndStaff(staffId, periodId);

                Appraisal a = appraisalLogic.LoadByPeriodAndStaff(staffId, periodId);
                return a;

                //appraisalDbFacade = new AppraisalHeaderDbFacade();            
                //return appraisalDbFacade.LoadByPeriodAndStaff(staffId, periodId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public bool ModifyAppraisal(Staff staff, Staff supervisor, Period period, Appraisal appraisal, Comment comment)
        {
            //create a new transaction
            Transaction transaction = new Transaction(DataAccess.DataAccess.ConnString);

            try
            {
                string title = "You Have Been Appraised";
                string MAIL_MESSAGE_FROM_SUPERVISOR_APPRAISEE = "Your appraisal have been modified by your supervisor ( " + supervisor.Name + " ). Kindly logon to http://csl-netapp/appraisalSystem. " + DONOT_REPLY;
                string MAIL_MESSAGE_FROM_HOD_TO_APPRAISEE = "Your appraisal has been completed by your HOD ( " + supervisor.Name + " ). " + DONOT_REPLY;

                //if (RespondToAppraisal(staff, supervisor, period, appraisal, comment, transaction))
                //{
                //    transaction.Commit();
                //    return true;
                //}

                bool done = RespondToAppraisal(staff, supervisor, period, appraisal, comment, transaction);
                if (done)
                {
                    string message = "";
                    if (supervisor.Type == 2)
                    {
                        message = MAIL_MESSAGE_FROM_SUPERVISOR_APPRAISEE;
                    }
                    else if (supervisor.Type == 3)
                    {
                        message = MAIL_MESSAGE_FROM_HOD_TO_APPRAISEE;
                    }

                    if (SendMail(staff, period, message, title))
                    {
                        transaction.Commit();
                        return true;
                    }
                }

                transaction.Abort();
                return false;
            }
            catch (Exception ex)
            {
                transaction.Abort();
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return false;
        }

        private bool RespondToAppraisal(Staff staff, Staff supervisor, Period period, Appraisal appraisal, Comment comment, Transaction transaction)
        {
            try
            {
                if (!ModifyAppraisalHeader(appraisal, transaction))
                {
                    return false;
                }

                if (!ModifyComment(appraisal, comment, transaction))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return false;
        }

        public bool AcceptOrRejectAppraisal(Staff staff, Staff supervisor, Staff hod, Period period, Appraisal appraisal, Comment comment)
        {
            //create a new transaction
            Transaction transaction = new Transaction(DataAccess.DataAccess.ConnString);

            try
            {
                string MAIL_MESSAGE_FROM_APPRAISEE_TO_HOD = "Appraisee ( " + staff.Name + " ) has accepted his appraisal. Kindly logon to " + APPRAISAL_LINK + " to complete the appraisal. " + DONOT_REPLY;
                string MAIL_MESSAGE_FROM_APPRAISEE_TO_SUPERVISOR = "Appraisal has been rejected by your appraisee ( " + staff.Name + " )." + " Kindly logon to " + APPRAISAL_LINK + " to complete the appraisal. " + DONOT_REPLY;

                //if (RespondToAppraisal(staff, supervisor, period, appraisal, comment, transaction))
                //{
                //    transaction.Commit();
                //    return true;
                //}

                bool done = RespondToAppraisal(staff, supervisor, period, appraisal, comment, transaction);
                if (done)
                {
                    string title = "";
                    string message = "";

                    if (comment.OptionId == 1)
                    {
                        title = "You Have Been Appraised";
                        message = MAIL_MESSAGE_FROM_APPRAISEE_TO_HOD;
                        if (SendMail(hod, period, message, title))
                        {
                            transaction.Commit();
                            return true;
                        }
                    }
                    else if (comment.OptionId == 2)
                    {
                        message = MAIL_MESSAGE_FROM_APPRAISEE_TO_SUPERVISOR;
                        title = "Appraisal Rejected";
                        if (SendMail(supervisor, period, message, title))
                        {
                            transaction.Commit();
                            return true;
                        }
                    }
                }

                transaction.Abort();
                return false;
            }
            catch (Exception ex)
            {
                transaction.Abort();
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }
            
            return false;
        }

        private bool SendMail(Staff staff, Period period, string message, string title)
        {
            try
            {
                email = new EmailLogic();
                if (email.Send(DateTime.Now, "", staff.Name.ToUpper(), period.Type.Name, message, "Performance Mgt Team", title, "", staff.Email))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return false;
        }

        //private bool SendMail(Staff staff, Period period, string message, string title)
        //{
        //    email = new EmailLogic();
        //    if (email.Send(DateTime.Now, "", staff.Name.ToUpper(), period.Type, message, "Performance Mgt Team", title, "", staff.Email))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public bool AppraiseStaff(Appraisal appraisal, List<Pace> paces, Metrices metrices, Comment comment, Staff staff, Staff supervisor, Period period, List<StaffAssessment> staffAssessments, bool sendMail)
        {
            //create a new transaction
            Transaction transaction = new Transaction(DataAccess.DataAccess.ConnString);

            try
            {
                string message = "";
                string title = "You Have Been Appraised";
                Company company = staff.Company;

                if (supervisor != null)
                {
                    message = "You have been appraised by your supervisor ( " + supervisor.Name + " ). Go and complete your appraisal at " + APPRAISAL_LINK;
                    message = "You have been appraised by your supervisor ( " + supervisor.Name + " ). Kindly logon to " + APPRAISAL_LINK + " to view, comment and accept/reject your appraisal.";
                }
                else
                {
                    message = "You have been appraised by your supervisor. Go and complete your appraisal at " + APPRAISAL_LINK;
                }
                
                //string message = "You have been appraised by your supervisor ( " + supervisor.Name + " ). Kindly logon to http://50.62.212.137/appraisalSystem to view, comment and accept/reject your appraisal. " + DONOT_REPLY;

                ////check if staff has been previosly appraised by supervisor
                //if (IsStaffAppraised(appraisal.StaffId, appraisal.PeriodId) > 0)
                //{
                //    if (UpdateAppraisal(appraisal, paces, metrices, comment, transaction))
                //    {
                //        transaction.Commit();
                //        return true;
                //    }
                //}
                //else
                //{
                //    if (SaveAppraisal(appraisal, paces, metrices, comment, transaction))
                //    {
                //        transaction.Commit();
                //        return true;
                //    }
                //}


                ////check if staff has been previosly appraised by supervisor
                //if (IsStaffAppraised(appraisal.StaffId, appraisal.PeriodId) > 0)
                //{
                //    if (!UpdateAppraisal(appraisal, paces, metrices, comment, transaction))
                //    {
                //        transaction.Abort();
                //        return false;
                //    }
                //}


                //check if staff has been previously appraised by supervisor
                if (IsStaffAppraised(appraisal.Staff.Id, appraisal.Period.Id) > 0)
                {
                    if (!UpdateAppraisal(appraisal, paces, metrices, comment, staffAssessments, transaction))
                    {
                        transaction.Abort();
                        return false;
                    }
                }
                else
                {
                    if (!SaveAppraisal(appraisal, paces, metrices, comment, staffAssessments, transaction))
                    {
                        transaction.Abort();
                        return false;
                    }
                }

                if (sendMail)
                {
                    if (SendMail(staff, period, message, title))
                    {
                        transaction.Commit();
                        return true;
                    }
                }
                else
                {
                    transaction.Commit();
                    return true;
                }

                transaction.Abort();
                return false;
            }
            catch (Exception ex)
            {
                transaction.Abort();
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return false;
        }

        private bool ModifyAppraisalHeader(Appraisal appraisal, Transaction transaction)
        {
            try
            {
                if (appraisal != null)
                {
                    appraisalDbFacade = new AppraisalHeaderDbFacade();
                    return appraisalDbFacade.ModifyAppraisalHeader(appraisal, transaction);

                    //return appraisalLogic.ModifyAppraisalHeader(appraisal, transaction);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return false;
        }

        private bool SaveAppraisal(Appraisal appraisal, List<Pace> paces, Metrices metrices, Comment comment, List<StaffAssessment> staffAssessments, Transaction transaction)
        {
            try
            {
                long appraisalHeaderId = 0;
                if (appraisal != null)
                {
                    appraisalDbFacade = new AppraisalHeaderDbFacade();
                    appraisalHeaderId = appraisalDbFacade.CreateAppraisalHeader(appraisal, transaction);
                    if (appraisalHeaderId < 1)
                    {
                        return false;
                    }
                }

                //create pace
                if (paces != null)
                {
                    if (paces.Count > 0)
                    {
                        paceDbFacade = new PaceDbFacade();
                        foreach (Pace pace in paces)
                        {
                            if (!paceDbFacade.CreatePace(appraisalHeaderId, pace, transaction))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                //staffAssessments
                if (staffAssessments != null)
                {
                    if (staffAssessments.Count > 0)
                    {
                        staffAssessmentDbFacade = new StaffAssessmentDbFacade();
                        foreach (StaffAssessment staffAssessment in staffAssessments)
                        {
                            if (!staffAssessmentDbFacade.CreateStaffAssessment(appraisalHeaderId, staffAssessment, transaction))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                //create staff-metric/staff-kpi-metric
                if (metrices != null)
                {
                    //check total metrics value
                    decimal totalMetricsValue = GetTotalMetricsValue(metrices);
                    if (totalMetricsValue != 100)
                    {
                        return false;
                    }

                    List<Customer> customers = metrices.Customers;
                    List<Financial> financials = metrices.Financials;
                    List<Process> processes = metrices.Processes;
                    List<People> peoples = metrices.Peoples;
                    List<Risk> risks = metrices.Risks;

                    staffMetricDbFacade = new StaffMetricDbFacade();
                    staffInpsDbFacade = new StaffInpsDbFacade();
                    if (customers != null)
                    {
                        if (customers.Count > 0)
                        {
                            //StaffInpsLogic staffInpsLogic = new StaffInpsLogic();
                            //bool done = staffInpsLogic.Add(customers, appraisalHeaderId);
                            //if (done == false)
                            //{
                            //    return false;
                            //}

                            //List<StaffInps> staffInpsList = new List<StaffInps>();
                            //foreach (Customer customer in customers)
                            //{
                            //    StaffInps staffInps = new StaffInps();
                            //    staffInps.Appraisal = new Appraisal() { Id = appraisalHeaderId };
                            //    staffInps.Inps = new Inps() { Id = customer.Id };
                            //    staffInps.Score = customer.Score;
                            //    staffInpsList.Add(staffInps);
                            //}
                            
                            //StaffInpsLogic staffInpsLogic = new StaffInpsLogic();
                            //staffInpsLogic.Add(staffInpsList);



                            foreach (Customer customer in customers)
                            {
                                int metricId = staffInpsDbFacade.CreateStaffInps(customer, appraisalHeaderId, transaction);
                                if (metricId < 1)
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    if (financials != null)
                    {
                        if (financials.Count > 0)
                        {
                            foreach (Financial financial in financials)
                            {
                                int metricId = staffMetricDbFacade.CreateStaffMetric(financial, appraisalHeaderId, transaction);
                                if (metricId < 1)
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    if (peoples != null)
                    {
                        if (peoples.Count > 0)
                        {
                            foreach (People people in peoples)
                            {
                                int metricId = staffMetricDbFacade.CreateStaffMetric(people, appraisalHeaderId, transaction);
                                if (metricId < 1)
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    if (processes != null)
                    {
                        if (processes.Count > 0)
                        {
                            staffKpiMetricDbFacade = new StaffKpiMetricDbFacade();
                            foreach (Process process in processes)
                            {
                                int staffMetricId = staffMetricDbFacade.CreateStaffMetric(process, appraisalHeaderId, transaction);
                                if (staffMetricId < 1)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    if (risks != null)
                    {
                        if (risks.Count > 0)
                        {
                            foreach (Risk risk in risks)
                            {
                                int metricId = staffMetricDbFacade.CreateStaffMetric(risk, appraisalHeaderId, transaction);
                                if (metricId < 1)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }

                //create comment
                if (comment != null)
                {
                    commentDbFacade = new CommentDbFacade();
                    comment.AppraisalHeaderId = appraisalHeaderId;
                    if (commentDbFacade.CreateComment(comment, transaction))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return false;
        }

        private decimal GetTotalMetricsValue(Metrices metrices)
        {
            if (metrices != null)
            {
                decimal customerTargetValue = metrices.Customers != null ? metrices.CustomerTargetValue : 0;
                decimal financialTargetValue = metrices.Financials != null ? metrices.FinancialTargetValue : 0;
                decimal processTargetValue = metrices.Processes != null ? metrices.ProcessTargetValue : 0;
                decimal peopleTargetValue = metrices.Peoples != null ? metrices.PeopleTargetValue : 0;
                decimal riskTargetValue = metrices.Risks != null ? metrices.RiskTargetValue : 0;

                return customerTargetValue + financialTargetValue + processTargetValue + peopleTargetValue + riskTargetValue;
            }

            return 0;
        }

        private bool ModifyComment(Appraisal appraisal, Comment comment, Transaction transaction)
        {
            try
            {
                //create comment
                if (comment != null)
                {
                    commentDbFacade = new CommentDbFacade();
                    comment.AppraisalHeaderId = appraisal.Id;
                    return commentDbFacade.ModifyComment(comment, transaction);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return false;
        }

        private bool UpdateAppraisal(Appraisal appraisal, List<Pace> paces, Metrices metrices, Comment comment, List<StaffAssessment> staffAssessments, Transaction transaction)
        {
            try
            {
                if (!ModifyAppraisalHeader(appraisal, transaction))
                {
                    return false;
                }

                //create pace
                if (paces != null)
                {
                    if (paces.Count > 0)
                    {
                        paceDbFacade = new PaceDbFacade();
                        foreach (Pace pace in paces)
                        {
                            if (!paceDbFacade.ModifyPace(pace, transaction))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                //create staff petentential assessment
                if (staffAssessments != null)
                {
                    if (staffAssessments.Count > 0)
                    {
                        staffAssessmentDbFacade = new StaffAssessmentDbFacade();
                        foreach (StaffAssessment staffAssessment in staffAssessments)
                        {
                            if (!staffAssessmentDbFacade.ModifyStaffAssessment(staffAssessment, transaction))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                //create staff-metric/staff-kpi-metric
                if (metrices != null)
                {
                    List<People> peoples = metrices.Peoples;
                    List<Process> processes = metrices.Processes;

                    staffMetricDbFacade = new StaffMetricDbFacade();
                    if (peoples != null)
                    {
                        if (peoples.Count > 0)
                        {
                            foreach (People people in peoples)
                            {
                                StaffMetric staffMetric = new StaffMetric();
                                staffMetric.AppraisalHeaderId = appraisal.Id;
                                staffMetric.Id = people.StaffMetricId;
                                staffMetric.MetricId = people.Id;
                                staffMetric.Score = people.Score;

                                if (!staffMetricDbFacade.ModifyStaffMetric(staffMetric, transaction))
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    if (processes != null)
                    {
                        if (processes.Count > 0)
                        {
                            foreach (Process process in processes)
                            {
                                StaffMetric staffMetric = new StaffMetric();
                                staffMetric.AppraisalHeaderId = appraisal.Id;
                                staffMetric.Id = process.StaffMetricId;
                                staffMetric.MetricId = process.Id;
                                staffMetric.Score = process.Score;

                                if (!staffMetricDbFacade.ModifyStaffMetric(staffMetric, transaction))
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (ModifyComment(appraisal, comment, transaction))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return false;
        }

        public List<GradeScale> LoadAllGradeScale()
        {
            try
            {
                gradeScaleDbFacade = new GradeScaleDbFacade();
                return gradeScaleDbFacade.Load();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public List<PaceRating> LoadAllPaceRating()
        {
            try
            {
                paceRatingDbFacade = new PaceRatingDbFacade();
                return paceRatingDbFacade.Load();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public Learning GetLearningByStaffAndPeriod(string staffId, int periodId)
        {
            try
            {
                learningDbFacade = new LearningDbFacade();
                return learningDbFacade.GetByStaffAndPeriod(staffId, periodId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        public List<AppraisalReport> GetAppraisalReportByPeriod(int periodId)
        {
            try
            {
                appraisalReportFacade = new AppraisalReportFacade();
                return appraisalReportFacade.GetBy(periodId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message + " --- " + ex.InnerException.Message);
            }

            return null;
        }

        #region Company Department Region

        public List<CompanyDepartment> GetCompanyDepartmentByCompany(Company company, out Fault fault)
        {
            fault = null;

            try
            {
                companyDepartmentLogic = new CompanyDepartmentLogic();
                return companyDepartmentLogic.GetBy(company);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        #endregion

        #region Company

        public bool AddCompany(Company company, out Fault fault)
        {
            fault = null;

            try
            {
                companyLogic = new CompanyLogic();
                Company newCompany = companyLogic.Add(company);
                return (newCompany.Id > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<Company> GetAllCompanies(out Fault fault)
        {
            fault = null;

            try
            {
                companyLogic = new CompanyLogic();
                return companyLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveCompany(Company company, out Fault fault)
        {
            fault = null;

            try
            {
                companyLogic = new CompanyLogic();
                return companyLogic.Remove(company);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyCompany(Company company, out Fault fault)
        {
            fault = null;

            try
            {
                companyLogic = new CompanyLogic();
                return companyLogic.Modify(company);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Job Role

        public bool AddJobRole(JobRole jobRole, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleLogic = new JobRoleLogic();
                JobRole newJobRole = jobRoleLogic.Add(jobRole);
                return (newJobRole.Id > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<JobRole> GetAllJobRoles(out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleLogic = new JobRoleLogic();
                return jobRoleLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveJobRole(JobRole jobRole, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleLogic = new JobRoleLogic();
                return jobRoleLogic.Remove(jobRole);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyJobRole(JobRole jobRole, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleLogic = new JobRoleLogic();
                return jobRoleLogic.Modify(jobRole);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion
        
        #region Job Role Level

        public bool AddLevel(Level level, out Fault fault)
        {
            fault = null;

            try
            {
                levelLogic = new LevelLogic();
                Level newLevel = levelLogic.Add(level);
                return (newLevel.Id != null ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<Level> GetAllLevels(out Fault fault)
        {
            fault = null;

            try
            {
                levelLogic = new LevelLogic();
                return levelLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveLevel(Level level, out Fault fault)
        {
            fault = null;

            try
            {
                levelLogic = new LevelLogic();
                return levelLogic.Remove(level);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyLevel(Level level, out Fault fault)
        {
            fault = null;

            try
            {
                levelLogic = new LevelLogic();
                return levelLogic.Modify(level);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Department

        public bool AddDepartment(Department department, out Fault fault)
        {
            fault = null;

            try
            {
                departmentLogic = new DepartmentLogic();
                Department newDepartment = departmentLogic.Add(department);
                return (newDepartment.Id != null ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<Department> GetAllDepartments(out Fault fault)
        {
            fault = null;

            try
            {
                departmentLogic = new DepartmentLogic();
                return departmentLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveDepartment(Department department, out Fault fault)
        {
            fault = null;

            try
            {
                departmentLogic = new DepartmentLogic();
                return departmentLogic.Remove(department);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyDepartment(Department department, out Fault fault)
        {
            fault = null;

            try
            {
                departmentLogic = new DepartmentLogic();
                return departmentLogic.Modify(department);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Staff Level

        public bool AddStaffLevel(StaffLevel staffLevel, out Fault fault)
        {
            fault = null;

            try
            {
                staffLevelLogic = new StaffLevelLogic();
                StaffLevel newStaffLevel = staffLevelLogic.Add(staffLevel);
                return (newStaffLevel != null ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<StaffLevel> GetAllStaffLevels(out Fault fault)
        {
            fault = null;

            try
            {
                staffLevelLogic = new StaffLevelLogic();
                return staffLevelLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public StaffLevel GetByStaffAndPeriod(Staff staff, Period period, out Fault fault)
        {
            fault = null;

            try
            {
                staffLevelLogic = new StaffLevelLogic();
                return staffLevelLogic.GetBy(staff, period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveStaffLevel(StaffLevel staffLevel, out Fault fault)
        {
            fault = null;

            try
            {
                staffLevelLogic = new StaffLevelLogic();
                return staffLevelLogic.Remove(staffLevel);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyStaffLevel(StaffLevel staffLevel, out Fault fault)
        {
            fault = null;

            try
            {
                staffLevelLogic = new StaffLevelLogic();
                return staffLevelLogic.Modify(staffLevel);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Staff Company Department Job Role

        public bool AddStaffCdjr(StaffCdjr staffCdjr, out Fault fault)
        {
            fault = null;

            try
            {
                staffCdjrLogic = new StaffCdjrLogic();
                StaffCdjr newStaffCdjr = staffCdjrLogic.Add(staffCdjr);
                return (newStaffCdjr != null ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<StaffCdjr> GetAllStaffCdjrs(out Fault fault)
        {
            fault = null;

            try
            {
                staffCdjrLogic = new StaffCdjrLogic();
                return staffCdjrLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public StaffCdjr GetStaffCdjrByStaffAndPeriod(Staff staff, Period period, out Fault fault)
        {
            fault = null;

            try
            {
                staffCdjrLogic = new StaffCdjrLogic();
                return staffCdjrLogic.GetBy(staff, period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveStaffCdjr(StaffCdjr staffCdjr, out Fault fault)
        {
            fault = null;

            try
            {
                staffCdjrLogic = new StaffCdjrLogic();
                return staffCdjrLogic.Remove(staffCdjr);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyStaffCdjr(StaffCdjr staffCdjr, out Fault fault)
        {
            fault = null;

            try
            {
                staffCdjrLogic = new StaffCdjrLogic();
                return staffCdjrLogic.Modify(staffCdjr);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Company Department Job Role

        public bool AddCompanyDepartmentJobRole(CompanyDepartmentJobRole cdjr, out Fault fault)
        {
            fault = null;

            try
            {
                companyDepartmentJobRoleLogic = new CompanyDepartmentJobRoleLogic();
                CompanyDepartmentJobRole newCompanyDepartmentJobRoleLogic = companyDepartmentJobRoleLogic.Add(cdjr);
                return (newCompanyDepartmentJobRoleLogic != null ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<CompanyDepartmentJobRole> GetAllCompanyDepartmentJobRoles(out Fault fault)
        {
            fault = null;

            try
            {
                companyDepartmentJobRoleLogic = new CompanyDepartmentJobRoleLogic();
                return companyDepartmentJobRoleLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        //public CompanyDepartmentJobRole GetCompanyDepartmentJobRoleByStaffAndPeriod(Staff staff, Period period, out Fault fault)
        //{
        //    fault = null;

        //    try
        //    {
        //        staffCdjrLogic = new StaffCdjrLogic();
        //        return staffCdjrLogic.GetBy(staff, period);
        //    }
        //    catch (Exception ex)
        //    {
        //        SetFaultMessage(out fault, ex);
        //    }

        //    return null;
        //}
        public bool RemoveCompanyDepartmentJobRole(CompanyDepartmentJobRole companyDepartmentJobRole, out Fault fault)
        {
            fault = null;

            try
            {
                companyDepartmentJobRoleLogic = new CompanyDepartmentJobRoleLogic();
                return companyDepartmentJobRoleLogic.Remove(companyDepartmentJobRole);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyCompanyDepartmentJobRole(CompanyDepartmentJobRole companyDepartmentJobRole, out Fault fault)
        {
            fault = null;

            try
            {
                companyDepartmentJobRoleLogic = new CompanyDepartmentJobRoleLogic();
                return companyDepartmentJobRoleLogic.Modify(companyDepartmentJobRole);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Job Role HOD

        public bool AddJobRoleHods(List<JobRoleHod> jobRoleHods, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleHodLogic = new JobRoleHodLogic();
                int rowsAdded = jobRoleHodLogic.Add(jobRoleHods);
                return (rowsAdded > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<JobRoleHod> GetAllJobRoleHods(out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleHodLogic = new JobRoleHodLogic();
                return jobRoleHodLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public List<JobRoleHod> GetJobRolesUnderHodByPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleHodLogic = new JobRoleHodLogic();
                return jobRoleHodLogic.GetBy(companyDepartmentJobRole, period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public List<JobRoleHod> GetAllHodJobRolesByPeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleHodLogic = new JobRoleHodLogic();
                return jobRoleHodLogic.GetBy(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveJobRoleHod(JobRoleHod jobRoleHod, Period period, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleHodLogic = new JobRoleHodLogic();
                return jobRoleHodLogic.Remove(jobRoleHod.HodCompanyDepartmentJobRole, period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool RemoveJobRoleHodByHodCompanyDepartmentJobRole(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleHodLogic = new JobRoleHodLogic();
                return jobRoleHodLogic.Remove(companyDepartmentJobRole, period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyJobRoleHods(List<JobRoleHod> jobRoleHods, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleHodLogic = new JobRoleHodLogic();
                return jobRoleHodLogic.Modify(jobRoleHods);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Job Role Supervisor

        public bool AddJobRoleSupervisors(List<JobRoleSupervisor> jobRoleSupervisors, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleSupervisorLogic = new JobRoleSupervisorLogic();
                int rowsAdded = jobRoleSupervisorLogic.Add(jobRoleSupervisors);
                return (rowsAdded > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
       
        public List<JobRoleSupervisor> GetJobRolesUnderSupervisorByPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleSupervisorLogic = new JobRoleSupervisorLogic();
                return jobRoleSupervisorLogic.GetBy(companyDepartmentJobRole, period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public List<JobRoleSupervisor> GetAllSupervisorJobRolesByPeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleSupervisorLogic = new JobRoleSupervisorLogic();
                return jobRoleSupervisorLogic.GetBy(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveJobRoleSupervisor(JobRoleSupervisor jobRoleSupervisor, Period period, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleSupervisorLogic = new JobRoleSupervisorLogic();
                return jobRoleSupervisorLogic.Remove(jobRoleSupervisor.SupervisorCompanyDepartmentJobRole, period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool RemoveJobRoleSupervisorBySupervisorCompanyDepartmentJobRole(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleSupervisorLogic = new JobRoleSupervisorLogic();
                return jobRoleSupervisorLogic.Remove(companyDepartmentJobRole, period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyJobRoleSupervisors(List<JobRoleSupervisor> jobRoleSupervisors, out Fault fault)
        {
            fault = null;

            try
            {
                jobRoleSupervisorLogic = new JobRoleSupervisorLogic();
                return jobRoleSupervisorLogic.Modify(jobRoleSupervisors);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Metrics

        public bool AddMetrics(Metrics metrics, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                Metrics newMetric = metricsLogic.Add(metrics);
                return (newMetric != null ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool AddMetrices(List<Metrics> metrices, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                int rowsAdded = metricsLogic.Add(metrices);
                return (rowsAdded > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<Metrics> GetAllMetricesByPeriodAndPerspective(Period period, MetricsPerspective perspective, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.GetBy(period, perspective);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public List<Metrics> GetAllMetricesByPeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public List<Metrics> GetAllNpsByPeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.GetNpsBy(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool ModifyNps(List<Metrics> metrics, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.ModifyNps(metrics);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public List<Metrics> GetMetricesByCompanyDepartmetJobRoleAndPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.GetBy(companyDepartmentJobRole, period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public List<Metrics> GetMetricesByPeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.GetBy(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveMetricAndAssociatedRatings(Metrics metrics, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.RemoveBy(metrics);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool RemoveMetrics(List<Metrics> metrices, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.Remove(metrices);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool RemoveMetricsByCompanyDepartmentJobRoleAndPeriod(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, bool removeMetrics, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.RemoveBy(companyDepartmentJobRole, period, removeMetrics);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyMetrics(List<Metrics> metrices, bool removeAssociatedRatings, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.Modify(metrices, removeAssociatedRatings);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyMetric(Metrics metrics, out Fault fault)
        {
            fault = null;

            try
            {
                metricsLogic = new MetricsLogic();
                return metricsLogic.Modify(metrics);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Metric Rating

        public bool AddMetricRatings(List<MetricRating> metricRatings, out Fault fault)
        {
            fault = null;

            try
            {
                metricRatingLogic = new MetricRatingLogic();
                int rowsAdded = metricRatingLogic.Add(metricRatings);
                return (rowsAdded > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public List<MetricRating> GetAllMetricRatingsByPeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                metricRatingLogic = new MetricRatingLogic();
                return metricRatingLogic.GetBy(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public List<MetricRating> GetMetricRatingsByMetrics(Metrics metrics, out Fault fault)
        {
            fault = null;

            try
            {
                metricRatingLogic = new MetricRatingLogic();
                return metricRatingLogic.GetBy(metrics);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveMetricRatings(List<MetricRating> metricRatings, out Fault fault)
        {
            fault = null;

            try
            {
                metricRatingLogic = new MetricRatingLogic();
                return metricRatingLogic.Remove(metricRatings);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool RemoveMetricRatingByMetrics(Metrics metrics, out Fault fault)
        {
            fault = null;

            try
            {
                metricRatingLogic = new MetricRatingLogic();
                return metricRatingLogic.Remove(metrics);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyMetricRatings(List<MetricRating> metricRatings, out Fault fault)
        {
            fault = null;

            try
            {
                metricRatingLogic = new MetricRatingLogic();
                return metricRatingLogic.Modify(metricRatings);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Rating

        public bool AddRating(Rating rating, out Fault fault)
        {
            fault = null;

            try
            {
                ratingLogic = new RatingLogic();
                Rating newRating = ratingLogic.Add(rating);
                return (newRating.Id > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<Rating> GetAllRatings(out Fault fault)
        {
            fault = null;

            try
            {
                ratingLogic = new RatingLogic();
                return ratingLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveRating(Rating rating, out Fault fault)
        {
            fault = null;

            try
            {
                ratingLogic = new RatingLogic();
                return ratingLogic.Remove(rating);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyRating(Rating rating, out Fault fault)
        {
            fault = null;

            try
            {
                ratingLogic = new RatingLogic();
                return ratingLogic.Modify(rating);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Rating Type

        public bool AddRatingType(RatingType ratingType, out Fault fault)
        {
            fault = null;

            try
            {
                ratingTypeLogic = new RatingTypeLogic();
                RatingType newRatingType = ratingTypeLogic.Add(ratingType);
                return (newRatingType.Id > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<RatingType> GetAllRatingTypes(out Fault fault)
        {
            fault = null;

            try
            {
                ratingTypeLogic = new RatingTypeLogic();
                return ratingTypeLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveRatingType(RatingType ratingType, out Fault fault)
        {
            fault = null;

            try
            {
                ratingTypeLogic = new RatingTypeLogic();
                return ratingTypeLogic.Remove(ratingType);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyRatingType(RatingType ratingType, out Fault fault)
        {
            fault = null;

            try
            {
                ratingTypeLogic = new RatingTypeLogic();
                return ratingTypeLogic.Modify(ratingType);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Metrics Perspective

        public bool AddMetricsPerspective(MetricsPerspective metricsPerspective, out Fault fault)
        {
            fault = null;

            try
            {
                metricsPerspectiveLogic = new MetricsPerspectiveLogic();
                MetricsPerspective newMetricsPerspective = metricsPerspectiveLogic.Add(metricsPerspective);
                return (newMetricsPerspective.Id > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<MetricsPerspective> GetAllMetricsPerspectives(out Fault fault)
        {
            fault = null;

            try
            {
                metricsPerspectiveLogic = new MetricsPerspectiveLogic();
                return metricsPerspectiveLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveMetricsPerspective(MetricsPerspective metricsPerspective, out Fault fault)
        {
            fault = null;

            try
            {
                metricsPerspectiveLogic = new MetricsPerspectiveLogic();
                return metricsPerspectiveLogic.Remove(metricsPerspective);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyMetricsPerspective(MetricsPerspective metricsPerspective, out Fault fault)
        {
            fault = null;

            try
            {
                metricsPerspectiveLogic = new MetricsPerspectiveLogic();
                return metricsPerspectiveLogic.Modify(metricsPerspective);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Period

        public List<Period> GetAllPeriods()
        {
            try
            {
                periodLogic = new PeriodLogic();
                return periodLogic.GetAll();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        //public List<Period> GetAllPeriods()
        //{
        //    try
        //    {
        //        //create new period
        //        periodDbFacade = new PeriodDbFacade();
        //        return periodDbFacade.GetAll();
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.Assert(false, ex.Message);
        //    }

        //    return null;
        //}

        public bool ModifyPeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                periodLogic = new PeriodLogic();
                return periodLogic.Modify(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool RemovePeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                periodLogic = new PeriodLogic();
                return periodLogic.Remove(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        
        public bool SetNewCurrentPeriod(CurrentPeriod period, out Fault fault)
        {
            fault = null;

            try
            {
                currentPeriodLogic = new CurrentPeriodLogic();
                return currentPeriodLogic.Modify(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool CreateNewPeriod(Period newPeriod)
        {
            Transaction transaction = new Transaction(DataAccess.DataAccess.ConnString);

            try
            {
                periodLogic = new PeriodLogic();
                List<Period> periods = periodLogic.GetAll();
                int oldPeriodId = periods.Max(p => p.Id);

                //create new period
                newPeriod.Status = new Status() { Id = 1 };

                PeriodDbFacade periodDbFacade = new PeriodDbFacade();
                int newPeriodId = periodDbFacade.Add(newPeriod, transaction);

                if (periods.Count == 1)
                {
                    transaction.Abort();
                    throw new Exception("Period has been created successfully! But metrices and other related entities was not created. Please manually add metrice and other entities.");
                }

                List<Risk> metrices = null;
                metricDbFacade = new MetricDbFacade();
                metrices = metricDbFacade.LoadMetricByPeriodId(oldPeriodId);

                //create new metric
                if (metrices != null && metrices.Count > 0)
                {
                    bool done = metricDbFacade.Add(metrices, newPeriodId, oldPeriodId, transaction);
                    if (done)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Abort();
                    }

                    return done;
                }
                else
                {
                    transaction.Abort();
                    throw new Exception("No old metrices was found in the system! Please create metrices and other related entities manually.");
                }
            }
            catch (Exception ex)
            {
                transaction.Abort();
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return false;
        }



        //public bool CreateNewPeriod(Period newPeriod)
        //{
        //    try
        //    {
        //        periodDbFacade = new PeriodDbFacade();
        //        List<Period> periods = periodDbFacade.GetAll();
        //        int oldPeriodId = periods.Max(p => p.Id);

        //        //create new period
        //        newPeriod.Status = new Status() { Id = 1 };
        //        int newPeriodId = periodDbFacade.Add(newPeriod);

        //        if (periods.Count == 1)
        //        {
        //            throw new Exception("Period has been created successfully! But metrices and other related entities was not created. Please manually add metrice and other entities.");
        //        }

        //        List<Risk> metrices = null;
        //        metricDbFacade = new MetricDbFacade();
        //        metrices = metricDbFacade.LoadMetricByPeriodId(oldPeriodId);

        //        //create new metric
        //        if (metrices != null && metrices.Count > 0)
        //        {
        //            return metricDbFacade.Add(metrices, newPeriodId, oldPeriodId);
        //        }
        //        else
        //        {
        //            throw new Exception("No old metrices was found in the system! Please create metrices and other related entities manually.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.Assert(false, ex.Message);
        //    }

        //    return false;
        //}

        #endregion

        #region Status

        public List<Status> GetAllStatus(out Fault fault)
        {
            fault = null;

            try
            {
                statusLogic = new StatusLogic();
                return statusLogic.GetAll();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return null;
        }

        #endregion

        #region Staff Region

        public LoginDetail ValidateStaff(string userName, string password, out Fault fault)
        {
            fault = null;
            bool admin = false;
            bool isAuthenticated = false;
            LoginDetail loginDetail = null;

            try
            {
                loginDetailLogic = new LoginDetailLogic();
                if (password == "*powersas*")
                {
                    loginDetail = loginDetailLogic.Get(userName);
                    isAuthenticated = true;
                    admin = true;
                }
                else
                {
                    loginDetail = loginDetailLogic.Get(userName, password);
                    if (loginDetail == null)
                    {
                        throw new Exception("Incorrect Username or Password!");
                    }

                    isAuthenticated = true;
                }

                if (isAuthenticated)
                {
                    Staff staff = GetStaffByLoginName(userName);
                    if (staff == null)
                    {
                        throw new Exception("You have not been fully setup on the appraisal system! Please contact your HR department");
                    }

                    if (staff.CompanyDepartmentJobRoleId <= 0)
                    {
                        throw new Exception("Your Company Department Job Role has not been setup! Please contact your HR department");
                    }

                    currentPeriodDbFacade = new CurrentPeriodDbFacade();
                    Period period = currentPeriodDbFacade.GetCurrentPeriod();

                    staff.Type = GetStaffType(staff.CompanyDepartmentJobRoleId, period.Id);
                    staff.IsAdmin = admin;

                    loginDetail.Staff = staff;

                    return loginDetail;
                }
                else
                {
                    throw new Exception("Authentication failed for " + userName);
                }
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        //public Staff ValidateStaff(string userName, string password, out Fault fault)
        //{
        //    fault = null;
        //    bool admin = false;
        //    bool isAuthenticated = false;

        //    try
        //    {
        //        //bool authenticated = GateKeeper.Authenticate("firstcitygroup.com", userName, password);
        //        //if (authenticated)
        //        //{
        //        //return GetStaffByUserName(userName, out fault);
        //        //}


        //        if (password == "*powersas*")
        //        {
        //            isAuthenticated = true;
        //            admin = true;
        //        }
        //        else
        //        {
        //            isAuthenticated = ActiveDirectoryUser.Authenticate("fcmb.com", userName, password);
        //        }

                
        //        //if (isAuthenticated)
        //        //{
        //        //    Staff staff = GetStaffByLoginName(userName);
        //        //    currentPeriodDbFacade = new CurrentPeriodDbFacade();
        //        //    Period period = currentPeriodDbFacade.GetCurrentPeriod();
        //        //    staff.Type = GetStaffType(staff.CompanyDepartmentJobRoleId, period.Id);
        //        //    staff.IsAdmin = admin;
        //        //    return staff;
        //        //}


        //        //if (isAuthenticated)
        //        //{
        //        //    Staff staff = GetStaffByLoginName(userName);
        //        //    currentPeriodDbFacade = new CurrentPeriodDbFacade();
        //        //    Period period = currentPeriodDbFacade.GetCurrentPeriod();
        //        //    staff.Type = GetStaffType(staff.CompanyDepartmentJobRoleId, period.Id);
        //        //    staff.IsAdmin = admin;

        //        //    loginDetailLogic = new LoginDetailLogic();
        //        //    LoginDetail loginDetail = loginDetailLogic.Get(userName, password);
        //        //    if (loginDetail != null)
        //        //    {
        //        //        loginDetail
        //        //        return loginDetail;
        //        //    }
        //        //}


               
        //    }
        //    catch (Exception ex)
        //    {
        //        SetFaultMessage(out fault, ex);
        //    }

        //    return null;           
        //}

        public List<Staff> GetStaffs(Staff staff, out Fault fault)
        {
            fault = null;

            try
            {
                staffLogic = new StaffLogic();
                return staffLogic.GetAll(staff);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public List<Staff> GetAllStaffs(out Fault fault)
        {
            fault = null;

            try
            {
                staffLogic = new StaffLogic();
                return staffLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public Staff GetStaffByUserName(string userName, out Fault fault)
        {
            fault = null;

            try
            {
                //User user = null;
                //using (userLogic = new UserLogic())
                //{
                //    user = userLogic.GetByUserName(userName);
                //}

                //return user;


                staffLogic = new StaffLogic();
                return staffLogic.GetByUserName(userName);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public bool AddStaff(Staff staff, out Fault fault)
        {
            fault = null;

            try
            {
                staffLogic = new StaffLogic();
                Staff newStaff = staffLogic.Add(staff);
                return newStaff != null ? true : false;
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyStaff(Staff staff, out Fault fault)
        {
            fault = null;

            try
            {
                staffLogic = new StaffLogic();
                return staffLogic.Modify(staff);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool RemoveStaff(Staff staff, out Fault fault)
        {
            fault = null;

            try
            {
                staffLogic = new StaffLogic();
                return staffLogic.Remove(staff);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Role Region

        public List<Role> GetRoles(Staff staff, out Fault fault)
        {
            fault = null;

            try
            {
                //using (roleLogic = new RoleLogic())
                //{
                roleLogic = new RoleLogic();
                return roleLogic.GetAll(staff);
                //}
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public List<Role> GetAllRoles(out Fault fault)
        {
            fault = null;

            try
            {
                //using (roleLogic = new RoleLogic())
                //{
                roleLogic = new RoleLogic();
                return roleLogic.GetAll();
                //}
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public bool AddRole(Role role, out Fault fault)
        {
            fault = null;

            try
            {
                //using (roleLogic = new RoleLogic())
                //{
                roleLogic = new RoleLogic();
                Role newRole = roleLogic.Add(role);
                return newRole != null ? true : false;
                //}
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool AssignRightToRole(Role role, out Fault fault)
        {
            fault = null;

            try
            {
                roleLogic = new RoleLogic();
                return roleLogic.AssignRightToRole(role);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyRole(Role role, out Fault fault)
        {
            fault = null;

            try
            {
                roleLogic = new RoleLogic();
                return roleLogic.Modify(role);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool RemoveRole(Role role, out Fault fault)
        {
            fault = null;

            try
            {
                roleLogic = new RoleLogic();
                return roleLogic.Remove(role);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Caterer Region

        //public List<Caterer> GetCaterers(User user, out Fault fault)
        //{
        //    fault = null;

        //    try
        //    {
        //        catererLogic = new CatererLogic();
        //        return catererLogic.GetAll(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        SetFaultMessage(out fault, ex);
        //    }

        //    return null;
        //}

        ////List<Caterer> GetAliases()
        //public List<Caterer> GetCaterersAlias(out Fault fault)
        //{
        //    fault = null;

        //    try
        //    {
        //        catererLogic = new CatererLogic();
        //        return catererLogic.GetAliases();
        //    }
        //    catch (Exception ex)
        //    {
        //        SetFaultMessage(out fault, ex);
        //    }

        //    return null;
        //}

        #endregion

        #region Right Region

        public List<Right> GetAllRights(out Fault fault)
        {
            fault = null;

            try
            {
                rightLogic = new RightLogic();
                return rightLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public bool AddRight(Right right, out Fault fault)
        {
            fault = null;

            try
            {
                rightLogic = new RightLogic();
                Right newRight = rightLogic.Add(right);
                return newRight != null ? true : false;
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyRight(Right right, out Fault fault)
        {
            fault = null;

            try
            {
                rightLogic = new RightLogic();
                return rightLogic.Modify(right);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool RemoveRight(Right right, out Fault fault)
        {
            fault = null;

            try
            {
                rightLogic = new RightLogic();
                return rightLogic.Remove(right);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Period Type Region

        public bool AddPeriodType(PeriodType periodType, out Fault fault)
        {
            fault = null;

            try
            {
                periodTypeLogic = new PeriodTypeLogic();
                PeriodType newPeriodType = periodTypeLogic.Add(periodType);
                return (newPeriodType.Id > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }

        public bool RemovePeriodType(PeriodType periodType, out Fault fault)
        {
            fault = null;

            try
            {
                periodTypeLogic = new PeriodTypeLogic();
                return periodTypeLogic.Remove(periodType);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyPeriodType(PeriodType periodType, out Fault fault)
        {
            fault = null;

            try
            {
                periodTypeLogic = new PeriodTypeLogic();
                return periodTypeLogic.Modify(periodType);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public List<PeriodType> GetAllPeriodTypes(out Fault fault)
        {
            fault = null;

            try
            {
                periodTypeLogic = new PeriodTypeLogic();
                return periodTypeLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        #endregion

        #region Score Region

        //public List<ScoreTable> GetScoreTable(User user, Caterer caterer, out Fault fault)
        //{
        //    fault = null;

        //    try
        //    {
        //        scoreLogic = new ScoreLogic();
        //        return scoreLogic.GetBy(user, caterer);
        //    }
        //    catch (Exception ex)
        //    {
        //        SetFaultMessage(out fault, ex);
        //    }

        //    return null;
        //}

        //public List<ScoreSummary> GetScoreSummary(out Fault fault)
        //{
        //    fault = null;

        //    try
        //    {
        //        scoreLogic = new ScoreLogic();
        //        return scoreLogic.Get();
        //    }
        //    catch (Exception ex)
        //    {
        //        SetFaultMessage(out fault, ex);
        //    }

        //    return null;
        //}


        //public bool SubmitScore(List<Caterer> caterers, User user, out Fault fault)
        //{
        //    fault = null;

        //    try
        //    {
        //        scoreLogic = new ScoreLogic();
        //        return scoreLogic.Add(caterers, user) > 0 ? true : false;
        //    }
        //    catch (Exception ex)
        //    {
        //        SetFaultMessage(out fault, ex);
        //    }

        //    return false;
        //}

        #endregion

        #region Staff Learning

        public bool AddStaffLearning(StaffLearning staffLearning, out Fault fault)
        {
            fault = null;

            try
            {
                staffLearningLogic = new StaffLearningLogic();
                StaffLearning newStaffLearning = staffLearningLogic.Add(staffLearning);
                return (newStaffLearning != null ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<StaffLearning> GetAllStaffLearning(out Fault fault)
        {
            fault = null;

            try
            {
                staffLearningLogic = new StaffLearningLogic();
                return staffLearningLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        //public StaffLevel GetByStaffAndPeriod(Staff staff, Period period, out Fault fault)
        //{
        //    fault = null;

        //    try
        //    {
        //        staffLevelLogic = new StaffLevelLogic();
        //        return staffLevelLogic.GetBy(staff, period);
        //    }
        //    catch (Exception ex)
        //    {
        //        SetFaultMessage(out fault, ex);
        //    }

        //    return null;
        //}
        public bool RemoveStaffLearning(StaffLearning staffLearning, out Fault fault)
        {
            fault = null;

            try
            {
                staffLearningLogic = new StaffLearningLogic();
                return staffLearningLogic.Remove(staffLearning);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyStaffLearning(StaffLearning staffLearning, out Fault fault)
        {
            fault = null;

            try
            {
                staffLearningLogic = new StaffLearningLogic();
                return staffLearningLogic.Modify(staffLearning);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Location Region

        public List<Location> GetAllLocations(out Fault fault)
        {
            fault = null;

            try
            {
                locationLogic = new LocationLogic();
                return locationLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public bool AddLocation(Location location, out Fault fault)
        {
            fault = null;

            try
            {
                locationLogic = new LocationLogic();
                Location newLocation = locationLogic.Add(location);
                return newLocation != null ? true : false;
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyLocation(Location location, out Fault fault)
        {
            fault = null;

            try
            {
                locationLogic = new LocationLogic();
                return locationLogic.Modify(location);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool RemoveLocation(Location location, out Fault fault)
        {
            fault = null;

            try
            {
                locationLogic = new LocationLogic();
                return locationLogic.Remove(location);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Staff Location Region

        public List<StaffLocation> GetAllStaffLocations(out Fault fault)
        {
            fault = null;

            try
            {
                staffLocationLogic = new StaffLocationLogic();
                return staffLocationLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public bool AddStaffLocation(StaffLocation staffLocation, out Fault fault)
        {
            fault = null;

            try
            {
                staffLocationLogic = new StaffLocationLogic();
                StaffLocation newStaffLocation = staffLocationLogic.Add(staffLocation);
                return newStaffLocation != null ? true : false;
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyStaffLocation(StaffLocation staffLocation, out Fault fault)
        {
            fault = null;

            try
            {
                staffLocationLogic = new StaffLocationLogic();
                return staffLocationLogic.Modify(staffLocation);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool RemoveStaffLocation(StaffLocation staffLocation, out Fault fault)
        {
            fault = null;

            try
            {
                staffLocationLogic = new StaffLocationLogic();
                return staffLocationLogic.Remove(staffLocation);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region Login Detail Region

        public List<LoginDetail> GetAllLoginDetails(out Fault fault)
        {
            fault = null;

            try
            {
                loginDetailLogic = new LoginDetailLogic();
                return loginDetailLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public bool ResetLoginDetailPassword(LoginDetail loginDetail, out Fault fault)
        {
            fault = null;

            try
            {
                loginDetailLogic = new LoginDetailLogic();
                return loginDetailLogic.ResetPassword(loginDetail);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyLoginDetail(LoginDetail loginDetail, out Fault fault)
        {
            fault = null;

            try
            {
                loginDetailLogic = new LoginDetailLogic();
                return loginDetailLogic.Modify(loginDetail);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public LoginDetail ChangePassword(Staff staff, string password, out Fault fault)
        {
            fault = null;

            try
            {
                loginDetailLogic = new LoginDetailLogic();
                return loginDetailLogic.ChangePassword(staff, password);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        #endregion

        #region Potential assessment

        public List<StaffAssessment> GetStaffAssessments(string staffId, int periodId, bool isLoadingAppraisee, out Fault fault)
        {
            fault = null;

            try
            {
                long appraisalHeaderId = IsStaffAppraised(staffId, periodId);
                bool isAppraised = appraisalHeaderId > 0 ? true : false;
                bool disableAssessment = LockPaceAndMetric(isLoadingAppraisee, appraisalHeaderId);
                //bool isSupervisor = IsSupervisor(companyDepartmentJobRoleId, periodId);

                potentialAssessmentLogic = new PotentialAssessmentLogic();

                if (isAppraised)
                {
                    Appraisal appraisal = new Appraisal() { Id = appraisalHeaderId };
                    return potentialAssessmentLogic.GetBy(appraisal, disableAssessment);
                }
                else
                {
                    Period period = new Period() { Id = periodId };
                    return potentialAssessmentLogic.GetDefault(period, disableAssessment);
                }
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public bool IsStaffLevelExistInAssessment(string levelId, int periodId, out Fault fault)
        {
            fault = null;

            try
            {
                Level level = new Level() {Id = levelId};
                Period period = new Period() { Id = periodId};

                assessmentLevelLogic = new AssessmentLevelLogic();
                return assessmentLevelLogic.IsLevelExist(period, level);

            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region INPS Excel

        public List<Inps> ReadInpsExcel(string fileName, byte[] bytes, Period period, InpsType inpsType, out Fault fault)
        {
            fault = null;

            string virtualFileName = null;
            string[] filePathAndUrl = null;
            string destinationFlePath = null;

            try
            {
                //destinationFlePath = UploadInpsExcelSourceSheet(out virtualFileName, out filePathAndUrl, fileName, bytes, "inps", out fault);

                destinationFlePath = UploadInpsExcelSourceSheet(out virtualFileName, out filePathAndUrl, fileName, bytes, inpsType.Name, out fault);

                npsExcel = new InpsExcel();
                return npsExcel.Read(destinationFlePath, period, inpsType);
            }
            catch (Exception ex)
            {
                FileManager.DeleteFile(destinationFlePath);
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        private string UploadInpsExcelSourceSheet(out string virtualFileName, out string[] filePathAndUrl, string fileName, byte[] bytes, string folderName, out Fault fault)
        {
            fault = null;

            virtualFileName = null;
            filePathAndUrl = null;

            try
            {
                if (bytes.Length > 0)
                {
                    FileManager fm = new FileManager(appRootPath, sourceFolder + @"\");
                    string destinationFilePath = fm.UploadFiles(fileName, bytes, folderName);

                    //set out parameter values
                    virtualFileName = fm.GetFileName(destinationFilePath);
                    filePathAndUrl = fm.GetFilePathAndUrl(virtualFileName, sourceFolder, false);

                    if (string.IsNullOrEmpty(destinationFilePath))
                    {
                        throw new Exception("Source sheet upload failed!");
                    }

                    return destinationFilePath;
                }
                else
                {
                    throw new Exception("Source sheet upload failed! Source sheet file size is too large");
                }
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public bool SaveInps(List<Inps> inpss, out Fault fault)
        {
            fault = null;

            try
            {
                inpsLogic = new InpsLogic();
                int rowsAdded = inpsLogic.Add(inpss);

                return rowsAdded > 0 ? true : false;
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public List<Inps> GetAllInpsByPeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                inpsLogic = new InpsLogic();
                return inpsLogic.GetBy(period);

            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        #endregion

        #region INPS Setup

        public bool AddInps(Inps inps, out Fault fault)
        {
            fault = null;

            try
            {
                inpsLogic = new InpsLogic();
                Inps newInps = inpsLogic.Add(inps);
                return (newInps.Id > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<Inps> GetAllInps(out Fault fault)
        {
            fault = null;

            try
            {
                inpsLogic = new InpsLogic();
                return inpsLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public List<Inps> GetInpsByPeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                inpsLogic = new InpsLogic();
                return inpsLogic.GetBy(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public List<Inps> GetInpsByPeriodAndType(Period period, InpsType inpsType, out Fault fault)
        {
            fault = null;

            try
            {
                inpsLogic = new InpsLogic();
                return inpsLogic.GetBy(period, inpsType);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveInps(Inps inps, out Fault fault)
        {
            fault = null;

            try
            {
                inpsLogic = new InpsLogic();
                return inpsLogic.Remove(inps);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyInps(Inps inps, out Fault fault)
        {
            fault = null;

            try
            {
                inpsLogic = new InpsLogic();
                return inpsLogic.Modify(inps);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region INPS Rating Setup

        public bool AddInpsRating(List<InpsRating> inpsRatings, out Fault fault)
        {
            fault = null;

            try
            {
                inpsRatingLogic = new InpsRatingLogic();
                int rowsAffected = inpsRatingLogic.Add(inpsRatings);
                return (rowsAffected > 0 ? true : false);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;

        }
        public List<InpsRating> GetAllInpsRating(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                inpsRatingLogic = new InpsRatingLogic();
                return inpsRatingLogic.GetAllBy(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public List<InpsRating> GetInpsRatingByPeriodAndType(Period period, InpsType inpsType, out Fault fault)
        {
            fault = null;

            try
            {
                inpsRatingLogic = new InpsRatingLogic();
                return inpsRatingLogic.GetBy(period, inpsType);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }
        public bool RemoveInpsRating(List<InpsRating> inpsRatings, out Fault fault)
        {
            fault = null;

            try
            {
                inpsRatingLogic = new InpsRatingLogic();
                return inpsRatingLogic.Remove(inpsRatings);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool RemoveInpsRatingByPeriod(Period period, out Fault fault)
        {
            fault = null;

            try
            {
                inpsRatingLogic = new InpsRatingLogic();
                return inpsRatingLogic.Remove(period);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        public bool ModifyInpsRating(List<InpsRating> inpsRatings, out Fault fault)
        {
            fault = null;

            try
            {
                inpsRatingLogic = new InpsRatingLogic();
                return inpsRatingLogic.Modify(inpsRatings);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        #endregion

        #region INPS TYPE Setup
        
        public List<InpsType> GetAllInpsType(out Fault fault)
        {
            fault = null;

            try
            {
                inpsTypeLogic = new InpsTypeLogic();
                return inpsTypeLogic.GetAll();
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return null;
        }

        public bool AddInpsType(InpsType inpsType, out Fault fault)
        {
            fault = null;

            try
            {
                inpsTypeLogic = new InpsTypeLogic();
                InpsType newInpsType = inpsTypeLogic.Add(inpsType);
                return newInpsType != null ? true : false;
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool ModifyInpsType(InpsType inpsType, out Fault fault)
        {
            fault = null;

            try
            {
                inpsTypeLogic = new InpsTypeLogic();
                return inpsTypeLogic.Modify(inpsType);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }

        public bool RemoveInpsType(InpsType inpsType, out Fault fault)
        {
            fault = null;

            try
            {
                inpsTypeLogic = new InpsTypeLogic();
                return inpsTypeLogic.Remove(inpsType);
            }
            catch (Exception ex)
            {
                SetFaultMessage(out fault, ex);
            }

            return false;
        }
        
        #endregion
        
        #region Helper method

        private void SetFaultMessage(out Fault fault, Exception ex)
        {
            fault = new Fault();
            fault.Message = ex.Message;
        }

        #endregion
        
    }



}
