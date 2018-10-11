using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Mango.Setup.ViewModels;

namespace Mango.Setup.Views
{
    public partial class RoleSetupView : UserControl
    {
        public RoleSetupView(RoleSetupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            this.Loaded += (s, e) =>
            {
                int count = tcRoleSetupTab.Items.Count;
                if (count > 0)
                {
                    TabItem rolesUnderSupervisorTab = (TabItem)tcRoleSetupTab.Items[0];
                    rolesUnderSupervisorTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanAssignRolesUnderSupervisor;

                    TabItem rolesUnderHodTab = (TabItem)tcRoleSetupTab.Items[1];
                    rolesUnderHodTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanAssignRolesUnderHod;

                }
            };
        }




    }
}
