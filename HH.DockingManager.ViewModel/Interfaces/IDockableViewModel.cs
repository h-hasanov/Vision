using HH.Icons.Model.Enums;
using HH.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.Interfaces
{
    public interface IDockableViewModel : IViewModel, IVisible, ISelectable
    {
        bool IsActive { get; set; }
        string Title { get; set; }
        string ToolTip { get; }

        GlyphType IconSource { get; }
    }
}
