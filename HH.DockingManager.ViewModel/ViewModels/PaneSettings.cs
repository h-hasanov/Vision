using HH.DockingManager.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.ViewModels
{
    public sealed class PaneSettings : DockableViewSettingsBase, IPaneSettings
    {
        public string Title { get; set; }
        public string ContentId { get; set; }
        public string ParentPaneName { get; set; }
    }
}
