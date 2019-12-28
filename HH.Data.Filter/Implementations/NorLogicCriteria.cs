using System;
using HH.Data.Filter.Interfaces;

namespace HH.Data.Filter.Implementations
{
    internal sealed class NorLogicCriteria<T> : LogicCriteriaBase<T>, INorLogicCriteria<T>
    {
        public NorLogicCriteria(ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
            : base(firstCriteria, secondCriteria)
        {
        }

        public override bool MeetsCriteria(T entity)
        {
            return !FirstCriteria.MeetsCriteria(entity) && !SecondCriteria.MeetsCriteria(entity);
        }
    }
}
