using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace TBT.Api.Common.ExceptionHandlers
{
    public class CustomHandleResult : IHttpActionResult
    {
        private HttpStatusCode _responseCode;
        private ExceptionHandlerContext _exceptionContext;

        private CustomHandleResult() { }

        public CustomHandleResult(HttpStatusCode responseCode, ExceptionHandlerContext exceptionContext)
        {
            _responseCode = responseCode;
            _exceptionContext = exceptionContext;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_exceptionContext.Request.CreateResponse(_responseCode, _exceptionContext.Exception.Message));
        }
    }
}