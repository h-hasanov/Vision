using HH.Icons.Model.Enums;

namespace HH.DockingManager.ViewModel.Interfaces
{
    public interface IDockableViewSettings
    {
        GlyphType IconSource { get; set; }
        string ToolTip { get; set; }
    }
}
