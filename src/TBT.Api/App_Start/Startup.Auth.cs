using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using TBT.Business.Constants;
using TBT.WebApi.Providers;

namespace TBT.WebApi
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"), //WebApplicationConfig.TokenPath
                ApplicationCanDisplayErrors = true,
                AllowInsecureHttp = true,
                Provider = new ApplicationOAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(NumericConstants.TokenExpirationTimeInHours),
                RefreshTokenProvider = new ApplicationRefreshTokenProvider()
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AuthenticationType = "Bearer",
                AuthenticationMode = AuthenticationMode.Active,
            });

            app.UseCors(CorsOptions.AllowAll);
        }
    }
}