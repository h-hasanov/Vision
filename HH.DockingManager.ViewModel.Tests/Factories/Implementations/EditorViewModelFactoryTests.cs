using HH.Data.Entity.Model.Interfaces;
using HH.DockingManager.ViewModel.Factories.Implementations;
using HH.DockingManager.ViewModel.Factories.Interfaces;
using HH.DockingManager.ViewModel.Interfaces;
using HH.DockingManager.ViewModel.ViewModels;
using HH.Icons.Model.Enums;
using HH.TestUtils;
using HH.ViewModel.Interfaces;
using NUnit.Framework;

namespace HH.DockingManager.ViewModel.Tests.Factories.Implementations
{
    [TestFixture]
    internal sealed class EditorViewModelFactoryTests
    {
        private AutoMocker _autoMocker;
        private IEditorViewModelFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new EditorViewModelFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreatEditorViewModel_Creates_EditorViewModel_Correctly()
        {
            //Arrange
            var editorSettings = new EditorSettings { IconSource = GlyphType.DataSet, ToolTip = "blah" };
            var editorViewModelManager = _autoMocker.Mock<IEditorManager>();
            var model = _autoMocker.Mock<IEntity>();
            var editorContent = _autoMocker.Mock<IEditorContent>();
            var commandFactory = _autoMocker.Mock<ICommandFactory>();

            //Act
            var result = _sut.CreatEditorViewModel(editorViewModelManager, commandFactory, model, editorContent, editorSettings);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
