using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;

[assembly: OwinStartup(typeof(IntuneChatServer.Web.Startup))]
namespace IntuneChatServer.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
