using System;
using HH.EnvironmentServices.Utils;

namespace HH.Data.Filter.Implementations
{
    public sealed class PredicateCriteria<T> : CriteriaBase<T>
    {
        private readonly Predicate<T> _predicate;

        public PredicateCriteria(Predicate<T> predicate)
        {
            _predicate = predicate.ArgumentNullCheck(nameof(predicate));
        }

        public override bool MeetsCriteria(T entity)
        {
            return _predicate(entity);
        }
    }
}
