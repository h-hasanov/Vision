using System.Windows.Input;
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
    internal sealed class PaneViewModelTests
    {
        private AutoMocker _autoMocker;
        private IPaneContent _content;
        private IPaneSettings _paneSettings;
        private ICommandFactory _commandFactory;
        private ICommand _closePaneCommand;
        private ICommand _showPaneCommand;
        private PaneViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _content = _autoMocker.Mock<IPaneContent>();
            _paneSettings = _autoMocker.Mock<IPaneSettings>();
            _paneSettings.Expect(c => c.IconSource).Return(GlyphType.Exit);
            _paneSettings.Expect(c => c.ToolTip).Return("asd");
            _paneSettings.Expect(c => c.ContentId).Return("101");
            _paneSettings.Expect(c => c.ParentPaneName).Return("Parent XYZ");
            _paneSettings.Expect(c => c.Title).Return("Title 123");

            _commandFactory = _autoMocker.Mock<ICommandFactory>();
            _closePaneCommand = _autoMocker.Mock<ICommand>();
            _showPaneCommand = _autoMocker.Mock<ICommand>();

            _commandFactory.Expect(c => c.CreateCommand(null)).IgnoreArguments().Repeat.Once().Return(_closePaneCommand);
            _commandFactory.Expect(c => c.CreateCommand(null)).IgnoreArguments().Repeat.Once().Return(_showPaneCommand);

            _sut = new PaneViewModel(_commandFactory, _content, _paneSettings);
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
            Assert.AreEqual(_content, _sut.Content);
            Assert.AreEqual("101", _sut.ContentId);
            Assert.AreEqual("asd", _sut.ToolTip);
            Assert.AreEqual("Parent XYZ", _sut.ParentPaneName);
            Assert.AreEqual(GlyphType.Exit, _sut.IconSource);
            Assert.AreEqual("Title 123", _sut.Title);
            Assert.AreEqual(_closePaneCommand, _sut.ClosePaneCommand);
            Assert.AreEqual(_showPaneCommand, _sut.ShowPaneCommand);
        }

        [Test]
        public void ClosePaneCommand_ConvertsThePaneToInvisible()
        {
            //Arrange
            _sut.IsVisible = true;

            //Act
            _sut.ClosePane();

            //Assert
            Assert.IsFalse(_sut.IsVisible);
        }

        [Test]
        public void ShowPaneCommand_MakesPaneVisible_Correctly()
        {
            //Arrange
            _sut.IsVisible = false;
            _sut.IsSelected = false;
            _sut.IsActive = false;

            //Act
            _sut.ShowPane();

            //Assert
            Assert.IsTrue(_sut.IsVisible);
            Assert.IsTrue(_sut.IsSelected);
            Assert.IsTrue(_sut.IsActive);
        }
    }
}
