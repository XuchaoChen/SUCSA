using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SUCSA.Startup))]
namespace SUCSA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
