using System.ComponentModel;

namespace HH.EnvironmentServices.Interfaces
{
    public interface INotifyPropertyChangedEventArgsFactory
    {
        PropertyChangedEventArgs CreatePropertyChangedEventArgs(string propertyName);
    }
}
