using System.Windows.Input;
using HH.EnvironmentServices.Utils;
using HH.ErrorManager.Model.Enums;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.ErrorManager.ViewModel.Interfaces;
using HH.ViewModel.Interfaces;
using HH.ViewModel.ViewModels;

namespace HH.ErrorManager.ViewModel.Implementations
{
    public sealed class ErrorInfoViewModel : ViewModelBase, IErrorInfoViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IErrorInfo _errorInfo;
        private ICommand _executeActionCommand;

        public ErrorInfoViewModel(IErrorInfo errorInfo, ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
            _errorInfo = errorInfo.ArgumentNullCheck(nameof(errorInfo));
        }

        #region Commands

        public ICommand NavigateToErrorCommand
        {
            get
            {
                return _executeActionCommand ?? (_executeActionCommand = _commandFactory.CreateCommand(NavigateToError));
            }
        }

        #endregion Commands

        #region Properties

        public ErrorSeverity Severity { get { return _errorInfo.Severity; } }
        public string Description { get { return _errorInfo.Description; } }

        #endregion Properties

        #region Methods

        public void NavigateToError()
        {
            _errorInfo.NavigateToErrorAction();
        }

        #endregion Methods
    }
}
