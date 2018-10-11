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

using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.MangoService;
using System.ComponentModel;
using Mango.Infrastructure.ViewModelBase;
using System.Windows.Data;
using Mango.Infrastructure.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Mango.Setup.Services;

namespace Mango.Setup.ViewModels.Upload
{
    public class SetupInpsRatingViewModel : CollectionViewModelBase<InpsRating>
    {
        private ICollectionView ratings;
        private ICollectionView ratingTypes;
        private Infrastructure.MangoService.Rating rating;
        private RatingType ratingType;
        private bool isInfinity;
        private Period period;
        private InpsType inpsType;
        private ICollectionView inpsTypes;

        private ISetupService<InpsType> inpsTypeService;
        private ISetupService<Infrastructure.MangoService.Rating> ratingService;
        private ISetupService<RatingType> ratingTypeService;

        private InpsRatingService inpsRatingService;

        //public SetupInpsRatingViewModel(IMetricBaseService<InpsRating> _service, ISetupService<RatingType> _ratingTypeService, ISetupService<Infrastructure.MangoService.Rating> _ratingService)

        public SetupInpsRatingViewModel(IMetricBaseService<InpsRating> _service, ISetupService<RatingType> _ratingTypeService, ISetupService<Infrastructure.MangoService.Rating> _ratingService)
            : base(_service)
        {
            IsInfinity = false;
            //RemoveAllCommand = new DelegateCommand(OnRemoveAllCommand, CanRemoveAll);

            ratingService = _ratingService;
            ratingTypeService = _ratingTypeService;
            inpsTypeService = new InpsTypeService();

            inpsRatingService = new InpsRatingService();

            //service.LoadByPeriod(Utility.Period);
            //LoadAllInpsRatingsByPeriodCompleted();

            LoadAllInpsTypeCompleted();
            inpsTypeService.LoadAll();

            Period = Utility.Period;
            
            ModelName = "NPS/INPS Rating";

            OnInitialise("");

           

           
        }

