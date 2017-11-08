using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using TBT.Components.Interfaces.Logger;
using TBT.Business.Infrastructure.CastleWindsor;

namespace TBT.Api.Common
{
    public class MessageLogHandler: DelegatingHandler
    {
        private ILogManager _logManager;

        public MessageLogHandler()
        {
            _logManager = ServiceLocator.Current.Get<ILogManager>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logManager.Info($"RequestUri:{request.RequestUri}\r\nContent: {await request.Content.ReadAsStringAsync()}\r\nReturns: {await result.Content.ReadAsStringAsync()}\r\n");
            }
            return result;
        }
    }
}