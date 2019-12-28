using System;
using System.Linq;
using HH.Data.Filter.Implementations;
using HH.Data.Filter.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.Data.Filter.Tests.Implementations
{
    [TestFixture]
    internal sealed class NotLogicCriteriaTests
    {
        private AutoMocker _autoMocker;
        private ICriteria<IPerson> _originalCriteria;
        private INotLogicCriteria<IPerson> _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();

            _originalCriteria = _autoMocker.Mock<ICriteria<IPerson>>();

            _sut = new NotLogicCriteria<IPerson>(_originalCriteria);
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
            Assert.AreEqual(_originalCriteria, _sut.Criteria);
        }

        [TestCase(false, true)]
        [TestCase(true, false)]
        public void MeetsCriteria_Returns_Correctly(bool originalResult, bool expectedOutcome)
        {
            //Arrange
            var person = _autoMocker.Mock<IPerson>();
            _originalCriteria.Expect(c => c.MeetsCriteria(person)).Return(originalResult);

            //Act
            var result = _sut.MeetsCriteria(person);

            //Assert
            Assert.AreEqual(expectedOutcome, result);
        }

        [Test]
        public void MeetCriteria_Returns_Correctly()
        {
            //Arrange
            var personOne = _autoMocker.Mock<IPerson>();
            _originalCriteria.Expect(c => c.MeetsCriteria(personOne)).Repeat.Once().Return(false);

            var personTwo = _autoMocker.Mock<IPerson>();
            _originalCriteria.Expect(c => c.MeetsCriteria(personTwo)).Repeat.Once().Return(true);
            

            //Act
            var result = _sut.MeetCriteria(new[] { personOne, personTwo });

            //Assert
            CollectionAssert.AreEquivalent(new[] { personOne }, result);
        }

        [Test]
        public void MeetCriteria_With_EmptyCollection_Returns_Empty_Collection()
        {
            //Act
            var result = _sut.MeetCriteria(Enumerable.Empty<IPerson>());

            //Assert
            CollectionAssert.AreEquivalent(Enumerable.Empty<IPerson>(), result);
        }

        [Test]
        public void MeetCriteria_With_NullCollection_Throws_Exception()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _sut.MeetCriteria(null));
        }
    }
}
