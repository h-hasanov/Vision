using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace HH.View.Utils.Extensions
{
    [DebuggerNonUserCode]
    internal static class DependencyObjectExtensions
    {
        public static T GetVisualParent<T>(this DependencyObject child) where T : FrameworkElement
        {
            while ((child != null) && !(child is T))
            {
                child = VisualTreeHelper.GetParent(child);
            }
            return child as T;
        }
    }
}