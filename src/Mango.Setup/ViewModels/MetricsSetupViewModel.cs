﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Mango.Infrastructure.ViewModelBase;
using Microsoft.Practices.Prism.Events;
using Mango.Infrastructure.Models;

namespace Mango.Setup.ViewModels
{
    public class MetricsSetupViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;

        public MetricsSetupViewModel(IEventAggregator _eventAggregator)
        {
            eventAggregator = _eventAggregator;
            LoggedInUser = Utility.LoggedInUser;
        }
    }



}
