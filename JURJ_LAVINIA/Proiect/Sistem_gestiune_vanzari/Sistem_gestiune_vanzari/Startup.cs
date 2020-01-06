using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sistem_gestiune_vanzari.Startup))]
namespace Sistem_gestiune_vanzari
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
