using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TBT.Business.Interfaces;
using TBT.Business.Managers.Interfaces;

namespace TBT.Api.Controllers.Base
{
    public abstract class CrudApiController<TModel> : BaseApiController<TModel> where TModel : class, IModel
    {
        #region Constructors

        public CrudApiController(IManagerStore managerStore, ICrudManager<TModel> manager)
            : base(managerStore, manager)
        { }

        #endregion

        #region Actions

        [HttpGet]
        [Route("")]
        public virtual async Task<HttpResponseMessage> GetAsync()
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                await Manager.GetAsync());
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public virtual async Task<HttpResponseMessage> GetAsync(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                await Manager.GetAsync(id));
        }

        [HttpPost]
        [Route("")]
        public virtual async Task<HttpResponseMessage> CreateAsync(TModel model)
        {
            model.Id = await Manager.AddAsync(model);

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpPut]
        [Route("")]
        public virtual async Task<HttpResponseMessage> UpdateAsync(TModel model)
        {
            await Manager.UpdateAsync(model);

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public virtual async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            await Manager.DeleteAsync(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion
    }

}