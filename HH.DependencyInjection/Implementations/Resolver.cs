using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HH.DependencyInjection.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.Extensions.Types;

namespace HH.DependencyInjection.Implementations
{
    internal sealed class Resolver : IResolver
    {
        #region Fields

        private IContainer _container;

        #endregion

        #region Constructors

        public void SetContainer(IContainer container)
        {
            _container = container.ArgumentNullCheck("container");
        }

        #endregion

        #region Methods

        public T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }

        public object GetInstance(Type type)
        {
            return _container.GetInstance(type);
        }

        public TInterface GetInstance<TInterface, TImplementation>(string constructorArgumentName, object argument) 
            where TInterface : class
            where TImplementation : class, TInterface
        {
            if (string.IsNullOrEmpty(constructorArgumentName))
                throw new ArgumentNullException($"Invalid property name {constructorArgumentName}");

            var constructor = typeof(TImplementation).GetTypeInfo().DeclaredConstructors.First();
            var parameters = constructor.GetParameters();
            var args = new List<object>();
            var argumentFound = false;
            foreach (var param in parameters)
            {
                if (param.Name == constructorArgumentName)
                {
                    if (!param.ParameterType.IsInstanceOfType(argument))
                    {
                        throw new TypeLoadException(
                            $"Argument not of expected type or does not implement expected type. Expected type: {param.ParameterType}, Actual type: {argument.GetType()}");
                    }

                    argumentFound = true;
                    args.Add(argument);
                }
                else
                {
                    args.Add(GetInstance(param.ParameterType));
                }
            }

            if (!argumentFound)
            {
                throw new ArgumentException($"Provided argument {constructorArgumentName} was not found in constructor arguments list.");
            }

            return (TImplementation)constructor.Invoke(args.ToArray());
        }

        #endregion
    }
}
