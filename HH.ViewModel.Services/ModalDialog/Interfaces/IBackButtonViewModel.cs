using System.Windows.Input;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IBackButtonViewModel
    {
        ICommand MoveBackCommand { get; }
        void MoveBack();
        bool CanMoveBack();
    }
}
