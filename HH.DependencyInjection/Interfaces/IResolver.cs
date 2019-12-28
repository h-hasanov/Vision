using System;

namespace HH.DependencyInjection.Interfaces
{
    public interface IResolver
    {
        /// <summary>
        /// This method is used to resolve an intance of type {T}.
        /// </summary>
        /// <returns>The resolved instance.</returns>
        T GetInstance<T>() where T : class;

        /// <summary>
        /// This method is used to resolve an intance of type {T} with specific constructor argument
        /// </summary>
        /// <returns>The resolved instance.</returns>
        /// <param name="constructorArgumentName">The name of the argument in the constructor of the constructed class</param>
        /// <param name="argument">The constructor argument</param>
        TInterface GetInstance<TInterface, TImplementation>(string constructorArgumentName, object argument)
            where TInterface : class
            where TImplementation : class, TInterface;

        /// <summary>
        /// This method is used to resolve an intance of type {T}.
        /// </summary>
        /// <returns>The resolved instance.</returns>
        object GetInstance(Type type);
    }
}
