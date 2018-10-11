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

using System.Windows.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Mango.Appraisal.Services;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure;
using System.Collections.Generic;
using System.Collections.Specialized;
using Mango.Infrastructure.Events;
using Mango.Appraisal.Views;

using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Models;
using System.Windows.Threading;
using System.Linq;

namespace Mango.Appraisal.viewModel
{
    public class AppraisalViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;
        //private readonly IUnityContainer container;
        //private readonly IRegionManager regionManager;
        private readonly IAppraisalService appraisalService;
        //private readonly ILearningService learningService;

        private bool isAppraiseePresent;
        private decimal totalPaceScore;
        private decimal totalMetricScore;
        private Infrastructure.MangoService.Appraisal appraisal;
        private Staff staffSupervisor;
        private Staff staffHod;
        private decimal localProcessSumTotal;

        //private ObservableCollection<AppraisalSvc.PaceArea> paceAreas;
        private ObservableCollection<Staff> staffs;
        private Staff staff;
        private Period period;
        private ObservableCollection<Metric> metrics;
        private Metrices metrices;
        private ObservableCollection<Pace> paces;
        //private ICollectionView appraisees;
        private ICollectionView staffsView;
        private ICollectionView optionsView;
        private ICollectionView recommendationsView;

        private string totalActualMetricScore;
        private decimal customerSumTotal;
        private decimal financialSumTotal;
        private decimal processSumTotal;
        private decimal peopleSumTotal;
        private decimal riskSumTotal;
        private ObservableCollection<Option> options;
        private ObservableCollection<Recommendation> recommendations;
        private Comment comment;
        private bool lockPaceAndMetrics = false;
        private ObservableCollection<GradeScale> gradeScales;
        private ObservableCollection<PaceRating> paceRatings;

        private bool enableSubmitButton;
        private bool isMetric = false;
        private bool isPace = false;
        private bool isStrenghtAndWeakness = false;
        private bool isTrainingNeeds = false;
        private bool isSupervisorComment = false;
        private bool isAppraiseeComment = false;
        private bool isHodComment = false;
        private bool isRecommendation = false;
        private bool isOption = false;
        private bool saveToContinueLater = false;
        private string paceRating;
        private byte staffType;

        private bool displayPotentialAssessment;

        private ObservableCollection<StaffAssessment> staffAssessments;
        private decimal totalStaffAssessmentScore;

        private Dispatcher dispatcher;
        private bool isProcessing;

        //private const int UI_HEIGHT = 2300;

        public AppraisalViewModel(IAppraisalService _appraisalService, IEventAggregator _eventAggregator)
        {
            dispatcher = Deployment.Current.Dispatcher;
            ButtonClickCommand = new DelegateCommand(OnButtonClick, IsEnabled);
            SaveCommand = new DelegateCommand(OnSave);

            //IsProcessing = true;

            eventAggregator = _eventAggregator;
            appraisalService = _appraisalService;
           
            eventAggregator.GetEvent<StaffEvent>().Subscribe(OnFormLoad, ThreadOption.UIThread);
            eventAggregator.GetEvent<HodAppraiseesEvent>().Subscribe(OnHodAppraiseeLinkClicked, ThreadOption.UIThread);
        }

        public bool IsProcessing
        {
            get { return isProcessing; }
            set
            {
                isProcessing = value;
                base.OnPropertyChanged("IsProcessing");
            }
        }
        public bool DisplayPotentialAssessment
        {
            get { return displayPotentialAssessment; }
            set
            {
                displayPotentialAssessment = value;
                base.OnPropertyChanged("DisplayPotentialAssessment");
            }
        }
        public bool IsEnabled()
        {
            return EnableSubmitButton;
        }
               
        public Staff Staff
        {
            get { return staff; }
            set
            {
                staff = value;
                base.OnPropertyChanged("Staff");
            }
        }
        public byte StaffType
        {
            get { return staffType; }
            set
            {
                staffType = value;
                base.OnPropertyChanged("StaffType");
            }
        }
        //private Dictionary<string, string> LoggedInStaff { get; set; }
        private Staff LoggedInStaff { get; set; }
        public Infrastructure.MangoService.Appraisal Appraisal
        {
            get { return appraisal; }
            set
            {
                appraisal = value;
                base.OnPropertyChanged("Appraisal");
            }
        }
        public ObservableCollection<Staff> Staffs
        {
            get { return staffs; }
            set
            {
                staffs = value;
                base.OnPropertyChanged("Staffs");
            }
        }
        public Staff StaffSupervisor
        {
            get { return staffSupervisor; }
            set
            {
                staffSupervisor = value;
                base.OnPropertyChanged("StaffSupervisor");
            }
        }
        public Staff StaffHod
        {
            get { return staffHod; }
            set
            {
                staffHod = value;
                base.OnPropertyChanged("StaffHod");
            }
        }
        public ObservableCollection<Pace> Paces
        {
            get { return paces; }
            set
            {
                paces = value;
                base.OnPropertyChanged("Paces");
            }
        }
        public ObservableCollection<StaffAssessment> StaffAssessments
        {
            get { return staffAssessments; }
            set
            {
                staffAssessments = value;
                base.OnPropertyChanged("StaffAssessments");
            }
        }
        public bool IsMetric
        {
            get { return isMetric; }
            set
            {
                isMetric = value;
                base.OnPropertyChanged("IsMetric");
            }
        }
        public bool IsPace
        {
            get { return isPace; }
            set
            {
                isPace = value;
                base.OnPropertyChanged("IsPace");
            }
        }
        public bool IsOption
        {
            get { return isOption; }
            set
            {
                isOption = value;
                base.OnPropertyChanged("IsOption");
            }
        }
        public bool IsRecommendation
        {
            get { return isRecommendation; }
            set
            {
                isRecommendation = value;
                base.OnPropertyChanged("IsRecommendation");
            }
        }
        public bool IsHodComment
        {
            get { return isHodComment; }
            set
            {
                isHodComment = value;
                base.OnPropertyChanged("IsHodComment");
            }
        }
        public bool IsAppraiseeComment
        {
            get { return isAppraiseeComment; }
            set
            {
                isAppraiseeComment = value;
                base.OnPropertyChanged("IsAppraiseeComment");
            }
        }
        public bool IsSupervisorComment
        {
            get { return isSupervisorComment; }
            set
            {
                isSupervisorComment = value;
                base.OnPropertyChanged("IsSupervisorComment");
            }
        }
        public bool IsTrainingNeeds
        {
            get { return isTrainingNeeds; }
            set
            {
                isTrainingNeeds = value;
                base.OnPropertyChanged("IsTrainingNeeds");
            }
        }
        public bool IsStrenghtAndWeakness
        {
            get { return isStrenghtAndWeakness; }
            set
            {
                isStrenghtAndWeakness = value;
                base.OnPropertyChanged("IsStrenghtAndWeakness");
            }
        }
        public bool SaveToContinueLater
        {
            get { return saveToContinueLater; }
            set
            {
                saveToContinueLater = value;
                base.OnPropertyChanged("SaveToContinueLater");
            }
        }
        public Period Period
        {
            get { return period; }
            set
            {
                period = value;
                base.OnPropertyChanged("Period");
            }
        }
        public Metrices Metrices
        {
            get { return metrices; }
            set
            {
                metrices = value;
                base.OnPropertyChanged("Metrices");
                //base.OnCollectionChanged(metrices);
                //NumericUpDownChangedCommand.RaiseCanExecuteChanged();
            }
        }
        ObservableCollection<Process> processes;
        public ObservableCollection<Process> Processes
        {
            get { return processes; }
            set
            {
                processes = value;
                base.OnPropertyChanged("Processes");
            }
        }
        decimal number = 0;
        public decimal Number
        {
            get { return number; }
            set
            {
                number = value;
                base.OnPropertyChanged("Number");
                NumericUpDownChangedCommand.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Metric> Metrics
        {
            get { return metrics; }
            set
            {
                metrics = value;
                base.OnPropertyChanged("Metrics");
                NumericUpDownChangedCommand.RaiseCanExecuteChanged();
            }
        }
        public decimal CustomerSumTotal
        {
            get { return customerSumTotal; }
            set
            {
                customerSumTotal = value;
                base.OnPropertyChanged("CustomerSumTotal");
            }
        }
        public decimal FinancialSumTotal
        {
            get { return financialSumTotal; }
            set
            {
                financialSumTotal = value;
                base.OnPropertyChanged("FinancialSumTotal");
            }
        }
        public decimal PeopleSumTotal
        {
            get { return peopleSumTotal; }
            set
            {
                peopleSumTotal = value;
                base.OnPropertyChanged("PeopleSumTotal");
            }
        }
        public decimal LocalProcessSumTotal
        {
            get { return localProcessSumTotal; }
            set
            {
                localProcessSumTotal = value;
                base.OnPropertyChanged("LocalProcessSumTotal");
            }
        }
        public decimal ProcessSumTotal
        {
            get { return processSumTotal; }
            set
            {
                processSumTotal = value;
                base.OnPropertyChanged("ProcessSumTotal");
            }
        }
        public decimal RiskSumTotal
        {
            get { return riskSumTotal; }
            set
            {
                riskSumTotal = value;
                base.OnPropertyChanged("RiskSumTotal");
            }
        }
        public ObservableCollection<Option> Options
        {
            get { return options; }
            set
            {
                
                options = value;
                base.OnPropertyChanged("Options");
            }
        }
        public ObservableCollection<GradeScale> GradeScales
        {
            get { return gradeScales; }
            set
            {

                gradeScales = value;
                base.OnPropertyChanged("GradeScales");
            }
        }
        public ObservableCollection<PaceRating> PaceRatings
        {
            get { return paceRatings; }
            set
            {

                paceRatings = value;
                base.OnPropertyChanged("PaceRatings");
            }
        }
        public ICollectionView OptionsView
        {
            get { return optionsView; }
            set
            {
                optionsView = value;
                base.OnPropertyChanged("OptionsView");
            }
        }
        public Comment Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                base.OnPropertyChanged("Comment");
            }
        }
        public ObservableCollection<Recommendation> Recommendations
        {
            get { return recommendations; }
            set
            {
                recommendations = value;
                base.OnPropertyChanged("Recommendations");
            }
        }
        public ICollectionView RecommendationsView
        {
            get { return recommendationsView; }
            set
            {
                recommendationsView = value;
                base.OnPropertyChanged("RecommendationsView");
            }
        }
        public ICollectionView StaffsView
        {
            get { return staffsView; }
            set
            {
                staffsView = value;
                base.OnPropertyChanged("StaffsView");
            }
        }
        public bool IsAppraiseePresent
        {
            get { return isAppraiseePresent; }
            set
            {
                isAppraiseePresent = value;
                base.OnPropertyChanged("IsAppraiseePresent");
            }
        }
        public bool LockPaceAndMetrics
        {
            get { return lockPaceAndMetrics; }
            set
            {
                lockPaceAndMetrics = value;
                base.OnPropertyChanged("LockPaceAndMetrics");
                //ButtonClickCommand.RaiseCanExecuteChanged();
            }
        }
        public bool EnableSubmitButton
        {
            get { return enableSubmitButton; }
            set
            {
                enableSubmitButton = value;
                base.OnPropertyChanged("EnableSubmitButton");
                ButtonClickCommand.RaiseCanExecuteChanged();
            }
        }
        //public bool IsSubmitButtonEnabled
        //{
        //    get { return isSubmitButtonEnabled; }
        //    set
        //    {
        //        isSubmitButtonEnabled = value;
        //        base.OnPropertyChanged("IsSubmitButtonEnabled");
        //    }
        //}

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand ButtonClickCommand { get; private set; }
        public DelegateCommand<object> NumericUpDownChangedCommand { get; private set; }
        public decimal TotalPaceScore
        {
            get { return totalPaceScore; }
            set
            {
                totalPaceScore = value;
                base.OnPropertyChanged("TotalPaceScore");
            }
        }
        public decimal TotalStaffAssessmentScore
        {
            get { return totalStaffAssessmentScore; }
            set
            {
                totalStaffAssessmentScore = value;
                base.OnPropertyChanged("TotalStaffAssessmentScore");
            }
        }
        public decimal TotalMetricScore
        {
            get { return totalMetricScore; }
            set
            {
                totalMetricScore = value;
                base.OnPropertyChanged("TotalMetricScore");
            }
        }
        public string TotalActualMetricScore
        {
            get { return totalActualMetricScore; }
            set
            {
                totalActualMetricScore = value;
                base.OnPropertyChanged("TotalActualMetricScore");
            }
        }
        public string PaceRating
        {
            get { return paceRating; }
            set
            {
                paceRating = value;
                base.OnPropertyChanged("PaceRating");
            }
        }

