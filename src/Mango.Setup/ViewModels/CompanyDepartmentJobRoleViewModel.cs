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
using Mango.Setup.Services;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Linq;
using Mango.Setup.Interfaces;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.Events;

namespace Mango.Setup.ViewModels
{
    public class CompanyDepartmentJobRoleViewModel : SetupViewModelBase<CompanyDepartmentJobRole>
    {
        private IEventAggregator eventAggregator;

        private ISetupService<Company> companyService;
        private DepartmentService departmentService;
        private JobRoleService jobRoleService;
        private ICompanyDepartmentService companyDepartmentService;
        private CompanyDepartmentJobRoleService companyDepartmentJobRoleService;

        private Company company;
        private ICollectionView companies;
        private ICollectionView jobRoles;
        private JobRole jobRole;

        private CompanyDepartment companyDepartment;
        private ICollectionView companyDepartments;

        public CompanyDepartmentJobRoleViewModel(ISetupService<CompanyDepartmentJobRole> _service, IEventAggregator _eventAggregator)
            : base(new CompanyDepartmentJobRoleService())
        {
            modelName = "Coy Dept Job Role";
            Initialize();

            eventAggregator = _eventAggregator;
            companyService = new CompanyService();
            departmentService = new DepartmentService();
            jobRoleService = new JobRoleService();
            companyDepartmentService = new CompanyDepartmentService();
            companyDepartmentJobRoleService = new CompanyDepartmentJobRoleService();

            //LoadAllCompaniesCompleted();
            //companyService.LoadAll();

            //LoadAllJobRolesCompleted();
            //jobRoleService.LoadAll();

            base.addSelector = cdjr => cdjr.Company.Id == Model.Company.Id && cdjr.Department.Id == Model.Department.Id && cdjr.JobRole.Id == Model.JobRole.Id;
            base.modifySelector = cdjr => cdjr.Company.Id == Model.Company.Id && cdjr.Department.Id == Model.Department.Id && cdjr.JobRole.Id == Model.JobRole.Id && cdjr.Id != Model.Id;
            
            //eventAggregator.GetEvent<SetupEvent>().Subscribe(OnInitialise);

            OnInitialise("");
        }

        public void OnInitialise(string refresh)
        {
            LoadAll();

            LoadAllCompaniesCompleted();
            companyService.LoadAll();

            LoadAllJobRolesCompleted();
            jobRoleService.LoadAll();
        }
        
        public string TabCaption
        {
            get { return modelName; }
        }
        public ICollectionView Companies
        {
            get { return companies; }
            set
            {
                companies = value;
                base.OnPropertyChanged("Companies");
            }
        }
        public Company Company
        {
            get { return company; }
            set
            {
                company = value;
                base.OnPropertyChanged("Company");
            }
        }
        //public ICollectionView Departments
        //{
        //    get { return departments; }
        //    set
        //    {
        //        departments = value;
        //        base.OnPropertyChanged("Departments");
        //    }
        //}
        //public Department Department
        //{
        //    get { return department; }
        //    set
        //    {
        //        department = value;
        //        base.OnPropertyChanged("Department");
        //    }
        //}
        public ICollectionView JobRoles
        {
            get { return jobRoles; }
            set
            {
                jobRoles = value;
                base.OnPropertyChanged("JobRoles");
            }
        }
        public JobRole JobRole
        {
            get { return jobRole; }
            set
            {
                jobRole = value;
                base.OnPropertyChanged("JobRole");
            }
        }
        
        public ICollectionView CompanyDepartments
        {
            get { return companyDepartments; }
            set
            {
                companyDepartments = value;
                base.OnPropertyChanged("CompanyDepartments");
            }
        }
        public CompanyDepartment CompanyDepartment
        {
            get { return companyDepartment; }
            set
            {
                companyDepartment = value;
                base.OnPropertyChanged("CompanyDepartment");
            }
        }


        protected override void OnClearCommand()
        {
            try
            {
                UpdateViewState(Edit.Mode.Adding);
                Model = new CompanyDepartmentJobRole();

                if (Companies != null)
                {
                    Companies.MoveCurrentToFirst();
                }
                if (CompanyDepartments != null)
                {
                    CompanyDepartments.MoveCurrentToFirst();
                }
                if (JobRoles != null)
                {
                    JobRoles.MoveCurrentToFirst();
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
                if (IncompleteEntry())
                {
                    return;
                }

                Model.Company = Company;
                Model.Department = CompanyDepartment.Department;
                Model.JobRole = JobRole;

                if (base.InvalidEntry(Model.JobRole.Name))
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
        private bool IncompleteEntry()
        {
            try
            {
                if (Company == null)
                {
                    Utility.DisplayMessage("Please select company!");
                    return true;
                }
                else if (CompanyDepartment == null)
                {
                    Utility.DisplayMessage("Please select department!");
                    return true;
                }
                else if (JobRole == null)
                {
                    Utility.DisplayMessage("Please select job role!");
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void LoadAllCompaniesCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllCompaniesCompletedHelper();
                    companyService.GetAllModelsCompleted -= handler;
                };

                companyService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage("Error occured! " + ex.Message);
            }
        }
        protected void LoadAllCompaniesCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(companyService.Fault))
                               {
                                   return;
                               }

                               if (companyService.Models != null && companyService.Models.Count > 0)
                               {
                                   Company = companyService.Models.Where(c => c.Id == 2).SingleOrDefault();
                                   if (Company != null)
                                   {
                                       Companies = new PagedCollectionView(companyService.Models);
                                       Companies.MoveCurrentTo(Company);

                                       LoadAllCompanyDepartmentsByCompanyCompleted();
                                       companyDepartmentService.LoadBy(Company);
                                   }
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        //protected void LoadAllDepartmentsCompleted()
        //{
        //    EventHandler handler = null;

        //    try
        //    {
        //        handler = (s, e) =>
        //        {
        //            LoadAllDepartmentsCompletedHelper();
        //            departmentService.GetAllModelsCompleted -= handler;
        //        };

        //        departmentService.GetAllModelsCompleted += handler;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}
        //protected void LoadAllDepartmentsCompletedHelper()
        //{
        //    try
        //    {
        //        dispatcher.BeginInvoke
        //                   (() =>
        //                   {
        //                       if (Utility.FaultExist(departmentService.Fault))
        //                       {
        //                           return;
        //                       }

        //                       if (departmentService.Models != null && departmentService.Models.Count > 0)
        //                       {
        //                           departmentService.Models.Insert(0, new Department() { Id = "0", Name = "<< Select Department >>" });

        //                           Departments = new PagedCollectionView(departmentService.Models);
        //                           Departments.MoveCurrentToFirst();
        //                           Departments.CurrentChanged += (s, e) =>
        //                           {
        //                               Department = Departments.CurrentItem as Department;
        //                           };
        //                       }
        //                   });
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}
        protected void LoadAllJobRolesCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllJobRolesCompletedHelper();
                    jobRoleService.GetAllModelsCompleted -= handler;
                };

                jobRoleService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        protected void LoadAllJobRolesCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(jobRoleService.Fault))
                               {
                                   return;
                               }

