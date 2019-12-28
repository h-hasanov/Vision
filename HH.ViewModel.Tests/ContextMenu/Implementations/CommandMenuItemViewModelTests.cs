using System.Windows.Input;
using HH.Icons.Model.Enums;
using HH.TestUtils;
using HH.ViewModel.ContextMenu.Implementations;
using NUnit.Framework;

namespace HH.ViewModel.Tests.ContextMenu.Implementations
{
    [TestFixture]
    internal sealed class CommandMenuItemViewModelTests
    {
        private AutoMocker _autoMocker;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_Without_Icon_Creates_Correctly()
        {
            //Arrange
            const string header = "asdsad";
            var command = _autoMocker.Mock<ICommand>();

            //Act
            var sut = new CommandMenuItemViewModel(header, command);

            //Assert
            Assert.IsNull(sut.Icon);
            Assert.AreEqual(header, sut.Header);
            Assert.AreEqual(command, sut.Command);
        }

        [Test]
        public void Ctor_With_Icon_Creates_Correctly()
        {
            //Arrange
            const string header = "asdsad";
            const GlyphType icon = GlyphType.OpenEditor;
            var command = _autoMocker.Mock<ICommand>();

            //Act
            var sut = new CommandMenuItemViewModel(header, icon, command);

            //Assert
            Assert.AreEqual(icon, sut.Icon);
            Assert.AreEqual(header, sut.Header);
            Assert.AreEqual(command, sut.Command);
        }
    }
}
