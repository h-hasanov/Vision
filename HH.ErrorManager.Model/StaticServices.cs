using HH.ErrorManager.Model.Factories.Implementations;
using HH.ErrorManager.Model.Factories.Interfaces;

namespace HH.ErrorManager.Model
{
    internal static class StaticServices
    {
        public static IErrorInfoFactory ErrorInfoFactory = new ErrorInfoFactory();
        public static IErrorInfoCollectionFactory ErrorInfoCollectionFactory = new ErrorInfoCollectionFactory(ErrorInfoFactory);
        public static IErrorInfoContainerFactory ErrorInfoContainerFactory = new ErrorInfoContainerFactory();
    }
}
