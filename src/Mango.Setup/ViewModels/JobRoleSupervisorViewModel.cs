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

using System.ComponentModel;
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Interfaces;
using Mango.Setup.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Data;
using Mango.Infrastructure.Events;

namespace Mango.Setup.ViewModels
{
    public class JobRoleSupervisorViewModel : CollectionViewModelBase<JobRoleSupervisor>
    {
       private bool canRemoveAllJobRoleEntry;
        private ICollectionView supervisorCompanyDepartmentJobRoles;
        private ICollectionView staffCompanyDepartmentJobRoles;
        private CompanyDepartmentJobRole supervisorCompanyDepartmentJobRole;
        private CompanyDepartmentJobRole staffCompanyDepartmentJobRole;

        private ISetupService<CompanyDepartmentJobRole> companyDepartmentJobRoleService;
        private JobRoleSupervisorService jobRoleSupervisorService;

        public JobRoleSupervisorViewModel(IMetricBaseService<JobRoleSupervisor> _service, ISetupService<CompanyDepartmentJobRole> _companyDepartmentJobRoleService, IEventAggregator _eventAggregator)
            : base(_service)
        {
            jobRoleSupervisorService = new JobRoleSupervisorService();
            companyDepartmentJobRoleService = _companyDepartmentJobRoleService;

            ModelName = "Roles Under Supervisor";
            RemoveAllCommand = new DelegateCommand(OnRemoveAllCommand, CanRemoveAll);

            //LoadAllCompanyDepartmentJobRoleCompleted();
            //companyDepartmentJobRoleService.LoadAll();

            //LoadAllJobRolesByPeriod();

            //_eventAggregator.GetEvent<SetupEvent>().Subscribe(OnInitialise);

            OnInitialise("");
        }

        private void LoadAllJobRolesByPeriod()
        {
            LoadAllJobRoleSupervisorsByPeriodCompleted();
            jobRoleSupervisorService.LoadByPeriod(Utility.Period);
        }

        private ObservableCollection<JobRoleSupervisor> JobRoleSupervisors { get; set; }
        public DelegateCommand RemoveAllCommand { get; private set; }

        public string TabCaption
        {
            get { return ModelName; }
        }

        public void OnInitialise(string refresh)
        {
            LoadAllCompanyDepartmentJobRoleCompleted();
            companyDepartmentJobRoleService.LoadAll();

            LoadAllJobRolesByPeriod();
        }

        public ICollectionView SupervisorCompanyDepartmentJobRoles
        {
            get { return supervisorCompanyDepartmentJobRoles; }
            set
            {
                supervisorCompanyDepartmentJobRoles = value;
                base.OnPropertyChanged("SupervisorCompanyDepartmentJobRoles");
            }
        }
        public CompanyDepartmentJobRole SupervisorCompanyDepartmentJobRole
        {
            get { return supervisorCompanyDepartmentJobRole; }
            set
            {
                supervisorCompanyDepartmentJobRole = value;
                base.OnPropertyChanged("SupervisorCompanyDepartmentJobRole");
            }
        }

