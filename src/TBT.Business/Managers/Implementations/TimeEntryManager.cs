using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
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
            IConfigurationProvider configurationProvider)
            : base(unitOfWork, unitOfWork.TimeEntries, objectMapper, configurationProvider)
        {
        }

        #endregion

        #region Interface Members
        public override void Dispose()
        {
            base.Dispose();
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, string date)
        {
            return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                 await UnitOfWork.TimeEntries.GetByUserAsync(userId, date));
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, string from, string to)
        {
            return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                 await UnitOfWork.TimeEntries.GetByUserAsync(userId, from, to));
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, bool isRunning)
        {
            return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                await UnitOfWork.TimeEntries.GetByUserAsync(userId, isRunning));
        }

        public async Task<List<TimeEntryModel>> GetByUserFromAsync(int userId, string from)
        {
            return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                await UnitOfWork.TimeEntries.GetByUserFromAsync(userId, from));
        }

        public async Task<List<TimeEntryModel>> GetByUserToAsync(int userId, string to)
        {
            return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                await UnitOfWork.TimeEntries.GetByUserToAsync(userId, to));
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId)
        {
            return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                await UnitOfWork.TimeEntries.GetByUserAsync(userId));
        }

        public async Task<bool> StartAsync(int timeEntryId)
        {
            try
            {
                var result = await UnitOfWork.TimeEntries.StartAsync(timeEntryId);

                return await Task.FromResult(result);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> StopAsync(int timeEntryId)
        {
            try
            {
                var result = await UnitOfWork.TimeEntries.StopAsync(timeEntryId);

                return await Task.FromResult(result);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> RemoveAsync(int timeEntryId)
        {
            try
            {
                var result = await UnitOfWork.TimeEntries.RemoveAsync(timeEntryId);

                return await Task.FromResult(result);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateAsync(TimeEntryModel model, bool clientDuration = false)
        {
            try
            {
                var result = await (Repository as ITimeEntryRepository).UpdateAsync(
                    ObjectMapper.Map<TimeEntryModel, TimeEntry>(model), clientDuration);

                return await Task.FromResult(result);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<TimeSpan?> GetDurationAsync(int userId, string from, string to)
        {
            return await UnitOfWork.TimeEntries.GetDurationAsync(userId, from, to);
        }

        public async Task<List<TimeEntryModel>> GetByIsRunning(bool isRunning)
        {
            return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                await UnitOfWork.TimeEntries.GetByIsRunning(isRunning));
        }
        #endregion
    }
}
