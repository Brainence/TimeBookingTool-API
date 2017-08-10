using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using TBT.Business.Helpers;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.WebApi.Providers
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        private Task GrantClientCredetails(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity(
                new GenericIdentity(
                    context.ClientId,
                    OAuthDefaults.AuthenticationType),
                    context.Scope.Select(x => new Claim("urn:oauth:scope", x))
                    );

            context.Validated(identity);

            return Task.FromResult(0);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var accountManager = ServiceLocator.Current.Get<IAccountManager>();

                var account = accountManager.GetByEmail(context.UserName);

                if (account == null
                    || !PasswordHelpers.VerifyPassword(account.Password, context.Password)
                    || !account.IsActive)
                {
                    context.Response.Headers.Add("BadRequestHeader",
                        new[] { "Incorrect username or password." });

                    return Task.FromResult<object>(null);
                }

                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, account.Id.ToString(CultureInfo.InvariantCulture)));

                var properties = new AuthenticationProperties(GenerateAuthentiocationProperties(account));
                var ticket = new AuthenticationTicket(identity, properties);

                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(properties, identity);
            }
            catch (Exception ex)
            {
                context.SetError(ex.Message);
            }

            return Task.FromResult<object>(null);
        }

        private Dictionary<string, string> GenerateAuthentiocationProperties(Account account)
        {
            var result = new Dictionary<string, string>
                {
                    { "UserId", account.Id.ToString() },
                    { "UserName", account.Username }
                };

            return result;
        }
    }
}