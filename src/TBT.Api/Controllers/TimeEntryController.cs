using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.Filters.ControllersFilters;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Api.Controllers
{
    [RoutePrefix("api/TimeEntry")]
    public class TimeEntryController : CrudApiController<TimeEntryModel>
    {
        public TimeEntryController(IManagerStore managerStore)
            : base(managerStore, managerStore.TimeEntryManager)
        { }

        [HttpGet]
        [Route("{userId:int:min(1)}/{isRunning:bool}")]
        [TimeEntryControllerValidationFilter]
        public async Task<List<TimeEntryModel>> GetByUserAsync([Validator(ValidationMode.Exist)]int userId, bool isRunning)
        {
            return await ManagerStore.TimeEntryManager.GetByUserAsync(userId, isRunning);
        }

        [HttpGet]
        [Route("GetByUser/{userId:int:min(1)}/{date}")]
        [TimeEntryControllerValidationFilter]
        public async Task<List<TimeEntryModel>> GetByUserAsync([Validator(ValidationMode.Exist)]int userId, [Validator(ValidationMode.DataRelevance)]string date)
        {
            return await ManagerStore.TimeEntryManager.GetByUserAsync(userId, date);
        }

        [HttpGet]
        [Route("GetByUser/{userId:int:min(1)}/{from}/{to}/{running:bool=true}")]
        [TimeEntryControllerValidationFilter]
        public async Task<List<TimeEntryModel>> GetByUserAsync([Validator(ValidationMode.Exist)]int userId, string from, string to,bool running = true)
        {
            return await ManagerStore.TimeEntryManager.GetByUserAsync(userId, from, to,running);
        }

        [HttpGet]
        [Route("GetDuration/{userId:int:min(1)}/{from}/{to}")]
        [TimeEntryControllerValidationFilter]
        public async Task<TimeSpan> GetDurationAsync([Validator(ValidationMode.Exist)]int userId, string from, string to)
        {
            return await ManagerStore.TimeEntryManager.GetDurationAsync(userId, from, to);
        }


        [HttpGet]
        [Route("GetByUser/{userId:int:min(1)}")]
        [TimeEntryControllerValidationFilter]
        public async Task<List<TimeEntryModel>> GetByUserAsync([Validator(ValidationMode.Exist)]int userId)
        {
            return await ManagerStore.TimeEntryManager.GetByUserAsync(userId);
        }

        [HttpGet]
        [Route("Start/{id:int:min(1)}")]
        [TimeEntryControllerValidationFilter]
        public async Task<bool> StartAsync([Validator(ValidationMode.Exist)]int id)
        {
            return await ManagerStore.TimeEntryManager.StartAsync(id);
        }
        
        [HttpGet]
        [Route("Stop/{id:int:min(1)}")]
        [TimeEntryControllerValidationFilter]
        public async Task<bool> StopAsync([Validator(ValidationMode.Exist)]int id)
        {
            return await ManagerStore.TimeEntryManager.StopAsync(id);
        }
        
        [HttpGet]
        [Route("Remove/{id:int:min(1)}")]
        [TimeEntryControllerValidationFilter]
        public async Task<bool> RemoveAsync([Validator(ValidationMode.Exist)]int id)
        {
            return await ManagerStore.TimeEntryManager.RemoveAsync(id);
        }

        [HttpPut]
        [Route("ServerDuration")]
        [TimeEntryControllerValidationFilter]
        public override async Task<HttpResponseMessage> UpdateAsync([Validator(ValidationMode.Update)]TimeEntryModel timeEntry)
        {
            await ManagerStore.TimeEntryManager.UpdateAsync(timeEntry, false);

            return Request.CreateResponse(HttpStatusCode.OK, timeEntry);
        }


        [HttpPut]
        [Route("ClientDuration")]
        [TimeEntryControllerValidationFilter]
        public async Task<HttpResponseMessage> ClientDurationUpdateAsync([Validator(ValidationMode.Exist)]TimeEntryModel timeEntry)
        {
            await ManagerStore.TimeEntryManager.UpdateAsync(timeEntry, true);

            return Request.CreateResponse(HttpStatusCode.OK, timeEntry);
        }
    }
}
