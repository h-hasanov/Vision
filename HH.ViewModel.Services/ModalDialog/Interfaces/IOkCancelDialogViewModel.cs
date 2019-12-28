using HH.ViewModel.Services.ModalDialog.Enums;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IOkCancelDialogViewModel : IContentDialogViewModel<IEditableDialogContent, DialogResult>, IOkButtonViewModel, ICancelButtonViewModel
    {
    }
}
