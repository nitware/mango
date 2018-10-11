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
    public partial class JobRoleView : UserControl
    {
        public JobRoleView(JobRoleViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }


}
