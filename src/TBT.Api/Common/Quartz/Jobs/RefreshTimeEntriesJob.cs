using System;
using System.Linq;
using Quartz;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Business.Models.BusinessModels;

namespace TBT.Api.Common.Quartz.Jobs
{
    public class RefreshTimeEntriesJob: IJob
    {
        #region Interface members

        public async void Execute(IJobExecutionContext context)
        {
            var _manager = ServiceLocator.Current.Get<ITimeEntryManager>();
            var timeEntries = await _manager.GetByIsRunning(true);
            if (timeEntries.Any())
            {
                foreach (var item in timeEntries)
                {
                    if(await _manager.StopAsync(item.Id))
                    {
                        var tempTimeEntry = new TimeEntryModel()
                        {
                            Activity = item.Activity,
                            Comment = item.Comment,
                            Date = DateTime.UtcNow,
                            User = item.User,
                            IsActive = true
                        };
                        await _manager.StartAsync(await _manager.AddAsync(tempTimeEntry));
                    }
                }
            }
        }

        #endregion
    }
}