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
using System.Windows.Data;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Mango.Infrastructure.ViewModelBase;
using Mango.Users.Interfaces;
using Mango.Infrastructure.Models;
using Mango.Infrastructure.MangoService;
using Mango.Users.Services;

namespace mobak.Users.ViewModels
{
    public class AssignRightToRoleViewModel : ViewModelBase
    {
        private Dispatcher dispatcher;

        private RoleService roleService;
        private IAssignRightToRoleService service;

        private ICollectionView roles;
        private Role role;

        public AssignRightToRoleViewModel(IAssignRightToRoleService _service)
        {
            service = _service;
            roleService = new RoleService();
            dispatcher = Deployment.Current.Dispatcher;

            SaveCommand = new DelegateCommand(OnSaveCommand);

            LoadRolesCompleted();
            roleService.LoadAll(Utility.LoggedInUser);
        }

        public DelegateCommand SaveCommand { get; private set; }

        public string TabCaption
        {
            get { return "Assign Right To Role"; }
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
                if (EntryIsInvalid())
                {
                    return;
                }

                AssignRightToRoleCompleted();
                service.AssignRightToRole(Role);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private bool EntryIsInvalid()
        {
            try
            {
                if (Role == null || Role.Id == -1)
                {
                    Utility.DisplayMessage("No role selected! Please select role");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                               roleService.Models.Insert(0, new Role() { Id = -1, Name = "<< Select Role >>" });

                               Roles.MoveCurrentToFirst();
                               Roles.CurrentChanged += (s, e) =>
                               {
                                   Role = Roles.CurrentItem as Role;
                               };
                           }
                       });
        }

        protected void AssignRightToRoleCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    AssignRightToRoleCompletedHelper();
                    service.ActionCompleted -= handler;
                };

                service.ActionCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void AssignRightToRoleCompletedHelper()
        {
            dispatcher.BeginInvoke
                       (() =>
                       {
                           if (Utility.FaultExist(roleService.Fault))
                           {
                               return;
                           }

                           if (service.Done)
                           {
                               Utility.DisplayMessage("Right(s) has been successfully assigned to Role ( " + Role.Name + " )");
                           }
                           else
                           {
                               Utility.DisplayMessage("Assignment of Right(s) to Role failed!");
                           }
                       });
        }



    }



}
