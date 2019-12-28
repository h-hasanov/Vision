using HH.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.Interfaces
{
    public interface IEditorContent : IValidatableViewModel
    {
        bool CanClose();
    }
}
