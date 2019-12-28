using System;
using System.Threading.Tasks;
using HH.ViewModel.Services.Services;

namespace HH.ViewModel.Services.Interfaces
{
    public interface IApplicationService
    {
        void ForceShutDown();
        void CloseApplication();
        event EventHandler ApplicationLoaded;
        event EventHandler ApplicationClosed;

        void RegisterApplicationClosingCallback(Func<Task<ApplicationClosing>> callback);
    }
}
