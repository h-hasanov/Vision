using System.Windows.Input;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface ICancelButtonViewModel
    {
        ICommand CancelCommand { get; }
        void Cancel();
    }
}
