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

using Mango.Staff.ViewModels;

namespace Mango.Staff.Views
{
    public partial class AccessControlView : UserControl
    {
        public AccessControlView(AccessControlViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            
            this.Loaded += (s, e) =>
            {
                int count = tcUserTab.Items.Count;
                if (count > 0)
                {
                    //TabItem userTab = (TabItem)tcUserTab.Items[0];
                    //userTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupUser;

                    //TabItem roleTab = (TabItem)tcUserTab.Items[1];
                    //roleTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupRole;

                    //TabItem rightTab = (TabItem)tcUserTab.Items[2];
                    //rightTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupRight;

                    //TabItem assignRightToRoleTab = (TabItem)tcUserTab.Items[3];
                    //assignRightToRoleTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanAssignRightToRole;

                    //TabItem assignRoleToUserTab = (TabItem)tcUserTab.Items[4];
                    //assignRoleToUserTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanAssignRoleToUser;

                }
            };



        }
    }



}
