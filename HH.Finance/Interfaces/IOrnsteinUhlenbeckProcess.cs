namespace HH.Finance.Interfaces
{
    public interface IOrnsteinUhlenbeckProcess : IDiffusionProcess
    {
        double Speed { get; }
        double Volatility { get; }
    }
}
