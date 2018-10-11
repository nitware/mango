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
using System.Collections.ObjectModel;
using Mango.Setup.Interfaces;

namespace Mango.Setup.Services
{
    public class UploadService : IUploadService
    {
        private ServiceClient service;

        public event EventHandler GetInpsByPeriodAndTypeCompleted;
        //public event EventHandler GetAllInpsByPeriodCompleted;
        public event EventHandler ReadInpsExcelCompleted;
        public event EventHandler SaveInpsCompleted;

        public ObservableCollection<Inps> Inpss { get; set; }
        public bool Done { get; set; }
        public Fault Fault { get; set; }

        public void UploadInpsSourceFile(string fileName, byte[] bytes, Period period, InpsType inpsType)
        {
            try
            {
                service = new ServiceClient();
                service.ReadInpsExcelCompleted += new EventHandler<ReadInpsExcelCompletedEventArgs>(service_ReadInpsExcelCompleted);
                service.ReadInpsExcelAsync(fileName, bytes, period, inpsType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveInps(ObservableCollection<Inps> inpss)
        {
            try
            {
                service = new ServiceClient();
                service.SaveInpsCompleted += new EventHandler<SaveInpsCompletedEventArgs>(service_SaveInpsCompleted);
                service.SaveInpsAsync(inpss);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public void GetAllInpsBy(Period period)
        //{
        //    try
        //    {
        //        service = new ServiceClient();
        //        service.GetAllInpsByPeriodCompleted += new EventHandler<GetAllInpsByPeriodCompletedEventArgs>(service_GetAllInpsByPeriodCompleted);
        //        service.GetAllInpsByPeriodAsync(period);
        //        service.CloseAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public void GetAllInpsBy(Period period, InpsType inpsType)
        {
            try
            {
                service = new ServiceClient();
                service.GetInpsByPeriodAndTypeCompleted += new EventHandler<GetInpsByPeriodAndTypeCompletedEventArgs>(service_GetInpsByPeriodAndTypeCompleted);
                service.GetInpsByPeriodAndTypeAsync(period, inpsType);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        private void service_ReadInpsExcelCompleted(object sender, ReadInpsExcelCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Inpss = e.Result;

                if (ReadInpsExcelCompleted != null)
                {
                    ReadInpsExcelCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_SaveInpsCompleted(object sender, SaveInpsCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Done = e.Result;

                if (SaveInpsCompleted != null)
                {
                    SaveInpsCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //private void service_GetAllInpsByPeriodCompleted(object sender, GetAllInpsByPeriodCompletedEventArgs e)
        //{
        //    try
        //    {
        //        Fault = e.fault;
        //        Inpss = e.Result;

        //        if (GetAllInpsByPeriodCompleted != null)
        //        {
        //            GetAllInpsByPeriodCompleted(this, e);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        private void service_GetInpsByPeriodAndTypeCompleted(object sender, GetInpsByPeriodAndTypeCompletedEventArgs e)
        {
            try
            {
                Fault = e.fault;
                Inpss = e.Result;

                if (GetInpsByPeriodAndTypeCompleted != null)
                {
                    GetInpsByPeriodAndTypeCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




       
       

    }


}
