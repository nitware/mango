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
    public class keyUpBehavior : CommandBehaviorBase<Control>
    {
        public keyUpBehavior(Control element) : base(element)
        {
            element.KeyUp += new KeyEventHandler(element_keyUp);
        }

        void element_keyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                base.ExecuteCommand();
            }
        }

        //public keyUpBehavior(Control element) : base(element)
        //{
        //    element.KeyUp += new KeyEventHandler(element_keyUp);

        //    //element.KeyUp += (sender, args) =>
        //    //{
        //    //    if (args.Key == Key.Enter)
        //    //    {
        //    //        base.ExecuteCommand();
        //    //    }
        //    //};
        //}

        //void element_keyUp(object sender, KeyEventArgs e)
        //{
        //    //base.ExecuteCommand();
        //    if (e.Key == Key.Enter)
        //    {
        //        base.ExecuteCommand();
        //    }
        //}


    }
}
