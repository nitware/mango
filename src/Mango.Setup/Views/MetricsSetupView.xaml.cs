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
    public partial class MetricsSetupView : UserControl
    {
        public MetricsSetupView(MetricsSetupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            this.Loaded += (s, e) =>
            {
                int count = tcKpiSetupTab.Items.Count;
                if (count > 0)
                {
                    TabItem metricsTab = (TabItem)tcKpiSetupTab.Items[0];
                    metricsTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupMetrics;

                    TabItem metricsRatingTab = (TabItem)tcKpiSetupTab.Items[1];
                    metricsRatingTab.IsEnabled = viewModel.LoggedInUser.Role.UserRight.CanSetupMetricRating;
                }
            };



        }
    }



}
