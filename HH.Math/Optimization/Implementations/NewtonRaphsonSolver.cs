using System;
using System.Collections.Generic;
using System.IO;
using HH.Extensions.Numeric;
using HH.Math.Optimization.Interfaces;
using HH.Threading.Interfaces;

namespace HH.Math.Optimization.Implementations
{
    public class NewtonRaphsonSolver : INonLinearEquationsSolver
    {
        private double[,] _jacobian;

        public IEnumerable<double> Solve(Func<double, double, double>[] functions,
            double[] initalPoints,
            IProgressMonitor progressMonitor,
            IExecutionOptions executionOptions,
            int maxIterations = 1000,
            double tolerance = 0.00000000000001)
        {
            if (functions.Length != initalPoints.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            _jacobian = new double[functions.Length, functions.Length];
            var iterations = 0;


            var newInitialPoints = new[] { 0.0, 0.0 };
            var newResult = new[] { 0.0, 0.0 };
            initalPoints.CopyTo(newInitialPoints, 0);

            var continueCalculations = true;

            while (continueCalculations)
            {
                EvaluateJacobian(functions, newInitialPoints);

                var fOne = functions[0].Invoke(newInitialPoints[0], newInitialPoints[1]);
                var fTwo = functions[1].Invoke(newInitialPoints[0], newInitialPoints[1]);

                var determinant = _jacobian[0, 0] * _jacobian[1, 1] - _jacobian[0, 1] * _jacobian[1, 0];

                newResult[0] = newInitialPoints[0] - (1.0 / determinant) * (_jacobian[1, 1] * fOne - _jacobian[0, 1] * fTwo);
                newResult[1] = newInitialPoints[1] - (1.0 / determinant) * (-_jacobian[1, 0] * fOne + _jacobian[0, 0] * fTwo);

                var result1 = functions[0].Invoke(newResult[0], newResult[1]);
                var result2 = functions[1].Invoke(newResult[0], newResult[1]);

                Notifications2D(progressMonitor,executionOptions,iterations);

                if (System.Math.Abs(result1) < tolerance && System.Math.Abs(result2) < tolerance)
                {
                    continueCalculations = false;
                }


                if (iterations > maxIterations)
                {
                    throw new InvalidDataException();
                }

                newResult.CopyTo(newInitialPoints, 0);
                iterations++;
            }

            return newResult;
        }

        private void EvaluateJacobian(Func<double, double, double>[] functions, double[] pointsAt)
        {
            const double delta = 0.0000001;
            var fone = functions[0].Invoke(pointsAt[0], pointsAt[1]);
            var fTwo = functions[1].Invoke(pointsAt[0], pointsAt[1]);

            _jacobian[0, 0] = (functions[0].Invoke(pointsAt[0] + delta, pointsAt[1]) - fone) / delta;
            _jacobian[0, 1] = (functions[0].Invoke(pointsAt[0], pointsAt[1] + delta) - fone) / delta;

            _jacobian[1, 0] = (functions[1].Invoke(pointsAt[0] + delta, pointsAt[1]) - fTwo) / delta;
            _jacobian[1, 1] = (functions[1].Invoke(pointsAt[0], pointsAt[1] + delta) - fTwo) / delta;
        }


        public double Solve(
            Func<double, double> function, double initialPoint,
            IProgressMonitor progressMonitor,
            IExecutionOptions executionOptions,
            int maxIterations = 1000,
            double tolerance = 0.00000000000001)
        {
            var iterations = 0;
            const double delta = 0.0000001;
            double root;
            root = initialPoint;

            while (true)
            {
                var fx = function.Invoke(root);
                var dfx = (function.Invoke(root + delta) - fx) / delta;
                root = root - fx/dfx;

                Notifications1D(progressMonitor, executionOptions, iterations, fx);

                if (root.IsNaNOrInifinity())
                {
                    throw new NonConvergenceException();
                }

                if (System.Math.Abs(fx) < tolerance || iterations > maxIterations)
                {
                    return root;
                }

                iterations++;
            }
        }

        private static void Notifications1D(IProgressMonitor progressMonitor, IExecutionOptions executionOptions, int iterations, double minimum)
        {
            progressMonitor.ReportInformationMessage($"NRO: Iteration: {iterations}, Abs Error: {System.Math.Abs(minimum)}");
            executionOptions.CancellationToken.ThrowIfCancellationRequested();
        }

        private static void Notifications2D(IProgressMonitor progressMonitor, IExecutionOptions executionOptions, int iterations)
        {
            progressMonitor.ReportInformationMessage($"NRO: Iteration: {iterations}");
            executionOptions.CancellationToken.ThrowIfCancellationRequested();
        }
    }
}
