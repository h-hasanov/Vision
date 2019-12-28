using System.Collections.Specialized;

namespace HH.EnvironmentServices.Interfaces
{
    public interface INotifyCollectionChangedEventArgsFactory
    {
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs
        //     class that describes a System.Collections.Specialized.NotifyCollectionChangedAction.Reset
        //     change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This must be set to System.Collections.Specialized.NotifyCollectionChangedAction.Reset.
        NotifyCollectionChangedEventArgs CreateNotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action);


        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs
        //     class that describes a one-item change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can be set to System.Collections.Specialized.NotifyCollectionChangedAction.Reset,
        //     System.Collections.Specialized.NotifyCollectionChangedAction.Add, or System.Collections.Specialized.NotifyCollectionChangedAction.Remove.
        //
        //   changedItem:
        //     The item that is affected by the change.
        //
        //   index:
        //     The index where the change occurred.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If action is not Reset, Add, or Remove, or if action is Reset and either changedItems
        //     is not null or index is not -1.
        NotifyCollectionChangedEventArgs CreateNotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index);

        /// <summary>
        /// Should be used for Replacing items.
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="oldItem"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        NotifyCollectionChangedEventArgs CreateNotifyCollectionChangedEventArgs(object newItem, object oldItem, int index);
      
    }
}
