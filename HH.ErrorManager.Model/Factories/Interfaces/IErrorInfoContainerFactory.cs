using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Factories.Interfaces
{
    internal interface IErrorInfoContainerFactory
    {
        IErrorInfoContainer CreateErrorInfoContainer(string description);
    }
}
