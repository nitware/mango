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

using System.Globalization;
using System.Windows.Data;
using Mango.Infrastructure.MangoService;

namespace Mango.Infrastructure.Converters
{
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var status = (Status)value;
            //if (status.Name == "Not Started" || status.Name == "Closed")
            //{
            //    return "Red";
            //}
            //else if (status.Name == "Uncompleted" || status.Name == "In Progress")
            //{
            //    return "Blue";
            //}
            //else if (status.Name == "Almost Completed")
            //{
            //    return "Orange";
            //}
            //else
            //{
            //    return "Green";
            //}


            var status = (string)value;
            if (status == "Not Started" || status == "Closed")
            {
                return "Red";
            }
            else if (status == "Uncompleted" || status == "In Progress")
            {
                return "Blue";
            }
            else if (status == "Almost Completed")
            {
                return "Orange";
            }
            else
            {
                return "Green";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
