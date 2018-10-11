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

using Microsoft.Practices.Prism.Commands;

namespace Mango.Infrastructure.Behaviour
{
    public class NumericUpDownValueChangedBehavior : CommandBehaviorBase<NumericUpDown>
    {
        public NumericUpDownValueChangedBehavior(NumericUpDown element)
            : base(element)
        {
            element.ValueChanged += new RoutedPropertyChangedEventHandler<double>(element_ValueChanged);
        }

        void element_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            base.ExecuteCommand();
        }



    }

}
