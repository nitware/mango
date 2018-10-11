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
using Microsoft.Practices.Prism.Events;
using Mango.Setup.Interfaces;
using Mango.Infrastructure.ViewModelBase;
using System.ComponentModel;
using Mango.Infrastructure.Models;
using Mango.Infrastructure.Interfaces;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using Mango.Setup.Services;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using Mango.Infrastructure.Events;

namespace Mango.Setup.ViewModels
{
    public class JobRoleHodViewModel : CollectionViewModelBase<JobRoleHod>
    {
        private bool canRemoveAllJobRoleEntry;
        private ICollectionView hodCompanyDepartmentJobRoles;
        private ICollectionView staffCompanyDepartmentJobRoles;
        private CompanyDepartmentJobRole hodCompanyDepartmentJobRole;
        private CompanyDepartmentJobRole staffCompanyDepartmentJobRole;

        private ISetupService<CompanyDepartmentJobRole> companyDepartmentJobRoleService;
        private JobRoleHodService jobRoleHodService;

        public JobRoleHodViewModel(IMetricBaseService<JobRoleHod> _service, ISetupService<CompanyDepartmentJobRole> _companyDepartmentJobRoleService, IEventAggregator _eventAggregator)
            : base(_service)
        {
            jobRoleHodService = new JobRoleHodService();
            companyDepartmentJobRoleService = _companyDepartmentJobRoleService;

            ModelName = "Roles Under HOD";
            RemoveAllCommand = new DelegateCommand(OnRemoveAllCommand, CanRemoveAll);

            //LoadAllCompanyDepartmentJobRoleCompleted();
            //companyDepartmentJobRoleService.LoadAll();

            //LoadAllJobRolesByPeriod();

            //eventAggregator = _eventAggregator;
            //_eventAggregator.GetEvent<SetupEvent>().Subscribe(OnInitialise);

            OnInitialise("");
        }

        public void OnInitialise(string refresh)
        {
            LoadAllCompanyDepartmentJobRoleCompleted();
            companyDepartmentJobRoleService.LoadAll();

            LoadAllJobRolesByPeriod();
        }

        private void LoadAllJobRolesByPeriod()
        {
            LoadAllJobRoleHodsByPeriodCompleted();
            jobRoleHodService.LoadByPeriod(Utility.Period);
        }

        private ObservableCollection<JobRoleHod> JobRoleHods { get; set; }
        public DelegateCommand RemoveAllCommand { get; private set; }

        public string TabCaption
        {
            get { return ModelName; }
        }
       
