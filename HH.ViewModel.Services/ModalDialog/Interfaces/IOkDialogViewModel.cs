using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Enums;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IOkDialogViewModel : IContentDialogViewModel<IViewModel, DialogResult>, IOkButtonViewModel
    {
    }
}
