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
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.Models;
using System.ComponentModel;
using System.Windows.Data;
using Mango.Setup.Services;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using Mango.Infrastructure.Events;
using Microsoft.Practices.Prism.Events;

namespace Mango.Setup.ViewModels
{
    public class MetricsViewModel : CollectionViewModelBase<Metrics>
    {
        private DepartmentService departmentService;
        private ISetupService<CompanyDepartmentJobRole> cdjrService;
        private ISetupService<MetricsPerspective> metricsPerspectiveService;
        private MetricsService metricsService;

        private Department department;
        private ICollectionView departments;
        private ICollectionView companyDepartmentJobRoles;
        private CompanyDepartmentJobRole companyDepartmentJobRole;
        private MetricsPerspective metricsPerspective;
        private ICollectionView metricsPerspectives;
        private bool canRemoveAllMetrics;

        private Metrics selectedMetrics;
        
        public MetricsViewModel(IMetricBaseService<Metrics> _service, ISetupService<CompanyDepartmentJobRole> _cdjrService, ISetupService<MetricsPerspective> _metricsPerspectiveService, IEventAggregator _eventAggregator)
            : base(_service)
        {
            cdjrService = _cdjrService;
            departmentService = new DepartmentService();
            metricsPerspectiveService = _metricsPerspectiveService;
            
            Initialise();

            OnInitialise("");
        }

        private void Initialise()
        {
            RemoveAllCommand = new DelegateCommand(OnRemoveAllCommand, CanRemoveAll);
            RemoveAssociatedMetricRatingCommand = new DelegateCommand(OnRemoveAssociatedMetricRatingCommand, CanRemoveAssociatedMetricRating);

            ModelName = "Metrics";
            metricsService = new MetricsService();
        }
        public void OnInitialise(string refresh)
        {
            LoadAllCdjrCompleted();
            cdjrService.LoadAll();

            LoadAllDepartmentCompleted();
            departmentService.LoadAll();

            LoadAllMetricsPerspectiveCompleted();
            metricsPerspectiveService.LoadAll();
        }

