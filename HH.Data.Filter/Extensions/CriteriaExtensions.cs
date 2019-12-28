using HH.Data.Filter.Implementations;
using HH.Data.Filter.Interfaces;

namespace HH.Data.Filter.Extensions
{
    public static class CriteriaExtensions
    {
        public static ICriteria<T> And<T>(this ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
        {
            return new AndLogicCriteria<T>(firstCriteria, secondCriteria);
        }

        public static ICriteria<T> Or<T>(this ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
        {
            return new OrLogicCriteria<T>(firstCriteria, secondCriteria);
        }

        public static ICriteria<T> Not<T>(this ICriteria<T> criteria)
        {
            return new NotLogicCriteria<T>(criteria);
        }

        public static ICriteria<T> Nand<T>(this ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
        {
            return new NandLogicCriteria<T>(firstCriteria, secondCriteria);
        }

        public static ICriteria<T> Nor<T>(this ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
        {
            return new NorLogicCriteria<T>(firstCriteria, secondCriteria);
        }

        public static ICriteria<T> Xor<T>(this ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
        {
            return new XorLogicCriteria<T>(firstCriteria, secondCriteria);
        }

        public static ICriteria<T> Xnor<T>(this ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
        {
            return new XnorLogicCriteria<T>(firstCriteria, secondCriteria);
        }
    }
}
