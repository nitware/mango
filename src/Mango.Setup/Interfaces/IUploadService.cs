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

using System.Collections.ObjectModel;
using Mango.Infrastructure.MangoService;

namespace Mango.Setup.Interfaces
{
    public interface IUploadService
    {
        event EventHandler SaveInpsCompleted;
        event EventHandler ReadInpsExcelCompleted;
        //event EventHandler GetAllInpsByPeriodCompleted;

        event EventHandler GetInpsByPeriodAndTypeCompleted;

        ObservableCollection<Inps> Inpss { get; set; }
        bool Done { get; set; }
        Fault Fault { get; set; }

        void UploadInpsSourceFile(string fileName, byte[] bytes, Period period, InpsType inpsType);
        void SaveInps(ObservableCollection<Inps> inpss);
        void GetAllInpsBy(Period period, InpsType inpsType);

        //void GetAllInpsBy(Period period);
    }




}
