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

using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Models;
using Mango.Setup.Services;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using Mango.Setup.Interfaces;
using Microsoft.Practices.Prism.Commands;

namespace Mango.Setup.ViewModels
{
    public class ModifyMetricsViewModel : ViewModelBase //: SetupViewModelBase<Metrics>
    {
        private Dispatcher dispatcher;
        private ICollectionView companyDepartmentJobRoles;
        private CompanyDepartmentJobRole companyDepartmentJobRole;
        
        private Metrics metrics;
        private ICollectionView metrices;
        private ISetupService<MetricsPerspective> metricsPerspectiveService;
        private ISetupService<CompanyDepartmentJobRole> companyDepartmentJobRoleService;
        private ISetupService<Department> departmentService;
        private IMetricsSetupService metricsSetupService;
        private ICollectionView departments;
        private Department department;
        private MetricsPerspective metricsPerspective;
        private ICollectionView metricsPerspectives;
        private ICollectionView jobRoles;
        private CompanyDepartmentJobRole jobRole;

        private bool canSaveMetrics;
        private bool canClearMetrics;

        private string recordCount;

        private string kpi;
        private string measure;
        private string dataSource;
        private decimal target;
        private decimal score;


        //public ModifyMetricsViewModel(IMetricsSetupService _metricsSetupService)
        //{
        //    dispatcher = Deployment.Current.Dispatcher;

        //    metricsSetupService = _metricsSetupService;
        //    metricsPerspectiveService = new MetricsPerspectiveService();

        //    companyDepartmentJobRoleService = new CompanyDepartmentJobRoleService();
        //    LoadAllCompanyDepartmentJobRole();

        //    departmentService = new DepartmentService();
        //    LoadAllDepartment();

        //    LoadAllMetricsPerspectiveCompleted();
        //    metricsPerspectiveService.LoadAll();

        //    SaveCommand = new DelegateCommand(OnSaveCommand, CanSave);
        //    ClearCommand = new DelegateCommand(OnClearCommand, CanClear);

        //    CanSaveMetrics = false;
        //    CanClearMetrics = false;

        //    RecordCount = "Record Count: " + 0;
        //}


        public ModifyMetricsViewModel(IMetricsSetupService _metricsSetupService, ISetupService<MetricsPerspective> _metricsPerspectiveService, ISetupService<CompanyDepartmentJobRole> _companyDepartmentJobRoleService, ISetupService<Department> _departmentService)
        {
            dispatcher = Deployment.Current.Dispatcher;

            metricsSetupService = _metricsSetupService;
            metricsPerspectiveService = _metricsPerspectiveService;
            companyDepartmentJobRoleService = _companyDepartmentJobRoleService;
            departmentService = _departmentService;

            LoadAllDepartment();
            LoadAllCompanyDepartmentJobRole();
            LoadAllMetricsPerspectiveCompleted();
            metricsPerspectiveService.LoadAll();

            SaveCommand = new DelegateCommand(OnSaveCommand, CanSave);
            ClearCommand = new DelegateCommand(OnClearCommand, CanClear);

            CanSaveMetrics = false;
            CanClearMetrics = false;

            RecordCount = "Record Count: " + 0;
        }

        public DelegateCommand SaveCommand { get; protected set; }
        public DelegateCommand ClearCommand { get; protected set; }

        public string TabCaption
        {
            get { return "Edit Metrics"; }
        }
       
