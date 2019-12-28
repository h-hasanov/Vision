using System;

namespace HH.ViewModel.Services.Dispatcher
{
    public interface IDispatcherService
    {
        void InvokeOnUiThread(Action callback);
    }
}
