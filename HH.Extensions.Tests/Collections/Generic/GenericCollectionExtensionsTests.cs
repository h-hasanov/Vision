using System.Collections.Generic;
using HH.Extensions.Collections.Generic;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.Extensions.Tests.Collections.Generic
{
    [TestFixture]
    internal sealed class GenericCollectionExtensionsTests
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

        [TestCase("\\")]
        [TestCase(",")]
        [TestCase(";")]
        public void Flatten_Flattens_IEnumerable_Correctly(string separator)
        {
            //Arrange
            var items = new List<object> { "name", 2, "surname" };
            var expectedResult = "name" + separator + "2" + separator + "surname";

            //Act
            var result = items.Flatten(separator);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Flatten_With_NullInput_Returns_EmptyString()
        {
            //Assert
            Assert.AreEqual(string.Empty, default(IEnumerable<string>).Flatten(","));
        }

        [Test]
        public void ForEachOfType_Applies_Action_To_Correct_Type()
        {
            //Arrange
            var item1 = _autoMocker.Mock<IComplexInterface>();
            item1.Expect(c => c.DoSomething());
            var item2 = _autoMocker.Mock<ISimpleInterface>();
            var item3 = _autoMocker.Mock<IComplexInterface>();
            item3.Expect(c => c.DoSomething());
            var items = new[] { item1, item2, item3 };

            //Act
            items.ForEachOfType<IInterfaceWithMethod>(c => c.DoSomething());
        }
    }

    public interface IComplexInterface : ISimpleInterface, IInterfaceWithMethod
    {

    }

    public interface ISimpleInterface
    {

    }

    public interface IInterfaceWithMethod
    {
        void DoSomething();
    }
}
