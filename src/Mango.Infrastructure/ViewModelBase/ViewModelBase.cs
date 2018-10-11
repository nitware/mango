using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.ComponentModel;
using System.Collections.Specialized;
using Microsoft.Practices.Prism.ViewModel;
using Mango.Infrastructure.MangoService;

namespace Mango.Infrastructure.ViewModelBase
{
    public class ViewModelBase : NotificationObject, INotifyPropertyChanged, INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private Staff loggedInUser;
        private bool isLoggedInUserHasRight;
        protected string currentState;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        public string CurrentState
        {
            get
            {
                return this.currentState;
            }
            set
            {
                if (this.currentState == value)
                {
                    return;
                }

                this.currentState = value;
                this.RaisePropertyChanged(() => this.CurrentState);
            }
        }

        public Staff LoggedInUser
        {
            get { return loggedInUser; }
            set
            {
                loggedInUser = value;
                OnPropertyChanged("LoggedInUser");
            }
        }

        public bool IsLoggedInUserHasRight
        {
            get { return isLoggedInUserHasRight; }
            set
            {
                isLoggedInUserHasRight = value;
                OnPropertyChanged("IsLoggedInUserHasRight");
            }
        }



    }



}
