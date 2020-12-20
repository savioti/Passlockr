using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Passlockr.Startup))]
namespace Passlockr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
