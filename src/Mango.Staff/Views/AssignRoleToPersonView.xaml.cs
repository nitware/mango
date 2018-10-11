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

namespace Mango.Users.Views
{
    public partial class AssignRoleToPersonView : UserControl
    {
        public AssignRoleToPersonView(AssignRoleToPersonViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
