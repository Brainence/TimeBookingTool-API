using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface ITimeEntryRepository : IRepository, IRepository<TimeEntry>
    {
        Task<List<TimeEntry>> GetByUserAsync(int userId, bool isRunning);
        Task<List<TimeEntry>> GetByUserAsync(int userId, DateTime date);
        Task<List<TimeEntry>> GetByUserAsync(int userId, DateTime from, DateTime to, bool running);
        Task<TimeSpan> GetDurationAsync(int userId, DateTime from, DateTime to);
        Task<List<TimeEntry>> GetByUserAsync(int userId);
        Task<bool> StartAsync(int timeEntryId);
        Task<bool> StopAsync(int timeEntryId);
        Task<bool> RemoveAsync(int timeEntryId);
        Task<bool> UpdateAsync(TimeEntry entity, bool clientDuration);
        Task<List<TimeEntry>> GetByIsRunningAsync(bool isRunning);
    }
}
