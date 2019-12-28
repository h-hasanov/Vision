using HH.EnvironmentServices.Utils;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.ErrorManager.ViewModel.Interfaces;
using HH.ViewModel.Interfaces;

namespace HH.ErrorManager.ViewModel.Implementations
{
    public sealed class ErrorInfoViewModelFactory : IErrorInfoViewModelFactory
    {
        private readonly ICommandFactory _commandFactory;

        public ErrorInfoViewModelFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
        }

        public IErrorInfoViewModel CreateErrorInfoViewModel(IErrorInfo errorInfo)
        {
            return new ErrorInfoViewModel(errorInfo, _commandFactory);
        }
    }
}