                               if (jobRoleService.Models != null && jobRoleService.Models.Count > 0)
                               {
                                   jobRoleService.Models.Insert(0, new JobRole() { Id = 0, Name = "<< Select Job Role >>" });

                                   JobRoles = new PagedCollectionView(jobRoleService.Models);
                                   JobRoles.MoveCurrentToFirst();
                                   JobRoles.CurrentChanged += (s, e) =>
                                   {
                                       JobRole = JobRoles.CurrentItem as JobRole;
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllCompanyDepartmentsByCompanyCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllCompanyDepartmentsByCompanyCompletedHelper();
                    companyDepartmentService.GetCompanyDepartmentsByCompanyCompleted -= handler;
                };

                companyDepartmentService.GetCompanyDepartmentsByCompanyCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        protected void LoadAllCompanyDepartmentsByCompanyCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(companyDepartmentService.Fault))
                               {
                                   return;
                               }

                               if (companyDepartmentService.CompanyDepartments != null && companyDepartmentService.CompanyDepartments.Count > 0)
                               {
                                   companyDepartmentService.CompanyDepartments.Insert(0, new CompanyDepartment() { Department = new Department() { Id = "0", Name = "<< Select Department >>" } });

                                   CompanyDepartments = new PagedCollectionView(companyDepartmentService.CompanyDepartments);
                                   CompanyDepartments.MoveCurrentToFirst();
                                   CompanyDepartments.CurrentChanged += (s, e) =>
                                   {
                                       CompanyDepartment = CompanyDepartments.CurrentItem as CompanyDepartment;
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        //protected override void LoadAllCommandCompletedHelper()
        //{
        //    try
        //    {
        //        dispatcher.BeginInvoke
        //                   (() =>
        //                   {
        //                       if (Utility.FaultExist(companyDepartmentJobRoleService.Fault))
        //                       {
        //                           return;
        //                       }

        //                       Models = new PagedCollectionView(companyDepartmentJobRoleService.Models);
        //                       if (companyDepartmentJobRoleService.Models != null && companyDepartmentJobRoleService.Models.Count > 0)
        //                       {
        //                           RecordCount = RecordCountLabel + companyDepartmentJobRoleService.Models.Count;
        //                           Models.MoveCurrentTo(null);
        //                           Models.CurrentChanged += (s, e) =>
        //                           {
        //                               SetSelectedModel();
        //                           };
        //                       }
        //                       else
        //                       {
        //                           RecordCount = RecordCountLabel + 0;
        //                       }
        //                   });
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.DisplayMessage(ex.Message);
        //    }
        //}


        protected override void SetSelectedModel()
        {
            try
            {
                if (Models != null)
                {
                    Model = Models.CurrentItem as CompanyDepartmentJobRole;

                    if (Model != null)
                    {
                        //if (Companies != null)
                        //{
                        //    ObservableCollection<Company> companies = (ObservableCollection<Company>)Companies.SourceCollection;
                        //    Company company = companies.Where(c => c.Id == Model.Company.Id).SingleOrDefault();
                        //    if (company != null)
                        //    {
                        //        Companies.MoveCurrentTo(company);
                        //    }
                        //}

                        if (CompanyDepartments != null)
                        {
                            ObservableCollection<CompanyDepartment> departments = (ObservableCollection<CompanyDepartment>)CompanyDepartments.SourceCollection;
                            CompanyDepartment department = departments.Where(d => d.Department.Id == Model.Department.Id && d.Company.Id == Model.Company.Id).SingleOrDefault();
                            if (department != null)
                            {
                                CompanyDepartments.MoveCurrentTo(department);
                            }
                        }

                        if (JobRoles != null)
                        {
                            ObservableCollection<JobRole> jobRoles = (ObservableCollection<JobRole>)JobRoles.SourceCollection;
                            JobRole jobRole = jobRoles.Where(d => d.Id == Model.JobRole.Id).SingleOrDefault();
                            if (jobRole != null)
                            {
                                JobRoles.MoveCurrentTo(jobRole);
                            }
                        }
                    }

                    UpdateViewState(Edit.Mode.Editing);
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }




    }





}