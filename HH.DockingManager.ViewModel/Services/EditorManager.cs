using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HH.Data.Entity.Model.Interfaces;
using HH.DockingManager.ViewModel.Factories.Implementations;
using HH.DockingManager.ViewModel.Factories.Interfaces;
using HH.DockingManager.ViewModel.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.Services
{
    public sealed class EditorManager : IEditorManager
    {
        #region Fields

        private readonly ICommandFactory _commandFactory;
        private readonly IEditorViewModelFactory _editorViewModelFactory;
        internal readonly ObservableCollection<IEditorViewModel> EditorItems = new ObservableCollection<IEditorViewModel>();
        internal readonly IDictionary<IEntity, IEditorViewModel> EditorsMapper = new Dictionary<IEntity, IEditorViewModel>();
        private IEditorViewModel _activeEditor;

        #endregion

        #region Constructors

        public EditorManager(ICommandFactory commandFactory) : this(commandFactory, new EditorViewModelFactory())
        {

        }

        internal EditorManager(ICommandFactory commandFactory, IEditorViewModelFactory editorViewModelFactory)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
            _editorViewModelFactory = editorViewModelFactory.ArgumentNullCheck(nameof(editorViewModelFactory));
            Editors = new ReadOnlyObservableCollection<IEditorViewModel>(EditorItems);
        }

        #endregion

        #region Properties

        public IEditorViewModel ActiveEditor
        {
            get { return _activeEditor; }
            set
            {
                if (value == null)
                {
                    _activeEditor = null;
                }
                else
                {
                    if (!EditorItems.Contains(value))
                    {
                        throw new ArgumentException("EditorViewModel is not contained in Editors");
                    }
                    SetActiveEditor(value);
                }
            }
        }

        #endregion

        public ReadOnlyObservableCollection<IEditorViewModel> Editors { get; }

        #region DisplayEditor

        public IEditorViewModel DisplayEditor(IEntity entity, Func<IEditorContent> viewContentFactory, Func<IEditorSettings> editorSettingsFactory)
        {
            IEditorViewModel editorViewModel;
            if (!EditorsMapper.ContainsKey(entity))
            {
                editorViewModel = _editorViewModelFactory.CreatEditorViewModel(this, _commandFactory, entity, viewContentFactory(),
                    editorSettingsFactory());
                EditorsMapper.Add(entity, editorViewModel);
                EditorItems.Add(editorViewModel);
            }
            else
            {
                editorViewModel = EditorsMapper[entity];
            }
            SetActiveEditor(editorViewModel);
            return editorViewModel;
        }

        private void SetActiveEditor(IEditorViewModel documentViewModel)
        {
            _activeEditor = documentViewModel;

            _activeEditor.IsActive = true;
            _activeEditor.IsSelected = true;
        }

        #endregion

        #region CloseEditor

        public bool CloseEditor(IEditorViewModel editorViewModel)
        {
            var editor = EditorsMapper.FirstOrDefault(c => c.Value == editorViewModel);
            if (editor.Key != null)
            {
                return CloseEditor(editor);
            }
            return true;
        }

        public bool CloseEditor(IEntity entity)
        {
            var editor = EditorsMapper.FirstOrDefault(c => c.Key == entity);
            if (editor.Key != null)
            {
                return CloseEditor(editor);
            }
            return true;
        }

        public bool CanCloseEditor(IEntity entity)
        {
            var editor = EditorsMapper.FirstOrDefault(c => c.Key == entity);
            return editor.Key == null || editor.Value.CanCloseEditor();
        }

        private bool CloseEditor(KeyValuePair<IEntity, IEditorViewModel> editor)
        {
            var editorViewModel = editor.Value;
            if (!editorViewModel.CanCloseEditor())
                return false;

            if (ActiveEditor == editorViewModel)
                ActiveEditor = null;

            EditorsMapper.Remove(editor.Key);
            EditorItems.Remove(editorViewModel);
            editorViewModel.Dispose();
            return true;
        }


        public void CloseAllEditors()
        {
            var editorsCount = EditorItems.Count;

            for (var i = 0; i < editorsCount; i++)
            {
                CloseEditor(EditorItems[0]);
            }
        }

        #endregion
    }
}
