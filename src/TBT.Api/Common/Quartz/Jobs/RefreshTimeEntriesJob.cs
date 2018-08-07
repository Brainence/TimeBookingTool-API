using System;
using System.Linq;
using System.Threading.Tasks;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Business.Models.BusinessModels;

namespace TBT.Api.Common.Quartz.Jobs
{
    public class RefreshTimeEntriesJob
    {
        public async Task Check()
        {
            var manager = ServiceLocator.Current.Get<ITimeEntryManager>();
            foreach (var item in await manager.GetByIsRunning(true))
            {
                if (await manager.StopAsync(item.Id))
                {
                    var tempTimeEntry = new TimeEntryModel()
                    {
                        Activity = item.Activity,
                        Comment = item.Comment,
                        Date = DateTime.UtcNow,
                        User = item.User,
                        IsActive = true
                    };
                    await manager.StartAsync(await manager.AddAsync(tempTimeEntry));
                }
            }
        }
    }
}