using HH.ErrorManager.Model.Factories.Interfaces;
using HH.ErrorManager.Model.Models.Implementations;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Factories.Implementations
{
    internal sealed class ErrorInfoContainerFactory : IErrorInfoContainerFactory
    {
        public IErrorInfoContainer CreateErrorInfoContainer(string description)
        {
            return new ErrorInfoContainer(description);
        }
    }
}
