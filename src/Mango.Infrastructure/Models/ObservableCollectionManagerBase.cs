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

using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Threading;

namespace Mango.Infrastructure.Models
{
    public abstract class ObservableCollectionManagerBase<T> : ViewModelBase.ViewModelBase
    {
        private bool modelCanBeAdded;
        private bool modelCanBeSaved;
        private bool modelCanBeRemoved;
        private bool modelCanBeCleared;
        
        private T targetModel;
        private ICollectionView targetCollection;
        protected ObservableCollectionManager<T> collectionManager;
        private string recordCount;

        public ObservableCollectionManagerBase()
        {
            collectionManager = new ObservableCollectionManager<T>();
        }

        protected bool ModelExist { get; set; }
        public ICollectionView Models { get; set; }
        public DelegateCommand ClearCommand { get; protected set; }
        public DelegateCommand RemoveCommand { get; protected set; }
        public DelegateCommand SaveCommand { get; protected set; }
        public DelegateCommand AddCommand { get; protected set; }

        public string RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                OnPropertyChanged("RecordCount");
            }
        }
        public bool ModelCanBeAdded
        {
            get { return modelCanBeAdded; }
            set
            {
                modelCanBeAdded = value;
                AddCommand.RaiseCanExecuteChanged();
            }
        }
        public bool ModelCanBeRemoved
        {
            get { return modelCanBeRemoved; }
            set
            {
                modelCanBeRemoved = value;
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }
        public bool ModelCanBeSaved
        {
            get { return modelCanBeSaved; }
            set
            {
                modelCanBeSaved = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public bool ModelCanBeCleared
        {
            get { return modelCanBeCleared; }
            set
            {
                modelCanBeCleared = value;
                ClearCommand.RaiseCanExecuteChanged();
            }
        }
        public ICollectionView TargetCollection
        {
            get { return targetCollection; }
            set
            {
                targetCollection = value;
                base.OnPropertyChanged("TargetCollection");
            }
        }

        public T TargetModel
        {
            get { return targetModel; }
            set
            {
                targetModel = value;
                base.OnPropertyChanged("TargetModel");
            }
        }

        public bool CanClear()
        {
            return ModelCanBeCleared;
        }
        public bool CanSave()
        {
            return ModelCanBeSaved;
        }
        public bool CanAdd()
        {
            return ModelCanBeAdded;
        }
        public bool CanRemove()
        {
            return ModelCanBeRemoved;
        }

        protected abstract T GetNewModel();

        protected void UpdateModels()
        {
            try
            {
                collectionManager.Collection.Add(GetNewModel());
                RefreshModelCollection();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void RefreshModelCollection()
        {
            try
            {
                ObservableCollection<T> refreshedModels = collectionManager.Collection;
                SetCurrentTargetCollection(refreshedModels);

                if (refreshedModels.Count > 0)
                {
                    UpdateViewState(Edit.Mode.Adding);
                }
                else
                {
                    UpdateViewState(Edit.Mode.Loaded);
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void SetCurrentTargetCollection(ObservableCollection<T> refreshedModels)
        {
            try
            {
                TargetCollection = new PagedCollectionView(refreshedModels);
                if (refreshedModels != null)
                {
                    UpdateMetricsRecordCount(refreshedModels);

                    TargetCollection.MoveCurrentTo(null);
                    TargetCollection.CurrentChanged += (s, e) =>
                    {
                        SelectedTargetCollection();
                    };
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void UpdateMetricsRecordCount(ObservableCollection<T> models)
        {
            RecordCount = "Record Count: " + models.Count;
        }

        protected virtual void SelectedTargetCollection()
        {
            try
            {
                UpdateViewState(Edit.Mode.Selected);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual void UpdateViewState(Edit.Mode uiState)
        {
            switch (uiState)
            {
                case Edit.Mode.Loaded:
                    {
                        ModelCanBeAdded = true;
                        ModelCanBeRemoved = false;
                        ModelCanBeSaved = false;
                        ModelCanBeCleared = false;

                        break;
                    }
                case Edit.Mode.Adding:
                    {
                        ModelCanBeAdded = true;
                        ModelCanBeRemoved = false;
                        ModelCanBeSaved = true;
                        ModelCanBeCleared = true;

                        break;
                    }
                case Edit.Mode.Selected:
                    {
                        ModelCanBeAdded = true;
                        ModelCanBeRemoved = true;
                        ModelCanBeSaved = true;
                        ModelCanBeCleared = true;

                        break;
                    }
            }
        }

        protected virtual void ClearTargetCollection()
        {
            try
            {
                if (TargetCollection != null)
                {
                    collectionManager.Collection.Clear();
                    ObservableCollection<T> models = (ObservableCollection<T>)TargetCollection.SourceCollection;

                    models.Clear();
                    TargetCollection = new PagedCollectionView(models);
                    UpdateViewState(Edit.Mode.Loaded);
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void IsModelExist(ObservableCollection<T> models)
        {
            try
            {
                if (models != null)
                {
                    ModelExist = models.Count > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        



    }
}
