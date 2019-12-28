using System;
using System.Threading;
using System.Threading.Tasks;

namespace HH.ViewModel.Services.Dispatcher
{
    /// <summary>
    /// Class that helps executing methods on the UI thread.
    /// If this class does not provide the functionality needed for windows apps use "WinDispatcherService"
    /// </summary>
    public sealed class PortableDispatcherService : IDispatcherService
    {
        private static readonly TaskScheduler Context;
        private static readonly bool HeadlessMode;

        static PortableDispatcherService()
        {
            if (SynchronizationContext.Current == null)
            {
                // If there is no SyncContext for this thread (e.g. we are in a unit test
                // or console scenario instead of running in an app), then just use the
                // default scheduler because there is no UI thread to sync with.
                HeadlessMode = true;
            }
            else
            {
                Context = TaskScheduler.FromCurrentSynchronizationContext();
            }
        }

        public void InvokeOnUiThread(Action callback)
        {
            if (HeadlessMode)
            {
                callback();
            }
            else
            {
                Task.Factory.StartNew(callback, CancellationToken.None, TaskCreationOptions.None, Context);
            }
        }
    }
}
