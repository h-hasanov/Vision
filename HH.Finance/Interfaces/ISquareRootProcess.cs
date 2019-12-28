namespace HH.Finance.Interfaces
{
    public interface ISquareRootProcess : IDiffusionProcess
    {
        double Mean { get; }
        double Speed { get; }
        double Volatility { get; }
    }
}
