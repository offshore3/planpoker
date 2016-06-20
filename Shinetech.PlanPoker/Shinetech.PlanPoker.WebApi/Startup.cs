using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Shinetech.PlanPoker.WebApi.Startup))]

namespace Shinetech.PlanPoker.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            //app.MapSignalR();
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
