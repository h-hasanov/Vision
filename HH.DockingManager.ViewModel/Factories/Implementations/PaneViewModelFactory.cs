using HH.DockingManager.ViewModel.Factories.Interfaces;
using HH.DockingManager.ViewModel.Interfaces;
using HH.DockingManager.ViewModel.ViewModels;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.Factories.Implementations
{
    public sealed class PaneViewModelFactory : IPaneViewModelFactory
    {
        private readonly ICommandFactory _commandFactory;

        public PaneViewModelFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
        }

        public IPaneViewModel CreatePaneViewModel(IPaneContent paneContent, IPaneSettings paneSettings)
        {
            return new PaneViewModel(_commandFactory, paneContent, paneSettings);
        }
    }
}
