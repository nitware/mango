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

namespace Mango.Staff.Views.Popups
{
    public partial class ChangePasswordView : ChildWindow
    {
        public ChangePasswordView()
        {
            InitializeComponent();
        }
    }

    //public partial class ChangePasswordView
    //{
    //    public ChangePasswordView(ChangePasswordViewModel viewModel)
    //    {
    //        InitializeComponent();
    //        DataContext = viewModel;

    //        viewModel.OnSetPopUpCommand(this);
    //    }
    //}





}

