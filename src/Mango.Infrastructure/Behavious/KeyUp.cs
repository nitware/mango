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
    public class KeyUp
    {
        private static readonly DependencyProperty keyUpCommandBehaviorProperty = DependencyProperty.RegisterAttached("keyUpCommandBehavior", typeof(keyUpBehavior), typeof(KeyUp), null);

        /// <summary>
        /// Command to execute on click event.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(KeyUp), new PropertyMetadata(OnSetCommandCallback));

        /// <summary>
        /// Command parameter to supply on command execution.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(KeyUp), new PropertyMetadata(OnSetCommandParameterCallback));
        
        /// <summary>
        /// Sets the <see cref=”ICommand”/> to execute on the click event.
        /// </summary>
        /// <param name=”buttonBase”>UIElement dependency object to attach command</param>
        /// <param name=”command”>Command to attach</param>       
        public static void SetCommand(UIElement element, ICommand command)
        {
            if (element == null) throw new System.ArgumentNullException("element");
            element.SetValue(CommandProperty, command);
        }

        /// <summary>
        /// Retrieves the <see cref=”ICommand”/> attached to the <see cref=”ButtonBase”/>.
        /// </summary>
        /// <param name=”buttonBase”>ButtonBase containing the Command dependency property</param>
        /// <returns>The value of the command attached</returns>       
        public static ICommand GetCommand(UIElement element)
        {
            if (element == null) throw new System.ArgumentNullException("element");
            return element.GetValue(CommandProperty) as ICommand;
        }

        /// <summary>
        /// Sets the value for the CommandParameter attached property on the provided <see cref=”ButtonBase”/>.
        /// </summary>
        /// <param name=”buttonBase”>ButtonBase to attach CommandParameter</param>
        /// <param name=”parameter”>Parameter value to attach</param>       
        public static void SetCommandParameter(UIElement element, object parameter)
        {
            if (element == null) throw new System.ArgumentNullException("element");
            element.SetValue(CommandParameterProperty, parameter);
        }

        /// <summary>
        /// Gets the value in CommandParameter attached property on the provided <see cref=”ButtonBase”/>
        /// </summary>
        /// <param name=”buttonBase”>ButtonBase that has the CommandParameter</param>
        /// <returns>The value of the property</returns>       
        public static object GetCommandParameter(UIElement element)
        {
            if (element == null) throw new System.ArgumentNullException("element");
            return element.GetValue(CommandParameterProperty);
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            Control element = dependencyObject as Control;
            if (element != null)
            {
                keyUpBehavior behavior = GetOrCreateBehavior(element);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static keyUpBehavior keyUpCommandBehavior(DependencyObject dependencyObject)
        {
            return (keyUpBehavior)dependencyObject.GetValue(keyUpCommandBehaviorProperty);
        }

        private static void SetkeyUpCommandBehavior(DependencyObject dependencyObject, keyUpBehavior value)
        {
            dependencyObject.SetValue(keyUpCommandBehaviorProperty, value);
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            Control element = dependencyObject as Control;
            if (element != null)
            {
                keyUpBehavior behavior = GetOrCreateBehavior(element);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static keyUpBehavior GetOrCreateBehavior(Control element)
        {
            keyUpBehavior behavior = element.GetValue(keyUpCommandBehaviorProperty) as keyUpBehavior;
            if (behavior == null)
            {
                behavior = new keyUpBehavior(element);
                element.SetValue(keyUpCommandBehaviorProperty, behavior);
            }

            return behavior;
        }




    }


}
