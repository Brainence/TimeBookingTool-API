using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.Logger;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Managers.Implementations
{
    public class TimeEntryManager : CrudManager<TimeEntry, TimeEntryModel>, ITimeEntryManager
    {
        #region Constructors

        public TimeEntryManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, unitOfWork.TimeEntries, objectMapper, configurationProvider, logger)
        {
        }

        #endregion

        #region Interface Members

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, DateTime date)
        {
            return ObjectMapper.Map<List<TimeEntry>, List<TimeEntryModel>>(
                 await UnitOfWork.TimeEntries.GetByUserAsync(userId, date));
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, DateTime from, DateTime to, bool running)
        {
            return ObjectMapper.Map<List<TimeEntry>, List<TimeEntryModel>>(
                 await UnitOfWork.TimeEntries.GetByUserAsync(userId, from, to,running));
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, bool isRunning)
        {
            return ObjectMapper.Map<List<TimeEntry>, List<TimeEntryModel>>(
                await UnitOfWork.TimeEntries.GetByUserAsync(userId, isRunning));
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId)
        {
            return ObjectMapper.Map<List<TimeEntry>, List<TimeEntryModel>>(
                await UnitOfWork.TimeEntries.GetByUserAsync(userId));
        }

        public async Task<bool> StartAsync(int timeEntryId)
        {
            return await UnitOfWork.TimeEntries.StartAsync(timeEntryId);
        }

        public async Task<bool> StopAsync(int timeEntryId)
        {
            return await UnitOfWork.TimeEntries.StopAsync(timeEntryId);
        }

        public async Task<bool> RemoveAsync(int timeEntryId)
        {
            return await UnitOfWork.TimeEntries.RemoveAsync(timeEntryId);
        }

        public async Task<bool> UpdateAsync(TimeEntryModel model, bool clientDuration = false)
        {
            return await UnitOfWork.TimeEntries.UpdateAsync(
                ObjectMapper.Map<TimeEntryModel, TimeEntry>(model), clientDuration);
        }

        public async Task<TimeSpan> GetDurationAsync(int userId, DateTime from, DateTime to)
        {
            return await UnitOfWork.TimeEntries.GetDurationAsync(userId, from, to);
        }

        public async Task<List<TimeEntryModel>> GetByIsRunning(bool isRunning)
        {
            return ObjectMapper.Map<List<TimeEntry>, List<TimeEntryModel>>( await UnitOfWork.TimeEntries.GetByIsRunningAsync(isRunning));
        }

        #endregion
    }
}
