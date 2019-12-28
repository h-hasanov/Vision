using System.Windows.Input;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Enums;
using HH.ViewModel.Services.ModalDialog.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Implementations
{
    public sealed class OkDialogViewModel : ContentDialogViewModelBase<IViewModel, DialogResult>, IOkDialogViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private ICommand _okCommand;

        public OkDialogViewModel(ICommandFactory commandFactory, IViewModel content, IDialogSettings dialogSettings)
            : base(content, dialogSettings)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
        }

        public ICommand OkCommand
        {
            get { return _okCommand ?? (_okCommand = _commandFactory.CreateCommand(Ok)); }
        }

        public void Ok()
        {
            Close(DialogResult.Ok);
        }
    }
}
