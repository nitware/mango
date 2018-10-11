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
    public static class DefaultButton
    {
        public static DependencyProperty DefaultButtonProperty = DependencyProperty.RegisterAttached("DefaultButton", typeof(Button), typeof(DefaultButton), new PropertyMetadata(null, DefaultButtonChanged));

        private static void DefaultButtonChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = sender as UIElement;
            var button = e.NewValue as Button;
            if (uiElement != null && button != null)
            {
                uiElement.KeyUp += (s, arg) =>
                    {
                        var peer = new ButtonAutomationPeer(button);
                        if (arg.Key == Key.Enter)
                        {
                            peer.SetFocus();
                            uiElement.Dispatcher.BeginInvoke((Action)delegate
                            {
                                var invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                if (invokeProv != null)
                                {
                                    invokeProv.Invoke();
                                }
                            });
                        }
                    };
            }
        }

        public static void SetDefaultButton(DependencyObject obj, Button button)
        {
            obj.SetValue(DefaultButtonProperty, button);
        }
        public static Button GetDefaultButton(UIElement obj)
        {
           return (Button)obj.GetValue(DefaultButtonProperty);
        }


    }
}
