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

using Mango.Infrastructure.Animation;

namespace Mango.Shell
{
    public partial class Shell : UserControl
    {
        private int counter = 0;
        private Border border = null;

        public Shell(ShellViewModel viewModel)
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                this.DataContext = viewModel;
            };

            Animation.Init(this.Resources);
            Animation.SwitchToPage();
        }

        private void OnTimerTick(object s, EventArgs args)
        {
            ((PlaneProjection)border.Projection).RotationY = Convert.ToInt32("-" + (counter + 5).ToString());

            counter++;
            if (counter == 100000000)
            {
                counter = 0;
            }
        }

      
      

    }



}
