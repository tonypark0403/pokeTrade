using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PokeGoTrade.Startup))]
namespace PokeGoTrade
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
