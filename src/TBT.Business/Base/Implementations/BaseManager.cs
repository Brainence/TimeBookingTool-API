using System;
using TBT.Business.Providers.Interfaces;
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

        #endregion

        #region Constructors

        protected BaseManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider)
        {
            this.unitOfWork = unitOfWork;
            this.objectMapper = objectMapper;
            this.configurationProvider = configurationProvider;
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
