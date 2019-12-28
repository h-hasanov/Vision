using System.Collections.Generic;
using System.Linq;
using HH.Data.Filter.Interfaces;

namespace HH.Data.Filter.Implementations
{
    public abstract class CriteriaBase<T> : ICriteria<T>
    {
        public IEnumerable<T> MeetCriteria(IEnumerable<T> entities)
        {
            return entities.Where(MeetsCriteria);
        }

        public abstract bool MeetsCriteria(T entity);
    }
}
