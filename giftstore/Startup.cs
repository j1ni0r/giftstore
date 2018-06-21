using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(giftstore.Startup))]
namespace giftstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
