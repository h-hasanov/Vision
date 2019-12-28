using System;
using System.Linq;
using HH.DockingManager.ViewModel.Interfaces;
using Xceed.Wpf.AvalonDock.Layout;

namespace HH.DockingManager.View.Layout
{
    public sealed class DefaultLayoutStrategy : ILayoutUpdateStrategy
    {
        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
        {
            if (layout.Manager.IsLoaded)
                return false;

            var panes = layout.Descendents().OfType<LayoutAnchorablePane>();
            var paneViewModel = (IPaneViewModel)anchorableToShow.Content;
            var pane = panes.FirstOrDefault(c => c.Name == paneViewModel.ParentPaneName);
            if (pane == null)
                throw new NotImplementedException("Pane Container not found. Please specify where to insert the Anchorable");
            pane.Children.Add(anchorableToShow);
            return true;
        }

        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        { }

        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            return false;
        }

        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        { }
    }
}