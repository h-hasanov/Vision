using HH.DockingManager.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.Factories.Interfaces
{
    public interface IPaneViewModelFactory
    {
        IPaneViewModel CreatePaneViewModel(IPaneContent paneContent, IPaneSettings paneSettings);
    }
}
