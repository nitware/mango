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

using System.Windows.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Linq;

using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Services;
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.Models;
using Mango.Infrastructure.MangoService;
using Mango.Users.Services;

namespace Mango.Staff.ViewModels
{
    public class StaffViewModel : SetupViewModelBase<Infrastructure.MangoService.Staff>
    {
        private ICollectionView users;

        //private RoleService roleService;
        private StaffService staffService;
        private Infrastructure.MangoService.Staff staff;
        private bool canModifyStaffNo;

        public StaffViewModel(ISetupService<Infrastructure.MangoService.Staff> _service)
            : base(_service)
        {
            dispatcher = Deployment.Current.Dispatcher;

            modelName = "Staff";
            IsLoggedInUserHasRight = Utility.LoggedInUser.Role.UserRight.CanSetupUser;

            service = _service;
            staffService = new StaffService();

            Initialize();

            //roleService = new RoleService();
            //LoadRolesCompleted();
            //roleService.LoadAll(Utility.LoggedInUser);

            base.addSelector = l => (l.FirstName.Trim().Equals(Model.FirstName.Trim(), StringComparison.OrdinalIgnoreCase) && l.LastName.Trim().Equals(Model.LastName.Trim(), StringComparison.OrdinalIgnoreCase)) || l.Id == Model.Id;
            base.modifySelector = l => l.FirstName.Trim().Equals(Model.FirstName.Trim(), StringComparison.OrdinalIgnoreCase) && l.LastName.Trim().Equals(Model.LastName.Trim(), StringComparison.OrdinalIgnoreCase) && l.Id != Model.Id;

        }

        public string TabCaption
        {
            get { return modelName; }
        }

        public ICollectionView Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }
        public bool CanModifyStaffNo
        {
            get { return canModifyStaffNo; }
            set
            {
                canModifyStaffNo = value;
                OnPropertyChanged("CanModifyStaffNo");
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
        //public ICollectionView Roles
        //{
        //    get { return roles; }
        //    set
        //    {
        //        roles = value;
        //        OnPropertyChanged("Roles");
        //    }
        //}

        //public Role Role
        //{
        //    get { return role; }
        //    set
        //    {
        //        role = value;
        //        OnPropertyChanged("Role");
        //    }
        //}

        protected override void LoadAll()
        {
            try
            {
                LoadAllCompleted();
                staffService.LoadAll(Utility.LoggedInUser);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        protected override void LoadAllCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllCommandCompletedHelper();
                    staffService.GetAllModelsCompleted -= handler;
                };

                staffService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage("Error occured! " + ex.Message);
            }
        }
        protected override void LoadAllCommandCompletedHelper()
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

                               Models = new PagedCollectionView(staffService.Models);
                               if (Models != null)
                               {
                                   RecordCount = RecordCountLabel + staffService.Models.Count;

                                   Models.MoveCurrentTo(null);
                                   Models.CurrentChanged += (s, e) =>
                                   {
                                       Model = Models.CurrentItem as Infrastructure.MangoService.Staff;
                                       UpdateViewState(Edit.Mode.Editing);
                                       
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        //protected void LoadRolesCompleted()
        //{
        //    EventHandler handler = null;

        //    try
        //    {
        //        handler = (s, e) =>
        //        {
        //            LoadRolesCompletedHelper();
        //            roleService.GetAllModelsCompleted -= handler;
        //        };

        //        roleService.GetAllModelsCompleted += handler;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //protected void LoadRolesCompletedHelper()
        //{
        //    dispatcher.BeginInvoke
        //               (() =>
        //               {
        //                   if (Utility.FaultExist(roleService.Fault))
        //                   {
        //                       return;
        //                   }

        //                   Roles = new PagedCollectionView(roleService.Models);
        //                   if (roleService.Models != null)
        //                   {
        //                       Roles.MoveCurrentTo(null);
        //                       Roles.CurrentChanged += (s, e) =>
        //                       {
        //                           Role = Roles.CurrentItem as Role;

        //                       };
        //                   }
        //               });
        //}

        protected override void OnSaveCommand()
        {
            try
            {
                if (IncorrectDataEntered())
                {
                    return;
                }

                Model.LastName = Model.LastName.Trim().ToUpper();
                Model.FirstName = Model.FirstName.Trim().ToUpper();
                Model.FullName = Model.FirstName + " " + Model.LastName;

                if (base.InvalidEntry(Model.FullName))
                {
                    return;
                }
                if (!string.IsNullOrWhiteSpace(Model.OtherName))
                {
                    Model.OtherName = Model.OtherName.ToUpper();
                }

                Company company = new Company() { Id = 2 };
                Model.Company = company;
                base.OnSaveCommand();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected override void ActionCommandCompletedHelper()
        {
            dispatcher.BeginInvoke
                      (() =>
                      {
                          if (Utility.FaultExist(service.Fault))
                          {
                              return;
                          }

                          if (service.Done)
                          {
                              LoadAll();

                              UpdateViewState(Edit.Mode.Adding);

                              Utility.DisplayMessage("'" + Model.FullName + "' has been added successfully");
                              Model = new Infrastructure.MangoService.Staff();
                          }
                          else
                          {
                              Utility.DisplayMessage("User creation failed! Please try again");
                          }
                      });
        }

        private bool IncorrectDataEntered()
        {
            try
            {
                if (base.InvalidEntry(Model.LastName))
                {
                    return true;
                }

                if (string.IsNullOrWhiteSpace(Model.Id))
                {
                    Utility.DisplayMessage("Please enter staff number!");
                    return true;
                }
                else if (string.IsNullOrWhiteSpace(Model.LastName))
                {
                    Utility.DisplayMessage("Please enter surname!");
                    return true;
                }
                else if (string.IsNullOrWhiteSpace(Model.FirstName))
                {
                    Utility.DisplayMessage("Please enter First Name!");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void UpdateViewState(Edit.Mode viewState)
        {
            try
            {
                base.UpdateViewState(viewState);

                switch (viewState)
                {
                    case Edit.Mode.Adding:
                    case Edit.Mode.Loaded:
                        {
                            CanModifyStaffNo = true;
                            break;
                        }
                    case Edit.Mode.Editing:
                        {
                            CanModifyStaffNo = false;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }


    }




}
