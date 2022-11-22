using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LRWEB_V1_CLIENT_SIDE_T24.Startup))]
namespace LRWEB_V1_CLIENT_SIDE_T24
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
