using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using HH.DockingManager.ViewModel.Interfaces;

namespace HH.DockingManager.View.Selectors
{
    [DebuggerNonUserCode]
    public sealed class DockingStyleSelector : StyleSelector
    {
        public Style PaneStyle { get; set; }

        public Style AssetContentStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is IPaneViewModel) return PaneStyle;
            if (item is IEditorViewModel) return AssetContentStyle;
            return base.SelectStyle(item, container);
        }
    }
}
