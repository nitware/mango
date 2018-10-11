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
using Mango.Setup.Services;

using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Interfaces;
using System.ComponentModel;
using Mango.Infrastructure.Models;
using System.Linq;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.Events;
using System.Collections.Generic;

namespace Mango.Setup.ViewModels
{
    public class StaffLocationViewModel : SetupViewModelBase<StaffLocation>
    {
        private ISetupService<Staff> staffService;
        private LocationService locationService;

        private ICollectionView staffs;
        private Infrastructure.MangoService.Staff staff;
        private Location location;
        private ICollectionView locations;

        public StaffLocationViewModel(ISetupService<StaffLocation> _service, ISetupService<Staff> _staffService, IEventAggregator _eventAggregator)
            : base(_service)
        {
            staffService = _staffService;
            locationService = new LocationService();

            //LoadAllLocationsCompleted();
            //locationService.LoadAll();

            modelName = "Staff Location";
            Initialize();

            //LoadAllStaffCompleted();
            //staffService.LoadAll();

            base.addSelector = sl => sl.Staff.Id == Staff.Id && sl.Period.Id == Utility.Period.Id;
            //base.modifySelector = sl => sl.Staff.Id == Staff.Id && sl.Period.Id == Utility.Period.Id;

            //_eventAggregator.GetEvent<SetupEvent>().Subscribe(OnInitialise);

            OnInitialise("");
        }

        public void OnInitialise(string refresh)
        {
            LoadAllLocationsCompleted();
            locationService.LoadAll();

            if (staffService.Models == null)
            {
                LoadAllStaffCompleted();
                staffService.LoadAll();
            }
            else
            {
                PopulateStaffs();
            }

            
        }

        public string TabCaption
        {
            get { return modelName; }
        }
        public ICollectionView Staffs
        {
            get { return staffs; }
            set
            {
                staffs = value;
                OnPropertyChanged("Staffs");
            }
        }
        public Infrastructure.MangoService.Staff Staff
        {
            get { return staff; }
            set
            {
                staff = value;
                OnPropertyChanged("Staff");
            }
        }
        public ICollectionView Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                OnPropertyChanged("Locations");
            }
        }
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }
       
        protected override void OnSaveCommand()
        {
            try
            {
                if (Staff == null)
                {
                    Utility.DisplayMessage("Please select staff!");
                    return;
                }
                else if (Location == null)
                {
                    Utility.DisplayMessage("Please select Location!");
                    return;
                }
                //else if (Model.TrainingScore == 0 && Model.PercentScore > 0)
                //{
                //    Utility.DisplayMessage("Percent Score cannot be greater than zero when Training Score is equal to zero!");
                //    return;
                //}

                Model.Staff = Staff;
                Model.Location = Location;
                Model.Period = Utility.Period;

                if (base.InvalidEntry(Model.Staff.Name))
                {
                    return;
                }

                base.OnSaveCommand();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllStaffCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllStaffCompletedHelper();
                    staffService.GetAllModelsCompleted -= handler;
                };

                staffService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage("Error occured! " + ex.Message);
            }
        }
        protected void LoadAllStaffCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(staffService.Fault))
                               {
                                   return;
                               }

                               if (staffService.Models != null && staffService.Models.Count > 0)
                               {
                                   //Staff staff = staffService.Models.Where(s => s.Id == "0").SingleOrDefault();
                                   //if (staff == null)
                                   //{
                                       //staffService.Models.Insert(0, new Infrastructure.MangoService.Staff() { Id = "0", FullName = "<< Select Staff >>" });
                                   //}

                                       PopulateStaffs();
                               }

                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void PopulateStaffs()
        {
            try
            {
                List<Staff> staffs = staffService.Models.Where(s => s.Id == "0").ToList();
                if (staffs == null || staffs.Count == 0)
                {
                    staffService.Models.Insert(0, new Infrastructure.MangoService.Staff() { Id = "0", IsActive = true, FullName = "<< Select Satff >>" });
                }

                Staffs = new PagedCollectionView(staffService.Models);
                Staffs.MoveCurrentToFirst();
                Staffs.CurrentChanged += (s, e) =>
                {
                    Staff = Staffs.CurrentItem as Infrastructure.MangoService.Staff;
                };
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        protected void LoadAllLocationsCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllLocationsCompletedHelper();
                    locationService.GetAllModelsCompleted -= handler;
                };

                locationService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage("Error occured! " + ex.Message);
            }
        }
        protected void LoadAllLocationsCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(locationService.Fault))
                               {
                                   return;
                               }

                               if (locationService.Models != null && locationService.Models.Count > 0)
                               {
                                   Location location = locationService.Models.Where(s => s.Id == "").SingleOrDefault();
                                   if (location == null)
                                   {
                                       locationService.Models.Insert(0, new Location() { Id = "", Name = "<< Select Location >>" });
                                   }

                                   Locations = new PagedCollectionView(locationService.Models);
                                   Locations.MoveCurrentToFirst();
                                   Locations.CurrentChanged += (s, e) =>
                                   {
                                       Location = Locations.CurrentItem as Location;
                                   };
                               }

                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override void SetSelectedModel()
        {
            try
            {
                Model = Models.CurrentItem as StaffLocation;

                ObservableCollection<Staff> staffs = (ObservableCollection<Staff>)Staffs.SourceCollection;
                if (staffs != null)
                {
                    Staff staff = staffs.Where(s => s.Id == Model.Staff.Id).SingleOrDefault();
                    if (staff != null)
                    {
                        Staffs.MoveCurrentTo(staff);
                    }
                }

                ObservableCollection<Location> locations = (ObservableCollection<Location>)Locations.SourceCollection;
                if (locations != null)
                {
                    Location location = locations.Where(l => l.Id == Model.Location.Id).SingleOrDefault();
                    if (location != null)
                    {
                        Locations.MoveCurrentTo(location);
                    }
                }
                
                UpdateViewState(Edit.Mode.Editing);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override void OnClearCommand()
        {
            try
            {
                UpdateViewState(Edit.Mode.Adding);
                Model = new StaffLocation();

                if (Staffs != null)
                {
                    Staffs.MoveCurrentToFirst();
                }
                if (Locations != null)
                {
                    Locations.MoveCurrentToFirst();
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }



    }
}
