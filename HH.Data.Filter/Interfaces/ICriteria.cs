using System.Collections.Generic;

namespace HH.Data.Filter.Interfaces
{
    /// <summary>
    /// I've found the specification pattern a nice way to wrap up a piece of business rule to be reused through out the application, 
    /// for instance in our e-commerce product our promotion entity builds a specification based of it's values. 
    /// We can then use this specification to determine whether an order is valid for the promotion or even filter lists of orders using FindAll method on List and so on. 
    /// Tim McCarthy has a great article on building Composite Specifications at "[A Composite Specification Pattern Implementation in .NET 2.0]".
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICriteria<T>
    {
        IEnumerable<T> MeetCriteria(IEnumerable<T> entities);

        bool MeetsCriteria(T entity);
    }
}
