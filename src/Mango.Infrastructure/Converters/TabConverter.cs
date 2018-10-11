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
using System.Globalization;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Mango.Infrastructure.Converters
{
    public class TabConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var source = (IEnumerable)value;
                if (source != null)
                {
                    var controlTemplate = (ControlTemplate)parameter;
                    var tabItems = new List<TabItem>();

                    foreach (object item in source)
                    {
                        PropertyInfo[] propertyInfos = item.GetType().GetProperties();

                        var propertyInfo = propertyInfos.First(x => x.Name == "Name");

                        string headerText = null;
                        if (propertyInfo != null)
                        {
                            object propValue = propertyInfo.GetValue(item, null);
                            headerText = (propValue ?? string.Empty).ToString();
                        }

                        var tabItem = new TabItem()
                        {
                            DataContext = item,
                            Header = headerText,
                            Content = controlTemplate == null ? item : new ContentControl() { Template = controlTemplate }
                        };

                        tabItems.Add(tabItem);
                    }

                    return tabItems;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