        private void OnRemoveAssociatedMetricRatingCommand()
        {
            try
            {
                if (CompanyDepartmentJobRole == null || CompanyDepartmentJobRole.Id <= 0)
                {
                    Utility.DisplayMessage("No Job Role selected!");
                    return;
                }
                else if (SelectedMetrics == null || SelectedMetrics.Id <= 0)
                {
                    Utility.DisplayMessage("No Metrics Selected! Please select a metrics from the list.");
                    return;
                }

                //RemoveAssociatedMetricRatingCompleted();
                //metricsService.RemoveBy(SelectedMetrics);
                //metricsService.RemoveAssociatedRatings = true;

                RemoveAssociatedMetricRatingCompleted();
                ObservableCollection<Metrics> models = (ObservableCollection<Metrics>)TargetCollection.SourceCollection;
                metricsService.ModifyAndRemoveAssociatedRatings(models);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private bool CanRemoveAssociatedMetricRating()
        {
            return AssociatedMetricRatingIsRemovable;
        }

        public DelegateCommand RemoveAllCommand { get; private set; }
        public DelegateCommand RemoveAssociatedMetricRatingCommand { get; set; }

        private bool CanRemoveAll()
        {
            return CanRemoveAllMetrics;
        }
        private bool associatedMetricRatingIsRemovable;
        public string TabCaption
        {
            get { return ModelName; }
        }
       
        public bool CanRemoveAllMetrics
        {
            get { return canRemoveAllMetrics; }
            set
            {
                canRemoveAllMetrics = value;
                RemoveAllCommand.RaiseCanExecuteChanged();
            }
        }
        public bool AssociatedMetricRatingIsRemovable
        {
            get { return associatedMetricRatingIsRemovable; }
            set
            {
                associatedMetricRatingIsRemovable = value;
                RemoveAssociatedMetricRatingCommand.RaiseCanExecuteChanged();
            }
        }
        
        public ICollectionView CompanyDepartmentJobRoles
        {
            get { return companyDepartmentJobRoles; }
            set
            {
                companyDepartmentJobRoles = value;
                OnPropertyChanged("CompanyDepartmentJobRoles");
            }
        }
        public CompanyDepartmentJobRole CompanyDepartmentJobRole
        {
            get { return companyDepartmentJobRole; }
            set
            {
                companyDepartmentJobRole = value;
                OnPropertyChanged("CompanyDepartmentJobRole");
            }
        }
        public Metrics SelectedMetrics
        {
            get { return selectedMetrics; }
            set
            {
                selectedMetrics = value;
                OnPropertyChanged("SelectedMetrics");
            }
        }
        public ICollectionView Departments
        {
            get { return departments; }
            set
            {
                departments = value;
                OnPropertyChanged("Departments");
            }
        }
        public Department Department
        {
            get { return department; }
            set
            {
                department = value;
                OnPropertyChanged("Department");
            }
        }
        
        public ICollectionView MetricsPerspectives
        {
            get { return metricsPerspectives; }
            set
            {
                metricsPerspectives = value;
                OnPropertyChanged("MetricsPerspectives");
            }
        }
        public MetricsPerspective MetricsPerspective
        {
            get { return metricsPerspective; }
            set
            {
                metricsPerspective = value;
                OnPropertyChanged("MetricsPerspective");
            }
        }

        protected override void OnSaveCommand()
        {
            try
            {
                if (IncompleteEntry())
                {
                    return;
                }

                ObservableCollection<Metrics> models = (ObservableCollection<Metrics>)TargetCollection.SourceCollection;
                if (models == null || models.Count == 0)
                {
                    MessageBoxResult response = MessageBox.Show("This action will permanently remove all Metrices associated with " + CompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + ". Do you want to continue?", "METRICS", MessageBoxButton.OKCancel);
                    if (response == MessageBoxResult.OK)
                    {
                        RemoveAllCompleted();
                        metricsService.RemoveBy(CompanyDepartmentJobRole, Utility.Period, true);
                    }
                }
                else
                {
                    base.OnSaveCommand();
                    return;
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private bool IncompleteEntry()
        {
            try
            {
                if (CompanyDepartmentJobRole == null)
                {
                    Utility.DisplayMessage("Please select a Job Role!");
                    return true;
                }
                else if (Utility.Period == null)
                {
                    Utility.DisplayMessage("No Period found! Please contact your system administrator");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void OnRemoveAllCommand()
        {
            try
            {
                ObservableCollection<Metrics> models = (ObservableCollection<Metrics>)TargetCollection.SourceCollection;
                if (collectionManager.Collection != null && collectionManager.Collection.Count > 0)
                {
                    MessageBoxResult response = MessageBox.Show("You are about to remove all Metrices associated with " + CompanyDepartmentJobRole.JobRole.Name + ". Are you sure you want to continue?", "METRICS", MessageBoxButton.OKCancel);
                    if (response == MessageBoxResult.OK)
                    {
                        collectionManager.Collection.Clear();
                        RefreshModelCollection();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override bool IsRequiredDetailsEntered()
        {
            try
            {
                if (CompanyDepartmentJobRole == null || CompanyDepartmentJobRole.Id <= 0)
                {
                    Utility.DisplayMessage("Please select a Job Role!");
                    return false;
                }
                else if (MetricsPerspective == null || MetricsPerspective.Id <= 0)
                {
                    Utility.DisplayMessage("Please select Metrics Perspective!");
                    return false;
                }
                else if (Department == null || Department.Id == "")
                {
                    Utility.DisplayMessage("Please select Department!");
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(TargetModel.Kpi))
                {
                    Utility.DisplayMessage("Please enter Kpi!");
                    return false;
                }
                else if (TargetModel.Target <= 0)
                {
                    Utility.DisplayMessage("Please enter Target!");
                    return false;
                }
                else if (ModelAlreadyExist())
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
               
        protected override bool ModelAlreadyExist()
        {
            try
            {
                if (TargetCollection != null)
                {
                    foreach (Metrics metrics in TargetCollection)
                    {
                        if (metrics.Kpi == TargetModel.Kpi)
                        {
                            Utility.DisplayMessage("Duplicate entry found! Kpi enetered for " + CompanyDepartmentJobRole.JobRole.Name + " already exist on the list!. Please modify to continue.");
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void ClearView()
        {
            try
            {
                base.ClearTargetCollection();

                CompanyDepartmentJobRole = null;
                CompanyDepartmentJobRoles.MoveCurrentToPosition(0);
                MetricsPerspectives.MoveCurrentToPosition(0);
                Departments.MoveCurrentToPosition(0);
                TargetModel = new Metrics();

                UpdateViewState(Edit.Mode.Loaded);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }


        protected override Metrics GetNewModel()
        {
            try
            {
                Metrics metrics = new Metrics();
                metrics.Perspective = new MetricsPerspective();
                metrics.ResponsibleDepartment = new Department();
                metrics.CompanyDepartmentJobRole = new CompanyDepartmentJobRole();

                //metrics.Metrics.Id = Metrics.Id;
                metrics.Perspective = MetricsPerspective;
                metrics.CompanyDepartmentJobRole = CompanyDepartmentJobRole;
                metrics.Kpi = TargetModel.Kpi;
                metrics.Measure = TargetModel.Measure;
                metrics.DataSource = TargetModel.DataSource;
                metrics.ResponsibleDepartment = Department;
                metrics.Target = TargetModel.Target;
                metrics.Score = TargetModel.Score;
                metrics.Period = Utility.Period;

                return metrics;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllCdjrCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllCdjrCompletedHelper();
                    cdjrService.GetAllModelsCompleted -= handler;
                };

                cdjrService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void LoadAllCdjrCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(cdjrService.Fault))
                               {
                                   return;
                               }

                               if (cdjrService.Models != null && cdjrService.Models.Count > 0)
                               {
                                   cdjrService.Models.Insert(0, new CompanyDepartmentJobRole() { Id = 0, JobRole = new JobRole() { Name = "<< Select CDJR >>" } });
                                   if (cdjrService.Models != null && cdjrService.Models.Count > 0)
                                   {
                                       CompanyDepartmentJobRoles = new PagedCollectionView(cdjrService.Models);

                                       CompanyDepartmentJobRoles.MoveCurrentToFirst();
                                       CompanyDepartmentJobRoles.CurrentChanged += (s, e) =>
                                       {
                                           CompanyDepartmentJobRole = CompanyDepartmentJobRoles.CurrentItem as CompanyDepartmentJobRole;
                                           LoadMetricsByPeriodAndCompanyDepartmentJobRole();
                                       };
                                   }
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void LoadMetricsByPeriodAndCompanyDepartmentJobRole()
        {
            try
            {
                if (CompanyDepartmentJobRole != null)
                {
                    LoadModelsByCompanyDepartmentJobRoleAndPeriodCompleted();
                    metricsService.LoadByCompanyDepartmentJobRoleAndPeriod(CompanyDepartmentJobRole, Utility.Period);
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void LoadAllDepartmentCompleted()
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
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void LoadAllDepartmentCompletedHelper()
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
                                   ObservableCollection<Department> depts = new ObservableCollection<Department>();
                                   depts = departmentService.Models;
                                   depts.Insert(0, new Department() { Id = "", Name = "<< Select Department >>" });

                                   Departments = new PagedCollectionView(depts);
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

        private void LoadAllMetricsPerspectiveCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllMetricsPerspectiveCompletedCompletedHelper();
                    metricsPerspectiveService.GetAllModelsCompleted -= handler;
                };

                metricsPerspectiveService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void LoadAllMetricsPerspectiveCompletedCompletedHelper()
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
                                   metricsPerspectiveService.Models.Insert(0, new MetricsPerspective() { Id = 0, Name = "<< Select Perspective >>" });
                                   if (metricsPerspectiveService.Models != null && metricsPerspectiveService.Models.Count > 0)
                                   {
                                       MetricsPerspectives = new PagedCollectionView(metricsPerspectiveService.Models);

                                       MetricsPerspectives.MoveCurrentToFirst();
                                       MetricsPerspectives.CurrentChanged += (s, e) =>
                                       {
                                           MetricsPerspective = MetricsPerspectives.CurrentItem as MetricsPerspective;
                                       };
                                   }
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadModelsByCompanyDepartmentJobRoleAndPeriodCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadModelsByCompanyDepartmentJobRoleAndPeriodCompletedCompletedHelper();
                    metricsService.GetModelsCompleted -= handler;
                };

                metricsService.GetModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadModelsByCompanyDepartmentJobRoleAndPeriodCompletedCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(metricsService.Fault))
                               {
                                   return;
                               }

                               SetCurrentTargetCollection(metricsService.Models);
                               collectionManager.Collection = metricsService.Models;

                               if (metricsService.Models != null)
                               {
                                   if (metricsService.Models.Count > 0)
                                   {
                                       UpdateViewState(Edit.Mode.Editing);
                                   }
                                   else
                                   {
                                       UpdateViewState(Edit.Mode.Loaded);
                                   }

                                   IsModelExist(metricsService.Models);
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override void SelectedTargetCollection()
        {
            try
            {
                SelectedMetrics = TargetCollection.CurrentItem as Metrics;

                UpdateViewState(Edit.Mode.Selected);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RemoveAllCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    RemoveAllCompletedHelper();
                    metricsService.ActionCompleted -= handler;
                };

                metricsService.ActionCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void RemoveAllCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(metricsService.Fault))
                               {
                                   if (metricsService.Fault.Message.Contains("depends on Metric Rating(s) which cannot"))
                                   {
                                       AssociatedMetricRatingIsRemovable = true;
                                       ModelCanBeSaved = false;
                                   }
                                   else
                                   {
                                       AssociatedMetricRatingIsRemovable = false;
                                       ModelCanBeSaved = true;
                                   }

                                   return;
                               }

                               if (metricsService.Done)
                               {
                                   Utility.DisplayMessage("All " + ModelName + " associated with " + CompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + " have been successfully removed");
                               }
                               else
                               {
                                   Utility.DisplayMessage("All " + ModelName + " associated with " + CompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + " have not been removed! Please try again");
                               }

                               ClearView();
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void RemoveAssociatedMetricRatingCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    RemoveAssociatedMetricRatingCompletedHelper();
                    metricsService.ActionCompleted -= handler;
                };

                metricsService.ActionCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void RemoveAssociatedMetricRatingCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(metricsService.Fault))
                               {
                                   return;
                               }

                               if (metricsService.Done)
                               {
                                   Utility.DisplayMessage("Metrics and associated Ratings have been successfully removed");
                                   LoadMetricsByPeriodAndCompanyDepartmentJobRole();
                                   AssociatedMetricRatingIsRemovable = false;
                               }
                               else
                               {
                                   AssociatedMetricRatingIsRemovable = true;
                                   Utility.DisplayMessage("Removal of Metrics and associated Ratings failed! Please try again");
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override void UpdateViewState(Edit.Mode uiState)
        {
            try
            {
                base.UpdateViewState(uiState);

                switch (uiState)
                {
                    case Edit.Mode.Loaded:
                        {
                            if (CompanyDepartmentJobRole != null)
                            {
                                if (CompanyDepartmentJobRole.Id > 0)
                                {
                                    ModelCanBeSaved = true;
                                }
                                else
                                {
                                    ModelCanBeSaved = false;
                                }
                            }
                            else
                            {
                                ModelCanBeSaved = false;
                            }

                            if (RemoveAllCommand != null)
                            {
                                CanRemoveAllMetrics = false;
                            }

                            if (RemoveAssociatedMetricRatingCommand != null)
                            {
                                AssociatedMetricRatingIsRemovable = false;
                            }

                            break;
                        }
                    case Edit.Mode.Adding:
                        {
                            ModelCanBeSaved = true;
                            UpdateViewStateHelper();
                            break;
                        }
                    case Edit.Mode.Editing:
                        {
                            ModelCanBeAdded = true;
                            ModelCanBeRemoved = false;
                            ModelCanBeSaved = false;
                            ModelCanBeCleared = true;
                                                       
                            UpdateViewStateHelper();
                            break;
                        }
                    case Edit.Mode.Selected:
                        {
                            ModelCanBeSaved = false;
                            UpdateViewStateHelper();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void UpdateViewStateHelper()
        {
            try
            {
                if (RemoveAllCommand != null && collectionManager != null)
                {
                    if (collectionManager.Collection == null || collectionManager.Collection.Count == 0)
                    {
                        CanRemoveAllMetrics = false;
                    }
                    else
                    {
                        CanRemoveAllMetrics = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override bool IsFaultExceptionThrown()
        {
            try
            {
                if (Utility.FaultExist(service.Fault))
                {
                    if (service.Fault.Message.Contains("Metrics with KPI '"))
                    {
                        AssociatedMetricRatingIsRemovable = true;
                        return true;
                    }
                }

                AssociatedMetricRatingIsRemovable = false;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected override void SaveCompletedHelper()
        //{
        //    try
        //    {
        //        dispatcher.BeginInvoke
        //                   (() =>
        //                   {
        //                       if (Utility.FaultExist(service.Fault))
        //                       {
        //                           if (service.Fault.Message.Contains("Metrics with KPI '"))
        //                           {
        //                               UpdateViewStateHelper();
        //                               return;
        //                           }
        //                       }

        //                       if (service.Done)
        //                       {
        //                           SuccessfullActionHelper();
        //                       }
        //                       else
        //                       {
        //                           Utility.DisplayMessage(ModelName + " have not been saved! Please try again");
        //                       }
        //                   });
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}

         





    }


}
