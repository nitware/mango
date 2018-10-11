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

using mobak.Users.ViewModels;

namespace Mango.Users.Views
{
    public partial class AssignRightToRoleView : UserControl
    {
        public AssignRightToRoleView(AssignRightToRoleViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }


}
