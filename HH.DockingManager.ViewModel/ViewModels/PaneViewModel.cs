using System.Windows.Input;
using HH.DockingManager.ViewModel.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;

namespace HH.DockingManager.ViewModel.ViewModels
{
    internal sealed class PaneViewModel : DockableViewModelBase, IPaneViewModel
    {
        private string _contentId;

        public PaneViewModel(ICommandFactory commandFactory, IPaneContent paneContent, IPaneSettings paneSettings)
        {
            Content = paneContent.ArgumentNullCheck(nameof(paneContent));
            paneSettings.ArgumentNullCheck(nameof(paneSettings));
            commandFactory.ArgumentNullCheck(nameof(commandFactory));

            ClosePaneCommand = commandFactory.CreateCommand(ClosePane);
            ShowPaneCommand = commandFactory.CreateCommand(ShowPane);

            IconSource = paneSettings.IconSource;
            ToolTip = paneSettings.ToolTip;
            Title = paneSettings.Title;
            ContentId = paneSettings.ContentId;
            ParentPaneName = paneSettings.ParentPaneName;
        }

        public ICommand ShowPaneCommand { get; }
        public ICommand ClosePaneCommand { get; }
        public IPaneContent Content { get; }
        public string ParentPaneName { get; }

        public string ContentId
        {
            get { return _contentId; }
            set { SetProperty(ref _contentId, value); }
        }

        internal void ClosePane()
        {
            IsVisible = false;
        }

        public void ShowPane()
        {
            IsActive = true;
            IsVisible = true;
            IsSelected = true;
        }
    }
}
