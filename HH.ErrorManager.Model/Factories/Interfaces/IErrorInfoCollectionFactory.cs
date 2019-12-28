using HH.ErrorManager.Model.Collections.Interfaces;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Factories.Interfaces
{
    internal interface IErrorInfoCollectionFactory
    {
        IErrorInfoCollection CreatErrorInfoCollection();
    }
}
