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

using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using System.Linq;
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.Models;

namespace Mango.Infrastructure.ViewModelBase
{
    public abstract class SetupViewModelBase<T> : ViewModelBase where T : class, new()
    {
        protected Dispatcher dispatcher;

        protected string saveSuccessfulMessage;
        protected string saveFailedMessage;
        protected string modifySuccessfulMessage;
        protected string modifyFailedMessage;
        private string removeSuccessfulMessage;
        private string removeFailedMessage;

        protected string ActionSuccessfulMessage;
        protected string ActionFailedMessage;

        protected string modelName;
        private string recordCount;

        protected ISetupService<T> service;
        protected ICollectionView models;
        protected T model;

        private bool canSaveItem;
        private bool canRemoveItem;
        private bool canClearScreen;

        protected Func<T, bool> addSelector;
        protected Func<T, bool> modifySelector;

        public SetupViewModelBase(ISetupService<T> _service)
        {
            service = _service;
        }

        public Edit.Mode ViewState { get; set; }
        public DelegateCommand SaveCommand { get; protected set; }
        public DelegateCommand RemoveCommand { get; protected set; }
        public DelegateCommand ClearCommand { get; protected set; }

        public bool CanSaveItem
        {
            get { return canSaveItem; }
            set
            {
                canSaveItem = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CanRemoveItem
        {
            get { return canRemoveItem; }
            set
            {
                canRemoveItem = value;
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CanClearScreen
        {
            get { return canClearScreen; }
            set
            {
                canClearScreen = value;
                ClearCommand.RaiseCanExecuteChanged();
            }
        }

        protected bool CanSave()
        {
            return CanSaveItem;
        }
        protected bool CanRemove()
        {
            return CanRemoveItem;
        }
        protected bool CanClear()
        {
            return CanClearScreen;
        }

        public string RecordCountLabel
        {
            get { return "Record Count: "; }
        }
        public string RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                base.OnPropertyChanged("RecordCount");
            }
        }
        public T Model
        {
            get { return model; }
            set
            {
                model = value;
                base.OnPropertyChanged("Model");
            }
        }

        public ICollectionView Models
        {
            get { return models; }
            set
            {
                models = value;
                base.OnPropertyChanged("Models");
            }
        }

        protected virtual void OnClearCommand()
        {
            try
            {
                UpdateViewState(Edit.Mode.Adding);
                Model = new T();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void Initialize()
        {
            try
            {
                saveSuccessfulMessage = modelName + " has been added successfully";
                saveFailedMessage = modelName + " creation failed! Please try again";
                modifySuccessfulMessage = modelName + " has been modified successfully";
                modifyFailedMessage = modelName + " modification failed! Please try again";
                removeSuccessfulMessage = modelName + " has been removed successfully";
                removeFailedMessage = modelName + " removal failed! Please try again";

                ViewState = Edit.Mode.Loaded;
                dispatcher = Deployment.Current.Dispatcher;

                Model = new T();
                ClearCommand = new DelegateCommand(OnClearCommand, CanClear);
                SaveCommand = new DelegateCommand(OnSaveCommand, CanSave);
                RemoveCommand = new DelegateCommand(OnRemoveCommand, CanRemove);

                LoadAll();
                UpdateViewState(Edit.Mode.Loaded);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void LoadAll()
        {
            try
            {
                LoadAllCompleted();
                service.LoadAll();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        protected virtual void OnRemoveCommand()
        {
            try
            {
                if (ViewState == Edit.Mode.Editing)
                {
                    SetActionMessage(removeSuccessfulMessage, removeFailedMessage);

                    ActionCompleted();
                    service.Remove(Model);
                }
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
                ActionCompleted();

                if (ViewState == Edit.Mode.Editing)
                {
                    SetActionMessage(modifySuccessfulMessage, modifyFailedMessage);
                    service.Modify(Model);
                }
                else
                {
                    SetActionMessage(saveSuccessfulMessage, saveFailedMessage);
                    service.Save(Model);
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual bool InvalidEntry(string nameProperty)
        {
            try
            {
                string message = "'" + nameProperty + "' already exist in the system! Operation has been aborted.";

                if (Model != null)
                {
                    if (string.IsNullOrWhiteSpace(nameProperty))
                    {
                        Utility.DisplayMessage("Please enter " + modelName + " name!");
                        return true;
                    }
                }
                else
                {
                    Utility.DisplayMessage("Base object is empty! Please contact your system administrator");
                    return true;
                }

                if (ViewState == Edit.Mode.Editing)
                {
                    if (modifySelector != null)
                    {
                        if (ModelAlreadyExist(modifySelector))
                        {
                            Utility.DisplayMessage(message);
                            return true;
                        }
                    }
                    //else
                    //{
                    //    Utility.DisplayMessage("Modify Selector cannot be null! Please contact your system administrator");
                    //    return true;
                    //}
                }
                else
                {
                    if (addSelector != null)
                    {
                        if (ModelAlreadyExist(addSelector))
                        {
                            Utility.DisplayMessage(message);
                            return true;
                        }
                    }
                    else
                    {
                        Utility.DisplayMessage("Add Selector cannot be null! Please contact your system administrator");
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual void LoadAllCommandCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(service.Fault))
                               {
                                   return;
                               }

                               Models = new PagedCollectionView(service.Models);
                               if (service.Models != null && service.Models.Count > 0)
                               {
                                   RecordCount = RecordCountLabel + service.Models.Count;
                                   Models.MoveCurrentTo(null);
                                   Models.CurrentChanged += (s, e) =>
                                   {
                                       SetSelectedModel();
                                   };
                               }
                               else
                               {
                                   RecordCount = RecordCountLabel + 0;
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void SetSelectedModel()
        {
            try
            {
                Model = Models.CurrentItem as T;
                UpdateViewState(Edit.Mode.Editing);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void LoadAllCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllCommandCompletedHelper();
                    service.GetAllModelsCompleted -= handler;
                };

                service.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage("Error occured! " + ex.Message);
            }
        }

        protected void ActionCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    ActionCommandCompletedHelper();
                    service.ActionCompleted -= handler;
                };

                service.ActionCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void ActionCommandCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                          (() =>
                          {
                              if (Utility.FaultExist(service.Fault))
                              {
                                  return;
                              }

                              if (service.Done)
                              {
                                  ActionSuccessHelper();
                              }
                              else
                              {
                                  Utility.DisplayMessage(ActionFailedMessage);
                              }
                          });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected virtual void ActionSuccessHelper()
        {
            LoadAll();
            OnClearCommand();

            Utility.DisplayMessage(ActionSuccessfulMessage);
        }

        protected virtual void UpdateViewState(Edit.Mode viewState)
        {
            ViewState = viewState;

            switch (viewState)
            {
                case Edit.Mode.Adding:
                case Edit.Mode.Loaded:
                    {
                        CanSaveItem = true;
                        CanRemoveItem = false;
                        CanClearScreen = false;

                        break;
                    }
                case Edit.Mode.Editing:
                    {
                        CanSaveItem = true;
                        CanRemoveItem = true;
                        CanClearScreen = true;

                        break;
                    }
            }
        }

        protected void SetActionMessage(string success, string failed)
        {
            ActionSuccessfulMessage = success;
            ActionFailedMessage = failed;
        }

        protected virtual bool ModelAlreadyExist(Func<T, bool> selector)
        {
            try
            {
                ObservableCollection<T> models = (ObservableCollection<T>)Models.SourceCollection;
                if (models != null && models.Count > 0)
                {
                    T model = models.Where(selector).SingleOrDefault();
                    if (model != null)
                    {
                        if (ViewState == Edit.Mode.Editing)
                        {
                            LoadAll();
                        }

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
