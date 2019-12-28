using HH.DependencyInjection.Enums;
using HH.DependencyInjection.Implementations;
using HH.DependencyInjection.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.DependencyInjection.Tests.Implementations
{
    [TestFixture]
    internal sealed class BootstrapperBaseTests
    {
        private AutoMocker _autoMocker;
        private IContainer _container;
        private IBootstrapper _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _container = _autoMocker.Mock<IContainer>();
            _sut = new TestBootstrapper(_container);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_With_No_Container_Creates_Container()
        {
            //Arrange

            //Act
            _sut = new TestBootstrapper();

            //Assert
            Assert.IsNotNull(_sut.Container);
        }

        [Test]
        public void Setup_Setups_Correctly()
        {
            //Arrange
            _container.Expect(c => c.RegisterSingle<IResolver, Resolver>(null)).Repeat.Once().IgnoreArguments();
            _container.Expect(c => c.Register<IPerson, Person>(LifeSpan.Transient));
            _container.Expect(c => c.Verify());

            //Act
            _sut.Setup();

            //Assert
            Assert.AreEqual(_container, _sut.Container);
            Assert.IsTrue(((TestBootstrapper)_sut).SetupInternalWasCalled);
        }
    }

    internal sealed class TestBootstrapper : BootstrapperBase
    {
        public bool SetupInternalWasCalled { get; set; }

        public TestBootstrapper()
        {

        }

        public TestBootstrapper(IContainer container) : base(container)
        {

        }

        protected override void SetupInternal(IContainer container)
        {
            SetupInternalWasCalled = true;
            container.Register<IPerson, Person>(LifeSpan.Transient);
        }
    }
}
