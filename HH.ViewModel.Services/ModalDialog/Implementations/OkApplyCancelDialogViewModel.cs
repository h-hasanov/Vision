using System.Windows.Input;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Implementations
{
    public sealed class OkApplyCancelDialogViewModel : OkCancelDialogViewModel, IOkApplyCancelDialogViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private ICommand _applyCommand;

        public OkApplyCancelDialogViewModel(ICommandFactory commandFactory, IEditableDialogContent content, IDialogSettings dialogSettings) : base(commandFactory, content, dialogSettings)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
        }

        public ICommand ApplyCommand
        {
            get
            {
                return _applyCommand ?? (_applyCommand = _commandFactory.CreateCommand(Apply, Content.CanAcceptChanges));
            }
        }

        public void Apply()
        {
            Content.AcceptChanges();
        }
    }
}
