using HH.Data.Entity.Model.Interfaces;
using HH.DockingManager.ViewModel.Interfaces;
using HH.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.Factories.Interfaces
{
    internal interface IEditorViewModelFactory
    {
        IEditorViewModel CreatEditorViewModel(IEditorManager editorManager, ICommandFactory commandFactory, IEntity model,
            IEditorContent editorContent, IEditorSettings editorSettings);

    }
}
