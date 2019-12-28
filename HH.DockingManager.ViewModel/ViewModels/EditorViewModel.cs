using System.Windows.Input;
using HH.Data.Entity.Model.Interfaces;
using HH.DockingManager.ViewModel.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.ViewModels
{
    internal sealed class EditorViewModel : DockableViewModelBase, IEditorViewModel
    {
        private readonly IEditorManager _editorManager;
        private readonly ICommandFactory _commandFactory;
        private ICommand _closeEditorCommand;

        public EditorViewModel(IEditorManager editorManager,
            ICommandFactory commandFactory,
            IEntity model, 
            IEditorContent editorContent, 
            IEditorSettings editorSettings)
        {
            _editorManager = editorManager.ArgumentNullCheck(nameof(editorManager));
            Model = model.ArgumentNullCheck(nameof(model));
            Content = editorContent.ArgumentNullCheck(nameof(editorContent));
            editorSettings.ArgumentNullCheck(nameof(editorSettings));
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));

            IconSource = editorSettings.IconSource;
            ToolTip = editorSettings.ToolTip;
            Title = editorSettings.Title;
        }

        public IEntity Model { get; }
        public IEditorContent Content { get; }

        public ICommand CloseEditorCommand
        {
            get
            {
                return _closeEditorCommand ??
                       (_closeEditorCommand = _commandFactory.CreateCommand(CloseEditor));
            }
        }

        #region Methods

        public void CloseEditor()
        {
            _editorManager.CloseEditor(this);
        }

        public bool CanCloseEditor()
        {
            return Content.CanClose();
        } 

        #endregion

        #region Dispose

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            Content?.Dispose();
        }

        #endregion
    }
}
