using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApp_PSSC.Startup))]
namespace WebApp_PSSC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
