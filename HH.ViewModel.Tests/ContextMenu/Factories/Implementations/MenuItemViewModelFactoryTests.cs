using System;
using System.Windows.Input;
using HH.Icons.Model.Enums;
using HH.TestUtils;
using HH.ViewModel.ContextMenu.Factories.Implementations;
using HH.ViewModel.ContextMenu.Factories.Interfaces;
using HH.ViewModel.ContextMenu.Interfaces;
using NUnit.Framework;

namespace HH.ViewModel.Tests.ContextMenu.Factories.Implementations
{
    [TestFixture]
    internal sealed class MenuItemViewModelFactoryTests
    {
        private AutoMocker _autoMocker;
        private IMenuItemViewModelFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new MenuItemViewModelFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateContextMenuViewModel_Creates_ContextMenuViewModel()
        {
            //Arrange
            var menuItems = new IMenuItemViewModel[0];

            //Act
            var result = _sut.CreateContextMenuViewModel(menuItems);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateCompositeMenuItemViewModel_Creates_CompositeMenuItemViewModel()
        {
            //Arrange
            const string header = "asd";
            var menuItems = new IMenuItemViewModel[0];

            //Act
            var result = _sut.CreateCompositeMenuItemViewModel(header,menuItems);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateCommandMenuItemViewModel_Creates_CommandMenuItemViewModel()
        {
            //Arrange
            const string header = "asd";
            const GlyphType icon = GlyphType.Rename;
            var command = _autoMocker.Mock<ICommand>();

            //Act
            var result = _sut.CreateCommandMenuItemViewModel(header, icon, command);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreeateSeparatorMenuItemViewModel_Throws()
        {
            Assert.Throws<NotImplementedException>(() => _sut.CreeateSeparatorMenuItemViewModel());
        }
    }
}
