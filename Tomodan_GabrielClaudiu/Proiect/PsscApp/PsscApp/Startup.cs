using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PsscApp.Startup))]
namespace PsscApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
