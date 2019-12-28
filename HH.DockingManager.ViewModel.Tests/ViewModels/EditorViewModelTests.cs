using System.Windows.Input;
using HH.Data.Entity.Model.Interfaces;
using HH.DockingManager.ViewModel.Interfaces;
using HH.DockingManager.ViewModel.ViewModels;
using HH.Icons.Model.Enums;
using HH.TestUtils;
using HH.ViewModel.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.DockingManager.ViewModel.Tests.ViewModels
{
    [TestFixture]
    internal sealed class EditorViewModelTests
    {
        private AutoMocker _autoMocker;
        private IEditorManager _editorManager;
        private IEntity _model;
        private IEditorContent _content;
        private IEditorSettings _editorSettings;
        private ICommandFactory _commandFactory;
        private ICommand _closeEditorCommand;
        private IEditorViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _editorManager = _autoMocker.Mock<IEditorManager>();
            _model = _autoMocker.Mock<IEntity>();
            _editorSettings = _autoMocker.Mock<IEditorSettings>();
            _editorSettings.Expect(c => c.IconSource).Return(GlyphType.DataSet);
            _editorSettings.Expect(c => c.ToolTip).Return("asd");
            _editorSettings.Expect(c => c.Title).Return("Title 123");
            _content = _autoMocker.Mock<IEditorContent>();
            _commandFactory = _autoMocker.Mock<ICommandFactory>();
            _closeEditorCommand = _autoMocker.Mock<ICommand>();

            _sut = new EditorViewModel(_editorManager, _commandFactory, _model, _content, _editorSettings);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_Sets_Properties()
        {
            //Assert
            Assert.AreEqual(_model, _sut.Model);
            Assert.AreEqual(_content, _sut.Content);
            Assert.AreEqual(GlyphType.DataSet, _sut.IconSource);
            Assert.AreEqual("asd", _sut.ToolTip);
            Assert.AreEqual("Title 123", _sut.Title);
        }

        [Test]
        public void CloseEditorCommand_Returns_Expected_Command()
        {
            //Arrange
            _commandFactory.Expect(c => c.CreateCommand(_sut.CloseEditor))
                .Repeat.Once()
                .Return(_closeEditorCommand);

            //Act
            var resultOne = _sut.CloseEditorCommand;
            var resultTwo = _sut.CloseEditorCommand;

            //Assert
            Assert.AreEqual(_closeEditorCommand, resultOne);
            Assert.AreEqual(_closeEditorCommand, resultTwo);
        }

        [Test]
        public void CloseEditor_Closes_Editor_Correctly()
        {
            //Arrange
            _editorManager.Expect(c => c.CloseEditor(_sut)).Return(true);

            //Act
            _sut.CloseEditor();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanCloseEditor_Returns_Expected_Value(bool canClose)
        {
            //Arrange
            _content.Expect(c => c.CanClose()).Return(canClose);

            //Act
            var result = _sut.CanCloseEditor();

            //Assert
            Assert.AreEqual(canClose, result);
        }

        [Test]
        public void Dispose_Disposes_Correctly()
        {
            //Arrange
            _content.Expect(c => c.Dispose());

            //Act
            _sut.Dispose();
        }
    }
}
