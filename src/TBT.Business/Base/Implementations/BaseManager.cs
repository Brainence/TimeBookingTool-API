using System;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.Logger;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Implementations
{
    public abstract class BaseManager : IDisposable
    {
        #region Properties

        protected IApplicationUnitOfWork UnitOfWork { get; }

        protected IObjectMapper ObjectMapper { get; }

        protected IConfigurationProvider ConfigurationProvider { get; }

        protected ILogManager Logger { get; }

        #endregion

        #region Constructors

        protected BaseManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
        {
            UnitOfWork = unitOfWork;
            ObjectMapper = objectMapper;
            ConfigurationProvider = configurationProvider;
            Logger = logger;
        }

        #endregion

        #region Interface Members

        public virtual void Dispose()
        {
            UnitOfWork?.Dispose();
        }

        #endregion
    }
}
