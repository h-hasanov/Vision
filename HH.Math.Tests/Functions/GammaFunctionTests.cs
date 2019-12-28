using HH.Math.Functions;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Math.Tests.Functions
{
    [TestFixture]
    internal sealed class GammaFunctionTests
    {
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(2.5, 1.32934038818)]
        [TestCase(0.5, 1.7724538509055159)]
        public void GammaFunction_Returns_ExpectedValue(double x, double expectedResult)
        {
            //Act
            var result = x.Gamma();

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.HighToleranceDelta);
        }

        [TestCase(1, -0.57721566490153298d)]
        [TestCase(2, 0.42278433509846702d)]
        [TestCase(3, 0.92278433509846702)]
        [TestCase(2.5, 0.70315664064524341d)]
        [TestCase(0.5, -1.9635100260214227d)]
        public void Digammaunction_Returns_ExpectedValue(double x, double expectedResult)
        {
            //Act
            var result = x.Digamma();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 1.644934065473016d)]
        [TestCase(2, 0.64493406547301591d)]
        [TestCase(3, 0.39493406547301585d)]
        [TestCase(2.5, 0.49035775560882611d)]
        [TestCase(0.5, 4.9348022000532712d)]
        public void TrigammaFunction_Returns_ExpectedValue(double x, double expectedResult)
        {
            //Act
            var result = x.Trigamma();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 0.6931472)]
        [TestCase(5.0 / 2.0, 0.2846829)]
        [TestCase(0.5, 0.5723649)]
        public void LogGammaFunction_Returns_ExpectedValue(double x, double expectedResult)
        {
            //Act
            var result = x.LogGamma();

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.HighToleranceDelta);
        }

        [TestCase(0, 0, 1)]
        [TestCase(3, 5, 0.12465201948308108d)]
        [TestCase(1, 2, 0.1353352832366127d)]
        [TestCase(2, 1, 0.73575888234288467d)]
        [TestCase(-3, -1, 1)]
        public void UpperIncompleteRegularized_Returns_Expected_Value(double a, double x, double expectedResult)
        {
            //Act
            var result = GammaFunction.UpperIncompleteRegularized(a, x);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, 0, 0)]
        [TestCase(3, 5, 0.87534798051691887d)]
        [TestCase(1, 2, 0.8646647167633873d)]
        [TestCase(2, 1, 0.26424111765711528d)]
        [TestCase(-3, -1, 0)]
        public void LowerIncompleteRegularized_Returns_Expected_Value(double a, double x, double expectedResult)
        {
            //Act
            var result = GammaFunction.LowerIncompleteRegularized(a, x);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
