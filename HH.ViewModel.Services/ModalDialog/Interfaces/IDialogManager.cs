using System.Collections.Generic;
using System.Threading.Tasks;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Enums;
using HH.ViewModel.Services.Wizard.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IDialogManager
    {
        Task<DialogResult> OpenOkDialogAsync(IViewModel viewModel, IDialogSettings dialogSettings);
        Task<DialogResult> OpenOkCancelDialogAsync(IEditableDialogContent viewModel, IDialogSettings dialogSettings);
        Task<DialogResult> OpenOkApplyCancelDialogAsync(IEditableDialogContent viewModel, IDialogSettings dialogSettings);

        Task<DialogResult> ShowWizardDialogAsync(IEnumerable<IWizardStepViewModel> wizardSteps,
            IDialogSettings dialogSettings);
        Task<TResult> ShowDialogAsync<TResult>(IDialogViewModel<TResult> dialogViewModel);
    }
}
