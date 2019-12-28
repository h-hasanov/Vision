namespace HH.Finance.Interfaces
{
    public interface IInstrument
    {
        IISIN ISIN { get; }
        string Description { get; }
        double GetNetPresentValue();
        bool IsExpired();
    }
}
