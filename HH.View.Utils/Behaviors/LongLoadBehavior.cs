using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interactivity;

namespace HH.View.Utils.Behaviors
{
    [DebuggerNonUserCode]
    public class LongLoadBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Initialized += AssociatedObject_OnInitialized;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Initialized -= AssociatedObject_OnInitialized;
        }

        private void AssociatedObject_OnInitialized(object sender, EventArgs args)
        {
            var wc = new WaitCursor();
            var ao = AssociatedObject;
            RoutedEventHandler h = null;
            h = (s, e) =>
            {
                try
                {
                    wc.Dispose();
                }
                finally
                {
                    ao.Loaded -= h;
                }
            };
            ao.Loaded += h;
        }
    }
}