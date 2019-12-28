using System;
using System.Diagnostics;
using Xceed.Wpf.AvalonDock.Themes;

namespace HH.DockingManager.View.Themes.Metro
{
    [DebuggerNonUserCode]
    public class VisionTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
               "/HH.DockingManager.View;component/Themes/Metro/AvalonDockMetroTheme.xaml",
               UriKind.Relative);
        }
    }
}
