using HH.Data.Filter.Interfaces;

namespace HH.Data.Filter.Implementations
{
    internal sealed class OrLogicCriteria<T> : LogicCriteriaBase<T>, IOrLogicCriteria<T>
    {
        public OrLogicCriteria(ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
            : base(firstCriteria, secondCriteria)
        {
        }

        public override bool MeetsCriteria(T entity)
        {
            return FirstCriteria.MeetsCriteria(entity) || SecondCriteria.MeetsCriteria(entity);
        }
    }
}
