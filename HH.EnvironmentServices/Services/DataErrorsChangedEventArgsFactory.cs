using System.ComponentModel;
using HH.EnvironmentServices.Interfaces;

namespace HH.EnvironmentServices.Services
{
    public sealed class DataErrorsChangedEventArgsFactory : IDataErrorsChangedEventArgsFactory
    {
        public DataErrorsChangedEventArgs CreateDataErrorsChangedEventArgs(string propertyName)
        {
            return new DataErrorsChangedEventArgs(propertyName);
        }
    }
}
