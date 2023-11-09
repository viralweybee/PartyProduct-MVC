using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exercise_2.Startup))]
namespace Exercise_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
