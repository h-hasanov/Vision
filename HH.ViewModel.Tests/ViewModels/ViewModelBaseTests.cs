using HH.TestUtils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.ViewModels;
using NUnit.Framework;

namespace HH.ViewModel.Tests.ViewModels
{
    [TestFixture]
    internal sealed class ViewModelBaseTests
    {
        private AutoMocker _autoMocker;
        private IViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new TestViewModel();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }
    }

    // ReSharper disable once RedundantExtendsListEntry
    internal sealed class TestViewModel : ViewModelBase, IViewModel
    {
    }
}
