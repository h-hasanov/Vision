using System;
using HH.Data.Entity.Model.Entity;
using HH.ErrorManager.Model.Enums;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Models.Implementations
{
    internal sealed class ErrorInfo : EntityBase, IErrorInfo
    {
        public ErrorInfo(ErrorSeverity severity, string description, Action navigateToErrorAction)
        {
            Severity = severity;
            Description = description;
            NavigateToErrorAction = navigateToErrorAction;
        }

        public Action NavigateToErrorAction { get; }
        public ErrorSeverity Severity { get; }
        public string Description { get; }
    }
}
