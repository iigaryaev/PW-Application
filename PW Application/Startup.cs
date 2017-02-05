using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PW_Application.Startup))]
namespace PW_Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
