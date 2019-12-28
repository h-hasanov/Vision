using System.Collections.Generic;
using HH.Data.Entity.Model.Entity;
using HH.Data.Entity.Model.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.Model.Models.Implementations
{
    public sealed class ErrorManager : EntityBase, IErrorManager
    {
        private readonly IErrorInfoContainerFactory _errorInfoContainerFactory;
        private readonly IDictionary<IEntity, IErrorInfoContainer> _entityToErrorInfoContainerMapper;
        private readonly IEntityCollection<IErrorInfoContainer> _errorInfoContainerCollection; 

        public ErrorManager(IEntityCollectionFactory entityCollectionFactory)
            : this(entityCollectionFactory, new Dictionary<IEntity, IErrorInfoContainer>(), StaticServices.ErrorInfoContainerFactory)
        {

        }

        internal ErrorManager(IEntityCollectionFactory entityCollectionFactory,
            IDictionary<IEntity, IErrorInfoContainer> entityToErrorInfoContainerMapper,
            IErrorInfoContainerFactory errorInfoContainerFactory)
        {
            _errorInfoContainerFactory = errorInfoContainerFactory.ArgumentNullCheck(nameof(errorInfoContainerFactory));
            _errorInfoContainerCollection = entityCollectionFactory.ArgumentNullCheck(nameof(entityCollectionFactory))
                .CreateEntityCollection<IErrorInfoContainer>(this);

            _entityToErrorInfoContainerMapper = entityToErrorInfoContainerMapper.ArgumentNullCheck(nameof(entityCollectionFactory));
        }


        public IReadOnlyEntityCollection<IErrorInfoContainer> ErrorInfoContainerCollection
        {
            get { return _errorInfoContainerCollection; }
        }

        public bool ContainsOwner(IEntity owner)
        {
            return _entityToErrorInfoContainerMapper.ContainsKey(owner);
        }

        public IErrorInfoContainer CreateErrorInfoContainer(string description, IEntity owner)
        {
            if (ContainsOwner(owner))
                return _entityToErrorInfoContainerMapper[owner];

            var errorInfoContainer = _errorInfoContainerFactory.CreateErrorInfoContainer(description);
            _errorInfoContainerCollection.Add(errorInfoContainer);
            _entityToErrorInfoContainerMapper.Add(owner, errorInfoContainer);
            return errorInfoContainer;
        }

        public bool RemoveErrorInfoContainer(IEntity owner)
        {
            if (ContainsOwner(owner))
            {
                _errorInfoContainerCollection.Remove(_entityToErrorInfoContainerMapper[owner]);
                _entityToErrorInfoContainerMapper.Remove(owner);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            _errorInfoContainerCollection.Clear();
            _entityToErrorInfoContainerMapper.Clear();
        }
    }
}
