using System;

namespace HH.ViewModel.Services.Win.Dispatcher
{
    public sealed class WinDispatcherService
    {
        private static readonly System.Windows.Threading.Dispatcher Dispatcher;

        static WinDispatcherService()
        {
            Dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
        }

        public void Invoke(Action callback)
        {
            Dispatcher.Invoke(callback);
        }
    }
}
