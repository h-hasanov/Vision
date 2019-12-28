using System;
using HH.Data.Entity.Model.EntityCollections;
using HH.EnvironmentServices.Utils;
using HH.ErrorManager.Model.Collections.Interfaces;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Collections.Implementations
{
    internal sealed class ErrorInfoCollection : EntityCollection<IErrorInfo>, IErrorInfoCollection
    {
        private readonly IErrorInfoFactory _errorInfoFactory;

        public ErrorInfoCollection(IErrorInfoFactory errorInfoFactory)
        {
            _errorInfoFactory = errorInfoFactory.ArgumentNullCheck(nameof(errorInfoFactory));
        }

        #region Methods

        public void AddError(string error, Action navigateToErrorAction)
        {
            Add(_errorInfoFactory.CreateError(error, navigateToErrorAction));
        }

        public void AddInformation(string information, Action navigateToErrorAction)
        {
            Add(_errorInfoFactory.CreateInformation(information, navigateToErrorAction));
        }

        public void AddWarning(string warning, Action navigateToErrorAction)
        {
            Add(_errorInfoFactory.CreateWarning(warning, navigateToErrorAction));
        }

        #endregion Methods
    }
}
