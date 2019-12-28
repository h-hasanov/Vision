using System;
using System.Collections.ObjectModel;
using HH.Data.Entity.Model.Interfaces;

namespace HH.DockingManager.ViewModel.Interfaces
{
    public interface IEditorManager
    {
        IEditorViewModel ActiveEditor { get; set; }

        ReadOnlyObservableCollection<IEditorViewModel> Editors { get; }

        IEditorViewModel DisplayEditor(IEntity entity, Func<IEditorContent> viewContentFactory,
            Func<IEditorSettings> editorSettingsFactory);

        bool CloseEditor(IEditorViewModel editorViewModel);

        bool CloseEditor(IEntity entity);
        bool CanCloseEditor(IEntity entity);

        void CloseAllEditors();
    }
}
