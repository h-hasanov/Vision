using HH.Data.Filter.Interfaces;
using HH.EnvironmentServices.Utils;

namespace HH.Data.Filter.Implementations
{
    internal abstract class LogicCriteriaBase<T> : CriteriaBase<T>, ILogicCriteria<T>
    {
        protected LogicCriteriaBase(ICriteria<T> firstCriteria,ICriteria<T> secondCriteria)
        {
            FirstCriteria = firstCriteria.ArgumentNullCheck("firstCriteria");
            SecondCriteria = secondCriteria.ArgumentNullCheck("secondCriteria");
        }

        public ICriteria<T> FirstCriteria { get; private set; }
        public ICriteria<T> SecondCriteria { get; private set; }
    }
}
