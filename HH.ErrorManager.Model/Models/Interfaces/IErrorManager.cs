using HH.Data.Entity.Model.Interfaces;

namespace HH.ErrorManager.Model.Models.Interfaces
{
    public interface IErrorManager
    {
        IReadOnlyEntityCollection<IErrorInfoContainer> ErrorInfoContainerCollection { get; }

        bool ContainsOwner(IEntity owner);
        IErrorInfoContainer CreateErrorInfoContainer(string description, IEntity owner);
        bool RemoveErrorInfoContainer(IEntity owner);

        void Clear();
    }
}
