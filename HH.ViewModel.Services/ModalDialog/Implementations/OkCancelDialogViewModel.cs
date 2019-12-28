using System.Windows.Input;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Enums;
using HH.ViewModel.Services.ModalDialog.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Implementations
{
    public class OkCancelDialogViewModel : ContentDialogViewModelBase<IEditableDialogContent, DialogResult>, IOkCancelDialogViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private ICommand _okCommand;
        private ICommand _cancelCommand;

        public OkCancelDialogViewModel(ICommandFactory commandFactory, IEditableDialogContent content, IDialogSettings dialogSettings)
            : base(content, dialogSettings)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
        }

        public ICommand OkCommand
        {
            get { return _okCommand ?? (_okCommand = _commandFactory.CreateCommand(Ok, Content.CanAcceptChanges)); }
        }

        public ICommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = _commandFactory.CreateCommand(Cancel)); }
        }

        public void Ok()
        {
            Content.AcceptChanges();
            Close(DialogResult.Ok);
        }

        public void Cancel()
        {
            Close(DialogResult.Cancel);
        }
    }
}
