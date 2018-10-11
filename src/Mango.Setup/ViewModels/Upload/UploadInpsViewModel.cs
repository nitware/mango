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

using System.Windows.Threading;
using Mango.Setup.Interfaces;
using Mango.Infrastructure.Models;
using System.ComponentModel;
using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.MangoService;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using Mango.Infrastructure.Interfaces;
using Mango.Setup.Services;
using System.Collections.Generic;
using System.Windows.Data;
using System.Linq;

namespace Mango.Setup.ViewModels.Upload
{
    public class UploadInpsViewModel : ViewModelBase
    {
        private Dispatcher dispatcher;
        private IUploadService service;
        private ISetupService<InpsType> inpsTypeService;

        private InpsType inpsType;
        private ICollectionView inpsTypes;

        private string recordCount;
        private ObservableCollection<Inps> inpss;
        private bool isUploadDataPresent;
        private bool canSaveInps;
        private bool canClearInps;
        private bool canBrowseFile;

        public UploadInpsViewModel(IUploadService _service)
        {
            service = _service;
            inpsTypeService = new InpsTypeService();
            dispatcher = Deployment.Current.Dispatcher;

            //UpdateUiState();

            //BrowseCommand = new DelegateCommand(OnBrowseCommand, CanBrowse);


            BrowseCommand = new DelegateCommand(OnBrowseCommand);
            SaveCommand = new DelegateCommand(OnSaveCommand, CanSave);
            ClearCommand = new DelegateCommand(OnClearCommand, CanClear);

            //GetAllInpsByPeriod();

            LoadAllInpsTypeCompleted();
            inpsTypeService.LoadAll();
        }

        public DelegateCommand BrowseCommand { get; protected set; }
        public DelegateCommand SaveCommand { get; protected set; }
        public DelegateCommand ClearCommand { get; protected set; }

