using HH.Data.Entity.Model.Interfaces;
using HH.DockingManager.ViewModel.Factories.Interfaces;
using HH.DockingManager.ViewModel.Interfaces;
using HH.DockingManager.ViewModel.ViewModels;
using HH.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.Factories.Implementations
{
    internal sealed class EditorViewModelFactory : IEditorViewModelFactory
    {
        public IEditorViewModel CreatEditorViewModel(IEditorManager editorManager, ICommandFactory commandFactory, IEntity model,
            IEditorContent editorContent, IEditorSettings editorSettings)
        {
            return new EditorViewModel(editorManager, commandFactory, model, editorContent, editorSettings);
        }
    }
}
