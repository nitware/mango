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
using Mango.Setup.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Linq;
using System.Collections.ObjectModel;
using Mango.Setup.Interfaces;

namespace Mango.Setup.ViewModels.Upload
{
    public class EditInpsViewModel : SetupViewModelBase<Inps>
    {
        private InpsType inpsType;
        private ICollectionView inpsTypes;
        private Department department;
        private ICollectionView departments;
        private ICollectionView staffs;
        private Infrastructure.MangoService.Staff staff;

        private ISetupService<Staff> staffService;
        private ISetupService<Department> departmentService;
        private ISetupService<InpsType> inpsTypeService;
        private IUploadService uploadService;

        public EditInpsViewModel(ISetupService<Inps> _service, ISetupService<Staff> _staffService)
            : base(_service)
        {
            modelName = "INPS";
            Initialize();

            staffService = _staffService;
            uploadService = new UploadService();
            inpsTypeService = new InpsTypeService();
            departmentService = new DepartmentService();
            LoadAllDepartment();

            LoadAllStaffCompleted();
            staffService.LoadAll();

            LoadAllInpsTypeCompleted();
            inpsTypeService.LoadAll();

            base.addSelector = i => i.Staff.Id.Equals(Model.Staff.Id, StringComparison.OrdinalIgnoreCase) && i.Period.Id == Model.Period.Id;
            base.modifySelector = i => i.Staff.Id.Equals(Model.Staff.Id, StringComparison.OrdinalIgnoreCase) && i.Period.Id == Model.Period.Id && i.Type.Id == Model.Type.Id && i.Id != Model.Id;
        }
        
        public string TabCaption
        {
            get { return "Edit"; }
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

        protected override void OnSaveCommand()
        {
            try
            {
                Model.Period = Utility.Period;
                Model.Staff = Staff;
                Model.ResponsibleDepartment = Department;
                Model.Type = InpsType;

                if (InvalidEntry(""))
                {
                    return;
                }

                modelName = InpsType.Name;
                modifySuccessfulMessage = modelName + " has been modified successfully";
                modifyFailedMessage = modelName + " modification failed! Please try again";
                
                base.OnSaveCommand();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override bool InvalidEntry(string empty)
        {
            try
            {
                if (InpsType == null || InpsType.Id <= 0)
                {
                    Utility.DisplayMessage("No Type select!");
                    return true;
                }
                else if (Staff == null)
                {
                    Utility.DisplayMessage("Please select Staff!");
                    return true;
                }
                else if (Department == null)
                {
                    Utility.DisplayMessage("Please select a department!");
                    return true;
                }
                else if (Model.Target == 0 || Model.Target == null)
                {
                    Utility.DisplayMessage("Please enter Target!");
                    return true;
                }
                else if (Model.Score == 0 || Model.Score == null)
                {
                    Utility.DisplayMessage("Please enter Score!");
                    return true;
                }

                if (base.InvalidEntry(Model.Staff.Name))
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
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
                    staffService.Models.Insert(0, new Infrastructure.MangoService.Staff() { Id = "0", IsActive = true, Name = "<< Select Satff >>" });
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
                Model = Models.CurrentItem as Inps;

                if (Staffs != null)
                {
                    ObservableCollection<Infrastructure.MangoService.Staff> staffs = (ObservableCollection<Infrastructure.MangoService.Staff>)Staffs.SourceCollection;
                    Infrastructure.MangoService.Staff staff = staffs.Where(s => s.Id == Model.Staff.Id).SingleOrDefault();
                    Staffs.MoveCurrentTo(staff);
                }

                if (Departments != null)
                {
                    List<Department> departments = (List<Department>)Departments.SourceCollection;
                    Department department = departments.Where(d => d.Id == Model.ResponsibleDepartment.Id).SingleOrDefault();
                    Departments.MoveCurrentTo(department);
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
                Model = new Inps();

                if (Departments != null)
                {
                    Departments.MoveCurrentToFirst();
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
                                           GetAllInpsByPeriodAndType();
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

        public void GetAllInpsByPeriodAndType()
        {
            if (Utility.Period != null && InpsType != null)
            {
                GetAllInpsByPeriodAndTypeCompleted();

                //LoadAllCompleted();
                uploadService.GetAllInpsBy(Utility.Period, InpsType);
            }
        }

        protected void GetAllInpsByPeriodAndTypeCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    GetAllInpsByPeriodCompletedHelper();
                    uploadService.GetInpsByPeriodAndTypeCompleted -= handler;
                };

                uploadService.GetInpsByPeriodAndTypeCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void GetAllInpsByPeriodCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(uploadService.Fault))
                               {
                                   return;
                               }

                               Models = new PagedCollectionView(uploadService.Inpss);
                               Models.MoveCurrentTo(null);
                               Models.CurrentChanged += (s, e) =>
                               {
                                   Model = Models.CurrentItem as Inps;
                                   SetSelectedModel();


                                   //if (InpsType != null && InpsType.Id > 0)
                                   //{
                                   //    GetAllInpsByPeriodAndType();
                                   //}
                               };


                               //Models = uploadService.Inpss;
                               //if (Inpss != null && Inpss.Count > 0)
                               //{
                               //    CanSaveInps = false;
                               //    CanClearInps = false;
                               //}
                               //else
                               //{
                               //    Utility.DisplayMessage("No data exist for " + InpsType.Name);
                               //}

                               //UpdateUiState();
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override void LoadAll()
        {
            try
            {
                GetAllInpsByPeriodAndType();


                //if (InpsType != null && InpsType.Id > 0)
                //{
                //    LoadAllCompleted();
                    
                //    service.LoadAll();
                //}
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        //saveSuccessfulMessage = modelName + " has been added successfully";
        //saveFailedMessage = modelName + " creation failed! Please try again";
        //modifySuccessfulMessage = modelName + " has been modified successfully";
        //modifyFailedMessage = modelName + " modification failed! Please try again";
        //removeSuccessfulMessage = modelName + " has been removed successfully";
        //removeFailedMessage = modelName + " removal failed! Please try again";

        


       





    }
}
