using System;
using HH.Data.Entity.Model.Interfaces;
using HH.DockingManager.ViewModel.Factories.Interfaces;
using HH.DockingManager.ViewModel.Interfaces;
using HH.DockingManager.ViewModel.Services;
using HH.TestUtils;
using HH.ViewModel.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.DockingManager.ViewModel.Tests.Services
{
    [TestFixture]
    internal sealed class EditorManagerTests
    {
        private AutoMocker _autoMocker;
        private IEditorViewModelFactory _editorViewModelFactory;
        private ICommandFactory _commandFactory;
        private IEditorManager _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _editorViewModelFactory = _autoMocker.Mock<IEditorViewModelFactory>();
            _commandFactory = _autoMocker.Mock<ICommandFactory>();
            _sut = new EditorManager(_commandFactory, _editorViewModelFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_Default_Creates_Correctly()
        {
            //Act
            var sut = new EditorManager(_commandFactory);

            //Assert
            Assert.IsNotNull(sut.EditorItems);
            Assert.IsNotNull(sut.EditorsMapper);
            Assert.IsNotNull(sut.Editors);
            Assert.IsNull(_sut.ActiveEditor);
        }

        [Test]
        public void Ctor_Sets_Properties()
        {
            //Assert
            Assert.IsNotNull(_sut.Editors);
            Assert.IsNull(_sut.ActiveEditor);
        }

        [Test]
        public void ActiveEditor_Sets_Correctly_If_EditorViewModel_Contained()
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();
            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();

            ((EditorManager)_sut).EditorItems.Add(editorViewModel);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntity, editorViewModel);

            editorViewModel.Expect(c => c.IsActive = true);
            editorViewModel.Expect(c => c.IsSelected = true);

            //Act
            _sut.ActiveEditor = editorViewModel;

            //Assert
            Assert.AreEqual(editorViewModel, _sut.ActiveEditor);
        }

        [Test]
        public void ActiveEditor_Set_Throws_If_NewEditor_Not_Contained_In_Editors()
        {
            //Arrange
            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();

            //Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.ActiveEditor = editorViewModel);
        }

        [Test]
        public void ActiveEditor_Set_DoesNotThrow_If_NewEditor_Null()
        {
            //Arrange &Act & Assert
            Assert.DoesNotThrow(() => _sut.ActiveEditor = null);
        }

        [Test]
        public void DisplayEditor_Creates_New_Editor_If_Not_Already_Created()
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();
            var editorContent = _autoMocker.Mock<IEditorContent>();
            var editorContentFactory = _autoMocker.Mock<Func<IEditorContent>>();
            editorContentFactory.Expect(c => c.Invoke()).Return(editorContent);
            var editorSettings = _autoMocker.Mock<IEditorSettings>();
            var editorSettingsFactory = _autoMocker.Mock<Func<IEditorSettings>>();
            editorSettingsFactory.Expect(c => c.Invoke()).Return(editorSettings);

            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();
            _editorViewModelFactory.Expect(c => c.CreatEditorViewModel(_sut, _commandFactory, modelEntity, editorContent, editorSettings))
                .Return(editorViewModel);
            editorViewModel.Expect(c => c.IsSelected = true);
            editorViewModel.Expect(c => c.IsActive = true);

            //Act
            _sut.DisplayEditor(modelEntity, editorContentFactory, editorSettingsFactory);

            //Assert
            Assert.IsTrue(_sut.Editors.Contains(editorViewModel));
            Assert.AreEqual(editorViewModel, _sut.ActiveEditor);
        }

        [Test]
        public void DisplayEditor_Displays_ExistingEditor()
        {
            //Arrange
            var modelEntityOne = _autoMocker.Mock<IEntity>();
            var editorViewModelOne = _autoMocker.Mock<IEditorViewModel>();
            ((EditorManager)_sut).EditorItems.Add(editorViewModelOne);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntityOne, editorViewModelOne);

            var modelEntityTwo = _autoMocker.Mock<IEntity>();
            var editorViewModelTwo = _autoMocker.Mock<IEditorViewModel>();
            ((EditorManager)_sut).EditorItems.Add(editorViewModelTwo);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntityTwo, editorViewModelTwo);

            var modelEntityThree = _autoMocker.Mock<IEntity>();
            var editorViewModelThree = _autoMocker.Mock<IEditorViewModel>();
            ((EditorManager)_sut).EditorItems.Add(editorViewModelThree);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntityThree, editorViewModelThree);

            editorViewModelTwo.Expect(c => c.IsSelected = true);
            editorViewModelTwo.Expect(c => c.IsActive = true);


            //Act
            _sut.DisplayEditor(modelEntityTwo, null, null);

            //Assert
            Assert.AreEqual(editorViewModelTwo, _sut.ActiveEditor);
        }

        [Test]
        public void CloseEditor_With_EditorViewModel_Closes_Correctly()
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();
            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();
            editorViewModel.Expect(c => c.Dispose());
            editorViewModel.Expect(c => c.CanCloseEditor()).Return(true);

            ((EditorManager)_sut).EditorItems.Add(editorViewModel);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntity, editorViewModel);

            //Act
            var result = _sut.CloseEditor(editorViewModel);

            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(!_sut.Editors.Contains(editorViewModel));
            Assert.IsTrue(!((EditorManager)_sut).EditorsMapper.ContainsKey(modelEntity));
        }

        [Test]
        public void CloseEditor_With_EditorViewModel_DoesNot_Close_If_Editor_Rejects()
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();
            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();
            editorViewModel.Expect(c => c.CanCloseEditor()).Return(false);

            ((EditorManager)_sut).EditorItems.Add(editorViewModel);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntity, editorViewModel);

            //Act
            var result = _sut.CloseEditor(editorViewModel);

            //Assert
            Assert.IsFalse(result);
            Assert.IsTrue(_sut.Editors.Contains(editorViewModel));
            Assert.IsTrue(((EditorManager)_sut).EditorsMapper.ContainsKey(modelEntity));
        }

        [Test]
        public void CloseEditor_With_EditorViewModel_If_ActiveEditor_Closes_Correctly()
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();
            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();
            editorViewModel.Expect(c => c.Dispose());
            editorViewModel.Expect(c => c.CanCloseEditor()).Return(true);

            ((EditorManager)_sut).EditorItems.Add(editorViewModel);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntity, editorViewModel);
            _sut.ActiveEditor = editorViewModel;

            //Act
            var result = _sut.CloseEditor(editorViewModel);

            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(!_sut.Editors.Contains(editorViewModel));
            Assert.IsTrue(!((EditorManager)_sut).EditorsMapper.ContainsKey(modelEntity));
            Assert.IsNull(_sut.ActiveEditor);
        }


        [Test]
        public void CloseEditor_With_EditorViewModel_Does_Not_Close_If_Does_Not_Exist()
        {
            //Arrange
            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();

            //Act
            var result = _sut.CloseEditor(editorViewModel);

            //Assert
            Assert.IsTrue(result);
            editorViewModel.AssertWasNotCalled(c => c.Dispose());
        }

        [Test]
        public void CloseEditor_With_Entity_Closes_Correctly()
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();
            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();
            editorViewModel.Expect(c => c.Dispose());
            editorViewModel.Expect(c => c.CanCloseEditor()).Return(true);

            ((EditorManager)_sut).EditorItems.Add(editorViewModel);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntity, editorViewModel);

            //Act
            var result = _sut.CloseEditor(modelEntity);

            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(!_sut.Editors.Contains(editorViewModel));
            Assert.IsTrue(!((EditorManager)_sut).EditorsMapper.ContainsKey(modelEntity));
        }

        [Test]
        public void CloseEditor_With_Entity_DoesNot_Close_If_Editor_Rejects()
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();
            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();
            editorViewModel.Expect(c => c.CanCloseEditor()).Return(false);

            ((EditorManager)_sut).EditorItems.Add(editorViewModel);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntity, editorViewModel);

            //Act
            var result = _sut.CloseEditor(modelEntity);

            //Assert
            Assert.IsFalse(result);
            Assert.IsTrue(_sut.Editors.Contains(editorViewModel));
            Assert.IsTrue(((EditorManager)_sut).EditorsMapper.ContainsKey(modelEntity));
        }

        [Test]
        public void CloseEditor_With_Entity_If_ActiveEditor_Closes_Correctly()
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();
            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();
            editorViewModel.Expect(c => c.Dispose());
            editorViewModel.Expect(c => c.CanCloseEditor()).Return(true);

            ((EditorManager)_sut).EditorItems.Add(editorViewModel);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntity, editorViewModel);
            _sut.ActiveEditor = editorViewModel;

            //Act
            var result = _sut.CloseEditor(modelEntity);

            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(!_sut.Editors.Contains(editorViewModel));
            Assert.IsTrue(!((EditorManager)_sut).EditorsMapper.ContainsKey(modelEntity));
            Assert.IsNull(_sut.ActiveEditor);
        }

        [Test]
        public void CloseEditor_With_Entity_Does_Not_Close_If_Does_Not_Exist()
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();

            //Act
            var result = _sut.CloseEditor(modelEntity);

            //Assert
            Assert.IsTrue(result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanCloseEditor_With_Entity_Returns_Expected_Value(bool canClose)
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();
            var editorViewModel = _autoMocker.Mock<IEditorViewModel>();
            editorViewModel.Expect(c => c.CanCloseEditor()).Return(canClose);

            ((EditorManager)_sut).EditorItems.Add(editorViewModel);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntity, editorViewModel);
            _sut.ActiveEditor = editorViewModel;

            //Act
            var result = _sut.CanCloseEditor(modelEntity);

            //Assert
            Assert.AreEqual(canClose, result);
        }

        [Test]
        public void CanCloseEditor_With_Entity_Returns_True_If_Does_Not_Exist()
        {
            //Arrange
            var modelEntity = _autoMocker.Mock<IEntity>();

            //Act
            var result = _sut.CanCloseEditor(modelEntity);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CloseAllEditors_Closes_All_Editors_Correctly()
        {
            //Arrange
            var modelEntityOne = _autoMocker.Mock<IEntity>();
            var editorViewModelOne = _autoMocker.Mock<IEditorViewModel>();
            editorViewModelOne.Expect(c => c.CanCloseEditor()).Return(true);
            ((EditorManager)_sut).EditorItems.Add(editorViewModelOne);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntityOne, editorViewModelOne);

            var modelEntityTwo = _autoMocker.Mock<IEntity>();
            var editorViewModelTwo = _autoMocker.Mock<IEditorViewModel>();
            editorViewModelTwo.Expect(c => c.CanCloseEditor()).Return(true);
            ((EditorManager)_sut).EditorItems.Add(editorViewModelTwo);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntityTwo, editorViewModelTwo);

            var modelEntityThree = _autoMocker.Mock<IEntity>();
            var editorViewModelThree = _autoMocker.Mock<IEditorViewModel>();
            editorViewModelThree.Expect(c => c.CanCloseEditor()).Return(true);
            ((EditorManager)_sut).EditorItems.Add(editorViewModelThree);
            ((EditorManager)_sut).EditorsMapper.Add(modelEntityThree, editorViewModelThree);

            //Act
            _sut.CloseAllEditors();

            //Assert
            Assert.IsEmpty(_sut.Editors);
            Assert.IsEmpty(((EditorManager)_sut).EditorsMapper);
        }
    }
}
