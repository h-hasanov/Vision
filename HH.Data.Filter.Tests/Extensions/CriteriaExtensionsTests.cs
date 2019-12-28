using HH.Data.Filter.Extensions;
using HH.Data.Filter.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Data.Filter.Tests.Extensions
{
    [TestFixture]
    internal sealed class CriteriaExtensionsTests
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
        public void And_Creates_AndLogicCriteria_Correctly()
        {
            //Arrange
            var firstCriteria = _autoMocker.Mock<ICriteria<IPerson>>();
            var secondCriteria = _autoMocker.Mock<ICriteria<IPerson>>();

            //Act
            var result = firstCriteria.And(secondCriteria);

            //Assert
            Assert.IsTrue(result is IAndLogicCriteria<IPerson>);
        }

        [Test]
        public void Or_Creates_OrLogicCriteria_Correctly()
        {
            //Arrange
            var firstCriteria = _autoMocker.Mock<ICriteria<IPerson>>();
            var secondCriteria = _autoMocker.Mock<ICriteria<IPerson>>();

            //Act
            var result = firstCriteria.Or(secondCriteria);

            //Assert
            Assert.IsTrue(result is IOrLogicCriteria<IPerson>);
        }

        [Test]
        public void Not_Creates_NotLogicCriteria_Correctly()
        {
            //Arrange
            var criteria = _autoMocker.Mock<ICriteria<IPerson>>();

            //Act
            var result = criteria.Not();

            //Assert
            Assert.IsTrue(result is INotLogicCriteria<IPerson>);
        }

        [Test]
        public void Nand_Creates_NandLogicCriteria_Correctly()
        {
            //Arrange
            var firstCriteria = _autoMocker.Mock<ICriteria<IPerson>>();
            var secondCriteria = _autoMocker.Mock<ICriteria<IPerson>>();

            //Act
            var result = firstCriteria.Nand(secondCriteria);

            //Assert
            Assert.IsTrue(result is INandLogicCriteria<IPerson>);
        }

        [Test]
        public void Nor_Creates_NorLogicCriteria_Correctly()
        {
            //Arrange
            var firstCriteria = _autoMocker.Mock<ICriteria<IPerson>>();
            var secondCriteria = _autoMocker.Mock<ICriteria<IPerson>>();

            //Act
            var result = firstCriteria.Nor(secondCriteria);

            //Assert
            Assert.IsTrue(result is INorLogicCriteria<IPerson>);
        }

        [Test]
        public void Xor_Creates_XorLogicCriteria_Correctly()
        {
            //Arrange
            var firstCriteria = _autoMocker.Mock<ICriteria<IPerson>>();
            var secondCriteria = _autoMocker.Mock<ICriteria<IPerson>>();

            //Act
            var result = firstCriteria.Xor(secondCriteria);

            //Assert
            Assert.IsTrue(result is IXorLogicCriteria<IPerson>);
        }

        [Test]
        public void Xnor_Creates_XnorLogicCriteria_Correctly()
        {
            //Arrange
            var firstCriteria = _autoMocker.Mock<ICriteria<IPerson>>();
            var secondCriteria = _autoMocker.Mock<ICriteria<IPerson>>();

            //Act
            var result = firstCriteria.Xnor(secondCriteria);

            //Assert
            Assert.IsTrue(result is IXnorLogicCriteria<IPerson>);
        }
    }
}
