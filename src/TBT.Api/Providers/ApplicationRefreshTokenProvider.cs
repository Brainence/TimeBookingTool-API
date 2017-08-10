using Microsoft.Owin.Security.Infrastructure;
using System;
using TBT.Business.Constants;

namespace TBT.WebApi.Providers
{
    public class ApplicationRefreshTokenProvider : AuthenticationTokenProvider
    {
        public override void Create(AuthenticationTokenCreateContext context)
        {
            context.Ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.UtcNow.AddHours(2 * NumericConstants.TokenExpirationTimeInHours));
            context.SetToken(context.SerializeTicket());
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }
    }
}