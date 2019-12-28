using System.Collections.Generic;

namespace HH.Data.Filter.Interfaces
{
    public interface ISearchable<T>
    {
        IEnumerable<T> Search(ICriteria<T> searchCriteria);
    }
}
