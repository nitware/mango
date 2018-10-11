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
    public partial class PeriodSetupView : UserControl
    {
        public PeriodSetupView(PeriodSetupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            this.Loaded += (s, e) =>
            {
                int count = tcPeriodSetupTab.Items.Count;
                if (count > 0)
                {
                    TabItem currentTab = (TabItem)tcPeriodSetupTab.Items[0];
                    currentTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupCdjr;

                    TabItem modifyTab = (TabItem)tcPeriodSetupTab.Items[1];
                    modifyTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupStaffLevel;

                    TabItem newTab = (TabItem)tcPeriodSetupTab.Items[2];
                    //newTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.ca;
                }
            };


        }
    }



}
