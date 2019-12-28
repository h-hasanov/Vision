using System;
using System.Diagnostics;
using HH.DependencyInjection.Enums;
using HH.DependencyInjection.Interfaces;
using SimpleInjector;

namespace HH.DependencyInjection.Implementations
{
    [DebuggerNonUserCode]
    internal sealed class SimpleInjectorContainer : IContainer
    {
        private readonly Container _container;

        public SimpleInjectorContainer()
        {
            _container = new Container();
        }

        public void Register<TService, TImplementation>(LifeSpan lifeSpan)
            where TService : class
            where TImplementation : class, TService
        {
            var lifeStyle = ConvertToLifeStyle(lifeSpan);
            _container.Register<TService, TImplementation>(lifeStyle);
        }

        public void RegisterSingle<TService, TImplementation>(TImplementation instance)
            where TService : class
            where TImplementation : class, TService
        {
            _container.RegisterSingleton<TService>(instance) ;
        }

        private static Lifestyle ConvertToLifeStyle(LifeSpan lifeSpan)
        {
            switch (lifeSpan)
            {
                case LifeSpan.Transient:
                    return Lifestyle.Transient;
                case LifeSpan.Singleton:
                    return Lifestyle.Singleton;
                default:
                    throw new NotImplementedException();
            }
        }

        public T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }

        public object GetInstance(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        public void Verify()
        {
            _container.Verify();
        }
    }
}
