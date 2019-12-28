using System;
using HH.DependencyInjection.Enums;

namespace HH.DependencyInjection.Interfaces
{
    public interface IContainer
    {
        //
        // Summary:
        //     Gets an instance of the given T.
        //
        // Type parameters:
        //   T:
        //     Type of object requested.
        //
        // Returns:
        //     The requested instance.
        T GetInstance<T>() where T : class;


        //
        // Summary:
        //     Gets an instance of the given type.
        //
        // Parameters:
        //   type:
        //     Type of object requested.
        //
        // Returns:
        //     The requested instance.
        object GetInstance(Type type);

        //
        // Summary:
        //     Registers that an instance of TImplementation will be returned when an instance
        //     of type TInterface is requested. The instance is cached according to the supplied
        //     lifeSpan.
        //
        // Parameters:
        //   lifeSpan:
        //     The lifeSpan that specifies how the returned instance will be cached.
        //
        // Type parameters:
        //   TInterface:
        //     The interface or base type that can be used to retrieve the instances.
        //
        //   TImplementation:
        //     The concrete type that will be registered.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     Thrown when this container instance is locked and can not be altered, or
        //     when an the TService has already been registered.
        //
        //   System.ArgumentException:
        //     Thrown when the given TImplementation type is not a type that can be created
        //     by the container.
        void Register<TInterface, TImplementation>(LifeSpan lifeSpan)
            where TInterface : class
            where TImplementation : class, TInterface;

        //
        // Summary:
        //     Registers a single instance that will be returned when an instance of type
        //     TInterface is requested. This instance must be thread-safe when working in
        //     a multi-threaded environment.
        //
        // Parameters:
        //   instance:
        //     The instance to register.
        //
        // Type parameters:
        //   TInterface:
        //     The interface or base type that can be used to retrieve the instance.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     Thrown when this container instance is locked and can not be altered, or
        //     when the TInterface has already been registered.
        //
        //   System.ArgumentNullException:
        //     Thrown when instance is a null reference.
        void RegisterSingle<TInterface, TImplementation>(TImplementation instance)
            where TInterface : class
            where TImplementation : class, TInterface;
      

        //
        // Summary:
        //     Verifies the Container. This method will call all registered delegates, iterate
        //     registered collections and throws an exception if there was an error.
        void Verify();
    }
}
