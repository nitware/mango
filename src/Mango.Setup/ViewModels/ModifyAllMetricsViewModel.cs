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
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Mango.Setup.Services;
using Mango.Setup.Interfaces;
using System.ComponentModel;
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Models;
using System.Windows.Data;
using System.Collections.Generic;
using Mango.Infrastructure.Interfaces;
using System.Linq;
using System.Collections.ObjectModel;

namespace Mango.Setup.ViewModels
{
    public class ModifyAllMetricsViewModel : ViewModelBase
    {
       private Dispatcher dispatcher;
       
        private Metrics metrics;
        private ObservableCollection<Metrics> metrices;
        private ISetupService<MetricsPerspective> metricsPerspectiveService;
       
        private ISetupService<Department> departmentService;
        private IMetricsSetupService metricsSetupService;
        private ICollectionView departments;
        private Department department;
        private MetricsPerspective metricsPerspective;
        private ICollectionView metricsPerspectives;
        
        //private bool canSaveMetrics;
        //private bool canClearMetrics;

        private string recordCount;

        private string kpi;
        private string measure;
        private string dataSource;
        private decimal target;
        private decimal score;

        public ModifyAllMetricsViewModel(IMetricsSetupService _metricsSetupService)
        {
            dispatcher = Deployment.Current.Dispatcher;

            metricsSetupService = _metricsSetupService;
            metricsPerspectiveService = new MetricsPerspectiveService();

            departmentService = new DepartmentService();
            LoadAllDepartment();

            LoadAllMetricsPerspectiveCompleted();
            metricsPerspectiveService.LoadAll();

            SaveCommand = new DelegateCommand(OnSaveCommand);

            //SaveCommand = new DelegateCommand(OnSaveCommand, CanSave);
            //ClearCommand = new DelegateCommand(OnClearCommand, CanClear);

            //CanSaveMetrics = false;
            //CanClearMetrics = false;

            RecordCount = "Record Count: " + 0;
        }

        public DelegateCommand SaveCommand { get; protected set; }
        public DelegateCommand ClearCommand { get; protected set; }

        public string TabCaption
        {
            get { return "Edit Accross Metrics"; }
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
        public ObservableCollection<Metrics> Metrices
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
        //public bool CanSaveMetrics
        //{
        //    get { return canSaveMetrics; }
        //    set
        //    {
        //        canSaveMetrics = value;
        //        SaveCommand.RaiseCanExecuteChanged();
        //    }
        //}
        
        //public bool CanClearMetrics
        //{
        //    get { return canClearMetrics; }
        //    set
        //    {
        //        canClearMetrics = value;
        //        ClearCommand.RaiseCanExecuteChanged();
        //    }
        //}

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
        //private bool CanSave()
        //{
        //    return CanSaveMetrics;
        //}
        //private bool CanClear()
        //{
        //    return CanClearMetrics;
        //}
        protected void OnSaveCommand()
        {
            try
            {
                if (InvalidEntry())
                {
                    return;
                }

                foreach (Metrics metrics in Metrices)
                {
                    metrics.Kpi = Kpi;
                    metrics.Measure = Measure;
                    metrics.DataSource = DataSource;
                    metrics.Target = Target;
                    metrics.Score = Score;
                    metrics.ResponsibleDepartment = Department;
                }

                SaveOperationCompleted();
                metricsSetupService.ModifyNps(Metrices);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        //protected void OnSaveCommand()
        //{
        //    try
        //    {
        //        if (InvalidEntry())
        //        {
        //            return;
        //        }

        //        Metrics.Kpi = Kpi;
        //        Metrics.Measure = Measure;
        //        Metrics.DataSource = DataSource;
        //        Metrics.Target = Target;
        //        Metrics.Score = Score;
        //        Metrics.ResponsibleDepartment = Department;
        //        Metrics.Perspective = MetricsPerspective;
        //        //Metrics.CompanyDepartmentJobRole = JobRole;

        //        SaveOperationCompleted();
        //        metricsSetupService.Modify(Metrics);
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}
        private void OnClearCommand()
        {
            try
            {
                if (MetricsPerspectives != null)
                {
                    MetricsPerspectives.MoveCurrentToFirst();
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

                //CanSaveMetrics = false;
                //CanClearMetrics = false;
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
                else if (string.IsNullOrWhiteSpace(Kpi))
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

        protected void LoadMetricsByPeriodAndPerspectiveCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadMetricsByPeriodAndPerspectiveCompletedHelper();
                    metricsSetupService.GetAllMetricesByPeriodAndPerspectiveCompleted -= handler;
                };

                metricsSetupService.GetAllMetricesByPeriodAndPerspectiveCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadMetricsByPeriodAndPerspectiveCompletedHelper()
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
                                   Metrices = metricsSetupService.Models;
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
                                   //List<MetricsPerspective> metricsPerspectives = metricsPerspectiveService.Models.Where(d => d.Id != 0).ToList();

                                   List<MetricsPerspective> metricsPerspectives = metricsPerspectiveService.Models.Where(d => d.Id == 3 || d.Id == 5).ToList();
                                   if (metricsPerspectives.Count > 0)
                                   {
                                       metricsPerspectives.Insert(0, new MetricsPerspective() { Name  = "<< Select Metrics Perspective >>" });
                                   }

                                   MetricsPerspectives = new PagedCollectionView(metricsPerspectives);
                                   MetricsPerspectives.MoveCurrentToFirst();
                                   MetricsPerspectives.CurrentChanged += (s, e) =>
                                   {
                                       MetricsPerspective = MetricsPerspectives.CurrentItem as MetricsPerspective;
                                       if (MetricsPerspective != null && MetricsPerspective.Id > 0)
                                       {
                                           LoadMetricsByPeriodAndPerspectiveCompleted();
                                           metricsSetupService.GetAllMetricsByPeriodAndPerspective(Utility.Period, MetricsPerspective);
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

        protected void SaveOperationCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    SaveOperationCompletedHelper();
                    metricsSetupService.ModifyNpsCompleted -= handler;
                };

                metricsSetupService.ModifyNpsCompleted += handler;
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
                                  OnClearCommand();
                                  Utility.DisplayMessage("Metrics '" + MetricsPerspective.Name + "' has been successfully modified");
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
