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
    internal sealed class LogicCriteriaBaseTests
    {
        private AutoMocker _autoMocker;
        private ICriteria<IPerson> _firstCriteria;
        private ICriteria<IPerson> _secondCriteria;
        private ILogicCriteria<IPerson> _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _firstCriteria = _autoMocker.Mock<ICriteria<IPerson>>();
            _secondCriteria = _autoMocker.Mock<ICriteria<IPerson>>();

            _sut = new PersonLogicCriteria(_firstCriteria, _secondCriteria);
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

        [TestCase(true)]
        [TestCase(false)]
        public void MeetsCriteria_Returns_Correctly(bool expectedOutcome)
        {
            //Arrange
            var age = expectedOutcome ? 3 : 4;
            var person = GetPerson(age);

            //Act
            var result = _sut.MeetsCriteria(person);

            //Assert
            Assert.AreEqual(expectedOutcome, result);
        }

        [Test]
        public void MeetCriteria_Returns_Correctly()
        {
            //Arrange
            var personOne = GetPerson(4);
            var personTwo = GetPerson(3);
            var personThree = GetPerson(10);

            //Act
            var result = _sut.MeetCriteria(new[] { personOne, personTwo, personThree });

            //Assert
            CollectionAssert.AreEquivalent(new[] { personTwo }, result);
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

        private IPerson GetPerson(int age)
        {
            var person = _autoMocker.Mock<IPerson>();
            person.Expect(c => c.Age).Return(age);

            return person;
        }

        public sealed class PersonLogicCriteria : LogicCriteriaBase<IPerson>
        {
            public PersonLogicCriteria(ICriteria<IPerson> firstCriteria, ICriteria<IPerson> secondCriteria)
                : base(firstCriteria, secondCriteria)
            {
            }

            public override bool MeetsCriteria(IPerson entity)
            {
                return entity.Age == 3;
            }
        }
    }
}
