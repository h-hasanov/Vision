namespace HH.Finance.Interfaces
{
    public interface IDiffusionProcess
    {
        /// <summary>
        /// Returns the drift part of the process
        /// </summary>
        /// <param name="time"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        double Drift(double time, double x);

        /// <summary>
        /// Returns the difussion part of the process
        /// </summary>
        /// <param name="t"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        double Diffusion(double t, double x);

        /// <summary>
        /// Returns the expectation of the process after a time interval
        /// Returns E(x_{t_0 + delta t} | x_{t_0} = x_0) since it is Markov.
        /// By default, it returns the Euler approximation defined by
        /// x_0 + mu(t_0, x_0) delta t.
        /// </summary>
        /// <param name="t0"></param>
        /// <param name="dt"></param>
        /// <param name="x0"></param>
        /// <returns></returns>
        double Expectation(double t0, double x0, double dt);

        /// <summary>
        /// Returns the variance of the process after a time interval
        /// returns Var(x_{t_0 + Delta t} | x_{t_0} = x_0).
        /// By default, it returns the Euler approximation defined by
        /// sigma(t_0, x_0)^2 \Delta t .
        /// </summary>
        /// <param name="t0"></param>
        /// <param name="x0"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        double Variance(double t0, double x0, double dt);
    }
}
