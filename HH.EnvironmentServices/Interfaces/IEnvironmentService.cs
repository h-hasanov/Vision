namespace HH.EnvironmentServices.Interfaces
{
    public interface IEnvironmentService
    {
        string UserDomainName { get; }

        string UserName { get; }

        string CurrentPrincipalName { get; }
    }
}
