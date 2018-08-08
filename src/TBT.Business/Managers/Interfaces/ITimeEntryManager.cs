using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBT.Business.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface ITimeEntryManager : ICrudManager<TimeEntryModel>
    {
        Task<List<TimeEntryModel>> GetByUserAsync(int userId, bool isRunning);
        Task<List<TimeEntryModel>> GetByUserAsync(int userId, DateTime date);
        Task<List<TimeEntryModel>> GetByUserAsync(int userId, DateTime from, DateTime to,bool running);
        Task<TimeSpan> GetDurationAsync(int userId, DateTime from, DateTime to);
        Task<List<TimeEntryModel>> GetByUserAsync(int userId);
        Task<bool> StartAsync(int timeEntryId);
        Task<bool> StopAsync(int timeEntryId);
        Task<bool> RemoveAsync(int timeEntryId);
        Task<bool> UpdateAsync(TimeEntryModel entity, bool clientDuration);
        Task<List<TimeEntryModel>> GetByIsRunning(bool isRunning);
    }
}
