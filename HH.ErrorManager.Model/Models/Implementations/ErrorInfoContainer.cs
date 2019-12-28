using HH.Data.Entity.Model.Entity;
using HH.EnvironmentServices.Utils;
using HH.ErrorManager.Model.Collections.Interfaces;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Models.Implementations
{
    internal sealed class ErrorInfoContainer : EntityBase, IErrorInfoContainer
    {
        #region Constructors

        public ErrorInfoContainer(string description)
            : this(description, StaticServices.ErrorInfoCollectionFactory)
        {

        }

        internal ErrorInfoContainer(string description, IErrorInfoCollectionFactory errorInfoCollectionFactory)
        {
            ErrorInfoCollection =
                errorInfoCollectionFactory.ArgumentNullCheck(nameof(errorInfoCollectionFactory))
                    .CreatErrorInfoCollection();
            Description = description;
        }

        #endregion Constructors

        #region Properties

        public IErrorInfoCollection ErrorInfoCollection { get; }

        public string Description { get; }

        #endregion Properties
    }
}
