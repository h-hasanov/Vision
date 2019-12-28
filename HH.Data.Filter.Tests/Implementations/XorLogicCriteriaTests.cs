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
    internal sealed class XorLogicCriteriaTests
    {
        private AutoMocker _autoMocker;
        private ICriteria<IPerson> _firstCriteria;
        private ICriteria<IPerson> _secondCriteria;
        private IXorLogicCriteria<IPerson> _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();

            _firstCriteria = _autoMocker.Mock<ICriteria<IPerson>>();
            _secondCriteria = _autoMocker.Mock<ICriteria<IPerson>>();

            _sut = new XorLogicCriteria<IPerson>(_firstCriteria, _secondCriteria);
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
            Assert.AreEqual(_firstCriteria, _sut.FirstCriteria);
            Assert.AreEqual(_secondCriteria, _sut.SecondCriteria);
        }

        [TestCase(false, false, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        public void MeetsCriteria_Returns_Correctly(bool firstResult, bool secondResult, bool expectedOutcome)
        {
            //Arrange
            var person = _autoMocker.Mock<IPerson>();
            _firstCriteria.Expect(c => c.MeetsCriteria(person)).Return(firstResult);
            _secondCriteria.Expect(c => c.MeetsCriteria(person)).Return(secondResult);

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
            _firstCriteria.Expect(c => c.MeetsCriteria(personOne)).Repeat.Once().Return(false);
            _secondCriteria.Expect(c => c.MeetsCriteria(personOne)).Repeat.Once().Return(false);

            var personTwo = _autoMocker.Mock<IPerson>();
            _firstCriteria.Expect(c => c.MeetsCriteria(personTwo)).Repeat.Once().Return(false);
            _secondCriteria.Expect(c => c.MeetsCriteria(personTwo)).Repeat.Once().Return(true);

            var personThree = _autoMocker.Mock<IPerson>();
            _firstCriteria.Expect(c => c.MeetsCriteria(personThree)).Repeat.Once().Return(true);
            _secondCriteria.Expect(c => c.MeetsCriteria(personThree)).Repeat.Once().Return(false);

            var personFour = _autoMocker.Mock<IPerson>();
            _firstCriteria.Expect(c => c.MeetsCriteria(personFour)).Repeat.Once().Return(true);
            _secondCriteria.Expect(c => c.MeetsCriteria(personFour)).Repeat.Once().Return(true);

            //Act
            var result = _sut.MeetCriteria(new[] { personOne, personTwo, personThree, personFour });

            //Assert
            CollectionAssert.AreEquivalent(new[] { personTwo, personThree }, result);
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
