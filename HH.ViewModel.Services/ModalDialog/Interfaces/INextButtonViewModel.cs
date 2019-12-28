using System.Windows.Input;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface INextButtonViewModel
    {
        ICommand MoveNextCommand { get; }
        void MoveNext();
        bool CanMoveNext();
    }
}
