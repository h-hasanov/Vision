using System;
using HH.EnvironmentServices.BaseModels;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.EnvironmentServices.Tests.BaseModels
{
    [TestFixture]
    internal sealed class DisposableBaseTests
    {
        private AutoMocker _autoMocker;
        private TestDisposable _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new TestDisposable();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void ThrowExceptionIfDisposed_Throws_If_Disposed()
        {
            //Arrange
            _sut.SetDisposed();

            //Act
            _sut.Dispose();

            //Assert
            Assert.Throws<ObjectDisposedException>(() => _sut.ThrowException());
        }

        [Test]
        public void Dispose_Executes_Only_Once()
        {
            //Act
            _sut.Dispose();
            _sut.Dispose();

            //Assert
            Assert.AreEqual(1, _sut.NumberOfDisposedTimes);
        }
    }

    public sealed class TestDisposable : DisposableBase
    {
        public int NumberOfDisposedTimes { get; set; }

        public void SetDisposed()
        {
            Disposed = true;
        }

        public void ThrowException()
        {
            ThrowExceptionIfDisposed();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            NumberOfDisposedTimes++;
        }
    }
}
