using HH.Data.Entity.Model.Interfaces;
using HH.ErrorManager.Model.Collections.Interfaces;

namespace HH.ErrorManager.Model.Models.Interfaces
{
    public interface IErrorInfoContainer : IEntity, IDescriptive
    {
        IErrorInfoCollection ErrorInfoCollection { get; }
    }
}
