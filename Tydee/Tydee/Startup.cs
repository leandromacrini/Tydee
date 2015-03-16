using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tydee.Startup))]
namespace Tydee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
