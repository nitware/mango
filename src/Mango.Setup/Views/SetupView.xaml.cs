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
    public partial class SetupView : UserControl
    {
        public SetupView(SetupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

             this.Loaded += (s, e) =>
            {
                int count = tcSetupTab.Items.Count;
                if (count > 0)
                {
                    TabItem jobRoleTab = (TabItem)tcSetupTab.Items[0];
                    jobRoleTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupJobRole;

                    TabItem jobLevelTab = (TabItem)tcSetupTab.Items[1];
                    jobLevelTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupJobLevel;

                    TabItem departmentTab = (TabItem)tcSetupTab.Items[2];
                    departmentTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupDepartment;

                    TabItem locationTab = (TabItem)tcSetupTab.Items[3];
                    //locationTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.cansetup;
                    
                    TabItem periodTypeTab = (TabItem)tcSetupTab.Items[4];
                    //periodTypeTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupCdjr;

                    //TabItem resolutionStatusTab = (TabItem)tcSetupTab.Items[5];
                    //resolutionStatusTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanAssignRolesUnderSupervisor;

                    //TabItem departmentTab = (TabItem)tcSetupTab.Items[6];
                    //departmentTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanAssignRolesUnderHod;
                    
                    //TabItem departmentHeadTab = (TabItem)tcSetupTab.Items[7];
                    //departmentHeadTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupMetricRating;

                    //TabItem branchTab = (TabItem)tcSetupTab.Items[8];
                    //branchTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupMetrics;
                    
                    //TabItem documentTypeTab = (TabItem)tcSetupTab.Items[9];
                    //documentTypeTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanCreateNewPeriod;
                    
                    //TabItem bankTab = (TabItem)tcSetupTab.Items[10];
                    //bankTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanModifyPeriod;

                    //TabItem bankersConfirmationTab = (TabItem)tcSetupTab.Items[11];
                    //bankersConfirmationTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetCurrentPeriod;
                    
                }
            };
        }
        }
    }

