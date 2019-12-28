using System.ComponentModel;
using HH.EnvironmentServices.Interfaces;

namespace HH.EnvironmentServices.Services
{
    public sealed class NotifyPropertyChangedEventArgsFactory : INotifyPropertyChangedEventArgsFactory
    {
        public PropertyChangedEventArgs CreatePropertyChangedEventArgs(string propertyName)
        {
            return new PropertyChangedEventArgs(propertyName);
        }
    }
}
