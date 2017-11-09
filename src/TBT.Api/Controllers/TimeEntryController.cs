using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.WebApi.Common.Filters;

namespace TBT.Api.Controllers
{
    [CommonActionFilter]
    [RoutePrefix("api/TimeEntry")]
    public class TimeEntryController : CrudApiController<TimeEntryModel>
    {
        public TimeEntryController(IManagerStore managerStore)
            : base(managerStore, managerStore.TimeEntryManager)
        { }

        [HttpGet]
        [Route("{userId:int:min(1)}/{isRunning:bool}")]
        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, bool isRunning)
        {
            return await ManagerStore.TimeEntryManager.GetByUserAsync(userId, isRunning);
        }

        [HttpGet]
        [Route("GetByUser/{userId:int:min(1)}/{date}")]
        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, string date)
        {
            return await ManagerStore.TimeEntryManager.GetByUserAsync(userId, date);
        }

        [HttpGet]
        [Route("GetByUser/{userId:int:min(1)}/{from}/{to}")]
        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId, string from, string to)
        {
            return await ManagerStore.TimeEntryManager.GetByUserAsync(userId, from, to);
        }

        [HttpGet]
        [Route("GetDuration/{userId:int:min(1)}/{from}/{to}")]
        public async Task<TimeSpan?> GetDurationAsync(int userId, string from, string to)
        {
            return await ManagerStore.TimeEntryManager.GetDurationAsync(userId, from, to);
        }

        [HttpGet]
        [Route("GetByUserTo/{userId:int:min(1)}/{to}")]
        public async Task<List<TimeEntryModel>> GetByUserToAsync(int userId, string to)
        {
            return await ManagerStore.TimeEntryManager.GetByUserToAsync(userId, to);
        }

        [HttpGet]
        [Route("GetByUserFrom/{userId:int:min(1)}/{from}")]
        public async Task<List<TimeEntryModel>> GetByActivityFromAsync(int userId, string from)
        {
            return await ManagerStore.TimeEntryManager.GetByUserFromAsync(userId, from);
        }

        [HttpGet]
        [Route("GetByUser/{userId:int:min(1)}")]
        public async Task<List<TimeEntryModel>> GetByUserAsync(int userId)
        {
            return await ManagerStore.TimeEntryManager.GetByUserAsync(userId);
        }

        [HttpGet]
        [Route("Start/{timeEntryId:int:min(1)}")]
        public async Task<bool> StartAsync(int timeEntryId)
        {
            return await ManagerStore.TimeEntryManager.StartAsync(timeEntryId);
        }
        
        [HttpGet]
        [Route("Stop/{timeEntryId:int:min(1)}")]
        public async Task<bool> StopAsync(int timeEntryId)
        {
            return await ManagerStore.TimeEntryManager.StopAsync(timeEntryId);
        }
        
        [HttpGet]
        [Route("Remove/{timeEntryId:int:min(1)}")]
        public async Task<bool> RemoveAsync(int timeEntryId)
        {
            return await ManagerStore.TimeEntryManager.RemoveAsync(timeEntryId);
        }

        [HttpPut]
        [Route("ServerDuration")]
        public async override Task<HttpResponseMessage> UpdateAsync(TimeEntryModel timeEntry)
        {
            await ManagerStore.TimeEntryManager.UpdateAsync(timeEntry, false);

            return Request.CreateResponse(HttpStatusCode.OK, timeEntry);
        }


        [HttpPut]
        [Route("ClientDuration")]
        public async Task<HttpResponseMessage> ClientDurationUpdateAsync(TimeEntryModel timeEntry)
        {
            await ManagerStore.TimeEntryManager.UpdateAsync(timeEntry, true);

            return Request.CreateResponse(HttpStatusCode.OK, timeEntry);
        }
    }
}