        private bool IsAppraiseeSelected;
        private bool IsSecondLevelAppraiseeLoaded;
        //private void SelectedAppraiseeChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LoadSeletedStaff();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void LoadSeletedStaff()
        {
            if (StaffsView != null)
            {
                StaffsView.CurrentChanged += (s, e) =>
                {
                    Staff staff = StaffsView.CurrentItem as Staff;
                    if (staff != null)
                    {
                        if (!string.IsNullOrEmpty(staff.Id))
                        {
                            if (IsSecondLevelAppraiseeLoaded)
                            {
                                AppraisalState = (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee;
                            }
                            else
                            {
                                AppraisalState = (int)AppraisalEnum.State.SupervisorLodingAppraisee;
                            }

                            IsAppraiseeSelected = true;
                            OnLoad(staff);
                        }
                    }
                };
            }
        }

        //private void LoadSeletedStaff(Staff selectedStaff)
        //{
        //    ////Staff staff = StaffsView.CurrentItem as Staff;
        //    //Staff selectedStaff = StaffsView.CurrentItem as Staff;

        //    if (selectedStaff != null)
        //    {
        //        if (!string.IsNullOrEmpty(selectedStaff.Id))
        //        {
        //            //Dictionary<string, string> selectedStaff = new Dictionary<string, string>();

        //            //selectedStaff.Add("id", staff.Id);
        //            //selectedStaff.Add("departmentName", staff.Department.Name);
        //            //selectedStaff.Add("companyDepartmentJobRoleId", staff.CompanyDepartmentJobRoleId.ToString());
        //            //selectedStaff.Add("companyName", staff.Company.Name);
        //            //selectedStaff.Add("companyId", staff.Company.Id);
        //            //selectedStaff.Add("periodId", Period.Id.ToString());

        //            if (IsSecondLevelAppraiseeLoaded)
        //            {
        //                AppraisalState = (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee;
        //            }
        //            else
        //            {
        //                AppraisalState = (int)AppraisalEnum.State.SupervisorLodingAppraisee;
        //            }

        //            IsAppraiseeSelected = true;

        //            OnLoad(selectedStaff);
        //        }
        //    }
        //}

        //public void SetUiHeight(int height)
        //{
        //    var dispatcher = Deployment.Current.Dispatcher;
        //    dispatcher.BeginInvoke(() =>
        //    {
        //        Height = height;
        //    });
        //}

        //load all appraised staff under the logged in hod
        public void OnHodAppraiseeLinkClicked(Staff staff)
        {
            if (staffType == 3)
            {
                if (staff != null)
                {
                    LoggedInStaff = staff;

                    AppraisalState = (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee;

                    IsSecondLevelAppraiseeLoaded = true;
                    int companyDepartmentJobRoleId = staff.CompanyDepartmentJobRoleId; // Convert.ToInt32(staff["companyDepartmentJobRoleId"]);
                    //int periodId = Convert.ToInt32(staff["periodId"]);

                    int periodId = Utility.Period.Id;
                    byte optionId = 1;

                    OnLoad(staff);
                    LoadHodAppraisees(companyDepartmentJobRoleId, periodId, optionId);
                }
                else
                {
                    MessageBox.Show("Required parameters are missing! This action has been aborted.");
                }
            }
            else
            {
                MessageBox.Show("You are likely not an HOD");
            }
        }
        //public void OnHodAppraiseeLinkClicked(Dictionary<string, string> staff)
        //{
        //    if (staffType == 3)
        //    {
        //        if (staff != null)
        //        {
        //            LoggedInStaff = staff;

        //            AppraisalState = (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee;

        //            IsSecondLevelAppraiseeLoaded = true;
        //            int companyDepartmentJobRoleId = Convert.ToInt32(staff["companyDepartmentJobRoleId"]);
        //            int periodId = Convert.ToInt32(staff["periodId"]);
        //            byte optionId = 1;

        //            OnLoad(staff);
        //            LoadHodAppraisees(companyDepartmentJobRoleId, periodId, optionId);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Required parameters are missing! This action has been aborted.");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("You are likely not an HOD");
        //    }
        //}

        private void OnSave()
        {
            try
            {
                IsProcessing = true;
                AppraiseStaff(false);
            }
            catch (Exception ex)
            {
                IsProcessing = false;
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void OnButtonClick()
        {
            try
            {
                if (DoValidation())
                {
                    IsProcessing = true;
                    AppraiseStaff(true);
                }
            }
            catch (Exception ex)
            {
                IsProcessing = false;
                Utility.DisplayMessage(ex.Message);
            }
        }

        private bool DoValidation()
        {
            //
            if (DisplayPotentialAssessment)
            {
                if (StaffAssessments == null || StaffAssessments.Count == 0)
                {
                    MessageBox.Show("No Potential Assessment found for " + Staff.Name + "! Please contact your HR department.", "Potential Assessment", MessageBoxButton.OK);
                    return false;
                }
            }

            //check total metrics value
            decimal totalMetricsValue = GetTotalMetricsValue();
            if (totalMetricsValue != 100)
            {
                MessageBox.Show("Invalid metrics score of " + totalMetricsValue + " was found! The total allowable metrics score must be equal to 100. Please contact your HR department.", "Invalid Metrics Score", MessageBoxButton.OK);
                return false;
            }

            //check if staff has hod
            if (StaffHod == null)
            {
                MessageBox.Show("No HOD found for " + Staff.Name + "! in the system. Please contact your HR department.", "No HOD Found", MessageBoxButton.OK);
                return false;
            }
            
            if (!ValidatePaceAndMetrics())
            {
                return false;
            }

            switch (StaffType)
            {
                case 1:
                    {
                        return IncompleteStaffComment();
                    }
                case 2:
                    {
                        if (AppraisalState == (int)AppraisalEnum.State.IncompletedSupervisorAppraisal)
                        {
                            return IncompleteStaffComment();
                        }
                        else if (AppraisalState == (int)AppraisalEnum.State.IncompleteAppraisal)
                        {
                            return IncompleteCommentBySupervisor();
                        }

                        return true;
                    }
                case 3:
                    {
                        if (AppraisalState == (int)AppraisalEnum.State.IncompleteAppraisal)
                        {
                            return IncompleteCommentBySupervisor();
                        }
                        else if (AppraisalState == (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee)
                        {
                            if (Comment != null)
                            {
                                if (string.IsNullOrEmpty(Comment.HodComment.Trim()))
                                {
                                    MessageBox.Show("HOD Comment cannot be empty! Please enter comment!", "No HOD Comment Entered", MessageBoxButton.OK);
                                    return false;
                                }
                            }
                        }
                        else if (AppraisalState == (int)AppraisalEnum.State.IncompletedHodAppraisal)
                        {
                            return IncompleteStaffComment();
                        }

                        return true;                       
                    }
            }

            return true;
        }

        private bool IncompleteStaffComment()
        {
            try
            {
                if (Comment != null)
                {
                    if (string.IsNullOrEmpty(Comment.AppraiseeComment.Trim()))
                    {
                        MessageBox.Show("Appraisee Comment cannot be empty! Please enter comment!", "No Comment Entered", MessageBoxButton.OK);
                        return false;
                    }
                    else if (OptionsView != null)
                    {
                        Option option = OptionsView.CurrentItem as Option;
                        if (option.Id <= 0)
                        {
                            MessageBox.Show("Please select an option!", "No Option Selected", MessageBoxButton.OK);
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IncompleteCommentBySupervisor()
        {
            if (Comment != null)
            {
                if (string.IsNullOrEmpty(Comment.Strenght))
                {
                    MessageBox.Show("Please enter appraisee's strength!", "No Strength Entered", MessageBoxButton.OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(Comment.Weakness))
                {
                    MessageBox.Show("Please enter appraisee's weakness!", "No Weakness Entered", MessageBoxButton.OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(Comment.TrainingNeed))
                {
                    MessageBox.Show("Please enter appraisee's training need!", "No Training Need Entered", MessageBoxButton.OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(Comment.SupervisorComment))
                {
                    MessageBox.Show("Supervisor Comment cannot be empty! Please enter comment.", "No Comment Entered", MessageBoxButton.OK);
                    return false;
                }
                else if (RecommendationsView != null)
                {
                    Recommendation recommendation = RecommendationsView.CurrentItem as Recommendation;
                    if (recommendation.Id <= 0)
                    {
                        MessageBox.Show("Please select a recommendation!", "No Recommendation Selected", MessageBoxButton.OK);
                        return false;
                    }
                }
            }

            return true;
        }
      
        private bool ValidatePaceAndMetrics()
        {
            //pace validation
            if (Paces != null)
            {
                if (Paces.Count > 0)
                {
                    foreach (Pace pace in Paces)
                    {
                        decimal score = pace.Score;
                        string justification = pace.Justification;

                        if (string.IsNullOrEmpty(justification))
                        {
                            MessageBox.Show("No justification specified for " + pace.Name + " in PACE!", "Pace", MessageBoxButton.OK);
                            return false;
                        }
                        if (score == 0)
                        {
                            MessageBox.Show("No score entered for " + pace.Name + " in PACE!", "Pace", MessageBoxButton.OK);
                            return false;
                        }
                    }
                }
            }

            //metrics validation
            if (Metrices.Peoples != null)
            {
                if (Metrices.Peoples.Count > 0)
                {
                    foreach (People people in Metrices.Peoples)
                    {
                        if (people.Score == 0 && people.Rating == 0)
                        {
                            MessageBox.Show("One or more score is not entered for PEOPLE METRICS!", "People", MessageBoxButton.OK);
                            return false;
                        }
                    }
                }
            }

            //metrics validation
            if (Metrices.Processes != null)
            {
                if (Metrices.Processes.Count > 0)
                {
                    foreach (Process process in Metrices.Processes)
                    {
                        if (process.Score == 0 && process.Rating == 0)
                        {
                            MessageBox.Show("One or more score is not entered for PROCESS METRICS!", "Process", MessageBoxButton.OK);
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private int AppraisalState { get; set; }
        public void OnFormLoad(Staff staff)
        {
            LockPaceAndMetrics = false;
            IsSecondLevelAppraiseeLoaded = false;
            StaffType = Convert.ToByte(staff.Type);

            //StaffType = Convert.ToByte(staff["type"]);
           
            if (StaffType == 1)
            {
                AppraisalState = (int)AppraisalEnum.State.StaffLoaded;
            }
            if (StaffType == 2)
            {
                AppraisalState = (int)AppraisalEnum.State.SupervisorLoaded;
            }
            else if (StaffType == 3)
            {
                AppraisalState = (int)AppraisalEnum.State.HodLoaded;
            }

            OnLoad(staff);
        }

        public int PeriodId { get; set; }
        public void OnLoad(Staff staff)
        {
            Staff = staff;
            staffId = staff.Id;
            companyDepartmentJobRoleId = staff.CompanyDepartmentJobRoleId;
        
            periodId = Utility.Period.Id;
            PeriodId = periodId;

            LoadStaffDetail(staffId, companyDepartmentJobRoleId, periodId);

            LoadStaffAppraisal(staffId, periodId);
            LoadCurrentAppraisalDetail(periodId);
            LoadPaceRating();
            LoadGrade();
                        
            LoadStaffSupervisor(companyDepartmentJobRoleId, periodId);
            GetStaffHod(companyDepartmentJobRoleId, periodId);
        }

        //public void OnLoad(Dictionary<string, string> staff)
        //{
        //    staffId = staff["id"];
        //    companyDepartmentJobRoleId = Convert.ToInt32(staff["companyDepartmentJobRoleId"]);
        //    string companyName = staff["companyName"];
        //    int companyId = Convert.ToInt32(staff["companyId"]);
        //    string departmentName = staff["departmentName"];
        //    periodId = Convert.ToInt32(staff["periodId"]);
        //    PeriodId = periodId;

        //    LoadStaffAppraisal(staffId, periodId);
        //    LoadCurrentAppraisalDetail(periodId);
        //    LoadPaceRating();
        //    LoadGrade();
        //    LoadOption();
            
        //    LoadStaffDetail(staffId, companyDepartmentJobRoleId, periodId);
        //    LoadStaffSupervisor(companyDepartmentJobRoleId, periodId);
        //    GetStaffHod(companyDepartmentJobRoleId, periodId);
                        
        //    //LoadComment(staffId, periodId);
        //}

        private string staffId;
        private int companyDepartmentJobRoleId;
        private int periodId;

        private void LoadOthers(string staffId, int companyDepartmentJobRoleId, int periodId)
        {
            if (AppraisalState == (int)AppraisalEnum.State.SupervisorLodingAppraisee)
            {
                LockPaceAndMetrics = true;
            }

            LoadPace(staffId, companyDepartmentJobRoleId, periodId);
            LoadStaffAssessment(staffId, periodId);
            LoadMetrices(companyDepartmentJobRoleId, staffId, periodId);
        }
        private void LoadPaceRating()
        {
            PaceRatingLoadCompleted();
            appraisalService.LoadPaceRating();
        }
        private void LoadGrade()
        {
            GradeScaleLoadCompleted();
            appraisalService.LoadGrade();
        }
        private void LoadStaffSupervisor(int companyDepartmentJobRoleId, int periodId)
        {
            AppraiseeSupervisorLoadCompleted();
            appraisalService.LoadStaffSupervisor(companyDepartmentJobRoleId, periodId);
        }
        private void GetStaffHod(int companyDepartmentJobRoleId, int periodId)
        {
            AppraiseeHodGetCompleted();
            appraisalService.GetStaffHod(companyDepartmentJobRoleId, periodId);
        }
        private void LoadStaffAppraisal(string staffId, int periodId)
        {
            StaffAppraisalLoadCompleted();
            appraisalService.LoadStaffAppraisal(staffId, periodId);
        }
        private void LoadCurrentAppraisalDetail(int periodId)
        {
            CurrentAppraisalDetailsLoadCompleted();
            appraisalService.LoadCurrentAppraisalDetails(periodId);
        }
        private void LoadStaffDetail(string staffId, int companyDepartmentJobRoleId, int periodId)
        {
            StaffDetailLoadCompleted();
            appraisalService.GetStaffById(staffId, companyDepartmentJobRoleId, periodId);
        }
      
        private void LoadPace(string staffId, int companyDepartmentJobRoleId, int periodId)
        {
            PaceLoadCompleted();

           
            appraisalService.LoadPace(staffId, companyDepartmentJobRoleId, periodId, LockPaceAndMetrics);
        }
        private void LoadStaffAssessment(string staffId, int periodId)
        {
            StaffAssessmentLoadCompleted();
            appraisalService.LoadStaffAssessment(staffId, periodId, LockPaceAndMetrics);
        }
        private void LoadMetrices(int companyDepartmentJobRoleId, string staffId, int periodId)
        {
            MetricesLoadCompleted();
            appraisalService.LoadMetrices(companyDepartmentJobRoleId, staffId, periodId, LockPaceAndMetrics);
        }
        private void LoadComment(string staffId, int periodId)
        {
            CommentLoadCompleted();
            appraisalService.LoadComment(staffId, periodId);
        }
        private void LoadOption()
        {
            OptionLoadCompleted();
            appraisalService.LoadAppraisalOption();
        }
        private void CheckIfStaffLevelExistInAssessment(int periodId, Staff staff)
        {
            StaffLevelExistInAssessmentCompleted();
            appraisalService.IsStaffLevelExistInAssessment(periodId, staff.Level.Id);
        }
        private void LoadRecommendation()
        {
            RecommendationLoadCompleted();
            appraisalService.LoadRecommendation();
        }
        private void LoadSupervisorAppraisees(int companyDepartmentJobRoleId, int periodId)
        {
            SupervisorAppraiseeListLoadCompleted();
            appraisalService.LoadSupervisorsAppraisees(companyDepartmentJobRoleId, periodId);
        }
        private void LoadHodAppraisees(int companyDepartmentJobRoleId, int periodId, byte optionId)
        {
            HodAppraiseesLoadCompleted();
            appraisalService.LoadHodAppraisees(companyDepartmentJobRoleId, periodId, optionId);
        }

        private void AcceptOrRejectAppraisal()
        {
            try
            {
                Appraisal.ResponseDate = DateTime.Now;
                if (Appraisal != null && Comment != null)
                {
                    if (Comment.OptionId == 0)
                    {
                        MessageBox.Show("Please select an option!");
                        return;
                    }
                    else if (string.IsNullOrEmpty(Comment.AppraiseeComment))
                    {
                        MessageBox.Show("Please enter appraisee comment!");
                        return;
                    }

                    if (Comment.OptionId == 1)
                    {
                        Appraisal.Status.Id = 6;
                    }

                    AppraisalAcceptOrRejectCompleted();
                    appraisalService.AcceptOrRejectAppraisal(Staff, StaffSupervisor, StaffHod, Period, Appraisal, Comment);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void AcceptOrRejectAppraisal()
        //{
        //    try
        //    {
        //        Appraisal.StaffResponseDate = DateTime.Now;
        //        if (Appraisal != null && Comment != null)
        //        {
        //            if (Comment.OptionId == 0)
        //            {
        //                MessageBox.Show("Please select an option!");
        //                return;
        //            }
        //            else if (string.IsNullOrEmpty(Comment.AppraiseeComment))
        //            {
        //                MessageBox.Show("Please enter appraisee comment!");
        //                return;
        //            }

        //            if (Comment.OptionId == 1)
        //            {
        //                Appraisal.StatusId = 6;
        //            }

        //            AppraisalAcceptOrRejectCompleted();
        //            appraisalService.AcceptOrRejectAppraisal(Staff, StaffSupervisor, StaffHod, Period, Appraisal, Comment);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        //private void AppraiseStaff()
        //{
        //    try
        //    {
        //        Appraisal.PeriodId = Period.Id;
        //        Appraisal.StaffId = Staff.Id;
        //        Appraisal.SupervisorId = StaffSupervisor.Id;
        //        Appraisal.HodId = StaffHod.Id;

        //        Recommendation recommendation = RecommendationsView.CurrentItem as Recommendation;
        //        Option option = OptionsView.CurrentItem as Option;

        //        Comment.RecommendationId = recommendation.Id;
        //        Comment.OptionId = option.Id;

        //        switch (StaffType)
        //        {
        //            case 1:
        //                {
        //                    //Appraisal.StaffResponseDate = DateTime.Now;
        //                    //if (Appraisal != null && Comment != null)
        //                    //{
        //                    //    appraisalService.AcceptOrRejectAppraisal(Staff, StaffSupervisor, StaffHod, Period, Appraisal, Comment);
        //                    //}

        //                    AcceptOrRejectAppraisal();

        //                    break;
        //                }
        //            case 2:
        //                {
        //                    StaffSupervisor.Type = 2;

        //                    //state = AppraisalState;
        //                    Appraisal.SupervisorAppraisalDate = DateTime.Now;
        //                    if (AppraisalState == (int)AppraisalEnum.State.IncompleteAppraisal)
        //                    {
        //                        //if (Appraisal.StatusId == 1 || Appraisal.StatusId == 0)
        //                        //{
        //                        //    if (Appraisal != null && Paces != null && Metrices != null && Comment != null)
        //                        //    {
        //                        //        if (Paces.Count > 0)
        //                        //        {
        //                        //            Appraisal.StatusId = 2;
        //                        //            Appraisal.SupervisorAppraisalDate = DateTime.Now;
                                            
        //                        //            appraisalService.AppraiseStaff(Appraisal, Paces, Metrices, Comment, Staff, StaffSupervisor, Period);
        //                        //        }
        //                        //    }
        //                        //}
        //                        //else if (Appraisal.StatusId == 2)
        //                        //{
        //                        //    appraisalService.ModifyAppraisal(Staff, StaffSupervisor, Period, Appraisal, Comment);
        //                        //}
                                
        //                        if (Appraisal.StatusId < 3)
        //                        {
        //                            if (Appraisal != null && Paces != null && Metrices != null && Comment != null)
        //                            {
        //                                if (Paces.Count > 0)
        //                                {
        //                                    Appraisal.StatusId = 2;
        //                                    Appraisal.SupervisorAppraisalDate = DateTime.Now;

        //                                    ObservableCollection<People> peoples = Metrices.Peoples;
        //                                    ObservableCollection<Process> processes = Metrices.Processes;

        //                                    LoadAppraiseStaffCompleted();
        //                                    appraisalService.AppraiseStaff(Appraisal, Paces, Metrices, Comment, Staff, StaffSupervisor, Period);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else if (AppraisalState == (int)AppraisalEnum.State.IncompletedSupervisorAppraisal)
        //                    {
        //                        //appraisalService.AcceptOrRejectAppraisal(Staff, StaffSupervisor, StaffHod, Period, Appraisal, Comment);
        //                        AcceptOrRejectAppraisal();
        //                    }

        //                    break;
        //                }
        //            case 3:
        //                {
        //                    StaffHod.Type = 3;

        //                    string name = Staff.Name;
        //                    string sup = StaffSupervisor.Name;
        //                    string hod = StaffHod.Name;

        //                    //state = AppraisalState;
        //                    Appraisal.HodAppraisalDate = DateTime.Now;
        //                    if (Appraisal != null && Comment != null)
        //                    {
        //                        if (AppraisalState == (int)AppraisalEnum.State.IncompletedHodAppraisal)
        //                        {
        //                            AcceptOrRejectAppraisal();
        //                        }
        //                        if (AppraisalState == (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee)
        //                        {
        //                            Appraisal.StatusId = 3;
        //                            ModifyAppraisal();


        //                            //appraisalService.ModifyAppraisal(Staff, StaffHod, Period, Appraisal, Comment);
        //                        }
        //                        else if (AppraisalState == (int)AppraisalEnum.State.IncompleteAppraisal)
        //                        {
        //                            if (IsSecondLevelAppraiseeLoaded)
        //                            {
        //                                if (Appraisal.StatusId == 1 || Appraisal.StatusId == 0)
        //                                {
        //                                    if (Appraisal != null && Paces != null && Metrices != null && Comment != null)
        //                                    {
        //                                        if (Paces.Count > 0)
        //                                        {
        //                                            Appraisal.StatusId = 2;
        //                                            Appraisal.SupervisorAppraisalDate = DateTime.Now;
        //                                            appraisalService.AppraiseStaff(Appraisal, Paces, Metrices, Comment, Staff, StaffSupervisor, Period);
        //                                        }
        //                                    }
        //                                }
        //                                else if (Appraisal.StatusId == 2 || Appraisal.StatusId == 3)
        //                                {
        //                                    Appraisal.StatusId = 3;
        //                                    ModifyAppraisal();
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if (Appraisal.StatusId < 3)
        //                                {
        //                                    if (Appraisal != null && Paces != null && Metrices != null && Comment != null)
        //                                    {
        //                                        if (Paces.Count > 0)
        //                                        {
        //                                            Appraisal.StatusId = 2;
        //                                            Appraisal.SupervisorAppraisalDate = DateTime.Now;

        //                                            LoadAppraiseStaffCompleted();
        //                                            appraisalService.AppraiseStaff(Appraisal, Paces, Metrices, Comment, Staff, StaffSupervisor, Period);
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }

        //                    break;
        //                }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Occurred! " + ex.Message);
        //    }
        //}

        private void AppraiseStaff(bool isSubmitting)
        {
            try
            {
                Appraisal.Period = new Infrastructure.MangoService.Period();
                Appraisal.Period.Id = Period.Id;

                Appraisal.Staff = new Infrastructure.MangoService.Staff();
                Appraisal.Staff.Id = Staff.Id;

                Appraisal.Supervisor = new Infrastructure.MangoService.Staff();
                Appraisal.Supervisor.Id = StaffSupervisor.Id;
                Appraisal.AppraisalDate = DateTime.Now;

                Appraisal.Hod = new Infrastructure.MangoService.Staff();
                Appraisal.Hod.Id = StaffHod.Id;

                Recommendation recommendation = RecommendationsView.CurrentItem as Recommendation;
                Option option = OptionsView.CurrentItem as Option;

                Comment.RecommendationId = recommendation.Id;
                Comment.OptionId = option.Id;

                StaffAssessments = DisplayPotentialAssessment ? StaffAssessments : null;

                switch (StaffType)
                {
                    case 1:
                        {
                            AcceptOrRejectAppraisal();
                            break;
                        }
                    case 2:
                        {
                            StaffSupervisor.Type = 2;

                            //Appraisal.AppraisalDate = DateTime.Now;
                            if (AppraisalState == (int)AppraisalEnum.State.IncompleteAppraisal)
                            {
                                if (Appraisal.Status.Id < 3)
                                {
                                    if (Appraisal != null && Paces != null && Metrices != null && Comment != null)
                                    {
                                        if (Paces.Count > 0)
                                        {
                                            Appraisal.Status.Id = isSubmitting ? (byte)2 : (byte)1;
                                            //Appraisal.AppraisalDate = DateTime.Now;

                                            ObservableCollection<People> peoples = Metrices.Peoples;
                                            ObservableCollection<Process> processes = Metrices.Processes;

                                            LoadAppraiseStaffCompleted(isSubmitting);
                                            appraisalService.AppraiseStaff(Appraisal, Paces, Metrices, Comment, Staff, StaffSupervisor, Period, StaffAssessments, isSubmitting);
                                        }
                                    }
                                }
                            }
                            else if (AppraisalState == (int)AppraisalEnum.State.IncompletedSupervisorAppraisal)
                            {
                                AcceptOrRejectAppraisal();
                            }

                            break;
                        }
                    case 3:
                        {
                            StaffHod.Type = 3;

                            //Appraisal.HodAppraisalDate = DateTime.Now;
                            if (Appraisal != null && Comment != null)
                            {
                                if (AppraisalState == (int)AppraisalEnum.State.IncompletedHodAppraisal)
                                {
                                    AcceptOrRejectAppraisal();
                                }
                                if (AppraisalState == (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee)
                                {
                                    Appraisal.Status.Id = 3;
                                    ModifyAppraisal();
                                }
                                else if (AppraisalState == (int)AppraisalEnum.State.IncompleteAppraisal)
                                {
                                    if (IsSecondLevelAppraiseeLoaded)
                                    {
                                        if (Appraisal.Status.Id == 1 || Appraisal.Status.Id == 0)
                                        {
                                            if (Appraisal != null && Paces != null && Metrices != null && Comment != null)
                                            {
                                                if (Paces.Count > 0)
                                                {
                                                    //Appraisal.AppraisalDate = DateTime.Now;
                                                    Appraisal.Status.Id = isSubmitting ? (byte)2 : (byte)1;
                                                    appraisalService.AppraiseStaff(Appraisal, Paces, Metrices, Comment, Staff, StaffSupervisor, Period, StaffAssessments, isSubmitting);
                                                }
                                            }
                                        }
                                        else if (Appraisal.Status.Id == 2 || Appraisal.Status.Id == 3)
                                        {
                                            Appraisal.Status.Id = 3;
                                            ModifyAppraisal();
                                        }
                                    }
                                    else
                                    {
                                        if (Appraisal.Status.Id < 3)
                                        {
                                            if (Appraisal != null && Paces != null && Metrices != null && Comment != null)
                                            {
                                                if (Paces.Count > 0)
                                                {
                                                    Appraisal.Status.Id = isSubmitting ? (byte)2 : (byte)1;
                                                    //Appraisal.AppraisalDate = DateTime.Now;

                                                    LoadAppraiseStaffCompleted(isSubmitting);
                                                    appraisalService.AppraiseStaff(Appraisal, Paces, Metrices, Comment, Staff, StaffSupervisor, Period, StaffAssessments, isSubmitting);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                IsProcessing = false;
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void ModifyAppraisal()
        {
            try
            {
                AppraisalEditingCompleted();
                appraisalService.ModifyAppraisal(Staff, StaffHod, Period, Appraisal, Comment);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAppraiseStaffCompleted(bool isSubmitting)
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAppraiseStaffCompletedHelper(isSubmitting);
                    appraisalService.AppraiseStaffCompleted -= handler;
                };

                appraisalService.AppraiseStaffCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadAppraiseStaffCompletedHelper(bool isSubmitting)
        {
            try
            {
                IsProcessing = false;

                dispatcher.BeginInvoke(() =>
                {
                    if (appraisalService.Happy)
                    {
                        DisableButtons();

                        if (isSubmitting)
                        {
                            Appraisal.Status.Name = "Uncompleted";
                            MessageBox.Show("Staff has been successfully appraised. A mail has been sent to the appraisee.", "Staff Appraisal", MessageBoxButton.OK);
                        }
                        else
                        {
                            Appraisal.Status.Name = "Not Started";
                            MessageBox.Show("Staff Appraisal was successfully saved, to be continued at a later time.", "Staff Appraisal", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        IsProcessing = false;
                        MessageBox.Show("Staff Appraisal failed during processing! Please try again", "Staff Appraisal", MessageBoxButton.OK);
                    }
                });
            }
            catch (Exception ex)
            {
                IsProcessing = false;
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void DisableButtons()
        {
            try
            {
                EnableSubmitButton = false;
                SaveToContinueLater = false;
            }
            catch(Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        //private void LoadAppraiseStaffCompletedHelper()
        //{
        //    try
        //    {
        //        dispatcher.BeginInvoke(() =>
        //        {
        //            if (appraisalService.Happy)
        //            {
        //                Appraisal.Status = "Uncompleted";
        //                MessageBox.Show("Staff has been successfully appraised.", "Staff Appraised", MessageBoxButton.OK);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Staff appraisal failed during processing! Please try again", "Staff Appraised", MessageBoxButton.OK);
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}

        
        //protected void ResponseToAppraisalCompleted()
        //{
        //    EventHandler handler = null;

        //    try
        //    {
        //        handler = (s, e) =>
        //        {
        //            ResponseToAppraisalCompletedHelper();
        //            appraisalService.ResponseToAppraisalCompleted -= handler;
        //        };

        //        appraisalService.ResponseToAppraisalCompleted += handler;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void ResponseToAppraisalCompletedHelper()
        //{
        //    try
        //    {
        //        dispatcher.BeginInvoke(() =>
        //        {
        //            Staff = appraisalService.Staff;
        //            Appraisal = appraisalService.Appraisal;
        //            if (Staff != null)
        //            {
        //                if (!IsAppraiseeSelected && !IsSecondLevelAppraiseeLoaded)
        //                {
        //                    LoadSupervisorAppraisees(Staff.CompanyDepartmentJobRoleId, PeriodId);
        //                }

        //                IsAppraiseeSelected = false;
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}

        protected void StaffAppraisalLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    StaffAppraisalLoadCompletedHelper();
                    appraisalService.StaffAppraisalLoadCompleted -= handler;
                };

                appraisalService.StaffAppraisalLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StaffAppraisalLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    Appraisal = appraisalService.Appraisal;
                    if (Appraisal != null)
                    {
                        if (Appraisal.Status == null)
                        {
                            Appraisal.Status = new Status();
                            Appraisal.Status.Name = "Not Started";
                        }
                    }

                    //else
                    //{
                    //    Appraisal = new Infrastructure.MangoService.Appraisal();
                    //    Appraisal.Status = new Status();
                    //    Appraisal.Status.Name = "Not Started";
                    //}
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        //private void StaffAppraisalLoadCompletedHelper()
        //{
        //    try
        //    {
        //        dispatcher.BeginInvoke(() =>
        //        {
        //            Appraisal = appraisalService.Appraisal;
        //            if (Appraisal != null)
        //            {
        //                if (string.IsNullOrEmpty(Appraisal.Status))
        //                {
        //                    Appraisal.Status = "Not Started";
        //                }
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}
        private void MetricesLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    MetricesLoadCompletedHelper();
                    appraisalService.MetricesLoadCompleted -= handler;
                };

                appraisalService.MetricesLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void MetricesLoadCompletedHelper()
        {
            try
            {
                decimal processTotalRating = 0;
                decimal peopleTotalRating = 0;

                dispatcher.BeginInvoke(() =>
                {
                    Metrices = appraisalService.Metrices;
                    Metrices.ProcessActualScoreTotal = 0;

                    foreach (Process process in Metrices.Processes)
                    {
                        process.PropertyChanged += new PropertyChangedEventHandler(process_PropertyChanged);
                        process.Rating = GetGrade(process.Score);

                        decimal score;
                        if (decimal.TryParse(process.Score.ToString(), out score))
                        {
                            process.Rating = GetGrade(process.MetricRatings, (decimal)process.Score);
                            processTotalRating += process.Rating;
                        }

                        if (process.MetricRatings != null && process.MetricRatings.Count > 0)
                        {
                            decimal maximumProcessRating = process.MetricRatings.Max(p => p.Rating.Id);
                            //decimal maximumProcessRating = process.MetricRatings.Max(p => p.Rating);

                            //metrices.ProcessActualScoreTotal += Math.Round((Convert.ToDecimal(process.Rating) / Convert.ToDecimal(5)) * process.Target, 2);
                            metrices.ProcessActualScoreTotal += Math.Round((Convert.ToDecimal(process.Rating) / maximumProcessRating) * process.Target, 2);
                        }
                    }

                    metrices.PeopleActualScoreTotal = 0;
                    foreach (People people in Metrices.Peoples)
                    {
                        people.PropertyChanged += new PropertyChangedEventHandler(people_PropertyChanged);
                        people.Rating = GetGrade(people.Score);

                        decimal score;
                        if (decimal.TryParse(people.Score.ToString(), out score))
                        {
                            people.Rating = GetGrade(people.MetricRatings, (decimal)people.Score);
                            peopleTotalRating += people.Rating;
                        }

                        if (people.MetricRatings != null && people.MetricRatings.Count > 0)
                        {
                            //decimal maximumPeopleRating = people.MetricRatings.Max(p => p.Rating);

                            decimal maximumPeopleRating = people.MetricRatings.Max(p => p.Rating.Id);
                            metrices.PeopleActualScoreTotal += Math.Round((Convert.ToDecimal(people.Rating) / maximumPeopleRating) * people.Target, 2);
                            //metrices.PeopleActualScoreTotal += Math.Round((Convert.ToDecimal(people.Rating) / Convert.ToDecimal(5)) * people.Target, 2);
                        }
                    }

                    decimal totalActualScore = Metrices.CustomerActualScoreTotal + Metrices.FinancialActualScoreTotal + Metrices.PeopleActualScoreTotal + Metrices.ProcessActualScoreTotal + Metrices.RiskActualScoreTotal;

                    //re-calculate 
                    TotalMetricScore = Metrices.CustomerSumTotal + Metrices.FinancialSumTotal + Metrices.PeopleSumTotal + Metrices.ProcessSumTotal + Metrices.RiskSumTotal;
                    TotalActualMetricScore = Convert.ToString(totalActualScore) + " %";
                    Metrices.Grade = GetGrade(totalActualScore);

                    //ComputeUiHeight();
                });

            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void PaceLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    PaceLoadCompletedHelper();
                    appraisalService.PaceLoadCompleted -= handler;
                };

                appraisalService.PaceLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void PaceLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    this.TotalPaceScore = appraisalService.TotalPaceScore;
                    Paces = appraisalService.Paces;

                    decimal paceRawScore = 0;
                    foreach (Pace pace in Paces)
                    {
                        pace.PropertyChanged += new PropertyChangedEventHandler(pace_PropertyChanged);
                        pace.Rating = GetPaceRating(pace.Score);
                        paceRawScore += pace.Score;
                    }

                    PaceRating = paceRawScore > 0 ? GetPaceRating(paceRawScore / Paces.Count) : GetPaceRating(paceRawScore);
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }


        private void StaffAssessmentLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    StaffAssessmentLoadCompletedHelper();
                    appraisalService.GetStaffAssessmentCompleted -= handler;
                };

                appraisalService.GetStaffAssessmentCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void StaffAssessmentLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    StaffAssessments = appraisalService.StaffAssessments;

                    foreach (StaffAssessment staffAssessment in StaffAssessments)
                    {
                        staffAssessment.PropertyChanged += new PropertyChangedEventHandler(staffAssessment_PropertyChanged);
                    }

                    if (StaffAssessments.Count > 0)
                    {
                        TotalStaffAssessmentScore = Math.Round((decimal)StaffAssessments.Sum(sa => sa.Score) / StaffAssessments.Count, 2);
                    }
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        public void staffAssessment_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Score"))
            {
                decimal sum = 0;
                foreach (StaffAssessment staffAssessment in StaffAssessments)
                {
                    sum += staffAssessment.Score;
                }

                if (StaffAssessments.Count > 0)
                {
                    TotalStaffAssessmentScore = Math.Round(sum / StaffAssessments.Count, 2);
                }
            }
        }

        private void StaffDetailLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    StaffDetailLoadCompletedHelper();
                    appraisalService.StaffDetailLoadCompleted -= handler;
                };

                appraisalService.StaffDetailLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void StaffDetailLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    Staff = appraisalService.Staff;
                    if (Staff != null)
                    {
                        if (!IsAppraiseeSelected && !IsSecondLevelAppraiseeLoaded)
                        {
                            LoadSupervisorAppraisees(Staff.CompanyDepartmentJobRoleId, PeriodId);
                        }

                        IsAppraiseeSelected = false;
                        LoadOthers(staffId, companyDepartmentJobRoleId, periodId);
                        LoadOption();

                        CheckIfStaffLevelExistInAssessment(periodId, staff);
                    }
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void StaffLevelExistInAssessmentCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    StaffLevelExistInAssessmentCompletedHelper();
                    appraisalService.IsStaffLevelExistInAssessmentCompleted -= handler;
                };

                appraisalService.IsStaffLevelExistInAssessmentCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void StaffLevelExistInAssessmentCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    DisplayPotentialAssessment = appraisalService.StaffLevelExistInAssessment;
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void SupervisorAppraiseeListLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    SupervisorAppraiseeListLoadCompletedHelper();
                    appraisalService.SupervisorAppraiseeListLoadCompleted -= handler;
                };

                appraisalService.SupervisorAppraiseeListLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void SupervisorAppraiseeListLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    Staffs = appraisalService.Staffs;
                    if (Staffs != null && Staffs.Count > 0)
                    {
                        //Staffs.Insert(0, new Infrastructure.MangoService.Staff() { Name = "< Select Appraisee >" });

                        StaffsView = new PagedCollectionView(Staffs);
                        LoadSeletedStaff();
                    }

                    //hide container
                    HideAppraissesUnderMeContainer();
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void CommentLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    CommentLoadCompletedHelper();
                    appraisalService.CommentLoadCompleted -= handler;
                };

                appraisalService.CommentLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void CommentLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    Comment = appraisalService.Comment;
                    if (Comment != null)
                    {
                        if (RecommendationsView != null)
                        {
                            IEnumerable<Recommendation> recommendations = (IEnumerable<Recommendation>)RecommendationsView.SourceCollection;
                            Recommendation recommendation = recommendations.Where(r => r.Id == Comment.RecommendationId).SingleOrDefault();
                            RecommendationsView.MoveCurrentTo(recommendation);
                        }
                        if (OptionsView != null)
                        {
                            IEnumerable<Option> options = (IEnumerable<Option>)OptionsView.SourceCollection;
                            Option option = options.Where(o => o.Id == Comment.OptionId).SingleOrDefault();
                            OptionsView.MoveCurrentTo(option);

                            UpdateUI();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void RecommendationLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    RecommendationLoadCompletedHelper();
                    appraisalService.RecommendationLoadCompleted -= handler;
                };

                appraisalService.RecommendationLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void RecommendationLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    Recommendations = appraisalService.Recommendations;
                    RecommendationsView = new PagedCollectionView(Recommendations);

                    LoadComment(staffId, periodId);
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void AppraisalAcceptOrRejectCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    AppraisalAcceptOrRejectCompletedHelper();
                    appraisalService.AppraisalAcceptOrRejectCompleted -= handler;
                };

                appraisalService.AppraisalAcceptOrRejectCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void AppraisalAcceptOrRejectCompletedHelper()
        {
            try
            {
               dispatcher.BeginInvoke(() =>
               {
                   if (appraisalService.IsAppraisalAcceptedOrRejected)
                   {
                       Option option = OptionsView.CurrentItem as Option;
                       string acceptMessage = "You have successfully accepted your appraisal. A mail has been sent to your HOD.";
                       string rejectMessage = "You have successfully rejected your appraisal. A mail has been sent to your Supervisor.";
                       string message = "";

                       if (option.Id == 1)
                       {
                           Appraisal.Status.Name = "Almost Completed";
                           message = acceptMessage;
                       }
                       else if (option.Id == 2)
                       {
                           message = rejectMessage;
                       }

                       IsProcessing = false;
                       DisableButtons();
                       MessageBox.Show(message, "RESPONSE TO APPRAISAL", MessageBoxButton.OK);
                   }
                   else
                   {
                       IsProcessing = false;
                       MessageBox.Show("Error occurred! Please try again.", "RESPONSE TO APPRAISAL", MessageBoxButton.OK);
                   }
               });
            }
            catch (Exception ex)
            {
                IsProcessing = false;
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void CurrentAppraisalDetailsLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    CurrentAppraisalDetailsLoadCompletedHelper();
                    appraisalService.CurrentAppraisalDetailsLoadCompleted -= handler;
                };

                appraisalService.CurrentAppraisalDetailsLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void CurrentAppraisalDetailsLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() => { Period = appraisalService.Period; });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void AppraiseeSupervisorLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    AppraiseeSupervisorLoadCompletedHelper();
                    appraisalService.AppraiseeSupervisorLoadCompleted -= handler;
                };

                appraisalService.AppraiseeSupervisorLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void AppraiseeSupervisorLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() => { StaffSupervisor = appraisalService.StaffSupervisor; });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void AppraiseeHodGetCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    AppraiseeHodGetCompletedHelper();
                    appraisalService.AppraiseeHodGetCompleted -= handler;
                };

                appraisalService.AppraiseeHodGetCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void AppraiseeHodGetCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() => { StaffHod = appraisalService.StaffHod; });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void HodAppraiseesLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    HodAppraiseesLoadCompletedHelper();
                    appraisalService.HodAppraiseesLoadCompleted -= handler;
                };

                appraisalService.HodAppraiseesLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void HodAppraiseesLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    IsAppraiseePresent = true;
                    Staffs = appraisalService.Staffs;

                    if (Staffs != null)
                    {
                        StaffsView = new PagedCollectionView(Staffs);
                        LoadSeletedStaff();
                    }
                    else
                    {
                        Staffs = new ObservableCollection<Staff>();
                        StaffsView = new PagedCollectionView(Staffs);
                    }
                                        
                    //HideAppraissesUnderMeContainer();
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void OptionLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    OptionLoadCompletedHelper();
                    appraisalService.OptionLoadCompleted -= handler;
                };

                appraisalService.OptionLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void OptionLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    Options = appraisalService.Options;
                    OptionsView = new PagedCollectionView(Options);

                    LoadRecommendation();
                });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void GradeScaleLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    GradeScaleLoadCompletedHelper();
                    appraisalService.GradeScaleLoadCompleted -= handler;
                };

                appraisalService.GradeScaleLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void GradeScaleLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() => { GradeScales = appraisalService.GradeScales; });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void PaceRatingLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    PaceRatingLoadCompletedHelper();
                    appraisalService.PaceRatingLoadCompleted -= handler;
                };

                appraisalService.PaceRatingLoadCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void PaceRatingLoadCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() => { PaceRatings = appraisalService.PaceRatings; });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void AppraisalEditingCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    AppraisalModificationCompletedHelper();
                    appraisalService.AppraisalModificationCompleted -= handler;
                };

                appraisalService.AppraisalModificationCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void AppraisalModificationCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke(() =>
                {
                    DisableButtons();
                    IsProcessing = false;

                    if (appraisalService.IsAppraisalModified)
                    {
                        switch (StaffType)
                        {
                            case 2:
                            case 3:
                                {
                                    if (AppraisalState == (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee)
                                    {
                                        int companyDepartmentJobRoleId = LoggedInStaff.CompanyDepartmentJobRoleId; // Convert.ToInt32(LoggedInStaff["companyDepartmentJobRoleId"]);
                                        byte optionId = 1;

                                        LoadHodAppraisees(companyDepartmentJobRoleId, periodId, optionId);
                                        Appraisal.Status.Name = "Completed";
                                    }

                                    //IsProcessing = false;
                                    MessageBox.Show("You have successfully appraised your staff. A mail has been sent to the Appraisee", "STAFF APPRAISAL", MessageBoxButton.OK);
                                    break;
                                }
                        }
                    }
                    else
                    {
                        //IsProcessing = false;
                        MessageBox.Show("Error occurred! During staff appraisal.", "STAFF APPRAISAL", MessageBoxButton.OK);
                    }
                });
            }
            catch (Exception ex)
            {
                IsProcessing = false;
                Utility.DisplayMessage(ex.Message);
            }
        }
               
        //private void AppraisalModificationCompletedHelper()
        //{
        //    try
        //    {
        //       dispatcher.BeginInvoke(() =>
        //       {
        //           if (appraisalService.IsAppraisalModified)
        //           {
        //               switch (StaffType)
        //               {
        //                   case 2:
        //                   case 3:
        //                       {
        //                           if (AppraisalState == (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee)
        //                           {
        //                               int companyDepartmentJobRoleId = LoggedInStaff.CompanyDepartmentJobRoleId; // Convert.ToInt32(LoggedInStaff["companyDepartmentJobRoleId"]);
        //                               byte optionId = 1;

        //                               LoadHodAppraisees(companyDepartmentJobRoleId, periodId, optionId);

        //                               Appraisal.Status = "Completed";
        //                           }

        //                           MessageBox.Show("You have successfully appraised your staff.", "STAFF APPRAISAL", MessageBoxButton.OK);
        //                           break;
        //                       }
        //               }
        //           }
        //           else
        //           {
        //               MessageBox.Show("Error occurred! During staff appraisal.", "STAFF APPRAISAL", MessageBoxButton.OK);
        //           }
        //       });
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}

        //private void DataLoadCompleted()
        //{
        //    //var dispatcher = Deployment.Current.Dispatcher;

        //    //appraisalService.AppraiseStaffCompleted += (s, e) =>
        //    //{
        //    //    dispatcher.BeginInvoke(() =>
        //    //    {
        //    //        if (appraisalService.Happy)
        //    //        {
        //    //            Appraisal.Status = "Uncompleted";
        //    //            MessageBox.Show("Staff has been successfully appraised.", "Staff Appraised", MessageBoxButton.OK);
        //    //        }
        //    //        else
        //    //        {
        //    //            MessageBox.Show("Staff appraisal failed during processing! Please try again", "Staff Appraised", MessageBoxButton.OK);
        //    //        }
        //    //    });
        //    //};
        //    //appraisalService.AppraisalAcceptOrRejectCompleted += (s, e) =>
        //    //{
        //    //    if (appraisalService.IsAppraisalAcceptedOrRejected)
        //    //    {
        //    //        Option option = OptionsView.CurrentItem as Option;
        //    //        string acceptMessage = "You have successfully accepted your appraisal. A mail has been sent to your HOD.";
        //    //        string rejectMessage = "You have successfully rejected your appraisal. A mail has been sent to your Supervisor.";
        //    //        string message = "";

        //    //        if (option.Id == 1)
        //    //        {
        //    //            Appraisal.Status = "Almost Completed";
        //    //            message = acceptMessage;
        //    //        }
        //    //        else if (option.Id == 2)
        //    //        {
        //    //            message = rejectMessage;
        //    //        }

        //    //        MessageBox.Show(message, "RESPONSE TO APPRAISAL", MessageBoxButton.OK);
        //    //    }
        //    //    else
        //    //    {
        //    //        MessageBox.Show("Error occurred! Please try again.", "RESPONSE TO APPRAISAL", MessageBoxButton.OK);
        //    //    }
        //    //};
        //    //appraisalService.AppraisalModificationCompleted += (s, e) =>
        //    //{
        //    //    if (appraisalService.IsAppraisalModified)
        //    //    {
        //    //        switch (StaffType)
        //    //        {
        //    //            case 2:
        //    //            case 3:
        //    //                {
        //    //                    if (AppraisalState == (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee)
        //    //                    {
        //    //                        int companyDepartmentJobRoleId = LoggedInStaff.CompanyDepartmentJobRoleId; // Convert.ToInt32(LoggedInStaff["companyDepartmentJobRoleId"]);
        //    //                        byte optionId = 1;

        //    //                        LoadHodAppraisees(companyDepartmentJobRoleId, periodId, optionId);

        //    //                        Appraisal.Status = "Completed";
        //    //                    }
                                
        //    //                    //LoadSeletedStaff();
        //    //                    MessageBox.Show("You have successfully appraised your staff.", "STAFF APPRAISAL", MessageBoxButton.OK);
        //    //                    break;
        //    //                }
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        MessageBox.Show("Error occurred! During staff appraisal.", "STAFF APPRAISAL", MessageBoxButton.OK);
        //    //    }
        //    //};
        //    //appraisalService.ResponseToAppraisalCompleted += (s, e) =>
        //    //{
        //    //    if (appraisalService.Happy)
        //    //    {
        //    //        switch (StaffType)
        //    //        {
        //    //            case 1:
        //    //                {
        //    //                    AppraisalSvc.Option option = OptionsView.CurrentItem as AppraisalSvc.Option;
        //    //                    MessageBox.Show("You have successfully " + option.Name.ToLower() + "ed your appraisal. A mail has been sent to your HOD.", "APPRAISAL", MessageBoxButton.OK);
        //    //                    break;
        //    //                }
        //    //            //case 2:
        //    //            //    {

        //    //            //    }
        //    //            case 3:
        //    //                {
        //    //                    MessageBox.Show("You have successfully appraised your staff.", "APPRAISAL", MessageBoxButton.OK);
        //    //                    break;
        //    //                }
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        MessageBox.Show("Error occurred! Please try again.", "APPRAISAL", MessageBoxButton.OK);
        //    //    }
        //    //};
        //    //appraisalService.PaceRatingLoadCompleted += (s, e) =>
        //    //{
        //    //    dispatcher.BeginInvoke(() => { PaceRatings = appraisalService.PaceRatings; });
        //    //};
        //    //appraisalService.GradeScaleLoadCompleted += (s, e) =>
        //    //{
        //    //    dispatcher.BeginInvoke(() => { GradeScales = appraisalService.GradeScales; });
        //    //};
        //    //appraisalService.AppraiseeSupervisorLoadCompleted += (s, e) =>
        //    //{
        //    //    dispatcher.BeginInvoke(() => { StaffSupervisor = appraisalService.StaffSupervisor; });
        //    //};
        //    //appraisalService.AppraiseeHodGetCompleted += (s, e) =>
        //    //{
        //    //    dispatcher.BeginInvoke(() => { StaffHod = appraisalService.StaffHod; });
        //    //};
        //    //appraisalService.StaffDetailLoadCompleted += (s, e) =>
        //    //    {
        //    //        dispatcher.BeginInvoke(() =>
        //    //        {
        //    //            Staff = appraisalService.Staff;
        //    //            if (Staff != null)
        //    //            {
        //    //                if (!IsAppraiseeSelected && !IsSecondLevelAppraiseeLoaded)
        //    //                {
        //    //                    //LoadSupervisorAppraisees(Staff.CompanyDepartmentJobRoleId);
        //    //                    LoadSupervisorAppraisees(Staff.CompanyDepartmentJobRoleId, PeriodId);
        //    //                }

        //    //                IsAppraiseeSelected = false;
        //    //            }
        //    //        });
        //    //    };
        //    //appraisalService.PaceLoadCompleted += (s, e) =>
        //    //    {
        //    //        dispatcher.BeginInvoke(() =>
        //    //        {
        //    //            this.TotalPaceScore = appraisalService.TotalPaceScore;
        //    //            Paces = appraisalService.Paces;

        //    //            decimal paceRawScore = 0;
        //    //            foreach (Pace pace in Paces)
        //    //            {
        //    //                pace.PropertyChanged += new PropertyChangedEventHandler(pace_PropertyChanged);
        //    //                pace.Rating = GetPaceRating(pace.Score);
        //    //                paceRawScore += pace.Score;
        //    //            }

        //    //            PaceRating = paceRawScore > 0 ? GetPaceRating(paceRawScore / Paces.Count) : GetPaceRating(paceRawScore);
        //    //        });
        //    //    };
        //    //appraisalService.CurrentAppraisalDetailsLoadCompleted += (s, e) =>
        //    //    {
        //    //        dispatcher.BeginInvoke(() => { Period = appraisalService.Period; });
        //    //    };
        //    //appraisalService.StaffAppraisalLoadCompleted += (s, e) =>
        //    //{
        //    //    dispatcher.BeginInvoke(() =>
        //    //       {
        //    //           Appraisal = appraisalService.Appraisal;
        //    //           if (Appraisal != null)
        //    //           {
        //    //               if (string.IsNullOrEmpty(Appraisal.Status))
        //    //               {
        //    //                   Appraisal.Status = "Not Started";
        //    //               }
        //    //           }
        //    //       });
        //    //};
        //    //appraisalService.MetricesLoadCompleted += (s, e) =>
        //    //    {
        //    //        decimal processTotalRating = 0;
        //    //        decimal peopleTotalRating = 0;

        //    //        dispatcher.BeginInvoke(() =>
        //    //        {
        //    //          Metrices = appraisalService.Metrices;
        //    //          Metrices.ProcessActualScoreTotal = 0;
                     
        //    //          foreach (Process process in Metrices.Processes)
        //    //          {
        //    //              process.PropertyChanged += new PropertyChangedEventHandler(process_PropertyChanged);
        //    //              process.Rating = GetGrade(process.Score);

        //    //              decimal score;
        //    //              if (decimal.TryParse(process.Score.ToString(), out score))
        //    //              {
        //    //                  process.Rating = GetGrade(process.MetricRatings, (double)process.Score);
        //    //                  processTotalRating += process.Rating;
        //    //              }

        //    //              if (process.MetricRatings != null && process.MetricRatings.Count > 0)
        //    //              {
        //    //                  decimal maximumProcessRating = process.MetricRatings.Max(p => p.Rating);

        //    //                  //metrices.ProcessActualScoreTotal += Math.Round((Convert.ToDecimal(process.Rating) / Convert.ToDecimal(5)) * process.Target, 2);
        //    //                  metrices.ProcessActualScoreTotal += Math.Round((Convert.ToDecimal(process.Rating) / maximumProcessRating) * process.Target, 2);
        //    //              }
        //    //          }

        //    //          metrices.PeopleActualScoreTotal = 0;
        //    //          foreach (People people in Metrices.Peoples)
        //    //          {
        //    //              people.PropertyChanged += new PropertyChangedEventHandler(people_PropertyChanged);
        //    //              people.Rating = GetGrade(people.Score);

        //    //              decimal score;
        //    //              if (decimal.TryParse(people.Score.ToString(), out score))
        //    //              {
        //    //                  people.Rating = GetGrade(people.MetricRatings, (double)people.Score);
        //    //                  peopleTotalRating += people.Rating;
        //    //              }

        //    //              if (people.MetricRatings != null && people.MetricRatings.Count > 0)
        //    //              {
        //    //                  decimal maximumPeopleRating = people.MetricRatings.Max(p => p.Rating);
        //    //                  metrices.PeopleActualScoreTotal += Math.Round((Convert.ToDecimal(people.Rating) / maximumPeopleRating) * people.Target, 2);
        //    //                  //metrices.PeopleActualScoreTotal += Math.Round((Convert.ToDecimal(people.Rating) / Convert.ToDecimal(5)) * people.Target, 2);
        //    //              }
        //    //          }

        //    //          decimal totalActualScore = Metrices.CustomerActualScoreTotal + Metrices.FinancialActualScoreTotal + Metrices.PeopleActualScoreTotal + Metrices.ProcessActualScoreTotal + Metrices.RiskActualScoreTotal;

        //    //          //re-calculate 
        //    //          TotalMetricScore = Metrices.CustomerSumTotal + Metrices.FinancialSumTotal + Metrices.PeopleSumTotal + Metrices.ProcessSumTotal + Metrices.RiskSumTotal;
        //    //          TotalActualMetricScore = Convert.ToString(totalActualScore) + " %";
        //    //          Metrices.Grade = GetGrade(totalActualScore);

        //    //          //ComputeUiHeight();
        //    //      });
        //    //    };
        //    //appraisalService.OptionLoadCompleted += (s, e) =>
        //    //    {
        //    //        dispatcher.BeginInvoke(() =>
        //    //        {
        //    //            Options = appraisalService.Options;
        //    //            OptionsView = new PagedCollectionView(Options);

        //    //            LoadRecommendation();
        //    //        });
        //    //    };
        //    //appraisalService.RecommendationLoadCompleted += (s, e) =>
        //    //    {
        //    //        dispatcher.BeginInvoke(() =>
        //    //        {
        //    //            Recommendations = appraisalService.Recommendations;
        //    //            RecommendationsView = new PagedCollectionView(Recommendations);

        //    //            //if (Staff != null)
        //    //            //{
        //    //            //    LoadComment(Staff.Id, periodId);
        //    //            //}

        //    //            LoadComment(staffId, periodId);
        //    //        });
        //    //    };
        //    //appraisalService.CommentLoadCompleted += (s, e) =>
        //    //    {
        //    //        dispatcher.BeginInvoke(() =>
        //    //        {
        //    //            Comment = appraisalService.Comment;
        //    //            if (Comment != null)
        //    //            {
        //    //                if (RecommendationsView != null)
        //    //                {
        //    //                    IEnumerable<Recommendation> recommendations = (IEnumerable<Recommendation>)RecommendationsView.SourceCollection;
        //    //                    Recommendation recommendation = recommendations.Where(r => r.Id == Comment.RecommendationId).SingleOrDefault();
        //    //                    RecommendationsView.MoveCurrentTo(recommendation);

        //    //                    //RecommendationsView.MoveCurrentToPosition(Comment.RecommendationId);
        //    //                }
        //    //                if (OptionsView != null)
        //    //                {
        //    //                    //LockPaceAndMetrics = Comment.OptionId == 1 ? false : true;

        //    //                    IEnumerable<Option> options = (IEnumerable<Option>)OptionsView.SourceCollection;
        //    //                    Option option = options.Where(o => o.Id == Comment.OptionId).SingleOrDefault();
        //    //                    OptionsView.MoveCurrentTo(option);

        //    //                    //OptionsView.MoveCurrentToPosition(Comment.OptionId);
        //    //                    UpdateUI();

        //    //                    // UnLockPaceAndMetrics();
        //    //                }

        //    //                LoadOthers(staffId, companyDepartmentJobRoleId, periodId);
        //    //            }
        //    //        });
        //    //    };
        //    //appraisalService.SupervisorAppraiseeListLoadCompleted += (s, e) =>
        //    //    {
        //    //       dispatcher.BeginInvoke(() =>
        //    //       {
        //    //           Staffs = appraisalService.Staffs;
        //    //           if (Staffs != null)
        //    //           {
        //    //               StaffsView = new PagedCollectionView(Staffs);
        //    //               LoadSeletedStaff();
        //    //           }

        //    //           //hide container
        //    //           HideAppraissesUnderMeContainer();
        //    //       });
        //    //    };
        //    //appraisalService.HodAppraiseesLoadCompleted += (s, e) =>
        //    //{
        //    //    dispatcher.BeginInvoke(() =>
        //    //       {
        //    //           Staffs = appraisalService.Staffs;

        //    //           if (Staffs != null)
        //    //           {
        //    //               StaffsView = new PagedCollectionView(Staffs);
        //    //               LoadSeletedStaff();
        //    //           }
        //    //           else
        //    //           {
        //    //               Staffs = new ObservableCollection<Staff>();
        //    //               StaffsView = new PagedCollectionView(Staffs);
        //    //           }

        //    //       });
        //    //};
           
        //}


        private void HideAppraissesUnderMeContainer()
        {
            if (Staffs != null && Staffs.Count > 0)
            {
                IsAppraiseePresent = true;
            }
            else
            {
                IsAppraiseePresent = false;
            }
        }

        public void pace_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Score"))
            {
                //int rowCount = 0;
                decimal sum = 0;
                foreach (Pace pace in Paces)
                {
                    sum += pace.Score;
                    if (pace.Score > 0)
                    {
                        pace.Rating = GetPaceRating(pace.Score);
                    }
                }
                                
                //TotalPaceScore = Math.Round(((sum / Paces.Count)/105) * 100, 2);
                TotalPaceScore = Math.Round(sum / Paces.Count, 2);
                PaceRating = GetPaceRating(TotalPaceScore);
            }
        }
        private void process_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Score"))
            {
                decimal sum = 0;
                decimal totalRating = 0;
                decimal totalActualScore = 0;

                Metrices.Grade = 0;
                metrices.ProcessActualScoreTotal = 0;
                foreach (Process process in Metrices.Processes)
                {
                    ////update grade
                    //if (process.Score > 0)
                    //{
                    //    process.Rating = GetGrade(process.MetricRatings, (double)process.Score);
                    //    totalRating += process.Rating;
                    //}

                    //update grade
                    decimal score;
                    if (decimal.TryParse(process.Score.ToString(), out score))
                    {
                        process.Rating = GetGrade(process.MetricRatings, (decimal)process.Score);
                        totalRating += process.Rating;
                    }

                    decimal maximumProcessRating = process.MetricRatings.Max(p => p.Rating.Id);

                    sum += process.Score;
                    metrices.ProcessActualScoreTotal += Math.Round((Convert.ToDecimal(process.Rating) / maximumProcessRating) * process.Target, 2);
                    //metrices.ProcessActualScoreTotal += Math.Round((Convert.ToDecimal(process.Rating) / Convert.ToDecimal(5)) * process.Target, 2);
                }

                Metrices.ProcessSumTotal = sum;
                Metrices.Grade += (byte)totalRating;
                totalActualScore = Metrices.CustomerActualScoreTotal + Metrices.FinancialActualScoreTotal + Metrices.PeopleActualScoreTotal + Metrices.ProcessActualScoreTotal + Metrices.RiskActualScoreTotal;
                //Metrices.ProcessActualScoreTotal = metrices.ProcessActualScoreTotal;

                //re-calculate 
                TotalMetricScore = Metrices.CustomerSumTotal + Metrices.FinancialSumTotal + Metrices.PeopleSumTotal + Metrices.ProcessSumTotal + Metrices.RiskSumTotal;
                TotalActualMetricScore = Convert.ToString(totalActualScore) + " %";
                Metrices.Grade = GetGrade(totalActualScore);
            }
        }
        private void people_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Score"))
            {
                decimal sum = 0;
                decimal totalRating = 0;
                decimal totalActualScore = 0;

                //AppraisalSvc.Metric baseMetric = metric;
                Metrices.Grade = 0;
                metrices.PeopleActualScoreTotal = 0;
                foreach (People people in Metrices.Peoples)
                {
                    ////update grade
                    //if (people.Score > 0)
                    decimal score;
                    if (decimal.TryParse(people.Score.ToString(), out score))
                    {
                        people.Rating = GetGrade(people.MetricRatings, (decimal)people.Score);
                        totalRating += people.Rating;
                    }

                    sum += people.Score;

                    decimal maximumPeopleRating = people.MetricRatings.Max(p => p.Rating.Id);
                    metrices.PeopleActualScoreTotal += Math.Round((Convert.ToDecimal(people.Rating) / maximumPeopleRating) * people.Target, 2);
                    //metrices.PeopleActualScoreTotal += Math.Round((Convert.ToDecimal(people.Rating) / Convert.ToDecimal(5)) * people.Target, 2);
                }

                Metrices.PeopleSumTotal = sum;
                Metrices.Grade += (byte)totalRating;
                totalActualScore = Metrices.CustomerActualScoreTotal + Metrices.FinancialActualScoreTotal + Metrices.PeopleActualScoreTotal + Metrices.ProcessActualScoreTotal + Metrices.RiskActualScoreTotal;
                //Metrices.PeopleActualScoreTotal = totalRating;

                //re-calculate 
                TotalMetricScore = Metrices.CustomerSumTotal + Metrices.FinancialSumTotal + Metrices.PeopleSumTotal + Metrices.ProcessSumTotal + Metrices.RiskSumTotal;
                TotalActualMetricScore = Convert.ToString(totalActualScore) + " %";
                Metrices.Grade = GetGrade(totalActualScore);

            }
        }
        private byte GetGrade(ObservableCollection<MetricRating> metricRatings, decimal score)
        {
            byte finalGrade = 0;

            if (metricRatings != null)
            {
                if (metricRatings.Count > 0)
                {
                    foreach (MetricRating rating in metricRatings)
                    {
                        if (score >= rating.From && score <= rating.To)
                        {
                            finalGrade = rating.Rating.Id;
                            break;
                        }
                    }
                }
            }

            return finalGrade;
        }
        private byte GetGrade(decimal score)
        {
            byte finalGrade = 0;

            if (GradeScales != null)
            {
                if (GradeScales.Count > 0)
                {
                    foreach (GradeScale grade in GradeScales)
                    {
                        if (score >= grade.From && score <= grade.To)
                        {
                            finalGrade = grade.Id;
                            break;
                        }
                    }
                }
            }

            return finalGrade;
        }
        private string GetPaceRating(decimal score)
        {
            string finalRating = null;

            if (GradeScales != null)
            {
                if (GradeScales.Count > 0)
                {
                    foreach (PaceRating rating in PaceRatings)
                    {
                        if (score >= rating.From && score <= rating.To)
                        {
                            finalRating = rating.Rating;
                            break;
                        }
                    }
                }
            }

            return finalRating;
        }

        private void UpdateUI()
        {
            try
            {
                if (AppraisalState == (int)AppraisalEnum.State.StaffLoaded || AppraisalState == (int)AppraisalEnum.State.SupervisorLoaded || AppraisalState == (int)AppraisalEnum.State.HodLoaded)
                {
                    if (Comment != null && Staff != null)
                    {
                        if (Comment.OptionId == 1)
                        {
                            if (Staff.Type == 1)
                            {
                                //AppraisalState = (int)AppraisalEnum.State.CompletedStaffAppraisal;
                                if (Appraisal.Status.Id == 1 || Appraisal.Status.Id == 0)
                                {
                                    AppraisalState = (int)AppraisalEnum.State.UnAppraisedStaff;
                                }
                                else if (Appraisal.Status.Id == 2)
                                {
                                    AppraisalState = (int)AppraisalEnum.State.IncompletedStaffAppraisal;
                                }
                                else if (Appraisal.Status.Id == 3)
                                {
                                    AppraisalState = (int)AppraisalEnum.State.CompletedStaffAppraisal;
                                }
                            }
                            else if (Staff.Type == 2)
                            {
                                //AppraisalState = (int)AppraisalEnum.State.CompletedSupervisorAppraisal;

                                if (Appraisal.Status.Id == 1 || Appraisal.Status.Id == 0)
                                {
                                    AppraisalState = (int)AppraisalEnum.State.UnAppraisedSupervisor;
                                }
                                else if (Appraisal.Status.Id == 2)
                                {
                                    AppraisalState = (int)AppraisalEnum.State.IncompletedSupervisorAppraisal;
                                }
                                else if (Appraisal.Status.Id == 3)
                                {
                                    //AppraisalState = (int)AppraisalEnum.State.IncompletedHodAppraisal;
                                    AppraisalState = (int)AppraisalEnum.State.CompletedSupervisorAppraisal;
                                }
                            }
                            else if (Staff.Type == 3)
                            {
                                //AppraisalState = (int)AppraisalEnum.State.CompletedHodAppraisal;

                                if (Appraisal.Status.Id == 1 || Appraisal.Status.Id == 0)
                                {
                                    AppraisalState = (int)AppraisalEnum.State.CompletedStaffAppraisal;
                                }
                                else if (Appraisal.Status.Id == 2)
                                {
                                    AppraisalState = (int)AppraisalEnum.State.CompletedSupervisorAppraisal;
                                }
                                else if (Appraisal.Status.Id == 3)
                                {
                                    AppraisalState = (int)AppraisalEnum.State.CompletedHodAppraisal;
                                }
                            }

                            LockPaceAndMetrics = false;
                            EnableSubmitButton = false;
                        }
                        else if (Comment.OptionId == 2)
                        {
                            if (Staff.Type == 1)
                            {
                                //LockPaceAndMetrics = false;
                                //EnableSubmitButton = true;
                                AppraisalState = (int)AppraisalEnum.State.IncompletedStaffAppraisal;
                            }
                            else if (Staff.Type == 2)
                            {
                                //LockPaceAndMetrics = false;
                                //EnableSubmitButton = true;
                                AppraisalState = (int)AppraisalEnum.State.IncompletedSupervisorAppraisal;
                            }
                            else if (Staff.Type == 3)
                            {
                                //LockPaceAndMetrics = false;
                                //EnableSubmitButton = false;
                                AppraisalState = (int)AppraisalEnum.State.IncompletedHodAppraisal;
                            }

                            LockPaceAndMetrics = false;
                            EnableSubmitButton = true;
                        }
                        else
                        {
                            if (Appraisal != null)
                            {
                                if (Staff.Type == 1)
                                {
                                    if (Appraisal.Status.Id == 1 || Appraisal.Status.Id == 0)
                                    {
                                        LockPaceAndMetrics = false;
                                        EnableSubmitButton = false;
                                        SaveToContinueLater = false;

                                        AppraisalState = (int)AppraisalEnum.State.UnAppraisedStaff;
                                    }
                                    else if (Appraisal.Status.Id == 2)
                                    {
                                        LockPaceAndMetrics = false;
                                        EnableSubmitButton = true;
                                        AppraisalState = (int)AppraisalEnum.State.IncompletedStaffAppraisal;
                                    }
                                    else if (Appraisal.Status.Id == 3)
                                    {
                                        LockPaceAndMetrics = false;
                                        EnableSubmitButton = false;
                                        AppraisalState = (int)AppraisalEnum.State.CompletedStaffAppraisal;
                                    }
                                }
                                else if (Staff.Type == 2)
                                {
                                    //LockPaceAndMetrics = true;
                                    //AppraisalState = (int)AppraisalEnum.State.UnAppraisedSupervisor;

                                    if (Appraisal.Status.Id == 1 || Appraisal.Status.Id == 0)
                                    {
                                        LockPaceAndMetrics = false;
                                        EnableSubmitButton = false;
                                        SaveToContinueLater = false;

                                        AppraisalState = (int)AppraisalEnum.State.UnAppraisedSupervisor;

                                       
                                    }
                                    else if (Appraisal.Status.Id == 2)
                                    {
                                        LockPaceAndMetrics = false;
                                        EnableSubmitButton = true;
                                        AppraisalState = (int)AppraisalEnum.State.IncompletedSupervisorAppraisal;
                                    }
                                    else if (Appraisal.Status.Id == 3)
                                    {
                                        LockPaceAndMetrics = false;
                                        EnableSubmitButton = false;
                                        AppraisalState = (int)AppraisalEnum.State.CompletedSupervisorAppraisal;
                                    }
                                }
                                else if (Staff.Type == 3)
                                {
                                    //LockPaceAndMetrics = false;
                                    //AppraisalState = (int)AppraisalEnum.State.UnAppraisedHod;

                                    if (Appraisal.Status.Id == 1 || Appraisal.Status.Id == 0)
                                    {
                                        EnableSubmitButton = false;
                                        SaveToContinueLater = false;

                                        AppraisalState = (int)AppraisalEnum.State.UnAppraisedHod;
                                        

                                    }
                                    else if (Appraisal.Status.Id == 2)
                                    {
                                        AppraisalState = (int)AppraisalEnum.State.IncompletedHodAppraisal;
                                        EnableSubmitButton = true;
                                    }
                                    else if (Appraisal.Status.Id == 3)
                                    {
                                        AppraisalState = (int)AppraisalEnum.State.CompletedHodAppraisal;
                                        EnableSubmitButton = false;
                                    }

                                    LockPaceAndMetrics = false;

                                }
                            }
                        }
                    }
                }
                else if (AppraisalState == (int)AppraisalEnum.State.SupervisorLodingAppraisee)
                {
                    if (Comment.OptionId == 1)
                    {
                        LockPaceAndMetrics = false;
                        EnableSubmitButton = false;
                        AppraisalState = (int)AppraisalEnum.State.AppraisalCompleted;
                    }
                    else
                    {
                        LockPaceAndMetrics = true;
                        EnableSubmitButton = true;
                        AppraisalState = (int)AppraisalEnum.State.IncompleteAppraisal;

                        SetSaveToContinueLaterButton();

                    }
                }
                else if (AppraisalState == (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee)
                {
                    //if (Comment.OptionId == 1)
                    //{
                    //    //LockPaceAndMetrics = true;
                    //    AppraisalState = (int)AppraisalEnum.State.AppraisalCompleted;
                    //}
                    //else
                    //{

                    //    AppraisalState = (int)AppraisalEnum.State.IncompleteAppraisal;
                    //}


                    //AppraisalState = (int)AppraisalEnum.State.HodSecondLevelAppraiseeAppraisal;
                    LockPaceAndMetrics = false;
                    EnableSubmitButton = true;
                }

                switch (AppraisalState)
                {
                    case (int)AppraisalEnum.State.UnAppraisedStaff:
                    case (int)AppraisalEnum.State.UnAppraisedSupervisor:
                    case (int)AppraisalEnum.State.UnAppraisedHod:
                        {
                            IsStrenghtAndWeakness = false;
                            IsTrainingNeeds = false;
                            IsSupervisorComment = false;
                            IsAppraiseeComment = false;
                            IsHodComment = false;
                            IsRecommendation = false;
                            IsOption = false;

                            break;
                        }
                    case (int)AppraisalEnum.State.IncompletedStaffAppraisal:
                    case (int)AppraisalEnum.State.IncompletedSupervisorAppraisal:
                    case (int)AppraisalEnum.State.IncompletedHodAppraisal:
                        {
                            IsStrenghtAndWeakness = false;
                            IsTrainingNeeds = false;
                            IsSupervisorComment = false;
                            IsAppraiseeComment = true;
                            IsHodComment = false;
                            IsRecommendation = false;
                            IsOption = true;

                            //IsStrenghtAndWeakness = false;
                            //IsTrainingNeeds = false;
                            //IsSupervisorComment = false;
                            //IsAppraiseeComment = false;
                            //IsHodComment = false;
                            //IsRecommendation = false;
                            //IsOption = false;

                            break;
                        }
                    case (int)AppraisalEnum.State.CompletedStaffAppraisal:
                    case (int)AppraisalEnum.State.CompletedSupervisorAppraisal:
                    case (int)AppraisalEnum.State.CompletedHodAppraisal:
                        {
                            IsStrenghtAndWeakness = false;
                            IsTrainingNeeds = false;
                            IsSupervisorComment = false;
                            IsAppraiseeComment = false;
                            IsHodComment = false;
                            IsRecommendation = false;
                            IsOption = false;

                            //LockPaceAndMetrics = false;

                            break;
                        }
                    case (int)AppraisalEnum.State.AppraisalCompleted:
                        {
                            IsStrenghtAndWeakness = false;
                            IsTrainingNeeds = false;
                            IsSupervisorComment = false;
                            IsAppraiseeComment = false;
                            IsHodComment = false;
                            IsRecommendation = false;
                            IsOption = false;

                            //LockPaceAndMetrics = false;

                            break;
                        }
                    case (int)AppraisalEnum.State.IncompleteAppraisal:
                        {
                            IsStrenghtAndWeakness = true;
                            IsTrainingNeeds = true;
                            IsSupervisorComment = true;
                            IsAppraiseeComment = false;
                            IsHodComment = false;
                            IsRecommendation = true;
                            IsOption = false;

                            break;
                        }
                    case (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee:
                    case (int)AppraisalEnum.State.HodSecondLevelAppraiseeAppraisal:
                        {
                            IsStrenghtAndWeakness = false;
                            IsTrainingNeeds = false;
                            IsSupervisorComment = false;
                            IsAppraiseeComment = false;
                            IsHodComment = true;
                            IsRecommendation = false;
                            IsOption = false;

                            //LockPaceAndMetrics = true;

                            break;
                        }
                }

                
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void SetSaveToContinueLaterButton()
        {
            if (Appraisal.Status.Id == 1 || Appraisal.Status.Id == 0)
            {
                SaveToContinueLater = true;
            }
            else
            {
                SaveToContinueLater = false;
            }
        }

        //private void UpdateUI()
        //{
        //    if (AppraisalState == (int)AppraisalEnum.State.StaffLoaded || AppraisalState == (int)AppraisalEnum.State.SupervisorLoaded || AppraisalState == (int)AppraisalEnum.State.HodLoaded)
        //    {
        //        if (Comment != null && Staff != null)
        //        {
        //            if (Comment.OptionId == 1)
        //            {
        //                if (Staff.Type == 1)
        //                {
        //                    //AppraisalState = (int)AppraisalEnum.State.CompletedStaffAppraisal;
        //                    if (Appraisal.StatusId == 1 || Appraisal.StatusId == 0)
        //                    {
        //                        AppraisalState = (int)AppraisalEnum.State.UnAppraisedStaff;
        //                    }
        //                    else if (Appraisal.StatusId == 2)
        //                    {
        //                        AppraisalState = (int)AppraisalEnum.State.IncompletedStaffAppraisal;
        //                    }
        //                    else if (Appraisal.StatusId == 3)
        //                    {
        //                        AppraisalState = (int)AppraisalEnum.State.CompletedStaffAppraisal;
        //                    }
        //                }
        //                else if (Staff.Type == 2)
        //                {
        //                    //AppraisalState = (int)AppraisalEnum.State.CompletedSupervisorAppraisal;

        //                    if (Appraisal.StatusId == 1 || Appraisal.StatusId == 0)
        //                    {
        //                        AppraisalState = (int)AppraisalEnum.State.UnAppraisedSupervisor;
        //                    }
        //                    else if (Appraisal.StatusId == 2)
        //                    {
        //                        AppraisalState = (int)AppraisalEnum.State.IncompletedSupervisorAppraisal;
        //                    }
        //                    else if (Appraisal.StatusId == 3)
        //                    {
        //                        AppraisalState = (int)AppraisalEnum.State.IncompletedHodAppraisal;
        //                    }
        //                }
        //                else if (Staff.Type == 3)
        //                {
        //                    //AppraisalState = (int)AppraisalEnum.State.CompletedHodAppraisal;

        //                    if (Appraisal.StatusId == 1 || Appraisal.StatusId == 0)
        //                    {
        //                        AppraisalState = (int)AppraisalEnum.State.CompletedStaffAppraisal;
        //                    }
        //                    else if (Appraisal.StatusId == 2)
        //                    {
        //                        AppraisalState = (int)AppraisalEnum.State.CompletedSupervisorAppraisal;
        //                    }
        //                    else if (Appraisal.StatusId == 3)
        //                    {
        //                        AppraisalState = (int)AppraisalEnum.State.CompletedHodAppraisal;
        //                    }
        //                }

        //                LockPaceAndMetrics = false;
        //                EnableSubmitButton = false;
        //            }
        //            else if (Comment.OptionId == 2)
        //            {
        //                if (Staff.Type == 1)
        //                {
        //                    LockPaceAndMetrics = false;
        //                    EnableSubmitButton = true;
        //                    AppraisalState = (int)AppraisalEnum.State.IncompletedStaffAppraisal;
        //                }
        //                else if (Staff.Type == 2)
        //                {
        //                    LockPaceAndMetrics = false;
        //                    EnableSubmitButton = true;
        //                    AppraisalState = (int)AppraisalEnum.State.IncompletedSupervisorAppraisal;
        //                }
        //                else if (Staff.Type == 3)
        //                {
        //                    LockPaceAndMetrics = false;
        //                    EnableSubmitButton = false;
        //                    AppraisalState = (int)AppraisalEnum.State.IncompletedHodAppraisal;
        //                }
        //            }
        //            else
        //            {
        //                if (Appraisal != null)
        //                {
        //                    if (Staff.Type == 1)
        //                    {
        //                        if (Appraisal.StatusId == 1 || Appraisal.StatusId == 0)
        //                        {
        //                            LockPaceAndMetrics = false;
        //                            EnableSubmitButton = false;
        //                            AppraisalState = (int)AppraisalEnum.State.UnAppraisedStaff;
        //                        }
        //                        else if (Appraisal.StatusId == 2)
        //                        {
        //                            LockPaceAndMetrics = false;
        //                            EnableSubmitButton = true;
        //                            AppraisalState = (int)AppraisalEnum.State.IncompletedStaffAppraisal;
        //                        }
        //                        else if (Appraisal.StatusId == 3)
        //                        {
        //                            LockPaceAndMetrics = false;
        //                            EnableSubmitButton = false;
        //                            AppraisalState = (int)AppraisalEnum.State.CompletedStaffAppraisal;
        //                        }
        //                    }
        //                    else if (Staff.Type == 2)
        //                    {
        //                        //LockPaceAndMetrics = true;
        //                        //AppraisalState = (int)AppraisalEnum.State.UnAppraisedSupervisor;

        //                        if (Appraisal.StatusId == 1 || Appraisal.StatusId == 0)
        //                        {
        //                            LockPaceAndMetrics = false;
        //                            EnableSubmitButton = false;
        //                            AppraisalState = (int)AppraisalEnum.State.UnAppraisedSupervisor;
        //                        }
        //                        else if (Appraisal.StatusId == 2)
        //                        {
        //                            LockPaceAndMetrics = false;
        //                            EnableSubmitButton = true;
        //                            AppraisalState = (int)AppraisalEnum.State.IncompletedSupervisorAppraisal;
        //                        }
        //                        else if (Appraisal.StatusId == 3)
        //                        {
        //                            LockPaceAndMetrics = false;
        //                            EnableSubmitButton = false;
        //                            AppraisalState = (int)AppraisalEnum.State.CompletedSupervisorAppraisal;
        //                        }
        //                    }
        //                    else if (Staff.Type == 3)
        //                    {
        //                        //LockPaceAndMetrics = false;
        //                        //AppraisalState = (int)AppraisalEnum.State.UnAppraisedHod;
                                
        //                        if (Appraisal.StatusId == 1 || Appraisal.StatusId == 0)
        //                        {
        //                            AppraisalState = (int)AppraisalEnum.State.UnAppraisedHod;
        //                            EnableSubmitButton = false;
        //                        }
        //                        else if (Appraisal.StatusId == 2)
        //                        {
        //                            AppraisalState = (int)AppraisalEnum.State.IncompletedHodAppraisal;
        //                            EnableSubmitButton = true;
        //                        }
        //                        else if (Appraisal.StatusId == 3)
        //                        {
        //                            AppraisalState = (int)AppraisalEnum.State.CompletedHodAppraisal;
        //                            EnableSubmitButton = false;
        //                        }

        //                        LockPaceAndMetrics = false;
                                
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else if (AppraisalState == (int)AppraisalEnum.State.SupervisorLodingAppraisee)
        //    {
        //        if (Comment.OptionId == 1)
        //        {
        //            LockPaceAndMetrics = false;
        //            EnableSubmitButton = false;
        //            AppraisalState = (int)AppraisalEnum.State.AppraisalCompleted;
        //        }
        //        else
        //        {
        //            LockPaceAndMetrics = true;
        //            EnableSubmitButton = true;
        //            AppraisalState = (int)AppraisalEnum.State.IncompleteAppraisal;
        //        }
        //    }
        //    else if (AppraisalState == (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee)
        //    {
        //        //if (Comment.OptionId == 1)
        //        //{
        //        //    //LockPaceAndMetrics = true;
        //        //    AppraisalState = (int)AppraisalEnum.State.AppraisalCompleted;
        //        //}
        //        //else
        //        //{
                    
        //        //    AppraisalState = (int)AppraisalEnum.State.IncompleteAppraisal;
        //        //}


        //        //AppraisalState = (int)AppraisalEnum.State.HodSecondLevelAppraiseeAppraisal;
        //        LockPaceAndMetrics = false;
        //        EnableSubmitButton = true;
        //    }

        //    switch(AppraisalState)
        //    {
        //        case (int)AppraisalEnum.State.UnAppraisedStaff:
        //        case (int)AppraisalEnum.State.UnAppraisedSupervisor:
        //        case (int)AppraisalEnum.State.UnAppraisedHod:
        //            {
        //                IsStrenghtAndWeakness = false;
        //                IsTrainingNeeds = false;
        //                IsSupervisorComment = false;
        //                IsAppraiseeComment = false;
        //                IsHodComment = false;
        //                IsRecommendation = false;
        //                IsOption = false;

        //                break;
        //            }
        //        case (int)AppraisalEnum.State.IncompletedStaffAppraisal:
        //        case (int)AppraisalEnum.State.IncompletedSupervisorAppraisal:
        //        case (int)AppraisalEnum.State.IncompletedHodAppraisal:
        //            {
        //                IsStrenghtAndWeakness = false;
        //                IsTrainingNeeds = false;
        //                IsSupervisorComment = false;
        //                IsAppraiseeComment = true;
        //                IsHodComment = false;
        //                IsRecommendation = false;
        //                IsOption = true;

        //                //IsStrenghtAndWeakness = false;
        //                //IsTrainingNeeds = false;
        //                //IsSupervisorComment = false;
        //                //IsAppraiseeComment = false;
        //                //IsHodComment = false;
        //                //IsRecommendation = false;
        //                //IsOption = false;

        //                break;
        //            }
        //        case (int)AppraisalEnum.State.CompletedStaffAppraisal:
        //        case (int)AppraisalEnum.State.CompletedSupervisorAppraisal:
        //        case (int)AppraisalEnum.State.CompletedHodAppraisal:
        //            {
        //                IsStrenghtAndWeakness = false;
        //                IsTrainingNeeds = false;
        //                IsSupervisorComment = false;
        //                IsAppraiseeComment = false;
        //                IsHodComment = false;
        //                IsRecommendation = false;
        //                IsOption = false;

        //                //LockPaceAndMetrics = false;

        //                break;
        //            }
        //        case (int)AppraisalEnum.State.AppraisalCompleted:
        //            {
        //                IsStrenghtAndWeakness = false;
        //                IsTrainingNeeds = false;
        //                IsSupervisorComment = false;
        //                IsAppraiseeComment = false;
        //                IsHodComment = false;
        //                IsRecommendation = false;
        //                IsOption = false;

        //                //LockPaceAndMetrics = false;

        //                break;
        //            }
        //        case (int)AppraisalEnum.State.IncompleteAppraisal:
        //            {
        //                IsStrenghtAndWeakness = true;
        //                IsTrainingNeeds = true;
        //                IsSupervisorComment = true;
        //                IsAppraiseeComment = false;
        //                IsHodComment = false;
        //                IsRecommendation = true;
        //                IsOption = false;

        //                //LockPaceAndMetrics = true;

        //                break;
        //            }
        //        case (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee:
        //        case (int)AppraisalEnum.State.HodSecondLevelAppraiseeAppraisal:
        //            {
        //                IsStrenghtAndWeakness = false;
        //                IsTrainingNeeds = false;
        //                IsSupervisorComment = false;
        //                IsAppraiseeComment = false;
        //                IsHodComment = true;
        //                IsRecommendation = false;
        //                IsOption = false;

        //                //LockPaceAndMetrics = true;

        //                break;
        //            }
        //    }
        //}

        //private void ComputeUiHeight()
        //{
        //    int metricsRowCount = Metrices.Customers.Count + Metrices.Financials.Count + Metrices.Peoples.Count + Metrices.Processes.Count + Metrices.Risks.Count;
        //    Height = UI_HEIGHT + (33 * metricsRowCount);

        //    eventAggregator.GetEvent<UiHeightEvent>().Publish(Height);
        //}

        private decimal GetTotalMetricsValue()
        {
            if (Metrices != null)
            {
                decimal customerTargetValue = Metrices.Customers != null ? Metrices.CustomerTargetValue : 0;
                decimal financialTargetValue = Metrices.Financials != null ? Metrices.FinancialTargetValue : 0;
                decimal processTargetValue = Metrices.Processes != null ? Metrices.ProcessTargetValue : 0;
                decimal peopleTargetValue = Metrices.Peoples != null ? Metrices.PeopleTargetValue : 0;
                decimal riskTargetValue = Metrices.Risks != null ? Metrices.RiskTargetValue : 0;

                return customerTargetValue + financialTargetValue + processTargetValue + peopleTargetValue + riskTargetValue;
            }

            return 0;
        }

        //private void UnLockPaceAndMetrics()
        //{
        //    if (Comment != null)
        //    {
        //        //if (AppraisalState == (int)AppraisalEnum.State.SupervisorLodingAppraisee)
        //        //{
        //        //    LockPaceAndMetrics = Comment.OptionId == 1 ? false : true;
        //        //}
        //        //else if (AppraisalState == (int)AppraisalEnum.State.HodLoadingSecondLevelAppraisee)
        //        //{
        //        //    LockPaceAndMetrics = Comment.OptionId == 1 ? true : false;
        //        //}
        //    }
        //}

         





    }
}
