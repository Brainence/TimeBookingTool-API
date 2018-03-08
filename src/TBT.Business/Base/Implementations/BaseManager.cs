using System;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.Logger;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Implementations
{
    public abstract class BaseManager : IDisposable
    {
        #region Fields

        private readonly IApplicationUnitOfWork unitOfWork;
        private readonly IObjectMapper objectMapper;
        private readonly IConfigurationProvider configurationProvider;
        private readonly ILogManager logger;

        #endregion

        #region Properties

        protected IApplicationUnitOfWork UnitOfWork
        {
            get { return unitOfWork; }
        }

        protected IObjectMapper ObjectMapper
        {
            get { return objectMapper; }
        }

        protected IConfigurationProvider ConfigurationProvider
        {
            get { return configurationProvider; }
        }

        protected ILogManager Logger
        {
            get { return logger; }
        }

        #endregion

        #region Constructors

        protected BaseManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
        {
            this.unitOfWork = unitOfWork;
            this.objectMapper = objectMapper;
            this.configurationProvider = configurationProvider;
            this.logger = logger;
        }

        #endregion

        #region Interface Members

        public virtual void Dispose()
        {
            if (unitOfWork != null)
            {
                unitOfWork.Dispose();
            }
        }

        #endregion
    }
}
