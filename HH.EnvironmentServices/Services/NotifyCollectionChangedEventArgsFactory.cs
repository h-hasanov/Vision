using System.Collections.Specialized;
using HH.EnvironmentServices.Interfaces;

namespace HH.EnvironmentServices.Services
{
    public sealed class NotifyCollectionChangedEventArgsFactory : INotifyCollectionChangedEventArgsFactory
    {
        public NotifyCollectionChangedEventArgs CreateNotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
        {
            return new NotifyCollectionChangedEventArgs(action);
        }

        public NotifyCollectionChangedEventArgs CreateNotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action,
            object changedItem, int index)
        {
            return new NotifyCollectionChangedEventArgs(action, changedItem, index);
        }

        public NotifyCollectionChangedEventArgs CreateNotifyCollectionChangedEventArgs(object newItem, object oldItem, int index)
        {
            return new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem, index);
        }
    }
}
