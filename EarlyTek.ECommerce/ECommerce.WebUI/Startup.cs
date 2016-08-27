using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ECommerce.WebUI.Startup))]
namespace ECommerce.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
