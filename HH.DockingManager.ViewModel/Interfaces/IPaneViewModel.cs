using System.Windows.Input;

namespace HH.DockingManager.ViewModel.Interfaces
{
    public interface IPaneViewModel : IDockableViewModel
    {
        string ContentId { get; }
        string ParentPaneName { get; }
        ICommand ShowPaneCommand { get; }
        void ShowPane();
        ICommand ClosePaneCommand { get;}
        IPaneContent Content { get; }
    }
}
