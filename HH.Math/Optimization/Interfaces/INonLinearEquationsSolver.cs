using System;
using System.Collections.Generic;
using HH.Threading.Interfaces;

namespace HH.Math.Optimization.Interfaces
{
    public interface INonLinearEquationsSolver
    {
        IEnumerable<double> Solve(Func<double, double, double>[] functions, double[] initalPoints,
            IProgressMonitor progressMonitor,
            IExecutionOptions executionOptions,
            int maxIterations = 1000,
            double tolerance = 0.00000000000001);

        double Solve(Func<double, double> function, double initialPoint, IProgressMonitor progressMonitor, IExecutionOptions executionOptions, int maxIterations = 1000,
            double tolerance = 0.00000000000001);
    }
}
