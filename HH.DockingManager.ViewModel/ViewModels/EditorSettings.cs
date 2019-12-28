using HH.DockingManager.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.ViewModels
{
    public sealed class EditorSettings : DockableViewSettingsBase, IEditorSettings
    {
        public string Title { get; set; }
    }
}
