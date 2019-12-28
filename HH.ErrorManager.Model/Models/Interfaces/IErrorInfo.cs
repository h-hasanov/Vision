using System;
using HH.Data.Entity.Model.Interfaces;
using HH.ErrorManager.Model.Enums;

namespace HH.ErrorManager.Model.Models.Interfaces
{
    public interface IErrorInfo : IEntity, IDescriptive
    {
        Action NavigateToErrorAction { get; }
        ErrorSeverity Severity { get; }
    }
}
