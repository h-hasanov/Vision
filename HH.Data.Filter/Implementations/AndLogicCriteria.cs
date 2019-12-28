using HH.Data.Filter.Interfaces;

namespace HH.Data.Filter.Implementations
{
    internal sealed class AndLogicCriteria<T> : LogicCriteriaBase<T>, IAndLogicCriteria<T>
    {
        public AndLogicCriteria(ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
            : base(firstCriteria, secondCriteria)
        {

        }

        public override bool MeetsCriteria(T entity)
        {
            return FirstCriteria.MeetsCriteria(entity) && SecondCriteria.MeetsCriteria(entity);
        }
    }
}