        public ICollectionView HodCompanyDepartmentJobRoles
        {
            get { return hodCompanyDepartmentJobRoles; }
            set
            {
                hodCompanyDepartmentJobRoles = value;
                base.OnPropertyChanged("HodCompanyDepartmentJobRoles");
            }
        }
        public CompanyDepartmentJobRole HodCompanyDepartmentJobRole
        {
            get { return hodCompanyDepartmentJobRole; }
            set
            {
                hodCompanyDepartmentJobRole = value;
                base.OnPropertyChanged("HodCompanyDepartmentJobRole");
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
                ObservableCollection<JobRoleHod> models = (ObservableCollection<JobRoleHod>)TargetCollection.SourceCollection;
                if (collectionManager.Collection == null || collectionManager.Collection.Count == 0)
                {
                    MessageBoxResult response = MessageBox.Show("This action will permanently remove all Job Roles associated with " + HodCompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + ". Do you want to continue?", "JOB ROLE", MessageBoxButton.OKCancel);
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

                if (HodCompanyDepartmentJobRole != null)
                {
                    RemoveAllCompleted();
                    jobRoleHodService.RemoveByHodJobRole(HodCompanyDepartmentJobRole, Utility.Period);
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

            HodCompanyDepartmentJobRoles.MoveCurrentToPosition(0);
            StaffCompanyDepartmentJobRoles.MoveCurrentToPosition(0);
            TargetModel = new JobRoleHod();

            UpdateViewState(Edit.Mode.Loaded);
        }

        protected override bool IsRequiredDetailsEntered()
        {
            try
            {
                if (HodCompanyDepartmentJobRole == null || HodCompanyDepartmentJobRole.Id == 0)
                {
                    Utility.DisplayMessage("Please select Hod Job Role!");
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
                if (JobRoleHods != null && JobRoleHods.Count > 0)
                {
                    JobRoleHod staffJobRole = JobRoleHods.Where(j => j.StaffCompanyDepartmentJobRole.Id == StaffCompanyDepartmentJobRole.Id).SingleOrDefault();
                    if (staffJobRole != null)
                    {
                        Utility.DisplayMessage("Staff Job Role '" + staffJobRole.StaffCompanyDepartmentJobRole.JobRole.Name + "' already exist under HOD '" + staffJobRole.HodCompanyDepartmentJobRole.JobRole.Name + "' for " + Utility.Period.Name + ". Kindly remove from the current Hod to re-assign");
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
                    foreach (JobRoleHod jobRoleHod in TargetCollection)
                    {
                        if (jobRoleHod.StaffCompanyDepartmentJobRole.Id == StaffCompanyDepartmentJobRole.Id)
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
                    foreach (JobRoleHod jobRoleHod in TargetCollection)
                    {
                        if (jobRoleHod.StaffCompanyDepartmentJobRole.Id == StaffCompanyDepartmentJobRole.Id)
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
                ObservableCollection<JobRoleHod> models = (ObservableCollection<JobRoleHod>)TargetCollection.SourceCollection;
                if (collectionManager.Collection != null && collectionManager.Collection.Count > 0)
                {
                    MessageBoxResult response = MessageBox.Show("You are about to remove all Job Roles associated with " + HodCompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + ". Are you sure you want to do this?", "JOB ROLE", MessageBoxButton.OKCancel);
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
        protected override JobRoleHod GetNewModel()
        {
            try
            {
                JobRoleHod jobRoleHod = new JobRoleHod();
                jobRoleHod.HodCompanyDepartmentJobRole = new CompanyDepartmentJobRole();
                jobRoleHod.StaffCompanyDepartmentJobRole = new CompanyDepartmentJobRole();
                jobRoleHod.Period = Utility.Period;

                jobRoleHod.HodCompanyDepartmentJobRole = HodCompanyDepartmentJobRole;
                jobRoleHod.StaffCompanyDepartmentJobRole = StaffCompanyDepartmentJobRole;

                return jobRoleHod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void LoadAllJobRoleHodsByPeriodCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllJobRoleHodsByPeriodCompletedHelper();
                    jobRoleHodService.GetAllHodJobRolesByPeriodCompleted -= handler;
                };

                jobRoleHodService.GetAllHodJobRolesByPeriodCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void LoadAllJobRoleHodsByPeriodCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(jobRoleHodService.Fault))
                               {
                                   return;
                               }

                               JobRoleHods = jobRoleHodService.JobRoleHods;
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
                                   List<CompanyDepartmentJobRole> hodCompanyDepartmentJobRoles = companyDepartmentJobRoleService.Models.Where(p => p.Id != 0).ToList();
                                   List<CompanyDepartmentJobRole> staffCompanyDepartmentJobRoles = companyDepartmentJobRoleService.Models.Where(p => p.Id != 0).ToList();
                                   if (hodCompanyDepartmentJobRoles.Count > 0)
                                   {
                                       //hodCompanyDepartmentJobRoles.Insert(0, new CompanyDepartmentJobRole() { Company = new Company() { Symbol = "<< Select Hod Job Role >>" } });
                                       hodCompanyDepartmentJobRoles.Insert(0, new CompanyDepartmentJobRole() { JobRole = new JobRole() { Name = "<< Select Hod Job Role >>" } });
                                   }

                                   if (staffCompanyDepartmentJobRoles.Count > 0)
                                   {
                                       //staffCompanyDepartmentJobRoles.Insert(0, new CompanyDepartmentJobRole() { Company = new Company() { Symbol = "<< Select Staff Job Role >>" } });
                                       staffCompanyDepartmentJobRoles.Insert(0, new CompanyDepartmentJobRole() { JobRole = new JobRole() { Name = "<< Select Staff Job Role >>" } });
                                   }

                                   HodCompanyDepartmentJobRoles = new PagedCollectionView(hodCompanyDepartmentJobRoles);
                                   HodCompanyDepartmentJobRoles.CurrentChanged += (s, e) =>
                                   {
                                       HodCompanyDepartmentJobRole = HodCompanyDepartmentJobRoles.CurrentItem as CompanyDepartmentJobRole;
                                       if (HodCompanyDepartmentJobRole != null && Utility.Period != null)
                                       {
                                           LoadModelsByHodCompanyDepartmentJobRoleCompleted();
                                           jobRoleHodService.LoadJobRolesUnderHodByPeriod(HodCompanyDepartmentJobRole, Utility.Period);
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

        protected void LoadModelsByHodCompanyDepartmentJobRoleCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadModelsByHodCompanyDepartmentJobRoleCompletedHelper();
                    jobRoleHodService.GetModelsCompleted -= handler;
                };

                jobRoleHodService.GetModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadModelsByHodCompanyDepartmentJobRoleCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(jobRoleHodService.Fault))
                               {
                                   return;
                               }

                               SetCurrentTargetCollection(jobRoleHodService.Models);
                               collectionManager.Collection = jobRoleHodService.Models;

                               if (jobRoleHodService.Models != null)
                               {
                                   RecordCount = RecordCountLabel + jobRoleHodService.Models.Count;
                                   if (jobRoleHodService.Models.Count > 0)
                                   {
                                       UpdateViewState(Edit.Mode.Editing);
                                       CanRemoveAllJobRoleEntry = true;
                                   }
                                   else
                                   {
                                       UpdateViewState(Edit.Mode.Loaded);
                                   }

                                   IsModelExist(jobRoleHodService.Models);
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
                    jobRoleHodService.ActionCompleted -= handler;
                };

                jobRoleHodService.ActionCompleted += handler;
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
                               if (Utility.FaultExist(jobRoleHodService.Fault))
                               {
                                   return;
                               }

                               if (jobRoleHodService.Done)
                               {
                                   Utility.DisplayMessage("All " + ModelName + " associated with " + HodCompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + " have been successfully removed");
                                   LoadAllJobRolesByPeriod();
                                   ClearView();
                               }
                               else
                               {
                                   Utility.DisplayMessage("All " + ModelName + " associated with " + HodCompanyDepartmentJobRole.JobRole.Name + " for " + Utility.Period.Name + " have not been removed! Please try again");
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
                            if (HodCompanyDepartmentJobRole != null)
                            {
                                if (HodCompanyDepartmentJobRole.Id > 0)
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
