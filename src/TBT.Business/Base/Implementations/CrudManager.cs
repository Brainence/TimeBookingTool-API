using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            try
            {
                return ObjectMapper.Map<IQueryable<TEntity>, List<TModel>>(
                    await Repository.GetAsync());
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}");
                return null;
            }
        }

        public virtual async Task<TModel> GetAsync(int id)
        {
            try
            {
                return ObjectMapper.Map<TEntity, TModel>(
                    await Repository.GetAsync(id));
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter:{id}");
                return null;
            }
        }

        public virtual async Task<int> AddAsync(TModel model)
        {
            try
            {
                if (model == null) return 0;

                var dBModel = ObjectMapper.Map<TModel, TEntity>(model);

                await Repository.AddAsync(dBModel);

                await UnitOfWork.SaveChangesAsync();

                return dBModel.Id;
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {model.ToString()}");
                return -1;
            }
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            try
            {
                if (model == null) return;

                await Repository.DetachAsync(
                    await Repository.GetAsync(model.Id));

                await Repository.UpdateAsync(
                    ObjectMapper.Map<TModel, TEntity>(model));

                await UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {model.ToString()}");
            }
        }

        public virtual async Task DeleteAsync(TModel model)
        {
            try
            {
                if (model == null) return;

                await Repository.DeleteAsync(
                    ObjectMapper.Map<TModel, TEntity>(model));

                await UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {model.ToString()}");
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            try
            {
                await Repository.DeleteAsync(id);

                await UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {id}");
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public virtual async Task BulkInsertAsync(IEnumerable<TModel> entities)
        {
            try
            {
                await Repository.BulkInsertAsync(
                    ObjectMapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(entities));

                await UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {string.Join(";", entities)}");
            }
        }
    }
}
