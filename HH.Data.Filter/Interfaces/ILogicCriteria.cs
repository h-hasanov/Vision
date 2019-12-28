namespace HH.Data.Filter.Interfaces
{
    internal interface ILogicCriteria<T> : ICriteria<T>
    {
        ICriteria<T> FirstCriteria { get; }
        ICriteria<T> SecondCriteria { get; }
    }
}