        public string TabCaption
        {
            get { return "Setup Rating"; }
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
        public Period Period
        {
            get { return period; }
            set
            {
                period = value;
                base.OnPropertyChanged("Period");
            }
        }
        
        public ICollectionView InpsTypes
        {
            get { return inpsTypes; }
            set
            {
                inpsTypes = value;
                OnPropertyChanged("InpsTypes");
            }
        }
        public InpsType InpsType
        {
            get { return inpsType; }
            set
            {
                inpsType = value;
                OnPropertyChanged("InpsType");
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

        protected override void OnSaveCommand()
        {
            try
            {
                if (InvalidEntry())
                {
                    return;
                }

                ObservableCollection<InpsRating> models = (ObservableCollection<InpsRating>)TargetCollection.SourceCollection;
                if (collectionManager.Collection == null || collectionManager.Collection.Count == 0)
                {
                    MessageBoxResult response = MessageBox.Show("This action will permanently remove all INPS Ratings associated with " + Utility.Period.Name + ". Do you want to continue?", "INPS RATING", MessageBoxButton.OKCancel);
                    if (response == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                }
                else
                {
                    ModelName = InpsType.Name + " Rating";
                    base.OnSaveCommand();
                }
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
                if (TargetModel == null || TargetCollection == null)
                {
                    Utility.DisplayMessage("The underlying model was not found! Contact your system administrator");
                    return true;
                }
                else if (InpsType == null || InpsType.Id <= 0)
                {
                    Utility.DisplayMessage("No Type selected! Please INPS Type.");
                    return true;
                }

                bool isGoingUp = true;

                ObservableCollection<InpsRating> inpsRatings = (ObservableCollection<InpsRating>)TargetCollection.SourceCollection;
                IEnumerable<InpsRating> newInpsRatings = inpsRatings.OrderBy(mr => mr.Rating.Id).ToList();
                if (inpsRatings != null && inpsRatings.Count > 0)
                {
                    decimal previousFrom = 0;
                    decimal previousTo = 0;
                    for (int i = 0; i < inpsRatings.Count; i++)
                    {
                        decimal from = inpsRatings[i].From;
                        decimal to = inpsRatings[i].To;

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

        public void OnInitialise(string refresh)
        {
            LoadAllRatingsCompleted();
            ratingService.LoadAll();

            LoadAllRatingTypesCompleted();
            ratingTypeService.LoadAll();
        }

        protected void LoadAllInpsRatingsByPeriodAndTypeCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllInpsRatingsByPeriodAndTypeCompletedHelper();
                    inpsRatingService.GetInpsRatingByPeriodAndTypeCompleted -= handler;
                };

                inpsRatingService.GetInpsRatingByPeriodAndTypeCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllInpsRatingsByPeriodAndTypeCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(inpsRatingService.Fault))
                               {
                                   return;
                               }

                               base.IsModelExist(inpsRatingService.Models);
                               base.collectionManager.Collection = inpsRatingService.Models;
                               base.RefreshModelCollection();


                               //SetCurrentTargetCollection(service.Models);

                               //if (service.Models != null)
                               //{
                               //    //service.Models.Insert(0, new Infrastructure.MangoService.InpsRating() { Name = 0 });

                               //    //TargetCollection = new PagedCollectionView(service.Models);
                               //    //TargetCollection.MoveCurrentTo(null);


                                   
                               //}
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

        protected override InpsRating GetNewModel()
        {
            try
            {
                //RatingType ratingType = new RatingType();

                InpsRating inpsRating = new InpsRating();
                Infrastructure.MangoService.Rating rating = new Infrastructure.MangoService.Rating();

                inpsRating.Type = InpsType;
                inpsRating.Rating = Rating;
                inpsRating.Period = Utility.Period;
                inpsRating.RatingType = RatingType;
                inpsRating.From = TargetModel.From;
                inpsRating.To = TargetModel.To;

                if (IsInfinity)
                {
                    inpsRating.To = 999999999999999;
                }
                              
                return inpsRating;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void ClearView()
        {
            ClearTargetCollection();

            Ratings.MoveCurrentToPosition(0);
            RatingTypes.MoveCurrentToPosition(0);
            
            TargetModel = new InpsRating();
        }

        protected override void ClearTargetCollection()
        {
            try
            {
                UpdateViewState(Edit.Mode.Loaded);
                
                //if (TargetCollection != null)
                //{
                //    //collectionManager.Collection.Clear();
                //    //ObservableCollection<T> models = (ObservableCollection<T>)TargetCollection.SourceCollection;

                //    //models.Clear();
                //    //TargetCollection = new PagedCollectionView(models);


                //    UpdateViewState(Edit.Mode.Loaded);
                //}
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
                if (Rating == null || Rating.Id == 0)
                {
                    Utility.DisplayMessage("Please select Rating! Rating of '0' cannot be added.");
                    return false;
                }
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

        protected override bool ModelAlreadyExist()
        {
            try
            {
                if (TargetCollection != null)
                {
                    foreach (InpsRating inpsRating in TargetCollection)
                    {
                        if (inpsRating.Rating.Id == Rating.Id)
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

        protected void LoadAllInpsTypeCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllInpsTypeCompletedHelper();
                    inpsTypeService.GetAllModelsCompleted -= handler;
                };

                inpsTypeService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllInpsTypeCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(inpsTypeService.Fault))
                               {
                                   return;
                               }

                               if (inpsTypeService.Models != null && inpsTypeService.Models.Count > 0)
                               {
                                   List<InpsType> inpsTypes = inpsTypeService.Models.Where(d => d.Id != 0).ToList();
                                   if (inpsTypes.Count > 0)
                                   {
                                       inpsTypes.Insert(0, new InpsType() { Name = "<< Select a Type >>" });
                                   }

                                   InpsTypes = new PagedCollectionView(inpsTypes);
                                   InpsTypes.MoveCurrentToFirst();
                                   InpsTypes.CurrentChanged += (s, e) =>
                                   {
                                       InpsType = InpsTypes.CurrentItem as InpsType;
                                       if (InpsType != null && InpsType.Id > 0)
                                       {
                                           //GetAllInpsByPeriodAndType();

                                           LoadAllInpsRatingsByPeriodAndTypeCompleted();
                                           inpsRatingService.LoadByPeriodAndType(Utility.Period, InpsType);
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





    }

    


}
