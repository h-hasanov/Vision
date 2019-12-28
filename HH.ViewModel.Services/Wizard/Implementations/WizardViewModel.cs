using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Enums;
using HH.ViewModel.Services.ModalDialog.Implementations;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using HH.ViewModel.Services.Wizard.Interfaces;

namespace HH.ViewModel.Services.Wizard.Implementations
{
    public sealed class WizardViewModel : DialogViewModelBase<DialogResult>, IWizardViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private ICommand _cancelCommand;
        private ICommand _backCommand;
        private ICommand _nextCommand;
        private ICommand _finishCommand;
        private IWizardStepViewModel _currentStep;
        private int _currentStepIndex;
        private readonly string _originalTitle;

        public WizardViewModel(IDialogSettings dialogSettings,
            IEnumerable<IWizardStepViewModel> wizardStepViewModels,
            ICommandFactory commandFactory) : base(dialogSettings)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
            _originalTitle = dialogSettings.Title;

            Steps = wizardStepViewModels.ArgumentNullCheck(nameof(wizardStepViewModels)).ToArray();
            _currentStep = Steps.First();
            _currentStepIndex = 0;

            UpdateTitle();
        }

        #region Commands

        public ICommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = _commandFactory.CreateCommand(Cancel)); }
        }

        public ICommand MoveBackCommand
        {
            get { return _backCommand ?? (_backCommand = _commandFactory.CreateCommand(MoveBack, CanMoveBack)); }
        }

        public ICommand MoveNextCommand
        {
            get { return _nextCommand ?? (_nextCommand = _commandFactory.CreateCommand(MoveNext, CanMoveNext)); }
        }

        public ICommand FinishCommand
        {
            get { return _finishCommand ?? (_finishCommand = _commandFactory.CreateCommand(Finish, CanFinish)); }
        }

        #endregion Commands

        #region Properties

        public IWizardStepViewModel[] Steps { get; }

        public IWizardStepViewModel CurrentStep
        {
            get { return _currentStep; }
            private set { SetProperty(ref _currentStep, value); }
        }

        #endregion Properties

        #region Cancel

        public void Cancel()
        {
            Close(DialogResult.Cancel);
        }

        #endregion Cancel

        #region MoveBack

        public void MoveBack()
        {
            _currentStep.AcceptChanges();
            _currentStepIndex--;
            CurrentStep = Steps[_currentStepIndex];
            UpdateTitle();
        }

        public bool CanMoveBack()
        {
            if (_currentStepIndex == 0)
                return false;
            return _currentStep.CanAcceptChanges();
        }

        #endregion MoveBack

        #region MoveNext

        public void MoveNext()
        {
            _currentStep.AcceptChanges();
            _currentStepIndex++;
            CurrentStep = Steps[_currentStepIndex];
            UpdateTitle();
        }

        public bool CanMoveNext()
        {
            if (_currentStepIndex == Steps.Length - 1)
                return false;
            return CurrentStep.CanAcceptChanges();
        }

        #endregion MoveNext

        #region Finish

        public void Finish()
        {
            CurrentStep.AcceptChanges();
            Close(DialogResult.Ok);
        }

        public bool CanFinish()
        {
            if (_currentStepIndex != Steps.Length - 1)
                return false;
            return CurrentStep.CanAcceptChanges();
        }

        #endregion Finish

        #region Helpers

        private void UpdateTitle()
        {
            DialogSettings.Title = $"{_originalTitle} - Step {_currentStepIndex + 1} of {Steps.Length}";
        }

        #endregion
    }
}
