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
using System.Windows.Threading;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Linq;
using System.Collections.Generic;
using Mango.Infrastructure.ViewModelBase;
using Mango.Users.Services;
using Mango.Infrastructure.Models;
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Services;

namespace Mango.Staff.ViewModels
{
    public class AssignRoleToPersonViewModel : ViewModelBase
    {
        private ICollectionView staffs;
        private Mango.Infrastructure.MangoService.Staff staff;
        private ICollectionView roles;

        private Role role;

        StaffService staffService;
        RoleService roleService;

        private Dispatcher dispatcher;

        public AssignRoleToPersonViewModel()
        {
            dispatcher = Deployment.Current.Dispatcher;

            SaveCommand = new DelegateCommand(OnSaveCommand);

            staffService = new StaffService();
            LoadAllUsersCompleted();
            staffService.LoadAll(Utility.LoggedInUser);

            roleService = new RoleService();
            LoadRolesCompleted();
            roleService.LoadAll(Utility.LoggedInUser);
        }

        public DelegateCommand SaveCommand { get; private set; }

        public string TabCaption
        {
            get { return "Assign Role To Person"; }
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
        public ICollectionView Roles
        {
            get { return roles; }
            set
            {
                roles = value;
                OnPropertyChanged("Roles");
            }
        }

        public Role Role
        {
            get { return role; }
            set
            {
                role = value;
                OnPropertyChanged("Role");
            }
        }

        private void OnSaveCommand()
        {
            try
            {
                ObservableCollection<Role> roles = (ObservableCollection<Role>)Roles.SourceCollection;
                if (roles != null)
                {
                    Role role = roles.Where(r => r.HasUser == true).SingleOrDefault();
                    if (role != null)
                    {
                        if (role.Name == Staff.Role.Name)
                        {
                            Utility.DisplayMessage("No role changes found for '" + Staff.FullName + "'");
                            return;
                        }

                        Staff.Role = role;
                        AssignRoleToPersonCompleted();
                        staffService.Modify(Staff);
                    }
                }
                else
                {
                    Utility.DisplayMessage("No role(s) found! Contact your system administrator.");
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllUsersCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllUsersCompletedHelper();
                    staffService.GetAllModelsCompleted -= handler;
                };

                staffService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllUsersCompletedHelper()
        {
            dispatcher.BeginInvoke
                       (() =>
                       {
                           if (Utility.FaultExist(staffService.Fault))
                           {
                               return;
                           }

                           Staffs = new PagedCollectionView(staffService.Models);
                           if (staffService.Models != null && staffService.Models.Count > 0)
                           {
                               staffService.Models.Insert(0, new Infrastructure.MangoService.Staff() { Id = "-1", FullName = "<< Select User >>" });

                               Staffs.MoveCurrentToFirst();
                               Staffs.CurrentChanged += (s, e) =>
                               {
                                   Staff = Staffs.CurrentItem as Infrastructure.MangoService.Staff;

                                   if (Roles != null)
                                   {
                                       foreach (Role role in Roles)
                                       {
                                           role.HasUser = role.Id == Staff.Role.Id ? true : false;

                                       }
                                   }
                               };

                           }
                       });
        }

        protected void LoadRolesCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadRolesCompletedHelper();
                    roleService.GetAllModelsCompleted -= handler;
                };

                roleService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadRolesCompletedHelper()
        {
            dispatcher.BeginInvoke
                       (() =>
                       {
                           if (Utility.FaultExist(roleService.Fault))
                           {
                               return;
                           }

                           Roles = new PagedCollectionView(roleService.Models);
                           if (roleService.Models != null)
                           {
                               Roles.MoveCurrentTo(null);
                               Roles.CurrentChanged += (s, e) =>
                               {
                                   Role = Roles.CurrentItem as Role;

                               };
                           }
                       });
        }

        protected void AssignRoleToPersonCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    AssignRoleToPersonCompletedHelper();
                    staffService.ActionCompleted -= handler;
                };

                staffService.ActionCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void AssignRoleToPersonCompletedHelper()
        {
            dispatcher.BeginInvoke
                       (() =>
                       {
                           if (Utility.FaultExist(roleService.Fault))
                           {
                               return;
                           }

                           if (staffService.Done)
                           {
                               Utility.DisplayMessage("Role '" + Staff.Role.Name + "' has been successfully assigned to '" + Staff.FullName + "'");
                           }
                           else
                           {
                               Utility.DisplayMessage("Assignment of Role to Person failed!");
                           }
                       });
        }




    }
}
