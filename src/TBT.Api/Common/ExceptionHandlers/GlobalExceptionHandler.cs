using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using TBT.Business.Exceptions;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Components.Interfaces.Logger;
using TBT.WebApi.Exceptions;

namespace TBT.Api.Common.ExceptionHandlers
{
    public class GlobalExceptionHandler: ExceptionHandler
    {
        private ILogManager _logManager;

        public GlobalExceptionHandler()
        {
            _logManager = ServiceLocator.Current.Get<ILogManager>();
        }

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var result = default(CustomHandleResult);
            if (context.Exception is BusinessLogicValidationException)
            {
                result = new CustomHandleResult(HttpStatusCode.BadRequest, context);
            }
            else if (context.Exception is RepositoryException)
            {
                result = new CustomHandleResult(HttpStatusCode.BadRequest, context);
            }
            else if (context.Exception is SecurityException)
            {
                result = new CustomHandleResult(HttpStatusCode.Forbidden, context);
            }
            else if (context.Exception is ApiValidationException)
            {
                result = new CustomHandleResult(HttpStatusCode.BadGateway, context);
            }
            else if (context.Exception is ApiSecurityException)
            {
                result = new CustomHandleResult(HttpStatusCode.Forbidden, context);
            }
            else if (context.Exception is ApiAuthorizationException)
            {
                result = new CustomHandleResult(HttpStatusCode.Unauthorized, context);
            }
            else if (context.Exception is ApiException)
            {
                result = new CustomHandleResult(HttpStatusCode.InternalServerError, context);
            }
            else
            {
                result = new CustomHandleResult(HttpStatusCode.InternalServerError, context);
            }
            context.Result = result;

            _logManager.Error(context.Exception, $"{context.Exception}\r\n{context.Exception.Message} {context.Exception.InnerException?.Message}\r\nThrown by: {context.Exception.TargetSite.ReflectedType?.Name}");
            return base.HandleAsync(context, cancellationToken);
        }
    }
}