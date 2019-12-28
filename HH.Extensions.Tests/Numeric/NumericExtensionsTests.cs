using HH.Extensions.Numeric;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Extensions.Tests.Numeric
{
    [TestFixture]
    internal sealed class NumericExtensionsTests
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

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(-1, 1)]
        [TestCase(-3.2, 10.240000000000002)]
        [TestCase(3.2, 10.240000000000002)]
        public void Square_Squares_Values_Correctly(double input, double inputSquared)
        {
            Assert.AreEqual(inputSquared, input.Square());
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(-1, 1)]
        [TestCase(-3, 9)]
        [TestCase(3, 9)]
        public void Square_Squares_Values_Correctly_With_Int(int input, double inputSquared)
        {
            Assert.AreEqual(inputSquared, input.Square());
        }

        [TestCase(double.NaN, true)]
        [TestCase(double.PositiveInfinity, true)]
        [TestCase(double.NegativeInfinity, true)]
        [TestCase(double.Epsilon, false)]
        [TestCase(0, false)]
        [TestCase(-1, false)]
        [TestCase(-10, false)]
        public void IsNaNOrInfinity_Retunrs_Correct_Value(double input, bool isNaNorInfinity)
        {
            Assert.AreEqual(isNaNorInfinity, input.IsNaNOrInifinity());
        }
    }
}
