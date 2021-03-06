﻿using System;
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

using Mango.Setup.ViewModels.Upload;

namespace Mango.Setup.Views
{
    public partial class UploadView : UserControl
    {
        public UploadView(UploadViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
