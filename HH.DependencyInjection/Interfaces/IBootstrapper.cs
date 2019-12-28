namespace HH.DependencyInjection.Interfaces
{
    public interface IBootstrapper
    {
        void Setup();

        IContainer Container { get; }
    }
}
