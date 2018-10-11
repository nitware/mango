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

using System.Windows.Browser;
using mobak.Users.ViewModels;

namespace Mango.Users.View
{
    public partial class LoginView : UserControl
    {
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                this.DataContext = viewModel;

                HtmlPage.Plugin.Focus();
                txtUserName.Text = "";
                pbPassword.Password = "";
                txtUserName.Focus();
            };
        }

       
    }



}
