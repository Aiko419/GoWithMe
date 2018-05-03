using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoWithMe.Startup))]
namespace GoWithMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
