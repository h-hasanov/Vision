using System;
using HH.Data.Entity.Model.Interfaces;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Collections.Interfaces
{
    public interface IErrorInfoCollection : IEntityCollection<IErrorInfo>
    {
        void AddError(string error, Action navigateToErrorAction);
        void AddInformation(string information, Action navigateToErrorAction);
        void AddWarning(string warning, Action navigateToErrorAction);
    }
}
