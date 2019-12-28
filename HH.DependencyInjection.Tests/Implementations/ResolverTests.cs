using System;
using HH.DependencyInjection.Implementations;
using HH.DependencyInjection.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.DependencyInjection.Tests.Implementations
{
    [TestFixture]
    internal sealed class ResolverTests
    {
        private AutoMocker _autoMocker;
        private IContainer _container;
        private IResolver _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _container = _autoMocker.Mock<IContainer>();

            var resolver = new Resolver();
            resolver.SetContainer(_container);
            _sut = resolver;
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void GetInstance_WithT_Gets_Instance_Correctly()
        {
            //Arrange
            var implementation = _autoMocker.Mock<ITestInterface>();
            _container.Expect(c => c.GetInstance<ITestInterface>()).Return(implementation);

            //Act
            var result = _sut.GetInstance<ITestInterface>();

            //Assert
            Assert.AreEqual(implementation, result);
        }

        [Test]
        public void GetInstance_WithType_Gets_Instance_Correctly()
        {
            //Arrange
            var implementation = _autoMocker.Mock<ITestInterface>();
            var type = typeof(ITestInterface);
            _container.Expect(c => c.GetInstance(type)).Return(implementation);

            //Act
            var result = _sut.GetInstance(type);

            //Assert
            Assert.AreEqual(implementation, result);
        }

        [Test]
        public void GetInstance_With_Argument_Gets_Instance_Correctly()
        {
            //Arrange
            var person = new Person();
            var testInterface = _autoMocker.Mock<ITestInterface>();
            var anotherTestInterface = _autoMocker.Mock<IAnotherTestInterface>();
            _container.Expect(c => c.GetInstance(typeof(ITestInterface))).Repeat.Once().Return(testInterface);
            _container.Expect(c => c.GetInstance(typeof(IAnotherTestInterface))).Repeat.Once().Return(anotherTestInterface);

            //Act
            var result = _sut.GetInstance<TestResolvedClass, TestResolvedClass>("person", person);

            //Assert
            Assert.AreEqual(person, result.Person);
            Assert.AreEqual(testInterface, result.TestInterface);
            Assert.AreEqual(anotherTestInterface, result.AnotherTestInterface);
        }

        [Test]
        public void GetInstance_With_Argument_Throws_If_Argument_DoesNot_Match_Type()
        {
            //Arrange
            var notPerson = new AutoMocker();
            var testInterface = _autoMocker.Mock<ITestInterface>();
            _container.Expect(c => c.GetInstance(typeof(ITestInterface))).Repeat.Once().Return(testInterface);

            //Assert
            Assert.Throws<TypeLoadException>(() => _sut.GetInstance<TestResolvedClass, TestResolvedClass>("person", notPerson));
        }

        [Test]
        public void GetInstance_With_Argument_Throws_If_ArgumentName_Not_Present()
        {
            //Arrange
            var person = _autoMocker.Mock<IPerson>();
            var testInterface = _autoMocker.Mock<ITestInterface>();
            var anotherTestInterface = _autoMocker.Mock<IAnotherTestInterface>();
            _container.Expect(c => c.GetInstance(typeof(ITestInterface))).Repeat.Once().Return(testInterface);
            _container.Expect(c => c.GetInstance(typeof(IPerson))).Repeat.Once().Return(person);
            _container.Expect(c => c.GetInstance(typeof(IAnotherTestInterface))).Repeat.Once().Return(anotherTestInterface);

            //Assert
            Assert.Throws<ArgumentException>(() => _sut.GetInstance<TestResolvedClass, TestResolvedClass>("personblah", _autoMocker.Mock<IPerson>()));
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetInstance_With_Argument_Throws_If_ArgumentName_Invalid(string invalidArgumentName)
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _sut.GetInstance<TestResolvedClass, TestResolvedClass>(invalidArgumentName, _autoMocker.Mock<IPerson>()));
        }
    }

    internal sealed class TestResolvedClass
    {
        public TestResolvedClass(ITestInterface testInterface, IPerson person, IAnotherTestInterface anotherTestInterface)
        {
            TestInterface = testInterface;
            Person = person;
            AnotherTestInterface = anotherTestInterface;
        }

        public ITestInterface TestInterface { get; set; }
        public IPerson Person { get; set; }
        public IAnotherTestInterface AnotherTestInterface { get; set; }
    }

    public interface IAnotherTestInterface
    {

    }
}
