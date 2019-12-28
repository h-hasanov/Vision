using System;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Factories.Interfaces
{
    internal interface IErrorInfoFactory
    {
        IErrorInfo CreateError(string error, Action navigateToErrorAction);
        IErrorInfo CreateInformation(string information, Action navigateToErrorAction);
        IErrorInfo CreateWarning(string warning, Action navigateToErrorAction);
    }
}
