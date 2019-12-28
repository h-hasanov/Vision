using System.Windows.Input;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IOkButtonViewModel
    {
        ICommand OkCommand { get; }
        void Ok();
    }
}
