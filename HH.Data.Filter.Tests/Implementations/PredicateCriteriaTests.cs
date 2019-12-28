using System;
using HH.Data.Filter.Implementations;
using HH.Data.Filter.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.Data.Filter.Tests.Implementations
{
    [TestFixture]
    internal sealed class PredicateCriteriaTests
    {
        private AutoMocker _autoMocker;
        private Predicate<IPerson> _predicate;
        private ICriteria<IPerson> _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _predicate = _autoMocker.Mock<Predicate<IPerson>>();

            _sut = new PredicateCriteria<IPerson>(_predicate);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MeetsCriteria_Returns_Expected_Value(bool meetsCriteria)
        {
            //Arrange
            var person = _autoMocker.Mock<IPerson>();
            _predicate.Expect(c => c(person)).Return(meetsCriteria);

            //Act
            var result = _sut.MeetsCriteria(person);

            //Assert
            Assert.AreEqual(meetsCriteria, result);
        }
    }
}
