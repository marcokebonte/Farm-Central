using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Farm_Central.Startup))]
namespace Farm_Central
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
