using HH.EnvironmentServices.Utils;
using HH.ErrorManager.Model.Collections.Implementations;
using HH.ErrorManager.Model.Collections.Interfaces;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.ErrorManager.Model.Models.Implementations;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Factories.Implementations
{
    internal sealed class ErrorInfoCollectionFactory : IErrorInfoCollectionFactory
    {
        private readonly IErrorInfoFactory _errorInfoFactory;
       
        public ErrorInfoCollectionFactory(IErrorInfoFactory errorInfoFactory)
        {
            _errorInfoFactory = errorInfoFactory.ArgumentNullCheck(nameof(errorInfoFactory));
        }

        public IErrorInfoCollection CreatErrorInfoCollection()
        {
            return new ErrorInfoCollection(_errorInfoFactory);
        }
    }
}
