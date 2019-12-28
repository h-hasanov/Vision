using HH.DockingManager.ViewModel.Interfaces;
using HH.DockingManager.ViewModel.ViewModels;
using HH.Icons.Model.Enums;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.DockingManager.ViewModel.Tests.ViewModels
{
    [TestFixture]
    internal sealed class DockableViewModelBaseTests
    {
        private AutoMocker _autoMocker;
        private IDockableViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new TestDockabelViewModel();
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
            Assert.IsTrue(_sut.IsVisible);
            Assert.IsFalse(_sut.IsActive);
            Assert.IsFalse(_sut.IsSelected);
            Assert.IsNull(_sut.ToolTip);
            Assert.AreEqual(default(GlyphType), _sut.IconSource);
        }
    }

    internal sealed class TestDockabelViewModel : DockableViewModelBase
    {

    }
}
