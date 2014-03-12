using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wikipedia.Startup))]
namespace Wikipedia
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
