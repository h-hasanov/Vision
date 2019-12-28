namespace HH.Finance.Interfaces
{
    public interface IPricingEngine
    {
        IResult Calculate(IInput inputs);
    }
}
