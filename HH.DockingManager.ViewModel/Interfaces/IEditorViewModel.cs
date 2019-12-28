using System.Windows.Input;
using HH.Data.Entity.Model.Interfaces;

namespace HH.DockingManager.ViewModel.Interfaces
{
    public interface IEditorViewModel : IDockableViewModel
    {
        IEntity Model { get; }

        IEditorContent Content { get; }

        ICommand CloseEditorCommand { get; }
        void CloseEditor();
        bool CanCloseEditor();
    }
}
