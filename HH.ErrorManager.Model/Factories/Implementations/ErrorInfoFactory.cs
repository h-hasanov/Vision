using System;
using HH.ErrorManager.Model.Enums;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.ErrorManager.Model.Models.Implementations;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Factories.Implementations
{
    internal sealed class ErrorInfoFactory : IErrorInfoFactory
    {
        public IErrorInfo CreateError(string error, Action navigateToErrorAction)
        {
            return new ErrorInfo(ErrorSeverity.Error, error, navigateToErrorAction);
        }

        public IErrorInfo CreateInformation(string information, Action navigateToErrorAction)
        {
            return new ErrorInfo(ErrorSeverity.Information, information, navigateToErrorAction);
        }

        public IErrorInfo CreateWarning(string warning, Action navigateToErrorAction)
        {
            return new ErrorInfo(ErrorSeverity.Warning, warning, navigateToErrorAction);
        }
    }
}
