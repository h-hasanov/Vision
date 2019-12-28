using System;
using HH.EnvironmentServices.Interfaces;

namespace HH.EnvironmentServices.Services
{
    public class DateTimeService : IDateTimeService
    {
        private static DateTimeService _instance;

        public static IDateTimeService Instance
        {
            get { return _instance ?? (_instance = new DateTimeService()); }
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
