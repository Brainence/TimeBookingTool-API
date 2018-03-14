using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBT.Business.Interfaces;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.Logger;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Implementations
{
    public abstract class CrudManager<TEntity, TModel> : BaseCrudManager<TEntity>, ICrudManager<TModel>
           where TModel : class, IModel
           where TEntity : class, IEntity
    {
        protected CrudManager(
            IApplicationUnitOfWork unitOfWork,
            IRepository<TEntity> repository,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, repository, objectMapper, configurationProvider, logger)
        { }

        public virtual async Task<List<TModel>> GetAsync()
        {
            return ObjectMapper.Map<IQueryable<TEntity>, List<TModel>>(
                await Repository.GetAsync());
        }

        public virtual async Task<TModel> GetAsync(int id)
        {
            return ObjectMapper.Map<TEntity, TModel>(
                await Repository.GetAsync(id));
        }

        public virtual async Task<int> AddAsync(TModel model)
        {
            if (model == null) return 0;

            var dBModel = ObjectMapper.Map<TModel, TEntity>(model);

            await Repository.AddAsync(dBModel);

            await UnitOfWork.SaveChangesAsync();

            return dBModel.Id;
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            if (model == null) return;

            await Repository.DetachAsync(
                await Repository.GetAsync(model.Id));

            await Repository.UpdateAsync(
                ObjectMapper.Map<TModel, TEntity>(model));

            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TModel model)
        {
            if (model == null) return;

            await Repository.DeleteAsync(
                ObjectMapper.Map<TModel, TEntity>(model));

            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            await Repository.DeleteAsync(id);

            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task BulkInsertAsync(IEnumerable<TModel> entities)
        {
            await Repository.BulkInsertAsync(
                ObjectMapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(entities));

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await Repository.ExistAsync(id);
        }
    }
}
