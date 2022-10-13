using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SwachhBharatAbhiyan.CMS.Startup))]
namespace SwachhBharatAbhiyan.CMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
