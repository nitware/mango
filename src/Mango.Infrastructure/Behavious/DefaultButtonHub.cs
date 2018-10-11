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

using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace Mango.Infrastructure.Behaviour
{
    public class DefaultButtonHub
    {
        ButtonAutomationPeer peer = null;
        
        private void Attach(DependencyObject source)
        {
            if (source is Button)
            {
                peer = new ButtonAutomationPeer(source as Button);
            }
            else if (source is TextBox)
            {
                TextBox tb = source as TextBox;
                tb.KeyUp += new KeyEventHandler(tb_KeyUp);
            }
            else if (source is PasswordBox)
            {
                PasswordBox pb = source as PasswordBox;
                pb.KeyUp += new KeyEventHandler(pb_KeyUp);
            }
        }

        private void tb_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void pb_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (peer != null)
                {
                    ((IInvokeProvider)peer).Invoke();
                }
            }
        }

        public static DefaultButtonHub GetDefaultHub(DependencyObject obj)
        {
            return (DefaultButtonHub)obj.GetValue(DefaultHubProperty);
        }

        public static void SetDefaultHub(DependencyObject obj, DefaultButtonHub value)
        {
            obj.SetValue(DefaultHubProperty, value);
        }

        //public static readonly DependencyProperty DefaultHubProperty = DependencyProperty.RegisterAttached("DefaultHub", typeof(DefaultButtonHub), typeof(DefaultButtonHub), new PropertyMetadata(OnHubAttach));

        public static readonly DependencyProperty DefaultHubProperty = DependencyProperty.RegisterAttached("DefaultButtonHub", typeof(Button), typeof(DefaultButtonHub), new PropertyMetadata((s, e) => { DefaultButtonHub hub = e.NewValue as DefaultButtonHub; hub.Attach(s); }));


        //public static void OnHubAttach(DependencyProperty source, DependencyPropertyChangedEventArgs e)
        //{
        //    DefaultButtonHub hub = e.NewValue as DefaultButtonHub;
        //    hub.Attach(source);
        //}


    }


}