        public ICollectionView StaffCompanyDepartmentJobRoles
        {
            get { return staffCompanyDepartmentJobRoles; }
            set
            {
                staffCompanyDepartmentJobRoles = value;
                base.OnPropertyChanged("StaffCompanyDepartmentJobRoles");
            }
        }
        public CompanyDepartmentJobRole StaffCompanyDepartmentJobRole
        {
            get { return staffCompanyDepartmentJobRole; }
            set
            {
                staffCompanyDepartmentJobRole = value;
                base.OnPropertyChanged("StaffCompanyDepartmentJobRole");
            }
        }
        public bool CanRemoveAllJobRoleEntry
        {
            get { return canRemoveAllJobRoleEntry; }
            set
            {
                canRemoveAllJobRoleEntry = value;
                RemoveAllCommand.RaiseCanExecuteChanged();
            }
        }
        private bool CanRemoveAll()
        {
            return CanRemoveAllJobRoleEntry;
        }
        protected override void OnSaveCommand()
        {
            try
            {
                ObservableCollection<JobRoleSupervisor> models = (ObservableCollection<JobRoleSupervisor>)TargetCollection.SourceCollection;
                if (collectionManager.Collection == null || collectionManager.Collection.Count == 0)
                {
                    MessageBoxResult response = MessageBox.Show("This action will permanently remove all Job Roles associated with " + SupervisorCompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + ". Do you want to continue?", "JOB ROLE", MessageBoxButton.OKCancel);
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

                if (SupervisorCompanyDepartmentJobRole != null)
                {
                    RemoveAllCompleted();
                    jobRoleSupervisorService.RemoveBySupervisorJobRole(SupervisorCompanyDepartmentJobRole, Utility.Period);
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        protected override void SuccessfullActionHelper()
        {
            try
            {
                Utility.DisplayMessage(ModelName + " have been successfully saved");
                LoadAllJobRolesByPeriod();
                ClearView();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override void ClearView()
        {
            base.ClearTargetCollection();

            SupervisorCompanyDepartmentJobRoles.MoveCurrentToPosition(0);
            StaffCompanyDepartmentJobRoles.MoveCurrentToPosition(0);
            TargetModel = new JobRoleSupervisor();

            UpdateViewState(Edit.Mode.Loaded);
        }

        protected override bool IsRequiredDetailsEntered()
        {
            try
            {
                if (SupervisorCompanyDepartmentJobRole == null || SupervisorCompanyDepartmentJobRole.Id == 0)
                {
                    Utility.DisplayMessage("Please select Supervisor Job Role!");
                    return false;
                }
                else if (StaffCompanyDepartmentJobRole == null || StaffCompanyDepartmentJobRole.Id <= 0)
                {
                    Utility.DisplayMessage("Please select Staff Job Role!");
                    return false;
                }
                else if (StaffJobRoleAlreadyExist())
                {
                    return false;
                }
                else if (DuplicateModelExist())
                {
                    return false;
                }
                //else if (ModelAlreadyExist())
                //{
                //    return false;
                //}

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool StaffJobRoleAlreadyExist()
        {
            try
            {
                if (JobRoleSupervisors != null && JobRoleSupervisors.Count > 0)
                {
                    JobRoleSupervisor staffJobRole = JobRoleSupervisors.Where(j => j.StaffCompanyDepartmentJobRole.Id == StaffCompanyDepartmentJobRole.Id).SingleOrDefault();
                    if (staffJobRole != null)
                    {
                        Utility.DisplayMessage("Staff Job Role '" + staffJobRole.StaffCompanyDepartmentJobRole.JobRole.Name + "' already exist under '" + staffJobRole.SupervisorCompanyDepartmentJobRole.JobRole.Name + "' for " + Utility.Period.Name + ". Kindly remove from the current Supervisor to re-assign");
                        return true;
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
                    foreach (JobRoleSupervisor jobRoleSupervisor in TargetCollection)
                    {
                        if (jobRoleSupervisor.StaffCompanyDepartmentJobRole.Id == StaffCompanyDepartmentJobRole.Id)
                        {
                            Utility.DisplayMessage(StaffCompanyDepartmentJobRole.JobRole.Name + " already exist! ");
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

        protected bool DuplicateModelExist()
        {
            try
            {
                if (TargetCollection != null)
                {
                    foreach (JobRoleSupervisor jobRoleSupervisor in TargetCollection)
                    {
                        if (jobRoleSupervisor.StaffCompanyDepartmentJobRole.Id == StaffCompanyDepartmentJobRole.Id)
                        {
                            Utility.DisplayMessage(StaffCompanyDepartmentJobRole.JobRole.Name + "' already exist on the list! Please modify to continue.");
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
        protected void OnRemoveAllCommand()
        {
            try
            {
                ObservableCollection<JobRoleSupervisor> models = (ObservableCollection<JobRoleSupervisor>)TargetCollection.SourceCollection;
                if (collectionManager.Collection != null && collectionManager.Collection.Count > 0)
                {
                    MessageBoxResult response = MessageBox.Show("You are about to remove all Job Roles associated with " + SupervisorCompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + ". Are you sure you want to do this?", "JOB ROLE", MessageBoxButton.OKCancel);
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
        protected override JobRoleSupervisor GetNewModel()
        {
            try
            {
                JobRoleSupervisor jobRoleSupervisor = new JobRoleSupervisor();
                jobRoleSupervisor.SupervisorCompanyDepartmentJobRole = new CompanyDepartmentJobRole();
                jobRoleSupervisor.StaffCompanyDepartmentJobRole = new CompanyDepartmentJobRole();
                jobRoleSupervisor.Period = Utility.Period;

                jobRoleSupervisor.SupervisorCompanyDepartmentJobRole = SupervisorCompanyDepartmentJobRole;
                jobRoleSupervisor.StaffCompanyDepartmentJobRole = StaffCompanyDepartmentJobRole;

                return jobRoleSupervisor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void LoadAllJobRoleSupervisorsByPeriodCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllJobRoleSupervisorsByPeriodCompletedHelper();
                    jobRoleSupervisorService.GetAllSupervisorJobRolesByPeriodCompleted -= handler;
                };

                jobRoleSupervisorService.GetAllSupervisorJobRolesByPeriodCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void LoadAllJobRoleSupervisorsByPeriodCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(jobRoleSupervisorService.Fault))
                               {
                                   return;
                               }

                               JobRoleSupervisors = jobRoleSupervisorService.JobRoleSupervisors;
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllCompanyDepartmentJobRoleCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllCompanyDepartmentJobRoleCompletedHelper();
                    companyDepartmentJobRoleService.GetAllModelsCompleted -= handler;
                };

                companyDepartmentJobRoleService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllCompanyDepartmentJobRoleCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(companyDepartmentJobRoleService.Fault))
                               {
                                   return;
                               }

                               if (companyDepartmentJobRoleService.Models != null)
                               {
                                   List<CompanyDepartmentJobRole> supervisorCompanyDepartmentJobRoles = companyDepartmentJobRoleService.Models.Where(p => p.Id != 0).ToList();
                                   List<CompanyDepartmentJobRole> staffCompanyDepartmentJobRoles = companyDepartmentJobRoleService.Models.Where(p => p.Id != 0).ToList();
                                   if (supervisorCompanyDepartmentJobRoles.Count > 0)
                                   {
                                       //supervisorCompanyDepartmentJobRoles.Insert(0, new CompanyDepartmentJobRole() { Company = new Company() { Symbol = "<< Select Supervisor Job Role >>" } });
                                       supervisorCompanyDepartmentJobRoles.Insert(0, new CompanyDepartmentJobRole() { JobRole = new JobRole() { Name = "<< Select Supervisor Job Role >>" } });

                                   }

                                   if (staffCompanyDepartmentJobRoles.Count > 0)
                                   {
                                       //staffCompanyDepartmentJobRoles.Insert(0, new CompanyDepartmentJobRole() { Company = new Company() { Symbol = "<< Select Staff Job Role >>" } });
                                       staffCompanyDepartmentJobRoles.Insert(0, new CompanyDepartmentJobRole() { JobRole = new JobRole() { Name = "<< Select Staff Job Role >>" } });
                                   }

                                   SupervisorCompanyDepartmentJobRoles = new PagedCollectionView(supervisorCompanyDepartmentJobRoles);
                                   SupervisorCompanyDepartmentJobRoles.CurrentChanged += (s, e) =>
                                   {
                                       SupervisorCompanyDepartmentJobRole = SupervisorCompanyDepartmentJobRoles.CurrentItem as CompanyDepartmentJobRole;
                                       if (SupervisorCompanyDepartmentJobRole != null && Utility.Period != null)
                                       {
                                           LoadModelsBySupervisorCompanyDepartmentJobRoleCompleted();
                                           jobRoleSupervisorService.LoadJobRolesUnderSupervisorByPeriod(SupervisorCompanyDepartmentJobRole, Utility.Period);
                                       }
                                   };

                                   StaffCompanyDepartmentJobRoles = new PagedCollectionView(staffCompanyDepartmentJobRoles);
                                   StaffCompanyDepartmentJobRoles.CurrentChanged += (s, e) =>
                                   {
                                       StaffCompanyDepartmentJobRole = StaffCompanyDepartmentJobRoles.CurrentItem as CompanyDepartmentJobRole;
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadModelsBySupervisorCompanyDepartmentJobRoleCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadModelsBySupervisorCompanyDepartmentJobRoleCompletedHelper();
                    jobRoleSupervisorService.GetModelsCompleted -= handler;
                };

                jobRoleSupervisorService.GetModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadModelsBySupervisorCompanyDepartmentJobRoleCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(jobRoleSupervisorService.Fault))
                               {
                                   return;
                               }

                               SetCurrentTargetCollection(jobRoleSupervisorService.Models);
                               collectionManager.Collection = jobRoleSupervisorService.Models;

                               if (jobRoleSupervisorService.Models != null)
                               {
                                   RecordCount = RecordCountLabel + jobRoleSupervisorService.Models.Count;
                                   if (jobRoleSupervisorService.Models.Count > 0)
                                   {
                                       UpdateViewState(Edit.Mode.Editing);
                                       CanRemoveAllJobRoleEntry = true;
                                   }
                                   else
                                   {
                                       UpdateViewState(Edit.Mode.Loaded);
                                   }

                                   IsModelExist(jobRoleSupervisorService.Models);
                               }
                           });
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
                    jobRoleSupervisorService.ActionCompleted -= handler;
                };

                jobRoleSupervisorService.ActionCompleted += handler;
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
                               if (Utility.FaultExist(jobRoleSupervisorService.Fault))
                               {
                                   return;
                               }

                               if (jobRoleSupervisorService.Done)
                               {
                                   Utility.DisplayMessage("All " + ModelName + " associated with " + SupervisorCompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + " have been successfully removed");
                                   LoadAllJobRolesByPeriod();
                                   ClearView();
                               }
                               else
                               {
                                   Utility.DisplayMessage("All " + ModelName + " associated with " + SupervisorCompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + " have not been removed! Please try again");
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
                            if (SupervisorCompanyDepartmentJobRole != null)
                            {
                                if (SupervisorCompanyDepartmentJobRole.Id > 0)
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
                                CanRemoveAllJobRoleEntry = false;
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
                        CanRemoveAllJobRoleEntry = false;
                    }
                    else
                    {
                        CanRemoveAllJobRoleEntry = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }


}
