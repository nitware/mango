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
    public class TabControlSelectionChangedBehavior : CommandBehaviorBase<TabControl>
    {
        public TabControlSelectionChangedBehavior(TabControl element) : base(element)
        {
            element.SelectionChanged += new SelectionChangedEventHandler(element_SelectionChanged);
        }

        void element_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            base.ExecuteCommand();
        }



    }

}
