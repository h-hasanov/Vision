using HH.DockingManager.ViewModel.Interfaces;
using HH.Icons.Model.Enums;

namespace HH.DockingManager.ViewModel.ViewModels
{
    public abstract class DockableViewSettingsBase : IDockableViewSettings
    {
        public GlyphType IconSource { get; set; }
        public string ToolTip { get; set; }
    }
}
