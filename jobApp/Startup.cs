using Microsoft.Owin;
using Owin;
using jobApp;

[assembly: OwinStartupAttribute(typeof(jobApp.Startup))]
namespace jobApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            ConfigureAuth(app);
        }
    }
}
