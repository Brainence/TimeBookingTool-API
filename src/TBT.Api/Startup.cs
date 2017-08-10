using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TBT.WebApi.Startup))]

namespace TBT.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}