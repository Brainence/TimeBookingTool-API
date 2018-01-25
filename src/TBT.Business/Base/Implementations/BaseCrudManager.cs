using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.Logger;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Implementations
{

    public abstract class BaseCrudManager<TEntity> : BaseManager
        where TEntity : class, IEntity
    {
        #region Fields

        private readonly IRepository<TEntity> _repository;

        #endregion

        #region Properties


        protected IRepository<TEntity> Repository => _repository;

        #endregion

        #region Constructors

        protected BaseCrudManager(
            IApplicationUnitOfWork unitOfWork,
            IRepository<TEntity> repository,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger) 
            : base(unitOfWork, objectMapper, configurationProvider, logger)
        {
            _repository = repository;
        }

        #endregion

    }
}
