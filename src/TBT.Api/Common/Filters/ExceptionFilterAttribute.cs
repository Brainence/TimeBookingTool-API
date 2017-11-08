using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using TBT.Business.Exceptions;
using TBT.WebApi.Exceptions;
using TBT.Components.Interfaces.Logger;
using TBT.Business.Infrastructure.CastleWindsor;
using System.Linq;

namespace TBT.WebApi.Common.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private ILogManager _logManager;

        public ExceptionFilter()
        {
            _logManager = ServiceLocator.Current.Get<ILogManager>();
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            base.OnException(context);
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            if (context.Exception is BusinessLogicValidationException)
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.BadRequest, context.Exception.Message);
            }
            else if (context.Exception is RepositoryException)
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.BadRequest);
            }
            else if (context.Exception is SecurityException)
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.Forbidden, context.Exception.Message);
            }
            else if (context.Exception is ApiValidationException)
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.BadRequest, context.Exception.Message);
            }
            else if (context.Exception is ApiSecurityException)
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.Forbidden, context.Exception.Message);
            }
            else if (context.Exception is ApiAuthorizationException)
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.Unauthorized);
            }
            else if (context.Exception is ApiException)
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.InternalServerError, context.Exception.Message);
            }
            else
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.InternalServerError);
            }

            _logManager.Error(context.Exception, $"{context.Exception.Message} {context.Exception.InnerException?.Message}\r\nThrown by: {context.Exception.TargetSite.ReflectedType.Name}");
            //_logManager.Error(context.Exception, $"{context.Exception.Message} {context.Exception.InnerException?.Message}\r\nThrown by: {context.Exception.TargetSite.Name}");
            //_logManager.Error(context.Exception.Message, context.Exception);

            return base.OnExceptionAsync(context, cancellationToken);
        }
    }
}