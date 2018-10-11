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

using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.ViewModelBase;
using Mango.Setup.Interfaces;
using Mango.Infrastructure.Interfaces;
using Mango.Setup.Services;
using Mango.Infrastructure.Models;
using System.Windows.Data;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.Events;

namespace Mango.Setup.ViewModels
{
    public class MetricRatingViewModel : CollectionViewModelBase<MetricRating>
    {
        private ISetupService<CompanyDepartmentJobRole> cdjrService;
        private ISetupService<Infrastructure.MangoService.Rating> ratingService;
        private ISetupService<RatingType> ratingTypeService;
        private MetricRatingService metricRatingService;
        private MetricsService metricsService;

        private ICollectionView metrices;
        private Metrics metrics;
        private RatingType ratingType;
        private ICollectionView ratingTypes;
        private ICollectionView ratings;
        private Infrastructure.MangoService.Rating rating;
        private bool isInfinity;
        private bool canRemoveAllMetricRating;

        private CompanyDepartmentJobRole companyDepartmentJobRole;
        private ICollectionView companyDepartmentJobRoles;

        public event EventHandler MetricesLoaded;

        public MetricRatingViewModel(IMetricBaseService<MetricRating> _service, ISetupService<RatingType> _ratingTypeService, ISetupService<Infrastructure.MangoService.Rating> _ratingService, ISetupService<CompanyDepartmentJobRole> _cdjrService, IEventAggregator _eventAggregator)
            : base(_service)
        {
            cdjrService = _cdjrService;
            ratingService = _ratingService;
            ratingTypeService = _ratingTypeService;

            Initialise();

            //_eventAggregator.GetEvent<SetupEvent>().Subscribe(OnInitialise);

            OnInitialise("");
        }

        public DelegateCommand RemoveAllCommand { get; private set; }

        private void Initialise()
        {
            IsInfinity = false;
            RemoveAllCommand = new DelegateCommand(OnRemoveAllCommand, CanRemoveAll);

            metricsService = new MetricsService();
            metricRatingService = new MetricRatingService();

            ModelName = "Metric Rating";

            //LoadMetricsByPeriodCompleted();
            //metricsService.LoadByPeriod(Utility.Period);

            //LoadAllRatingsCompleted();
            //ratingService.LoadAll();

            //LoadAllRatingTypesCompleted();
            //ratingTypeService.LoadAll();
        }
        public void OnInitialise(string refresh)
        {
            LoadAllCdjrCompleted();
            cdjrService.LoadAll();

            //LoadMetricsByPeriodCompleted();
            //metricsService.LoadByPeriod(Utility.Period);

            LoadAllRatingsCompleted();
            ratingService.LoadAll();

            LoadAllRatingTypesCompleted();
            ratingTypeService.LoadAll();
        }

