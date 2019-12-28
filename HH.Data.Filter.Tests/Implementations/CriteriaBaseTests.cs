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
    internal sealed class CriteriaBaseTests
    {
        private AutoMocker _autoMocker;
        private ICriteria<IPerson> _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new PersonCriteria();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
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
    }

    public sealed class PersonCriteria : CriteriaBase<IPerson>
    {
        public override bool MeetsCriteria(IPerson entity)
        {
            return entity.Age == 3;
        }
    }
}
