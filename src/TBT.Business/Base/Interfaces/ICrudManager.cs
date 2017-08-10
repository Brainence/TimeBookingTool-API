using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TBT.Business.Interfaces
{
    public interface ICrudManager<TModel> : IDisposable
        where TModel : class, IModel
    {
        Task<List<TModel>> GetAsync();
        Task<TModel> GetAsync(int id);
        Task<int> AddAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(TModel model);
        Task DeleteAsync(int id);
        Task BulkInsertAsync(IEnumerable<TModel> entities);
    }
}