        public bool CanSaveInps
        {
            get { return canSaveInps; }
            set
            {
                canSaveInps = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CanClearInps
        {
            get { return canClearInps; }
            set
            {
                canClearInps = value;
                ClearCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CanBrowseFile
        {
            get { return canBrowseFile; }
            set
            {
                canBrowseFile = value;
                BrowseCommand.RaiseCanExecuteChanged();
            }
        }

        public string TabCaption
        {
            get { return "Upload"; }
        }

        public ObservableCollection<Inps> Inpss
        {
            get { return inpss; }
            set
            {
                inpss = value;
                base.OnPropertyChanged("Inpss");
            }
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
        public bool IsUploadDataPresent
        {
            get { return isUploadDataPresent; }
            set
            {
                isUploadDataPresent = value;
                base.OnPropertyChanged("IsUploadDataPresent");
            }
        }
       
        public ICollectionView InpsTypes
        {
            get { return inpsTypes; }
            set
            {
                inpsTypes = value;
                OnPropertyChanged("InpsTypes");
            }
        }
        public InpsType InpsType
        {
            get { return inpsType; }
            set
            {
                inpsType = value;
                OnPropertyChanged("InpsType");
            }
        }
        public bool CanBrowse()
        {
            return CanBrowseFile;
        }
        public bool CanSave()
        {
            return CanSaveInps;
        }
        public bool CanClear()
        {
            return CanClearInps;
        }
        public void ReadInpsExcelFile(string fileName, byte[] bytes, InpsType inpsType)
        {
            try
            {
                ReadInpsExcelFileCompleted();
                service.UploadInpsSourceFile(fileName, bytes, Utility.Period, inpsType);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
            
        }
        public void GetAllInpsByPeriodAndType()
        {
            try
            {
                if (Utility.Period != null && InpsType != null)
                {
                    GetAllInpsByPeriodAndTypeCompleted();
                    service.GetAllInpsBy(Utility.Period, InpsType);
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void GetAllInpsByPeriodAndTypeCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    GetAllInpsByPeriodCompletedHelper();
                    service.GetInpsByPeriodAndTypeCompleted -= handler;
                };

                service.GetInpsByPeriodAndTypeCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void GetAllInpsByPeriodCompletedHelper()
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

                               Inpss = service.Inpss;
                               if (Inpss != null && Inpss.Count > 0)
                               {
                                   CanSaveInps = false;
                                   CanClearInps = false;

                                   //CanBrowseFile = true;
                               }
                               else
                               {
                                   //CanBrowseFile = false;
                                   Utility.DisplayMessage("No data exist for " + InpsType.Name );
                               }

                               UpdateUiState();
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void ReadInpsExcelFileCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    ReadInpsExcelFileCompletedHelper();
                    service.ReadInpsExcelCompleted -= handler;
                };

                service.ReadInpsExcelCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void ReadInpsExcelFileCompletedHelper()
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

                               Inpss = service.Inpss;
                               if (Inpss != null && Inpss.Count > 0)
                               {
                                   CanSaveInps = true;
                                   CanClearInps = true;
                               }
                               
                               UpdateUiState();
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void SaveInpsCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    SaveInpsCompletedHelper();
                    service.SaveInpsCompleted -= handler;
                };

                service.SaveInpsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void SaveInpsCompletedHelper()
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
                                   Utility.DisplayMessage(InpsType.Name + " saved successfully.");

                                   CanSaveInps = false;
                                   CanClearInps = false;

                                   CanBrowseFile = false;
                               }
                               else
                               {
                                   Utility.DisplayMessage("Could not save " + InpsType.Name + "! Please try again.");

                                   CanSaveInps = true;
                                   CanClearInps = true;
                                   CanBrowseFile = true;
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void UpdateUiState()
        {
            try
            {
                if (Inpss != null && Inpss.Count > 0)
                {
                    IsUploadDataPresent = true;
                    RecordCount = "Record Count: " + Inpss.Count;
                }
                else
                {
                    IsUploadDataPresent = false;
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void OnBrowseCommand() { }
       
        private void OnSaveCommand()
        {
            try
            {
                if (InvalidUserEntry())
                {
                    return;
                }

                SaveInpsCompleted();
                service.SaveInps(Inpss);
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private bool InvalidUserEntry()
        {
            try
            {
                if (InpsType == null || InpsType.Id <= 0)
                {
                      Utility.DisplayMessage("Please select INPS Type!");
                    return true;
                }
                else if (Inpss == null || Inpss.Count == 0)
                {
                    Utility.DisplayMessage("No INPS data to save! Please upload.");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnClearCommand()
        {
            try
            {
                if (Inpss != null)
                {
                    Inpss.Clear();
                    //UpdateUiState();

                    GetAllInpsByPeriodAndType();
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        protected void LoadAllInpsTypeCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllInpsTypeCompletedHelper();
                    inpsTypeService.GetAllModelsCompleted -= handler;
                };

                inpsTypeService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void LoadAllInpsTypeCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(inpsTypeService.Fault))
                               {
                                   return;
                               }

                               if (inpsTypeService.Models != null && inpsTypeService.Models.Count > 0)
                               {
                                   List<InpsType> inpsTypes = inpsTypeService.Models.Where(d => d.Id != 0).ToList();
                                   if (inpsTypes.Count > 0)
                                   {
                                       inpsTypes.Insert(0, new InpsType() { Name = "<< Select a Type >>" });
                                   }

                                   InpsTypes = new PagedCollectionView(inpsTypes);
                                   InpsTypes.MoveCurrentToFirst();
                                   InpsTypes.CurrentChanged += (s, e) =>
                                   {
                                       InpsType = InpsTypes.CurrentItem as InpsType;
                                       if (InpsType != null && InpsType.Id > 0)
                                       {
                                           GetAllInpsByPeriodAndType();
                                       }
                                   };
                               }
                              
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }





    }
}
