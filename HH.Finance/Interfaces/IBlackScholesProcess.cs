namespace HH.Finance.Interfaces
{
    public interface IBlackScholesProcess : IDiffusionProcess
    {
        double Rate { get; }
        double Volatility { get; }
    }
}