        public string TabCaption
        {
            get { return ModelName; }
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
        public bool IsInfinity
        {
            get { return isInfinity; }
            set
            {
                isInfinity = value;
                base.OnPropertyChanged("IsInfinity");
            }
        }
        public ICollectionView Ratings
        {
            get { return ratings; }
            set
            {
                ratings = value;
                base.OnPropertyChanged("Ratings");
            }
        }
        public Infrastructure.MangoService.Rating Rating
        {
            get { return rating; }
            set
            {
                rating = value;
                base.OnPropertyChanged("Rating");
            }
        }
        public ICollectionView RatingTypes
        {
            get { return ratingTypes; }
            set
            {
                ratingTypes = value;
                base.OnPropertyChanged("RatingTypes");
            }
        }
        public RatingType RatingType
        {
            get { return ratingType; }
            set
            {
                ratingType = value;
                base.OnPropertyChanged("RatingType");
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

        public bool CanRemoveAllMetricRating
        {
            get { return canRemoveAllMetricRating; }
            set
            {
                canRemoveAllMetricRating = value;
                RemoveAllCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanRemoveAll()
        {
            return CanRemoveAllMetricRating;
        }

        protected override void OnSaveCommand()
        {
            try
            {
                if (InvalidEntry())
                {
                    return;
                }

                ObservableCollection<MetricRating> models = (ObservableCollection<MetricRating>)TargetCollection.SourceCollection;
                if (collectionManager.Collection == null || collectionManager.Collection.Count == 0)
                {
                    MessageBoxResult response = MessageBox.Show("This action will permanently remove all Metric Ratings associated with '" + Metrics.Perspective.Name + " - " + Metrics.Kpi + "' for " + Metrics.JobRoleName + ". Do you want to continue?", "METRIC RATING", MessageBoxButton.OKCancel);
                    if (response == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                }
                else
                {
                    base.OnSaveCommand();
                    return;
                }

                if (Metrics != null)
                {
                    RemoveAllCompleted();
                    metricRatingService.RemoveBy(Metrics);
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
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
                    metricRatingService.ActionCompleted -= handler;
                };

                metricRatingService.ActionCompleted += handler;
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
                               if (Utility.FaultExist(metricRatingService.Fault))
                               {
                                   return;
                               }

                               if (metricRatingService.Done)
                               {
                                   Utility.DisplayMessage("All " + ModelName + " associated with '" + Metrics.Perspective.Name + " - " + Metrics.Kpi + "' for " + Metrics.JobRoleName + " have been successfully removed");
                                   ClearView();
                               }
                               else
                               {
                                   Utility.DisplayMessage("Removal of " + ModelName + " associated with '" + Metrics.Perspective.Name + " - " + Metrics.Kpi + "' for " + Metrics.JobRoleName + " was not successfull! Please try again");
                               }
                           });
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
                if (Metrics == null || Metrics.Id <= 0)
                {
                    Utility.DisplayMessage("Please select a Metric!");
                    return false;
                }
                else if (Rating == null || Rating.Id == 0)
                {
                    Utility.DisplayMessage("Please select Rating! Rating of '0' cannot be added.");
                    return false;
                }
                //else if (TargetModel.From == 0)
                //{
                //    Utility.DisplayMessage("From Field canot be empty!");
                //    return false;
                //}
                //else if (TargetModel.To == 0)
                //{
                //    Utility.DisplayMessage("To Field canot be empty!");
                //    return false;
                //}
                else if (RatingType == null || RatingType.Id <= 0)
                {
                    Utility.DisplayMessage("Please select Rating Type!");
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

        private bool InvalidEntry()
        {
            try
            {
                if (TargetModel == null || TargetCollection == null)
                {
                    Utility.DisplayMessage("The underlying model was not found! Contact your system administrator");
                    return true;
                }

                //int firstRow = 0;
                bool isGoingUp = true;

                ObservableCollection<MetricRating> metricRatings = (ObservableCollection<MetricRating>)TargetCollection.SourceCollection;
                IEnumerable<MetricRating> newMetricRatings = metricRatings.OrderBy(mr => mr.Rating.Id).ToList();
                if (metricRatings != null && metricRatings.Count > 0)
                {
                    decimal previousFrom = 0;
                    decimal previousTo = 0;
                    for (int i = 0; i < metricRatings.Count; i++)
                    {
                        decimal from = metricRatings[i].From;
                        decimal to = metricRatings[i].To;

                        if (from > to && i == 0)
                        {
                            isGoingUp = false;
                        }

                        if (isGoingUp && i > 0)
                        {
                            int j = i;
                            if (from > to)
                            {
                                Utility.DisplayMessage("From field '" + from + "' on row " + j + " cannot be greater than '" + to + "' on row " + (j + 1) + ". Please modify to continue");
                                return true;
                            }
                            else if (previousTo >= from)
                            {
                                Utility.DisplayMessage("To field '" + previousTo + "' on row '" + j + "' cannot be equal to or greater than From field '" + from + "' on row " + (j + 1) + ". Please modify to continue");
                                return true;
                            }
                            //else if (previousTo == from)
                            //{
                            //    Utility.DisplayMessage("Value overlap detected! To field '" + to + "' on row '" + (j + 1) + "' cannot be equal to From field '" + from + "' on row " + j + ". Please modify to continue");
                            //    return true;
                            //}
                        }

                        previousFrom = from;
                        previousTo = to;
                    }
                }

                return false;
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
                    foreach (MetricRating metricRating in TargetCollection)
                    {
                        if (metricRating.Rating.Id == Rating.Id)
                        {
                            Utility.DisplayMessage("Duplicate Rating found! Rating of " + Rating.Id + " already exist on the list!. Please change Rating to continue");
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

                //Metrics = new Infrastructure.MangoService.Metrics();

                Metrics = null;
                IsInfinity = false;
                Ratings.MoveCurrentToPosition(0);
                RatingTypes.MoveCurrentToPosition(0);
                TargetModel = new MetricRating();

                UpdateViewState(Edit.Mode.Loaded);

                //CanRemoveAllMetricRating = false;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override MetricRating GetNewModel()
        {
            try
            {
                MetricRating metricRating = new MetricRating();
                metricRating.Metrics = new Metrics();
                metricRating.Rating = new Infrastructure.MangoService.Rating();
                metricRating.RatingType = new RatingType();

                metricRating.Metrics.Id = Metrics.Id;
                metricRating.Rating = Rating;
                metricRating.From = TargetModel.From;
                metricRating.To = TargetModel.To;

                if (IsInfinity)
                {
                    metricRating.To = 999999999999999;
                }

                metricRating.RatingType = RatingType;
                metricRating.Period = Utility.Period;

                return metricRating;
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
                ObservableCollection<MetricRating> models = (ObservableCollection<MetricRating>)TargetCollection.SourceCollection;
                if (collectionManager.Collection != null && collectionManager.Collection.Count > 0)
                {
                    MessageBoxResult response = MessageBox.Show("You are about to remove all Metric Ratings associated with '" + Metrics.Perspective.Name + " - " + Metrics.Kpi + "' for " + Metrics.JobRoleName + ". Are you sure you want to do this?", "METRIC RATING", MessageBoxButton.OKCancel);
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

        protected void LoadMetricsByPeriodCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadMetricsByPeriodCompletedHelper();
                    metricsService.GetModelsCompleted -= handler;
                };

                metricsService.GetModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadMetricsByPeriodCompletedHelper()
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

                               if (metricsService.Models != null)
                               {
                                   Metrices = new PagedCollectionView(metricsService.Models);
                                   Metrices.MoveCurrentTo(null);

                                   PropertyGroupDescription groupDescription = new PropertyGroupDescription("JobRoleName");
                                   if (Metrices != null)
                                   {
                                       Metrices.GroupDescriptions.Add(groupDescription);
                                       if (MetricesLoaded != null)
                                       {
                                           MetricesLoaded(this, null);
                                       }
                                   }

                                   Metrices.CurrentChanged += (s, e) =>
                                   {
                                       Metrics = Metrices.CurrentItem as Metrics;
                                       if (Metrics != null)
                                       {
                                           LoadMetricRatingByMetricsCompleted();
                                           metricRatingService.LoadByMetrics(Metrics);
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

        protected void LoadAllRatingsCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllRatingsCompletedHelper();
                    ratingService.GetAllModelsCompleted -= handler;
                };

                ratingService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllRatingsCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(ratingService.Fault))
                               {
                                   return;
                               }

                               if (ratingService.Models != null)
                               {
                                   ratingService.Models.Insert(0, new Infrastructure.MangoService.Rating() { Name = 0 });

                                   Ratings = new PagedCollectionView(ratingService.Models);
                                   Ratings.MoveCurrentToFirst();
                                   Ratings.CurrentChanged += (s, e) =>
                                   {
                                       Rating = Ratings.CurrentItem as Infrastructure.MangoService.Rating;
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllRatingTypesCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllRatingTypesCompletedHelper();
                    ratingTypeService.GetAllModelsCompleted -= handler;
                };

                ratingTypeService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllRatingTypesCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(ratingTypeService.Fault))
                               {
                                   return;
                               }

                               if (ratingTypeService.Models != null)
                               {
                                   ratingTypeService.Models.Insert(0, new Infrastructure.MangoService.RatingType() { Name = "<< Select Rating Type >>" });

                                   RatingTypes = new PagedCollectionView(ratingTypeService.Models);
                                   RatingTypes.MoveCurrentToFirst();
                                   RatingTypes.CurrentChanged += (s, e) =>
                                   {
                                       RatingType = RatingTypes.CurrentItem as RatingType;
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadMetricRatingByMetricsCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadMetricRatingByMetricsCompletedHelper();
                    metricRatingService.GetModelsCompleted -= handler;
                };

                metricRatingService.GetModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadMetricRatingByMetricsCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(metricRatingService.Fault))
                               {
                                   return;
                               }

                               SetCurrentTargetCollection(metricRatingService.Models);
                               collectionManager.Collection = metricRatingService.Models;

                               if (metricRatingService.Models != null)
                               {
                                   if (metricRatingService.Models.Count > 0)
                                   {
                                       UpdateViewState(Edit.Mode.Editing);
                                       CanRemoveAllMetricRating = true;
                                   }
                                   else
                                   {
                                       UpdateViewState(Edit.Mode.Loaded);
                                   }

                                   IsModelExist(metricRatingService.Models);
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        //protected override void UpdateViewState(Edit.Mode uiState)
        //{
        //    try
        //    {
        //        base.UpdateViewState(uiState);

        //        switch (uiState)
        //        {
        //            case Edit.Mode.Loaded:
        //                {
        //                    if (Metrics != null)
        //                    {
        //                        ModelCanBeSaved = true;
        //                    }
        //                    else
        //                    {
        //                        ModelCanBeSaved = false;
        //                    }

        //                    if (RemoveAllCommand != null)
        //                    {
        //                        CanRemoveAllMetricRating = false;
        //                    }
        //                    break;
        //                }
        //            case Edit.Mode.Adding:
        //            case Edit.Mode.Selected:
        //                {
        //                    UpdateViewStateHelper();
        //                    break;
        //                }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}

        protected override void UpdateViewState(Edit.Mode uiState)
        {
            try
            {
                base.UpdateViewState(uiState);

                switch (uiState)
                {
                    case Edit.Mode.Loaded:
                        {
                            if (Metrics != null)
                            {
                                ModelCanBeSaved = true;
                            }
                            else
                            {
                                ModelCanBeSaved = false;
                            }

                            if (RemoveAllCommand != null)
                            {
                                CanRemoveAllMetricRating = false;
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
                if (RemoveAllCommand != null)
                {
                    if (collectionManager.Collection == null || collectionManager.Collection.Count == 0)
                    {
                        CanRemoveAllMetricRating = false;
                    }
                    else
                    {
                        CanRemoveAllMetricRating = true;
                    }
                }
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
                                   CompanyDepartmentJobRole cdjr = cdjrService.Models.Where(c => c.Id == 0).SingleOrDefault();
                                   if (cdjr == null)
                                   {
                                       cdjrService.Models.Insert(0, new CompanyDepartmentJobRole() { Id = 0, JobRole = new JobRole() { Name = "<< Select CDJR >>" } });
                                   }

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

                               TargetCollection = new PagedCollectionView(new ObservableCollection<MetricRating>());
                                
                               SetCurrentTargetCollection(metricsService.Models);
                              
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void SetCurrentTargetCollection(ObservableCollection<Metrics> refreshedModels)
        {
            try
            {
                Metrices = new PagedCollectionView(refreshedModels);
                if (refreshedModels != null)
                {
                    //UpdateMetricsRecordCount(refreshedModels);

                    Metrices.MoveCurrentTo(null);
                    Metrices.CurrentChanged += (s, e) =>
                    {
                        //SelectedTargetCollection();

                        Metrics = Metrices.CurrentItem as Metrics;
                        if (Metrics != null)
                        {
                            LoadMetricRatingByMetricsCompleted();
                            metricRatingService.LoadByMetrics(Metrics);
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }







    }

}
