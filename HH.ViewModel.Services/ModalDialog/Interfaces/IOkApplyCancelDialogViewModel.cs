using HH.ViewModel.Services.ModalDialog.Enums;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IOkApplyCancelDialogViewModel : IContentDialogViewModel<IEditableDialogContent, DialogResult>, IOkButtonViewModel, ICancelButtonViewModel, IApplyButtonViewModel
    {
    }
}
