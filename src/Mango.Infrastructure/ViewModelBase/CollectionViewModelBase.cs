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

using Mango.Infrastructure.Models;
using System.Windows.Threading;
using Mango.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;

namespace Mango.Infrastructure.ViewModelBase
{
    public abstract class CollectionViewModelBase<T> : ObservableCollectionManagerBase<T> where T : class, new()
    {
        protected Dispatcher dispatcher;

        protected IMetricBaseService<T> service;
        private string modelName;

        public CollectionViewModelBase(IMetricBaseService<T> _service)
        {
            TargetModel = new T();
            dispatcher = Deployment.Current.Dispatcher;

            service = _service;

            Initialize();
        }

        public string ModelName
        {
            get { return modelName; }
            set
            {
                modelName = value;
                base.OnPropertyChanged("ModelName");
            }
        }
        public string RecordCountLabel
        {
            get { return "Record Count: "; }
        }
      
        protected abstract bool IsRequiredDetailsEntered();
        protected abstract bool ModelAlreadyExist();
        protected abstract void ClearView();

        private void Initialize()
        {
            try
            {
                ClearCommand = new DelegateCommand(OnClearCommand, CanClear);
                RemoveCommand = new DelegateCommand(OnRemoveCommand, CanRemove);
                SaveCommand = new DelegateCommand(OnSaveCommand, CanSave);
                AddCommand = new DelegateCommand(OnAddCommand, CanAdd);

                UpdateViewState(Edit.Mode.Loaded);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void OnClearCommand()
        {
            try
            {
                ClearView();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void OnSaveCommand()
        {
            try
            {
                ObservableCollection<T> models = (ObservableCollection<T>)TargetCollection.SourceCollection;

                if (IncompleteUserInput(models))
                {
                    return;
                }

                if (ModelExist)
                {
                    SaveCompleted();
                    service.Modify(models);
                }
                else
                {
                    SaveCompleted();
                    service.Save(models);
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual bool IncompleteUserInput(ObservableCollection<T> models)
        {
            if (models == null)
            {
                Utility.DisplayMessage("No item to save!");
                return true;
            }

            return false;
        }
        
        protected virtual void OnRemoveCommand()
        {
            try
            {
                int index = TargetCollection.CurrentPosition;
                if (index > -1)
                {
                    collectionManager.Collection.RemoveAt(index);
                    RefreshModelCollection();
                }
                else
                {
                    Utility.DisplayMessage("No selection made! Please select a row for removal");
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
      
        protected virtual void OnAddCommand()
        {
            try
            {
                if (!IsRequiredDetailsEntered())
                {
                    return;
                }

                UpdateModels();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
      
        protected void SaveCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    SaveCompletedHelper();
                    service.ActionCompleted -= handler;
                };

                service.ActionCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected virtual void SaveCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (IsFaultExceptionThrown())
                               {
                                   return;
                               }

                               if (service.Done)
                               {
                                   SuccessfullActionHelper();
                               }
                               else
                               {
                                   Utility.DisplayMessage(ModelName + " have not been saved! Please try again");
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual bool IsFaultExceptionThrown()
        {
            if (Utility.FaultExist(service.Fault))
            {
                return true;
            }

            return false;
        }

        protected virtual void SuccessfullActionHelper()
        {
            Utility.DisplayMessage(ModelName + " have been successfully saved");
            ClearView();
        }



    }
}
