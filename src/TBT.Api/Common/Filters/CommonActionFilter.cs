using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBT.Components.Interfaces.Logger;
using TBT.Business.Infrastructure.CastleWindsor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;

namespace TBT.WebApi.Common.Filters
{
    public class CommonActionFilter: ActionFilterAttribute
    {
        private ILogManager _logger;

        public CommonActionFilter()
        {
            _logger = ServiceLocator.Current.Get<ILogManager>("info");
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Response != null && actionExecutedContext.Response?.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logger.Info($"RequestUri:{actionExecutedContext.Request.RequestUri}\r\nContent: {string.Join(";", actionExecutedContext.ActionContext?.ActionArguments?.Select(x => $"{x.Key} = {x.Value?.ToString()}"))}\r\nReturns: {actionExecutedContext.Response?.Content?.ReadAsStringAsync().Result}");
            }
            return Task.FromResult(0);
        }
    }
}