        public decimal Score
        {
            get { return score; }
            set
            {
                score = value;
                base.OnPropertyChanged("Score");
            }
        }
        public decimal Target
        {
            get { return target; }
            set
            {
                target = value;
                base.OnPropertyChanged("Target");
            }
        }
        public string Kpi
        {
            get { return kpi; }
            set
            {
                kpi = value;
                base.OnPropertyChanged("Kpi");
            }
        }
        public string Measure
        {
            get { return measure; }
            set
            {
                measure = value;
                base.OnPropertyChanged("Measure");
            }
        }
        public string DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                base.OnPropertyChanged("DataSource");
            }
        }

        public string RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                base.OnPropertyChanged("RecordCount");
            }
        }
        public ICollectionView MetricsPerspectives
        {
            get { return metricsPerspectives; }
            set
            {
                metricsPerspectives = value;
                base.OnPropertyChanged("MetricsPerspectives");
            }
        }
        public MetricsPerspective MetricsPerspective
        {
            get { return metricsPerspective; }
            set
            {
                metricsPerspective = value;
                base.OnPropertyChanged("MetricsPerspective");
            }
        }

        public ICollectionView Departments
        {
            get { return departments; }
            set
            {
                departments = value;
                base.OnPropertyChanged("Departments");
            }
        }
        public Department Department
        {
            get { return department; }
            set
            {
                department = value;
                base.OnPropertyChanged("Department");
            }
        }
        public ICollectionView CompanyDepartmentJobRoles
        {
            get { return companyDepartmentJobRoles; }
            set
            {
                companyDepartmentJobRoles = value;
                base.OnPropertyChanged("CompanyDepartmentJobRoles");
            }
        }
        public CompanyDepartmentJobRole CompanyDepartmentJobRole
        {
            get { return companyDepartmentJobRole; }
            set
            {
                companyDepartmentJobRole = value;
                base.OnPropertyChanged("CompanyDepartmentJobRole");
            }
        }
        
        public ICollectionView JobRoles
        {
            get { return jobRoles; }
            set
            {
                jobRoles = value;
                base.OnPropertyChanged("JobRoles");
            }
        }
        public CompanyDepartmentJobRole JobRole
        {
            get { return jobRole; }
            set
            {
                jobRole = value;
                base.OnPropertyChanged("JobRole");
            }
        }
        
        public ICollectionView Metrices
        {
            get { return metrices; }
            set
            {
                metrices = value;
                base.OnPropertyChanged("Metrices");
            }
        }
        public Metrics Metrics
        {
            get { return metrics; }
            set
            {
                metrics = value;
                base.OnPropertyChanged("Metrics");
            }
        }
        public bool CanSaveMetrics
        {
            get { return canSaveMetrics; }
            set
            {
                canSaveMetrics = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        
        public bool CanClearMetrics
        {
            get { return canClearMetrics; }
            set
            {
                canClearMetrics = value;
                ClearCommand.RaiseCanExecuteChanged();
            }
        }

        private void LoadAllCompanyDepartmentJobRole()
        {
            try
            {
                LoadAllCompanyDepartmentJobRoleCompleted();
                companyDepartmentJobRoleService.LoadAll();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void LoadAllDepartment()
        {
            try
            {
                LoadAllDepartmentCompleted();
                departmentService.LoadAll();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private bool CanSave()
        {
            return CanSaveMetrics;
        }
        private bool CanClear()
        {
            return CanClearMetrics;
        }
        protected void OnSaveCommand()
        {
            try
            {
                if (InvalidEntry())
                {
                    return;
                }

                Metrics.Kpi = Kpi;
                Metrics.Measure = Measure;
                Metrics.DataSource = DataSource;
                Metrics.Target = Target;
                Metrics.Score = Score;
                Metrics.ResponsibleDepartment = Department;
                Metrics.Perspective = MetricsPerspective;
                Metrics.CompanyDepartmentJobRole = JobRole;

                SaveOperationCompleted();
                metricsSetupService.Modify(Metrics);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void OnClearCommand()
        {
            try
            {
                if (MetricsPerspectives != null)
                {
                    MetricsPerspectives.MoveCurrentToFirst();
                }

                if (JobRoles != null)
                {
                    JobRoles.MoveCurrentToFirst();
                }

                if (Departments != null)
                {
                    Departments.MoveCurrentToFirst();
                }

                Kpi = "";
                Measure = "";
                DataSource = "";
                Target = 0;
                Score = 0;

                //Metrics = new Metrics();

                CanSaveMetrics = false;
                CanClearMetrics = false;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private bool InvalidEntry()
        {
            try
            {
                if (MetricsPerspective == null)
                {
                    Utility.DisplayMessage("Please select Metrics Perspective!");
                    return true;
                }
                else if (JobRole == null)
                {
                    Utility.DisplayMessage("Please select Job Role!");
                    return true;
                }
                else if (string.IsNullOrWhiteSpace(Metrics.Kpi))
                {
                    Utility.DisplayMessage("Please enter Kpi!");
                    return true;
                }
                else if (Department == null)
                {
                    Utility.DisplayMessage("Please select Department!");
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void LoadAllCompanyDepartmentJobRoleCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllCompanyDepartmentJobRoleCompletedHelper();
                    companyDepartmentJobRoleService.GetAllModelsCompleted -= handler;
                };

                companyDepartmentJobRoleService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllCompanyDepartmentJobRoleCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(companyDepartmentJobRoleService.Fault))
                               {
                                   return;
                               }

                               if (companyDepartmentJobRoleService.Models != null)
                               {
                                   List<CompanyDepartmentJobRole> companyDepartmentJobRoles = companyDepartmentJobRoleService.Models.Where(p => p.Id != 0).ToList();
                                   if (companyDepartmentJobRoles.Count > 0)
                                   {
                                       companyDepartmentJobRoles.Insert(0, new CompanyDepartmentJobRole() { Company = new Company() { Symbol = "<< Select Job Role >>" } });
                                   }

                                   CompanyDepartmentJobRoles = new PagedCollectionView(companyDepartmentJobRoles);
                                   CompanyDepartmentJobRoles.MoveCurrentToFirst();
                                   CompanyDepartmentJobRoles.CurrentChanged += (s, e) =>
                                   {
                                       CompanyDepartmentJobRole = CompanyDepartmentJobRoles.CurrentItem as CompanyDepartmentJobRole;
                                       PopulateMetricsByCdjr();
                                   };

                                   JobRoles = new PagedCollectionView(companyDepartmentJobRoles);
                                   JobRoles.MoveCurrentToFirst();
                                   JobRoles.CurrentChanged += (s, e) =>
                                   {
                                       JobRole = JobRoles.CurrentItem as CompanyDepartmentJobRole;
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void PopulateMetricsByCdjr()
        {
            if (CompanyDepartmentJobRole != null)
            {
                LoadMetricsByCompanyDepartmentJobRoleAndPeriodCompleted();
                metricsSetupService.LoadByCompanyDepartmentJobRoleAndPeriod(CompanyDepartmentJobRole, Utility.Period);
            }
        }

        protected void LoadMetricsByCompanyDepartmentJobRoleAndPeriodCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadMetricsByCompanyDepartmentJobRoleAndPeriodCompletedHelper();
                    metricsSetupService.GetModelsByCdjrAndPeriodCompleted -= handler;
                };

                metricsSetupService.GetModelsByCdjrAndPeriodCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadMetricsByCompanyDepartmentJobRoleAndPeriodCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(metricsSetupService.Fault))
                               {
                                   return;
                               }

                               if (metricsSetupService.Models != null && metricsSetupService.Models.Count > 0)
                               {
                                   RecordCount = "Record Count: " + metricsSetupService.Models.Count;

                                   Metrices = new PagedCollectionView(metricsSetupService.Models);
                                   Metrices.MoveCurrentTo(null);
                                   Metrices.CurrentChanged += (s, e) =>
                                   {
                                       Metrics = Metrices.CurrentItem as Metrics;
                                       if (Metrics != null)
                                       {
                                           if (MetricsPerspectives != null)
                                           {
                                               List<MetricsPerspective> perspectives = (List<MetricsPerspective>)MetricsPerspectives.SourceCollection;
                                               MetricsPerspective perspective = perspectives.Where(mp => mp.Id == Metrics.Perspective.Id).SingleOrDefault();
                                               MetricsPerspectives.MoveCurrentTo(perspective);
                                           }

                                           if (Departments != null)
                                           {
                                               List<Department> departments = (List<Department>)Departments.SourceCollection;
                                               Department department = departments.Where(d => d.Id == Metrics.ResponsibleDepartment.Id).SingleOrDefault();
                                               Departments.MoveCurrentTo(department);
                                           }

                                           if (JobRoles != null)
                                           {
                                               List<CompanyDepartmentJobRole> jobRoles = (List<CompanyDepartmentJobRole>)JobRoles.SourceCollection;
                                               CompanyDepartmentJobRole jobRole = jobRoles.Where(d => d.Id == Metrics.CompanyDepartmentJobRole.Id).SingleOrDefault();
                                               JobRoles.MoveCurrentTo(jobRole);
                                           }

                                           Kpi = Metrics.Kpi;
                                           Measure = Metrics.Measure;
                                           DataSource = Metrics.DataSource;
                                           Target = Metrics.Target.GetValueOrDefault();
                                           Score = Metrics.Score.GetValueOrDefault();

                                           CanSaveMetrics = true;
                                           CanClearMetrics = true;
                                       }
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllDepartmentCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllDepartmentCompletedHelper();
                    departmentService.GetAllModelsCompleted -= handler;
                };

                departmentService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllDepartmentCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(departmentService.Fault))
                               {
                                   return;
                               }

                               if (departmentService.Models != null && departmentService.Models.Count > 0)
                               {
                                   List<Department> departments = departmentService.Models.Where(d => d.Id != null && d.Id != "").ToList();
                                   if (departments.Count > 0)
                                   {
                                       departments.Insert(0, new Department() { Name = "<< Select Department >>" });
                                   }

                                   Departments = new PagedCollectionView(departments);
                                   Departments.MoveCurrentToFirst();
                                   Departments.CurrentChanged += (s, e) =>
                                   {
                                       Department = Departments.CurrentItem as Department;
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllMetricsPerspectiveCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllMetricsPerspectiveCompletedHelper();
                    metricsPerspectiveService.GetAllModelsCompleted -= handler;
                };

                metricsPerspectiveService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllMetricsPerspectiveCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(metricsPerspectiveService.Fault))
                               {
                                   return;
                               }

                               if (metricsPerspectiveService.Models != null && metricsPerspectiveService.Models.Count > 0)
                               {
                                   List<MetricsPerspective> metricsPerspectives = metricsPerspectiveService.Models.Where(d => d.Id != 0).ToList();
                                   if (metricsPerspectives.Count > 0)
                                   {
                                       metricsPerspectives.Insert(0, new MetricsPerspective() { Name  = "<< Select Metrics Perspective >>" });
                                   }

                                   MetricsPerspectives = new PagedCollectionView(metricsPerspectives);
                                   MetricsPerspectives.MoveCurrentToFirst();
                                   MetricsPerspectives.CurrentChanged += (s, e) =>
                                   {
                                       MetricsPerspective = MetricsPerspectives.CurrentItem as MetricsPerspective;
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void SaveOperationCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    SaveOperationCompletedHelper();
                    metricsSetupService.ActionCompleted -= handler;
                };

                metricsSetupService.ActionCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void SaveOperationCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                          (() =>
                          {
                              if (Utility.FaultExist(metricsSetupService.Fault))
                              {
                                  return;
                              }

                              if (metricsSetupService.Done)
                              {
                                  //PopulateMetricsByCdjr();
                                  OnClearCommand();
                                  Utility.DisplayMessage("Selected metrics has been successfully modified");
                              }
                              else
                              {
                                  Utility.DisplayMessage("Metrics modification failed!");
                              }
                          });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }





    }
}
