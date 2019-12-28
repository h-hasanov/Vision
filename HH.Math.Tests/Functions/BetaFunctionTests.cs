using HH.Math.Functions;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Math.Tests.Functions
{
    [TestFixture]
    internal sealed class BetaFunctionTests
    {
        [TestCase(1, 2, 0.5)]
        [TestCase(2.2, 1.2, 0.3393393)]
        [TestCase(0.2, 0.8, 5.344797)]
        [TestCase(0.1, 0.1, 19.71464)]
        [TestCase(0.1, 0.05, 29.7782445)]
        public void BetaFunction_Returns_ExpectedValue(double alpha, double beta, double expectedResult)
        {
            //Act
            var result = BetaFunction.Beta(alpha, beta);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.HighToleranceDelta);
        }

        [TestCase(1, 2, 0.51)]
        [TestCase(2.2, 1.2, 0.090427215777547212d)]
        [TestCase(0.2, 0.8, 0.74350674142019746d)]
        [TestCase(0.1, 0.1, 0.46280418611555296d)]
        [TestCase(0.1, 0.05, 0.30692499061671885d)]
        public void BetaIncompleteRegularized_Returns_ExpectedValue(double alpha, double beta, double expectedResult)
        {
            //Act
            var result = BetaFunction.BetaIncompleteRegularized(alpha, beta, 0.3);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
