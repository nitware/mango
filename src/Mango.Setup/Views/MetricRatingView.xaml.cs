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
using System.Windows.Data;
using Mango.Infrastructure.Models;

namespace Mango.Setup.Views
{
    public partial class MetricRatingView : UserControl
    {
        private MetricRatingViewModel viewModel;

        public MetricRatingView(MetricRatingViewModel _viewModel)
        {
            InitializeComponent();
            DataContext = _viewModel;

            viewModel = _viewModel;

            this.Loaded += (s, e) =>
            {
                MetricsLoadCompleted();
            };
            
        }

        private void MetricsLoadCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    MetricsLoadCompletedHelper();
                    viewModel.MetricesLoaded -= handler;
                };

                viewModel.MetricesLoaded += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void MetricsLoadCompletedHelper()
        {
            try
            {
                PagedCollectionView pcv = dgMetrics.ItemsSource as PagedCollectionView;

                if (pcv != null)
                {
                    foreach (CollectionViewGroup group in pcv.Groups)
                    {
                        dgMetrics.CollapseRowGroup(group, true);
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
