using System.ComponentModel;

namespace HH.EnvironmentServices.Interfaces
{
    public interface IDataErrorsChangedEventArgsFactory
    {
        DataErrorsChangedEventArgs CreateDataErrorsChangedEventArgs(string propertyName);
    }
}
