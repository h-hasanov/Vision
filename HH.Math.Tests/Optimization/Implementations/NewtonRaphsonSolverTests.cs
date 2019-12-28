using System;
using System.Linq;
using System.Threading;
using HH.Math.Optimization.Implementations;
using HH.TestUtils;
using HH.Threading.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.Math.Tests.Optimization.Implementations
{
    [TestFixture]
    internal sealed class NewtonRaphsonSolverTests
    {
        private AutoMocker _autoMocker;
        private Func<double, double, double>[] _functions;
        private Func<double, double, double>[,] _derivativeFunctions;
        private double[] _initialPoints;
        private double[] _expectedResults;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();

            _functions = new Func<double, double, double>[2];
            _derivativeFunctions = new Func<double, double, double>[2, 2];

            _functions[0] = (x, y) => System.Math.Sin(3 * x) + System.Math.Sin(3 * y);
            _functions[1] = (x, y) => System.Math.Sin(5 * x) + System.Math.Sin(5 * y);

            _derivativeFunctions[0, 0] = (x, y) => 3 * System.Math.Cos(3 * x);
            _derivativeFunctions[0, 1] = (x, y) => 3 * System.Math.Cos(3 * y);

            _derivativeFunctions[1, 0] = (x, y) => 5 * System.Math.Cos(5 * x);
            _derivativeFunctions[1, 1] = (x, y) => 5 * System.Math.Cos(5 * y);

            _initialPoints = new[] { 4, 3.1 };
            _expectedResults = new[] { 4.9218284906240095d, 3.8746309394274117d };
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }


        [Test]
        public void NonLinearEquationSolver_SolvesNonLinearWithoutDerivatives_Correctly()
        {
            //Arrange
            var solver = new NewtonRaphsonSolver();
            var progressMonitor = _autoMocker.Mock<IProgressMonitor>();
            var executionOptions = _autoMocker.Mock<IExecutionOptions>();

            //Act
            var results = solver.Solve(_functions, _initialPoints, progressMonitor, executionOptions).ToArray();

            //Assert
            var i = 0;
            foreach (var expectedResult in _expectedResults)
            {
                Assert.AreEqual(expectedResult, results[i], Constants.LowToleranceDelta);
                i++;
            }
        }

        [Test]
        public void NonLinearEquationSolver_2D_Cancels_Correctly()
        {
            //Arrange
            var solver = new NewtonRaphsonSolver();
            var progressMonitor = _autoMocker.Mock<IProgressMonitor>();
            var executionOptions = _autoMocker.Mock<IExecutionOptions>();
            executionOptions.Expect(c => c.CancellationToken).Return(new CancellationToken(true));

            //Act
            Assert.Throws<OperationCanceledException>(
                () => solver.Solve(_functions, _initialPoints, progressMonitor, executionOptions));
        }

        [TestCase(1, 2)]
        public void NonLinearEquationSolverWithoutDerivatives_Throws_Correctly(int funcLength, int initialPointsLength)
        {
            //Arrange
            var progressMonitor = _autoMocker.Mock<IProgressMonitor>();
            var executionOptions = _autoMocker.Mock<IExecutionOptions>();

            var solver = new NewtonRaphsonSolver();
            _functions = new Func<double, double, double>[funcLength];

            _initialPoints = new double[initialPointsLength];

            //Act 

            //Assrert
            Assert.Throws<ArgumentOutOfRangeException>(() => solver.Solve(_functions, _initialPoints, progressMonitor, executionOptions));
        }

        [Test]
        public void NonLinearEquationSolver_Solves1DNonLinear_Correctly()
        {
            //Arrange
            var progressMonitor = _autoMocker.Mock<IProgressMonitor>();
            var executionOptions = _autoMocker.Mock<IExecutionOptions>();

            var solver = new NewtonRaphsonSolver();
            Func<double, double> function = x => System.Math.Pow(x, 2) - 3;
            const double expectedResult = 1.7320508075688774;

            //Act
            var results = solver.Solve(function, 3, progressMonitor, executionOptions);

            //Assrert
            Assert.AreEqual(expectedResult, results, Constants.LowToleranceDelta);
        }

        [Test]
        public void NonLinearEquationSolver_1D_Cancels_Correctly()
        {
            //Arrange
            var progressMonitor = _autoMocker.Mock<IProgressMonitor>();
            var executionOptions = _autoMocker.Mock<IExecutionOptions>();
            executionOptions.Expect(c => c.CancellationToken).Return(new CancellationToken(true));

            var solver = new NewtonRaphsonSolver();
            Func<double, double> function = x => System.Math.Pow(x, 2) - 3;

            //Act
            Assert.Throws<OperationCanceledException>(() => solver.Solve(function, 3, progressMonitor, executionOptions));
        }
    }
}
