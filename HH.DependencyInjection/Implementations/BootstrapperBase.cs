using HH.DependencyInjection.Interfaces;
using HH.EnvironmentServices.Utils;

namespace HH.DependencyInjection.Implementations
{
    public abstract class BootstrapperBase : IBootstrapper
    {
        #region Fields

        private readonly IContainer _container;

        #endregion

        #region Constructors

        protected BootstrapperBase()
            : this(new SimpleInjectorContainer())
        {

        }

        internal BootstrapperBase(IContainer container)
        {
            _container = container.ArgumentNullCheck("container");
        }

        #endregion

        #region Properties

        public IContainer Container { get { return _container; } }

        #endregion

        #region Methods

        public void Setup()
        {
            _container.ArgumentNullCheck("container");

            //Set the Resolver.
            var resolver = new Resolver();
            resolver.SetContainer(_container);
            _container.RegisterSingle<IResolver, Resolver>(resolver);

            //Register rest of services
            SetupInternal(_container);

            //Verify container
            _container.Verify();
        }

        protected abstract void SetupInternal(IContainer container);

        #endregion
    }
}
