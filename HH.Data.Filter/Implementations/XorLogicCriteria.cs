using HH.Data.Filter.Interfaces;

namespace HH.Data.Filter.Implementations
{
    internal sealed class XorLogicCriteria<T> : LogicCriteriaBase<T>, IXorLogicCriteria<T>
    {
        public XorLogicCriteria(ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
            : base(firstCriteria, secondCriteria)
        {
        }

        public override bool MeetsCriteria(T entity)
        {
            return FirstCriteria.MeetsCriteria(entity) ^ SecondCriteria.MeetsCriteria(entity);
        }
    }
}
