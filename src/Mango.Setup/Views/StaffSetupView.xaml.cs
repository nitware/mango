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
    public partial class StaffSetupView : UserControl
    {
        public StaffSetupView(StaffSetupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            this.Loaded += (s, e) =>
            {
                int count = tcStaffSetupTab.Items.Count;
                if (count > 0)
                {
                    TabItem cdjrTab = (TabItem)tcStaffSetupTab.Items[0];
                    cdjrTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupCdjr;

                    TabItem staffLevelTab = (TabItem)tcStaffSetupTab.Items[1];
                    staffLevelTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupStaffLevel;

                    TabItem staffCdjrTab = (TabItem)tcStaffSetupTab.Items[2];
                    //staffCdjrTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.ca;

                    TabItem staffLearningTab = (TabItem)tcStaffSetupTab.Items[3];
                    //staffLearningTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.cansetup;

                    TabItem staffLocationTab = (TabItem)tcStaffSetupTab.Items[4];
                    //staffLocationTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupCdjr;

                   
                }
            };


        }
    }



}
