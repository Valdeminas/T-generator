using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(T_generator.Startup))]
namespace T_generator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
