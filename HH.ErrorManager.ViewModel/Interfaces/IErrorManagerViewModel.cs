using System.Windows.Input;
using HH.Data.Collections.Generics.Interfaces;
using HH.ViewModel.Interfaces;

namespace HH.ErrorManager.ViewModel.Interfaces
{
    public interface IErrorManagerViewModel : IViewModel
    {
        IReadOnlyObservableDataCollection<IErrorInfoContainerViewModel> ErrorInfoContainerViewModelCollection { get; }

        ICommand ExpandAllCommand { get; }
        void ExpandAll();
        bool CanExpandAll();

        ICommand CollapseAllCommand { get; }
        void CollapseAll();
        bool CanCollapseAll();
    }
}
