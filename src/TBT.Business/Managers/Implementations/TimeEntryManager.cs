using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public override void Dispose()
        {
            base.Dispose();
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, string date)
        {
            try
            {
                return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                     await UnitOfWork.TimeEntries.GetByUserAsync(userId, date));
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameters: userId={userId}; date={date}");
                return null;
            }
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, string from, string to)
        {
            try
            {
                return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                     await UnitOfWork.TimeEntries.GetByUserAsync(userId, from, to));
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameters: userId={userId}; from={from}; to={to}");
                return null;
            }
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, bool isRunning)
        {
            try
            {
                return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                    await UnitOfWork.TimeEntries.GetByUserAsync(userId, isRunning));
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameters: userId={userId}; isRunning={isRunning}");
                return null;
            }
        }

        public async Task<List<TimeEntryModel>> GetByUserFromAsync(int userId, string from)
        {
            try
            {
                return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                    await UnitOfWork.TimeEntries.GetByUserFromAsync(userId, from));
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameters: userId={userId}; from={from}");
                return null;
            }
        }

        public async Task<List<TimeEntryModel>> GetByUserToAsync(int userId, string to)
        {
            try
            {
                return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                    await UnitOfWork.TimeEntries.GetByUserToAsync(userId, to));
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameters: userId={userId}; to={to}");
                return null;
            }
        }

        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId)
        {
            try
            {
                return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                    await UnitOfWork.TimeEntries.GetByUserAsync(userId));
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameter: {userId}");
                return null;
            }
        }

        public async Task<bool> StartAsync(int timeEntryId)
        {
            try
            {
                var result = await UnitOfWork.TimeEntries.StartAsync(timeEntryId);

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameter: {timeEntryId}");
                return false;
            }
        }

        public async Task<bool> StopAsync(int timeEntryId)
        {
            try
            {
                var result = await UnitOfWork.TimeEntries.StopAsync(timeEntryId);

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameter: {timeEntryId}");
                return false;
            }
        }

        public async Task<bool> RemoveAsync(int timeEntryId)
        {
            try
            {
                var result = await UnitOfWork.TimeEntries.RemoveAsync(timeEntryId);

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameter: {timeEntryId}");
                return false;
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
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameters: model={model.ToString()}; cleintDuration={clientDuration}");
                return false;
            }
        }

        public async Task<TimeSpan?> GetDurationAsync(int userId, string from, string to)
        {
            try
            {
                return await UnitOfWork.TimeEntries.GetDurationAsync(userId, from, to);
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameters: userId={userId}; from={from}; to={to}");
                return null;
            }
        }

        public async Task<List<TimeEntryModel>> GetByIsRunning(bool isRunning)
        {
            try
            {
                return ObjectMapper.Map<IQueryable<TimeEntry>, List<TimeEntryModel>>(
                    await UnitOfWork.TimeEntries.GetByIsRunning(isRunning));
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameters: {isRunning}");
                return null;
            }
        }
        #endregion
    }
}
