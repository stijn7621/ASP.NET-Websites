using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute("SoccerHubConfig", typeof(SoccerHub.Startup))]
namespace SoccerHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
