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
using System.ComponentModel;
using Mango.Infrastructure.Models;
using System.Windows.Data;
using System.Linq;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.Events;
using System.Collections.Generic;

namespace Mango.Setup.ViewModels
{
    public class StaffLearningViewModel : SetupViewModelBase<StaffLearning>
    {
        private ISetupService<Staff> staffService;

        private ICollectionView staffs;
        private Infrastructure.MangoService.Staff staff;

        public StaffLearningViewModel(ISetupService<StaffLearning> _service, ISetupService<Staff> _staffService, IEventAggregator _eventAggregator)
            : base(_service)
        {
            staffService = _staffService;

            modelName = "Staff Learning";
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

        protected override void OnSaveCommand()
        {
            try
            {
                if (Staff == null)
                {
                    Utility.DisplayMessage("Please select staff!");
                    return;
                }
                else if (Model.TrainingScore > 0 && Model.PercentScore <= 0)
                {
                    Utility.DisplayMessage("Please enter Percent Score!");
                    return;
                }
                else if (Model.TrainingScore == 0 && Model.PercentScore > 0)
                {
                    Utility.DisplayMessage("Percent Score cannot be greater than zero when Training Score is equal to zero!");
                    return;
                }

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

        protected override void SetSelectedModel()
        {
            try
            {
                Model = Models.CurrentItem as StaffLearning;

                ObservableCollection<Staff> staffs = (ObservableCollection<Staff>)Staffs.SourceCollection;
                if (staffs != null)
                {
                    Staff staff = staffs.Where(s => s.Id == Model.Staff.Id).SingleOrDefault();
                    if (staff != null)
                    {
                        Staffs.MoveCurrentTo(staff);
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
                Model = new StaffLearning();

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





    }
}
