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
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Models;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.Events;
using System.Collections.Generic;

namespace Mango.Setup.ViewModels
{
    public class StaffCdjrViewModel : SetupViewModelBase<StaffCdjr>
    {
        private ICollectionView companyDepartmentJobRoles;
        private CompanyDepartmentJobRole companyDepartmentJobRole;

        private ICollectionView staffs;
        private Infrastructure.MangoService.Staff staff;

        private ISetupService<Staff> staffService;
        private ISetupService<CompanyDepartmentJobRole> cdjrService;

        public StaffCdjrViewModel(ISetupService<StaffCdjr> _service, ISetupService<Staff> _staffService, ISetupService<CompanyDepartmentJobRole> _cdjrService, IEventAggregator _eventAggregator)
            : base(_service)
        {
            staffService = _staffService;
            cdjrService = _cdjrService;

            modelName = "Staff CDJR";
            Initialize();

            //LoadAllStaffCompleted();
            //staffService.LoadAll();

            //LoadAllCdjrCompleted();
            //cdjrService.LoadAll();

            base.addSelector = sl => sl.Staff.Id == Staff.Id && sl.Period.Id == Utility.Period.Id;
            base.modifySelector = sl => sl.Staff.Id == Staff.Id && sl.Period.Id == Utility.Period.Id && sl.Staff.CompanyDepartmentJobRoleId == CompanyDepartmentJobRole.Id;

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

            LoadAllCdjrCompleted();
            cdjrService.LoadAll();
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
        protected override void OnClearCommand()
        {
            try
            {
                UpdateViewState(Edit.Mode.Adding);
                Model = new StaffCdjr();

                if (CompanyDepartmentJobRoles != null)
                {
                    CompanyDepartmentJobRoles.MoveCurrentToFirst();
                }
                if (Staffs != null)
                {
                    Staffs.MoveCurrentToFirst();
                }
                if (Models != null)
                {
                    Models.MoveCurrentTo(null);
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
                else if (CompanyDepartmentJobRole == null)
                {
                    Utility.DisplayMessage("Please select Company Department Job Role!");
                    return;
                }

                Model.CompanyDepartmentJobRole = CompanyDepartmentJobRole;
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

        protected void LoadAllCdjrCompleted()
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
        protected void LoadAllCdjrCompletedHelper()
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
                                   //cdjrService.Models.Insert(0, new CompanyDepartmentJobRole() { Id = 0, Company = new Company() { Symbol = "<< Select CDJR >>" } });

                                   cdjrService.Models.Insert(0, new CompanyDepartmentJobRole() { Id = 0, JobRole = new JobRole() { Name = "<< Select CDJR >>" } });
                                   if (cdjrService.Models != null && cdjrService.Models.Count > 0)
                                   {
                                       CompanyDepartmentJobRoles = new PagedCollectionView(cdjrService.Models);

                                       CompanyDepartmentJobRoles.MoveCurrentToFirst();
                                       CompanyDepartmentJobRoles.CurrentChanged += (s, e) =>
                                       {
                                           CompanyDepartmentJobRole = CompanyDepartmentJobRoles.CurrentItem as CompanyDepartmentJobRole;
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
                                       Model = Models.CurrentItem as StaffCdjr;
                                       if (Model != null)
                                       {
                                           if (CompanyDepartmentJobRoles != null)
                                           {
                                               ObservableCollection<CompanyDepartmentJobRole> companyDepartmentJobRoles = (ObservableCollection<CompanyDepartmentJobRole>)CompanyDepartmentJobRoles.SourceCollection;
                                               CompanyDepartmentJobRole companyDepartmentJobRole = companyDepartmentJobRoles.Where(l => l.Id == Model.CompanyDepartmentJobRole.Id).SingleOrDefault();
                                               CompanyDepartmentJobRoles.MoveCurrentTo(companyDepartmentJobRole);
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
