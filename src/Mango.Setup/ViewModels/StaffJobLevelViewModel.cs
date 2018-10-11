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
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.Models;
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mango.Setup.Services;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.Events;

namespace Mango.Setup.ViewModels
{
    public class StaffJobLevelViewModel : SetupViewModelBase<StaffLevel>
    {
        private ICollectionView staffs;
        private Infrastructure.MangoService.Staff staff;

        private ISetupService<Staff> staffService;
        private LevelService levelService;

        private Level jobLevel; 
        private ICollectionView jobLevels;

        public StaffJobLevelViewModel(ISetupService<StaffLevel> _service, ISetupService<Staff> _staffService, IEventAggregator _eventAggregator)
            : base(_service)
        {
            staffService = _staffService;
            levelService = new LevelService();

            modelName = "Staff Level";
            Initialize();

            //LoadAllStaffCompleted();
            //staffService.LoadAll();

            //LoadAllJobLevelCompleted();
            //levelService.LoadAll();

            base.addSelector = sl => sl.Staff.Id == Staff.Id && sl.Period.Id == Utility.Period.Id;
            base.modifySelector = sl => sl.Staff.Id == Staff.Id && sl.Period.Id == Utility.Period.Id && sl.Staff.Level.Id == JobLevel.Id;

            //_eventAggregator.GetEvent<SetupEvent>().Subscribe(OnInitialise);

            OnInitialise("");
        }

        public void OnInitialise(string refresh)
        {
            //LoadAllStaffCompleted();
            //staffService.LoadAll();

            if (staffService.Models == null)
            {
                LoadAllStaffCompleted();
                staffService.LoadAll();
            }
            else
            {
                PopulateStaffs();
            }

            LoadAllJobLevelCompleted();
            levelService.LoadAll();
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
        public ICollectionView JobLevels
        {
            get { return jobLevels; }
            set
            {
                jobLevels = value;
                OnPropertyChanged("JobLevels");
            }
        }
        public Level JobLevel
        {
            get { return jobLevel; }
            set
            {
                jobLevel = value;
                OnPropertyChanged("JobLevel");
            }
        }
        protected override void OnClearCommand()
        {
            try
            {
                UpdateViewState(Edit.Mode.Adding);
                Model = new StaffLevel();

                if (JobLevels != null)
                {
                    JobLevels.MoveCurrentToFirst();
                }
                if (Staffs != null)
                {
                    Staffs.MoveCurrentToFirst();
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
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
                else if (JobLevel == null)
                {
                    Utility.DisplayMessage("Please select Job Level!");
                    return;
                }

                
                               
                Model.Level = JobLevel;
                Model.Staff = Staff;
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
                                   //staffService.Models.Insert(0, new Infrastructure.MangoService.Staff() { Id = "0", IsActive = true, FullName = "<< Select Satff >>" });
                                   //if (staffService.Models != null && staffService.Models.Count() > 0)
                                   //{
                                       //Staffs = new PagedCollectionView(staffService.Models);

                                       //Staffs.MoveCurrentToFirst();
                                       //Staffs.CurrentChanged += (s, e) =>
                                       //{
                                       //    Staff = Staffs.CurrentItem as Infrastructure.MangoService.Staff;
                                       //};

                                       PopulateStaffs();
                                   //}
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

        protected void LoadAllJobLevelCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllJobLevelCompletedHelper();
                    levelService.GetAllModelsCompleted -= handler;
                };

                levelService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        protected void LoadAllJobLevelCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(levelService.Fault))
                               {
                                   return;
                               }

                               if (levelService.Models != null && levelService.Models.Count > 0)
                               {
                                   levelService.Models.Insert(0, new Level() { Id = "", Name = "<< Select Job Level >>" });
                                   if (levelService.Models != null && levelService.Models.Count() > 0)
                                   {
                                       JobLevels = new PagedCollectionView(levelService.Models);

                                       JobLevels.MoveCurrentToFirst();
                                       JobLevels.CurrentChanged += (s, e) =>
                                       {
                                           JobLevel = JobLevels.CurrentItem as Level;
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

        protected override void LoadAllCommandCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(service.Fault))
                               {
                                   return;
                               }

                               Models = new PagedCollectionView(service.Models);
                               if (service.Models != null && service.Models.Count > 0)
                               {
                                   RecordCount = RecordCountLabel + service.Models.Count;
                                   Models.MoveCurrentTo(null);
                                   Models.CurrentChanged += (s, e) =>
                                   {
                                       Model = Models.CurrentItem as StaffLevel;
                                       if (Model != null)
                                       {                                           
                                           if (JobLevels != null)
                                           {
                                               ObservableCollection<Level> levels = (ObservableCollection<Level>)JobLevels.SourceCollection;
                                               Level level = levels.Where(l => l.Id == Model.Level.Id).SingleOrDefault();
                                               JobLevels.MoveCurrentTo(level);
                                           }

                                           if (Staffs != null)
                                           {
                                               ObservableCollection<Infrastructure.MangoService.Staff> staffs = (ObservableCollection<Infrastructure.MangoService.Staff>)Staffs.SourceCollection;
                                               Infrastructure.MangoService.Staff staff = staffs.Where(l => l.Id == Model.Staff.Id).SingleOrDefault();
                                               Staffs.MoveCurrentTo(staff);
                                           }
                                       }

                                       UpdateViewState(Edit.Mode.Editing);
                                       CanSaveItem = false;
                                   };
                               }
                               else
                               {
                                   RecordCount = RecordCountLabel + 0;
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
