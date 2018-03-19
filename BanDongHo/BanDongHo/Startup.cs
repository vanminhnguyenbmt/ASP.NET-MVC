using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BanDongHo.Startup))]
namespace BanDongHo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
