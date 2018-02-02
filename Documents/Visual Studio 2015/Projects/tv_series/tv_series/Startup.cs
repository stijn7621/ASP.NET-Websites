using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tv_series.Startup))]
namespace tv_series
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
