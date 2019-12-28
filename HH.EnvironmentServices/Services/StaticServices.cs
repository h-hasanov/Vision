using HH.EnvironmentServices.Interfaces;

namespace HH.EnvironmentServices.Services
{
    public static class StaticServices
    {
        public static IDateTimeService DateTimeService = new DateTimeService();
    }
}
