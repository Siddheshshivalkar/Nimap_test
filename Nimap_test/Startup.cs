using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nimap_test.Startup))]
namespace Nimap_test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
