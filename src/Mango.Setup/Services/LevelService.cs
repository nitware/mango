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

using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Interfaces;
using System.Collections.ObjectModel;

namespace Mango.Setup.Services
{
    public class LevelService : ISetupService<Level>
    {
        private ServiceClient service;

        public event EventHandler ActionCompleted;
        public event EventHandler GetAllModelsCompleted;

        public ObservableCollection<Level> Models { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void LoadAll()
        {
            try
            {
                service = new ServiceClient();
                service.GetAllLevelsCompleted += new EventHandler<GetAllLevelsCompletedEventArgs>(service_GetAllLevelsCompleted);
                service.GetAllLevelsAsync();
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(Level level)
        {
            try
            {
                service = new ServiceClient();
                service.AddLevelCompleted += new EventHandler<AddLevelCompletedEventArgs>(service_AddLevelCompleted);
                service.AddLevelAsync(level);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modify(Level level)
        {
            try
            {
                service = new ServiceClient();
                service.ModifyLevelCompleted += new EventHandler<ModifyLevelCompletedEventArgs>(service_ModifyLevelCompleted);
                service.ModifyLevelAsync(level);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(Level level)
        {
            try
            {
                service = new ServiceClient();
                service.RemoveLevelCompleted += new EventHandler<RemoveLevelCompletedEventArgs>(service_RemoveLevelCompleted);
                service.RemoveLevelAsync(level);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_RemoveLevelCompleted(object sender, RemoveLevelCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Done = e.Result;

                if (ActionCompleted != null)
                {
                    ActionCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_AddLevelCompleted(object sender, AddLevelCompletedEventArgs e)
        {
            try
            {
                Done = e.Result;
                Fault = e.fault;

                if (ActionCompleted != null)
                {
                    ActionCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_ModifyLevelCompleted(object sender, ModifyLevelCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Done = e.Result;

                if (ActionCompleted != null)
                {
                    ActionCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void service_GetAllLevelsCompleted(object sender, GetAllLevelsCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Models = e.Result;

                if (GetAllModelsCompleted != null)
                {
                    GetAllModelsCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }

}
