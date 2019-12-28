using HH.Data.Filter.Interfaces;
using HH.EnvironmentServices.Utils;

namespace HH.Data.Filter.Implementations
{
    internal sealed class NotLogicCriteria<T> : CriteriaBase<T>, INotLogicCriteria<T>
    {
        public NotLogicCriteria(ICriteria<T> criteria)
        {
            Criteria = criteria.ArgumentNullCheck("criteria");
        }

        public ICriteria<T> Criteria { get; private set; }

        public override bool MeetsCriteria(T entity)
        {
            return !Criteria.MeetsCriteria(entity);
        }
    }
}
