namespace HH.Data.Filter.Interfaces
{
    internal interface INotLogicCriteria<T> : ICriteria<T>
    {
        ICriteria<T> Criteria { get; } 
    }
}
