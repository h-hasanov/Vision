using HH.TestUtils;
using HH.ViewModel.ContextMenu.Implementations;
using HH.ViewModel.ContextMenu.Interfaces;
using NUnit.Framework;

namespace HH.ViewModel.Tests.ContextMenu.Implementations
{
    [TestFixture]
    internal sealed class ContextMenuViewModelTests
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
        public void Ctor_Creates_Correctly()
        {
            //Arrange
            var menuItems = new[] { _autoMocker.Mock<IMenuItemViewModel>(), _autoMocker.Mock<IMenuItemViewModel>() };

            //Act
            var sut = new ContextMenuViewModel(menuItems);

            //Assert
            Assert.AreEqual(menuItems, sut.MenuItems);
        }
    }
}
