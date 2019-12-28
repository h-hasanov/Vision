using System.Collections.Generic;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.Wizard.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IDialogViewModelFactory
    {
        IOkDialogViewModel CreateOkDialogViewModel(IViewModel viewModel, IDialogSettings dialogSettings);
        IOkCancelDialogViewModel CreateOkCancelDialogViewModel(IEditableDialogContent viewModel, IDialogSettings dialogSettings);

        IOkApplyCancelDialogViewModel CreateOkApplyCancelDialogViewModel(IEditableDialogContent viewModel,
            IDialogSettings dialogSettings);

        IWizardViewModel CreateWizardViewModel(IEnumerable<IWizardStepViewModel> wizardSteps,
            IDialogSettings dialogSettings);
    }
}
