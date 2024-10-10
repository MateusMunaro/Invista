using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Investimentos.Startup))]
namespace Investimentos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
